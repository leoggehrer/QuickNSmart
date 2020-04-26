using System;
using System.Reflection;
using System.Threading.Tasks;
using AccountManager = QuickNSmart.Adapters.Modules.Account.AccountManager;

namespace QuickNSmart.ConApp
{
    // Search pattern for async calls without ConfigAwait: (?=\bawait\b(?!.*\bConfigureAwait\b))
    partial class Program
    {
        static string SaUser => "SysAdmin";
        static string SaEmail => "SysAdmin@QuickNSmart.gmx.at";
        static string SaPwd => "Sys2189!Admin";

        static string AaUser => "AppAdmin";
        static string AaEmail => "AppAdmin@QuickNSmart.gmx.at";
        static string AaPwd => "App2189!Admin";
        static string AaRole => "AppAdmin";
        static bool AaEnableJwt => true;

        static async Task Main(string[] args)
        {
            await Task.Run(() => Console.WriteLine("QuickNSmart"));

            BeforeExecuteMain(args);

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
                await AddAppAccessAsync(AaUser, AaEmail, AaPwd, AaEnableJwt, AaRole);

                //await AddAppAccessAsync("schueler1", "schueler1@gmx.com", "Passme1234!", AaEnableJwt);
                //await AddAppAccessAsync("schueler2", "schueler2@gmx.com", "Passme1234!", AaEnableJwt);
                //await AddAppAccessAsync("schueler3", "schueler3@gmx.com", "Passme1234!", AaEnableJwt);

                //var rmLogin = await rmAccountManager.LogonAsync("schueler1@gmx.com", "Passme123!");
                //var appLogin = await appAccountManager.LogonAsync(rmLogin.JsonWebToken);

                //await appAccountManager.LogoutAsync(appLogin.SessionToken);
                //await rmAccountManager.LogoutAsync(rmLogin.SessionToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }

            EndExecuteMain();
            Console.WriteLine("Press any key to end!");
            Console.ReadKey();
        }
        private static async Task InitAppAccessAsync()
        {
            await Logic.Modules.Account.AccountManager.InitAppAccessAsync(SaUser, SaEmail, SaPwd, true);
        }
        private static async Task AddAppAccessAsync(string user, string email, string pwd, bool enableJwtAuth, params string[] roles)
        {
            var accMngr = new AccountManager();
            var login = await accMngr.LogonAsync(SaEmail, SaPwd);
            using var ctrl = Adapters.Factory.Create<Contracts.Business.Account.IAppAccess>(login.SessionToken);
            var entity = await ctrl.CreateAsync();

            entity.FirstItem.Name = user;
            entity.FirstItem.Email = email;
            entity.FirstItem.Password = pwd;
            entity.FirstItem.EnableJwtAuth = enableJwtAuth;

            foreach (var item in roles)
            {
                var role = entity.CreateSecondItem();

                role.Designation = item;
                entity.AddSecondItem(role);
            }
            await ctrl.InsertAsync(entity);
            await accMngr.LogoutAsync(login.SessionToken);
        }

        static partial void BeforeExecuteMain(string[] args);
        static partial void EndExecuteMain();
    }
}
