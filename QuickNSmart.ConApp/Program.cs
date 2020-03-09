using System;
using System.Threading.Tasks;
using CommonBase.Extensions;
using QuickNSmart.Contracts.Persistence.Account;

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

            Adapters.Factory.BaseUri = "https://localhost:5001/api";
            Adapters.Factory.Adapter = Adapters.AdapterType.Service;

            //await InitAppAccessAsync();
            //await AddAppAccess(AaUser, AaEmail, AaPwd, AaEnableJwt, AaRole);
            
            //await AddAppAccess("ggehrer", "ggehrer@hotmail.com", "Passme1234!", AaEnableJwt);
            //await AddAppAccess("nhaslberger", "nhaslberger@hotmail.com", "Passme1234!", AaEnableJwt);
            //await AddAppAccess("thaslberger", "thaslberger@hotmail.com", "Passme1234!", AaEnableJwt);
            
            
            try
            {
                var login = await Adapters.Modules.Account.AccountManager.LogonAsync(SaEmail, SaPwd);

                await ChangePassword(login, SaPwd, SaPwd);

                var login2 = await Adapters.Modules.Account.AccountManager.LogonAsync(login.JsonWebToken);

                await Task.Delay(5000);
                await Adapters.Modules.Account.AccountManager.LogoutAsync(login.SessionToken);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
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
            var login = await Adapters.Modules.Account.AccountManager.LogonAsync(SaEmail, SaPwd);
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
            await Adapters.Modules.Account.AccountManager.LogoutAsync(login.SessionToken);
        }
        private static async Task ChangePassword(ILoginSession login, string oldPwd, string newPwd)
        {
            await Logic.Modules.Account.AccountManager.ChangePassword(login.SessionToken, oldPwd, newPwd);
        }
    }
}
