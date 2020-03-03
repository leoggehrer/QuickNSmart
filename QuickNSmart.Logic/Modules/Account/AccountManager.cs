//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using CommonBase.Extensions;
using CommonBase.Security;
using QuickNSmart.Adapters.Exceptions;
using QuickNSmart.Contracts.Persistence.Account;
using QuickNSmart.Logic.Entities.Persistence.Account;

namespace QuickNSmart.Logic.Modules.Account
{
    public static partial class AccountManager
    {
        static AccountManager()
        {
            ClassConstructing();
            if (SystemAuthorizationToken.IsNullOrEmpty())
            {
                SystemAuthorizationToken = Guid.NewGuid().ToString();
            }
            if (Secret.IsNullOrEmpty())
            {
                Secret = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";
            }
            if (Key.IsNullOrEmpty())
            {
                Key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            }
            Thread updateThread = new Thread(UpdateSession)
            {
                IsBackground = true
            };
            updateThread.Start();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        private const int UpdateDelay = 60000;

        private static List<LoginSession> LoginSessions { get; } = new List<LoginSession>();
        internal static string SystemAuthorizationToken { get; set; }
        internal static string Secret { get; set; }
        internal static string Key { get; set; }
        internal static string Issuer { get; set; } = nameof(QuickNSmart);
        internal static string Audience { get; set; } = nameof(QuickNSmart.Logic);

        public static async Task InitAppAccess(string name, string email, string password)
        {
            using var appAccessCtrl = new Controllers.Business.Account.AppAccessController(Factory.CreateContext())
            {
                AuthenticationToken = SystemAuthorizationToken,
            };
            if (await appAccessCtrl.CountAsync().ConfigureAwait(false) == 0)
            {
                var appAccess = await appAccessCtrl.CreateAsync().ConfigureAwait(false);

                appAccess.Identity.Name = name;
                appAccess.Identity.Email = email;
                appAccess.Identity.Password = password;
                var role = appAccess.CreateRole();

                role.Designation = "SysAdmin";
                appAccess.AddRole(role);
                await appAccessCtrl.InsertAsync(appAccess).ConfigureAwait(false);
                await appAccessCtrl.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                throw new LogicException(ErrorType.InitAppAccess, "The initialization of the app access is not permitted because an app access has already been initialized.");
            }
        }
        public static async Task<ILoginSession> LogonAsync(string email, string password)
        {
            LoginSession result = await QueryLoginAsync(email, password).ConfigureAwait(false);

            if (result == null)
            {
                throw new LogicException(ErrorType.InvalidAccount, "Invalid identity or password.");
            }
            return result;
        }
        public static async Task LogoutAsync(ILoginSession login)
        {
            login.CheckArgument(nameof(login));
            login.SessionToken.CheckArgument(nameof(login.SessionToken));

            using var sessionCtrl = new Controllers.Persistence.Account.LoginSessionController(Factory.CreateContext())
            {
                AuthenticationToken = SystemAuthorizationToken
            };
            var session = (await sessionCtrl.QueryAsync(e => e.IsActive
                                                          && e.SessionToken.Equals(login.SessionToken))
                                            .ConfigureAwait(false)).FirstOrDefault();

            if (session != null)
            {
                session.LogoutTime = DateTime.Now;

                await sessionCtrl.UpdateAsync(session).ConfigureAwait(false);
                await sessionCtrl.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        #region update thread
        private static void UpdateSession()
        {
            while (true)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        using var sessionCtrl = new Controllers.Persistence.Account.LoginSessionController(Factory.CreateContext())
                        {
                            AuthenticationToken = SystemAuthorizationToken,
                        };
                        bool saveChanges = false;
                        var qry = await sessionCtrl.QueryAsync(e => e.LogoutTime.HasValue == false)
                                                   .ConfigureAwait(false);

                        foreach (var item in qry.ToList())
                        {
                            bool itemUpdate = false;
                            bool curItemRemove = false;
                            var curItem = LoginSessions.SingleOrDefault(e => e.Id == item.Id);

                            if (curItem != null)
                            {
                                itemUpdate = true;
                                item.LastAccess = curItem.LastAccess;
                            }
                            if (item.IsTimeout)
                            {
                                item.LogoutTime = DateTime.Now;
                                itemUpdate = true;
                                curItemRemove = true;
                            }
                            if (itemUpdate)
                            {
                                saveChanges = true;
                                await sessionCtrl.UpdateAsync(item).ConfigureAwait(false);
                            }
                            if (curItem != null && curItemRemove)
                            {
                                LoginSessions.Remove(curItem);
                            }
                        }
                        if (saveChanges)
                        {
                            await sessionCtrl.SaveChangesAsync().ConfigureAwait(false);
                        }
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error: {e.Message}");
                    }
                });
                Thread.Sleep(UpdateDelay);
            }
        }
        #endregion update thread

