using CommonBase.Extensions;
using QuickNSmart.Contracts.Persistence.Account;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuickNSmart.ConApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Run(() => Console.WriteLine("QuickNSmart"));

            //await InitAppAccess();
            var login = await LogonAsync("g.gehrer@htl-leonding.ac.at", "passme");

            await Task.Delay(10000);
            await LogoutAsync(login);
            Console.ReadLine();
        }
        private static async Task<ILoginSession> LogonAsync(string email, string password)
        {
            return await Logic.Modules.Account.AccountManager.LogonAsync(email, password);
        }
        private static async Task LogoutAsync(ILoginSession login)
        {
            await Logic.Modules.Account.AccountManager.LogoutAsync(login);
        }
        private static async Task InitAppAccessAsync()
        {
            Adapters.Factory.Adapter = Adapters.Factory.AdapterType.Controller;
            var appAccCtrl = Adapters.Factory.Create<Contracts.Business.Account.IAppAccess>();
            var appAcc = await appAccCtrl.CreateAsync();

            appAcc.Identity.Name = "ggehrer";
            appAcc.Identity.Email = "g.gehrer@htl-leonding.ac.at";
            appAcc.Identity.Password = "passme";
            var role = appAcc.CreateRole();

            role.Designation = "Admin";
            appAcc.AddRole(role);
            role.Designation = "Manager";
            appAcc.AddRole(role);

            await Logic.Modules.Account.AccountManager.InitAppAccess(appAcc);
        }

        static async void CreateAccounts(ILoginSession login)
        {
            login.CheckArgument(nameof(login));

            Adapters.Factory.Adapter = Adapters.Factory.AdapterType.Controller;
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
