using System;
using System.Reflection;
using System.Threading.Tasks;
using QuickNSmart.Contracts.Persistence.Account;
using AccountManager = QuickNSmart.Adapters.Modules.Account.AccountManager;

namespace QuickNSmart.ConApp
{
    class Program
    {
        static string SaUser => "SysAdmin";
        static string SaEmail => "SysAdmin@QuickNSmart.gmx.at";
        static string SaPwd => "Sys2189!Admin";
        static bool SaEnableJwt => true;

        static string AaUser => "AppAdmin";
        static string AaEmail => "AppAdmin@QuickNSmart.gmx.at";
        static string AaPwd => "App2189!Admin";
        static string AaRole => "AppAdmin";
        static bool AaEnableJwt => true;

        static async Task Main(string[] args)
        {
            await Task.Run(() => Console.WriteLine("QuickNSmart"));

            var rmAccountManager = new AccountManager
            {
//                BaseUri = "https://localhost:5001/api",
                BaseUri = "https://localhost:5001/api",
                Adapter = Adapters.AdapterType.Service,
            };
            var appAccountManager = new AccountManager
            {
                BaseUri = "https://localhost:5001/api",
                Adapter = Adapters.AdapterType.Controller,
            };

            Adapters.Factory.BaseUri = "https://localhost:5001/api";
            Adapters.Factory.Adapter = Adapters.AdapterType.Controller;

            try
            {
                await InitAppAccessAsync();
                await AddAppAccess(AaUser, AaEmail, AaPwd, AaEnableJwt, AaRole);

                await AddAppAccess("schueler1", "schueler1@gmx.com", "Passme1234!", AaEnableJwt);
                await AddAppAccess("schueler2", "schueler2@gmx.com", "Passme1234!", AaEnableJwt);
                await AddAppAccess("schueler3", "schueler3@gmx.com", "Passme1234!", AaEnableJwt);

                var rmLogin = await rmAccountManager.LogonAsync("ggehrer@hotmail.com", "Passme123!");
                var appLogin = await appAccountManager.LogonAsync(rmLogin.JsonWebToken);

                await appAccountManager.LogoutAsync(appLogin.SessionToken);
                await rmAccountManager.LogoutAsync(rmLogin.SessionToken);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
            Console.WriteLine("Press any key to end!");
            Console.ReadKey();
        }
        private static async Task InitAppAccessAsync()
        {
            await Logic.Modules.Account.AccountManager.InitAppAccess(SaUser, SaEmail, SaPwd, true);
        }
        private static async Task AddAppAccess(string user, string email, string pwd, bool enableJwtAuth, params string[] roles)
        {
            var accMngr = new AccountManager();
            var login = await accMngr.LogonAsync(SaEmail, SaPwd);
            using var ctrl = Adapters.Factory.Create<Contracts.Business.Account.IAppAccess>(login.SessionToken);
            var entity = await ctrl.CreateAsync();

            entity.Identity.Name = user;
            entity.Identity.Email = email;
            entity.Identity.Password = pwd;
            entity.Identity.EnableJwtAuth = enableJwtAuth;

            foreach (var item in roles)
            {
                var role = entity.CreateRole();

                role.Designation = item;
                entity.AddRole(role);
            }
            await ctrl.InsertAsync(entity);
            await accMngr.LogoutAsync(login.SessionToken);
        }
    }
}