        #region Logon
        private static async Task<LoginSession> QueryLoginAsync(string email, string password)
        {
            email.CheckArgument(nameof(email));
            password.CheckArgument(nameof(password));

            LoginSession result = await QueryAliveSessionAsync(email, password).ConfigureAwait(false);

            if (result == null)
            {
                Byte[] calculatedPassword = CalculateHash(password);
                using var identityCtrl = new Controllers.Persistence.Account.IdentityController(Factory.CreateContext())
                {
                    AuthenticationToken = SystemAuthorizationToken,
                };
                var identity = (await identityCtrl.QueryAsync(e => e.State == Contracts.State.Active
                                                                && e.AccessFailedCount < 4
                                                                && e.Email.Equals(email, StringComparison.CurrentCulture)
                                                                && ComparePasswords(e.PasswordHash, calculatedPassword))
                                                  .ConfigureAwait(false)).FirstOrDefault();

                if (identity != null)
                {
                    using var sessionCtrl = new Controllers.Persistence.Account.LoginSessionController(identityCtrl);
                    var session = new LoginSession();

                    session.IdentityId = identity.Id;
                    session.Roles.AddRange(await QueryIdentityRolesAsync(sessionCtrl, identity.Id).ConfigureAwait(false));
                    var entity = await sessionCtrl.InsertAsync(session).ConfigureAwait(false);

                    if (identity.AccessFailedCount > 0)
                    {
                        identity.AccessFailedCount = 0;
                        await identityCtrl.UpdateAsync(identity).ConfigureAwait(false);
                    }
                    await sessionCtrl.SaveChangesAsync().ConfigureAwait(false);

                    result = new LoginSession();
                    result.CopyProperties(entity);
                    result.Identity = new Identity();
                    result.Identity.CopyProperties(identity);
                    result.Roles.AddRange(session.Roles);
                    LoginSessions.Add(result);
                }
            }
            return result;
        }
        internal static async Task<LoginSession> QueryAliveSessionAsync(string token)
        {
            LoginSession result = LoginSessions.SingleOrDefault(ls => ls.SessionToken.Equals(token));

            if (result == null)
            {
                using var sessionCtrl = new Controllers.Persistence.Account.LoginSessionController(Factory.CreateContext())
                {
                    AuthenticationToken = SystemAuthorizationToken
                };
                var session = (await sessionCtrl.QueryAsync(e => e.IsActive
                                                              && e.SessionToken.Equals(token))
                                                .ConfigureAwait(false)).FirstOrDefault();

                if (session != null)
                {
                    using var identityCtrl = new Controllers.Persistence.Account.IdentityController(sessionCtrl);
                    var identity = (await identityCtrl.QueryAsync(e => e.Id == session.IdentityId)
                                                      .ConfigureAwait(false))
                                                      .SingleOrDefault();

                    if (identity != null)
                    {
                        result = new LoginSession();
                        result.CopyProperties(session);
                        result.Identity = new Identity();
                        result.Identity.CopyProperties(identity);
                        result.Roles.AddRange(await QueryIdentityRolesAsync(sessionCtrl, identity.Id).ConfigureAwait(false));
                        LoginSessions.Add(result);
                    }
                }
            }
            return result;
        }
        internal static async Task<LoginSession> QueryAliveSessionAsync(string email, string password)
        {
            email.CheckArgument(nameof(email));
            password.CheckArgument(nameof(password));

            Byte[] calculatedPassword = CalculateHash(password);
            LoginSession result = LoginSessions.SingleOrDefault(e => e.IsActive
                                                                  && e.Email.Equals(email, StringComparison.CurrentCulture)
                                                                  && ComparePasswords(e.PasswordHash, calculatedPassword));

            if (result == null)
            {
                using var identityCtrl = new Controllers.Persistence.Account.IdentityController(Factory.CreateContext())
                {
                    AuthenticationToken = SystemAuthorizationToken,
                };
                var identity = (await identityCtrl.QueryAsync(e => e.Email.Equals(email, StringComparison.CurrentCulture)
                                                                   && ComparePasswords(e.PasswordHash, calculatedPassword))
                                                  .ConfigureAwait(false)).FirstOrDefault();

                if (identity != null)
                {
                    using var sessionCtrl = new Controllers.Persistence.Account.LoginSessionController(identityCtrl);
                    var session = (await sessionCtrl.QueryAsync(e => e.IsActive
                                                                  && e.IdentityId == identity.Id)
                                                    .ConfigureAwait(false)).FirstOrDefault();

                    if (session != null)
                    {
                        result = new LoginSession();
                        result.CopyProperties(session);
                        result.Identity = new Identity();
                        result.Identity.CopyProperties(identity);
                        result.Roles.AddRange(await QueryIdentityRolesAsync(sessionCtrl, identity.Id).ConfigureAwait(false));
                        LoginSessions.Add(result);
                    }
                }
            }
            return result;
        }
        internal static async Task<IEnumerable<Role>> QueryIdentityRolesAsync(Controllers.ControllerObject controllerObject, int identityId)
        {
            controllerObject.CheckArgument(nameof(controllerObject));

            List<Role> result = new List<Role>();
            using var identityXRoleCtrl = new Controllers.Persistence.Account.IdentityXRoleController(controllerObject);
            using var roleCtrl = new Controllers.Persistence.Account.RoleController(controllerObject);

            foreach (var item in (await identityXRoleCtrl.QueryAsync(e => e.IdentityId == identityId).ConfigureAwait(false)).ToList())
            {
                var entity = await roleCtrl.GetByIdAsync(item.RoleId).ConfigureAwait(false);

                if (entity != null)
                {
                    var role = new Role();

                    role.CopyProperties(entity);
                    result.Add(role);
                }
            }
            return result;
        }
        #endregion Logon

