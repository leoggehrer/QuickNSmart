using CommonBase.Extensions;
using QuickNSmart.Contracts.Persistence.Account;
using System;
using System.Threading.Tasks;

namespace QuickNSmart.ConApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Run(() => Console.WriteLine("QuickNSmart"));

            string user = "ggehrer";
            string email = "g.gehrer@htl-leonding.ac.at";
            string pwd = "Passme123!";

            //await InitAppAccessAsync(user, email, pwd);
            try
            {
                var login = await LogonAsync(email, pwd);

                await ChangePassword(login, pwd, "Passme123!");

                var login2 = await Logic.Modules.Account.AccountManager.QueryLoginAsync(login.SessionToken);
                var login3 = await Logic.Modules.Account.AccountManager.LogonAsync(login.JsonWebToken);

                await Task.Delay(5000);
                await LogoutAsync(login.SessionToken);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }
        private static async Task InitAppAccessAsync(string user, string email, string pwd)
        {
            await Logic.Modules.Account.AccountManager.InitAppAccess(user, email, pwd);
        }
        private static async Task<ILoginSession> LogonAsync(string email, string password)
        {
            return await Logic.Modules.Account.AccountManager.LogonAsync(email, password);
        }
        private static async Task LogoutAsync(string sessionToken)
        {
            await Logic.Modules.Account.AccountManager.LogoutAsync(sessionToken);
        }
        private static async Task ChangePassword(ILoginSession login, string oldPwd, string newPwd)
        {
            await Logic.Modules.Account.AccountManager.ChangePassword(login.SessionToken, oldPwd, newPwd);
        }
        static async void CreateAccounts(ILoginSession login)
        {
            login.CheckArgument(nameof(login));

            Adapters.Factory.Adapter = Adapters.AdapterType.Controller;
            var appAccCtrl = Adapters.Factory.Create<Contracts.Business.Account.IAppAccess>(login.SessionToken);
            var appAcc = await appAccCtrl.CreateAsync();

            appAcc.Identity.Name = "user1";
            appAcc.Identity.Email = "user1@gmx.at";
            appAcc.Identity.Password = "passme";
            var role = appAcc.CreateRole();

            role.Designation = "user";
            appAcc.AddRole(role);

            await appAccCtrl.InsertAsync(appAcc);

            appAcc = await appAccCtrl.CreateAsync();

            appAcc.Identity.Name = "user2";
            appAcc.Identity.Email = "user2@gmx.at";
            appAcc.Identity.Password = "passme";
            role = appAcc.CreateRole();

            role.Designation = "User";
            appAcc.AddRole(role);
            role.Designation = "Manager";
            appAcc.AddRole(role);
            role.Designation = "controller";
            appAcc.AddRole(role);

            await appAccCtrl.InsertAsync(appAcc);
        }
    }
}
