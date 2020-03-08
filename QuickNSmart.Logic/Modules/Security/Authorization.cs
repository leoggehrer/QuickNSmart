//@QnSBaseCode
//MdStart
using System;
using System.Linq;
using System.Reflection;
using CommonBase.Helpers;
using CommonBase.Extensions;
using QuickNSmart.Adapters.Exceptions;
using QuickNSmart.Logic.Entities.Persistence.Account;
using QuickNSmart.Logic.Modules.Account;
using System.Threading.Tasks;

namespace QuickNSmart.Logic.Modules.Security
{
    internal static partial class Authorization
    {
        static Authorization()
        {
            ClassConstructing();
            if (SystemAuthorizationToken.IsNullOrEmpty())
            {
                SystemAuthorizationToken = Guid.NewGuid().ToString();
            }
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        internal static int TimeOutInMin { get; private set; } = 30;
        internal static int TimeOutInSec => TimeOutInMin * 60;
        internal static string SystemAuthorizationToken { get; set; }

        internal static void CheckAuthorization(string token, MethodBase methodeBase)
        {
            token.CheckArgument(nameof(token));
            methodeBase.CheckArgument(nameof(methodeBase));

            bool handled = false;

            BeforeCheckAuthorization(token, methodeBase, ref handled);
            if (handled == false)
            {
                CheckAuthorizationInternal(token, methodeBase);
            }
            AfterCheckAuthorization(token, methodeBase);
        }
        private static void CheckAuthorizationInternal(string token, MethodBase methodBase)
        {
            methodBase = methodBase.GetOriginal();
            if (token.IsNullOrEmpty())
            {
                var authorization = methodBase.GetCustomAttribute<AuthorizeAttribute>();
                var isRequired = authorization?.IsRequired ?? false;

                if (isRequired)
                    throw new LogicException(ErrorType.NotLogedIn);
            }
            else if (token.Equals(SystemAuthorizationToken) == false)
            {
                var authorization = methodBase.GetCustomAttribute<AuthorizeAttribute>();
                bool isRequired = authorization?.IsRequired ?? false;

                if (isRequired)
                {
                    var curSession = AsyncHelper.RunSync<LoginSession>(() => AccountManager.QueryAliveSessionAsync(token));

                    if (curSession == null)
                        throw new LogicException(ErrorType.InvalidSessionToken);

                    if (curSession.IsTimeout)
                        throw new LogicException(ErrorType.AuthorizationTimeOut);

                    bool isAuthorized = authorization.Roles.Any() == false
                                        || curSession.Roles.Any(lr => authorization.Roles.Contains(lr.Designation));

                    if (isAuthorized == false)
                        throw new LogicException(ErrorType.NotAuthorized);

                    curSession.LastAccess = DateTime.Now;
                    LoggingAsync(curSession.IdentityId, methodBase.DeclaringType.Name, methodBase.Name, string.Empty);
                }
            }
        }
        static partial void BeforeCheckAuthorization(string token, MethodBase methodeBase, ref bool handled);
        static partial void AfterCheckAuthorization(string token, MethodBase methodeBase);

        internal static void CheckAuthorization(string token, Type instanceType, MethodBase methodeBase)
        {
            bool handled = false;

            BeforeCheckAuthorization(token, instanceType, methodeBase, ref handled);
            if (handled == false)
            {
                CheckAuthorizationInternal(token, instanceType, methodeBase);
            }
            AfterCheckAuthorization(token, instanceType, methodeBase);
        }
        private static void CheckAuthorizationInternal(string token, Type instanceType, MethodBase methodBase)
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

            methodBase = methodBase.GetOriginal();
            if (token.IsNullOrEmpty())
            {
                var authorization = methodBase.GetCustomAttribute<AuthorizeAttribute>()
                                    ?? GetClassAuthorization(instanceType);
                var isRequired = authorization?.IsRequired ?? false;

                if (isRequired)
                {
                    throw new LogicException(ErrorType.NotLogedIn);
                }
            }
            else if (token.Equals(SystemAuthorizationToken) == false)
            {
                var authorization = methodBase.GetCustomAttribute<AuthorizeAttribute>()
                                    ?? GetClassAuthorization(instanceType);
                var isRequired = authorization?.IsRequired ?? false;

                if (isRequired)
                {
                    var curSession = AsyncHelper.RunSync<LoginSession>(() => AccountManager.QueryAliveSessionAsync(token));

                    if (curSession == null)
                        throw new LogicException(ErrorType.InvalidSessionToken);

                    if (curSession.IsTimeout)
                        throw new LogicException(ErrorType.AuthorizationTimeOut);

                    bool isAuthorized = authorization.Roles.Any() == false
                                        || curSession.Roles.Any(lr => authorization.Roles.Contains(lr.Designation));

                    if (isAuthorized == false)
                        throw new LogicException(ErrorType.NotAuthorized);

                    curSession.LastAccess = DateTime.Now;
                    LoggingAsync(curSession.IdentityId, instanceType.Name, methodBase.Name, string.Empty);
                }
            }
        }

        static partial void BeforeCheckAuthorization(string token, Type instanceType, MethodBase methodeBase, ref bool handled);
        static partial void AfterCheckAuthorization(string token, Type instanceType, MethodBase methodeBase);

        static Task LoggingAsync(int identityId, string subject, string action, string info)
        {
            return Task.Run(async () =>
            {
                using var actionLogCtrl = new Logic.Controllers.Persistence.Account.ActionLogController(Factory.CreateContext())
                {
                    SessionToken = SystemAuthorizationToken
                };
                var entity = new ActionLog();

                entity.IdentityId = identityId;
                entity.Time = DateTime.Now;
                entity.Subject = subject;
                entity.Action = action;
                entity.Info = info;
                await actionLogCtrl.InsertAsync(entity).ConfigureAwait(false);
                await actionLogCtrl.SaveChangesAsync().ConfigureAwait(false);
            });
        }
    }
}
//MdEnd