        #region Helpers
        internal static string GenerateJsonWebToken(IEnumerable<Claim> claimsParam)
        {
            claimsParam.CheckArgument(nameof(claimsParam));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(
                signingCredentials: credentials,
                issuer: Issuer,
                audience: Audience,
                claims: claimsParam,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30));

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(secToken);
        }
        public static bool CheckJsonWebToken(string token)
        {
            SecurityToken validatedToken;

            return CheckJsonWebToken(token, out validatedToken);
        }
        internal static bool CheckJsonWebToken(string token, out SecurityToken validatedToken)
        {
            var result = false;
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            validatedToken = null;
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                result = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }
        internal static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)) // The same key as the one that generate the token
            };
        }
        internal static async Task CheckAuthorizationAsync(string token, Type type, MethodBase methodBase)
        {
            static AuthorizeAttribute GetClassAuthorization(Type classType)
            {
                var runType = classType;
                var result = default(AuthorizeAttribute);

                do
                {
                    result = runType.GetCustomAttribute<AuthorizeAttribute>();
                    runType = runType.BaseType;
                } while (result == null && runType != null);
                return result;
            }

            type.CheckArgument(nameof(type));
            methodBase.CheckArgument(nameof(methodBase));

            if (token == null)
            {
                var authorization = methodBase.GetCustomAttribute<AuthorizeAttribute>()
                                    ?? GetClassAuthorization(type);
                bool isRequired = authorization?.IsRequired ?? false;

                if (isRequired)
                {
                    throw new LogicException(ErrorType.NotLogedIn, "You are not logged in.");
                }
            }
            else if (token.Equals(SystemAuthorizationToken) == false)
            {
                var authorization = methodBase.GetCustomAttribute<AuthorizeAttribute>()
                                    ?? GetClassAuthorization(type);
                bool isRequired = authorization?.IsRequired ?? false;

                if (isRequired)
                {
                    var curSession = await QueryAliveSessionAsync(token).ConfigureAwait(false);

                    if (curSession == null)
                        throw new LogicException(ErrorType.InvalidAuthorizationToken, "Invalid authorization token!");

                    if (curSession.IsTimeout)
                    {
                        throw new LogicException(ErrorType.AuthorizationTimeOut, "Time out.");
                    }

                    bool isAuthorized = authorization.Roles.Any() == false
                                        || curSession.Roles.Any(lr => authorization.Roles.Contains(lr.Designation));

                    if (isAuthorized == false)
                    {
                        throw new LogicException(ErrorType.NotAuthorized, "You are not authorized.");
                    }
                    curSession.LastAccess = DateTime.Now;
                }
            }
        }
        internal static byte[] CalculateHash(string plainText)
        {
            if (String.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] hashedBytes = sha1.ComputeHash(plainTextBytes);
            return hashedBytes;
        }
        internal static bool ComparePasswords(Byte[] hashedPassword, string password)
        {
            return ComparePasswords(hashedPassword, CalculateHash(password));
        }
        internal static bool ComparePasswords(Byte[] hashedPassword, Byte[] calculatePassword)
        {
            if (hashedPassword == null)
                throw new ArgumentNullException(nameof(hashedPassword));

            if (calculatePassword == null)
                throw new ArgumentNullException(nameof(calculatePassword));

            byte[] originalArray = hashedPassword.ToArray();
            byte[] compareArray = calculatePassword.ToArray();

            if (compareArray.Length != originalArray.Length)
            {
                return false;
            }
            for (int i = 0; i < compareArray.Length; i++)
            {
                if (originalArray[i] != compareArray[i])
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Das Kennwort wenn es den Einstellungen im CommonBase/Modules/PasswordRules entspricht.
        /// </summary>
        /// <param name="password">Zu pruefendes Passwort</param>
        /// <returns>true wenn das Passwort mit PasswordRules entspricht, false sonst</returns>
        public static bool CheckPasswordSyntax(string password)
        {
            password.CheckArgument(nameof(password));

            int digitCount = 0;
            int letterCount = 0;
            int lowerLetterCount = 0;
            int upperLetterCount = 0;
            int specialLetterCount = 0;

            foreach (char ch in password)
            {
                if (char.IsDigit(ch))
                {
                    digitCount++;
                }
                else
                {
                    if (char.IsLetter(ch))
                    {
                        letterCount++;
                        if (char.IsLower(ch))
                        {
                            lowerLetterCount++;
                        }
                        else
                        {
                            upperLetterCount++;
                        }
                    }
                    else
                    {
                        specialLetterCount++;
                    }
                }
            }
            return password.Length >= PasswordRules.MinimumLength
                   && password.Length <= PasswordRules.MaximumLength
                   && letterCount >= PasswordRules.MinLetterCount
                   && upperLetterCount >= PasswordRules.MinUpperLetterCount
                   && lowerLetterCount >= PasswordRules.MinLowerLetterCount
                   && specialLetterCount >= PasswordRules.MinSpecialLetterCount
                   && digitCount >= PasswordRules.MinDigitCount;
        }

        /// <summary>
        /// Eine gueltige Mailadresse besteht aus einem mindestens zwei Zeichen vor dem @, 
        /// einem Hostname, der genau einen oder mehrere Punkte enthaelt (Domainname mindestens dreistellig)
        /// und als Topleveldomaene (letzter Teil) mindestens zweistellig ist
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <returns>Mailadresse ist gültig</returns>
        public static bool CheckMailAddressSyntax(string mailAddress)
        {
            mailAddress.CheckArgument(nameof(mailAddress));

            //return Regex.IsMatch(mailAddress, @"^([\w-\.]+){2,}@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            ////@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
            ////@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"); 
            return Regex.IsMatch(mailAddress, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            //return Regex.IsMatch(mailAddress, @"^\w{2,}@[a-zA-Z]{3,}\.[a-zA-Z]{2,}$");
        }
        #endregion Helpers
    }
}
//MdEnd