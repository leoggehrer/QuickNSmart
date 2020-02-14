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

            Adapters.Factory.Adapter = Adapters.Factory.AdapterType.Controller;
            var loginUserCtrl = Adapters.Factory.Create<Contracts.Business.Account.ILoginUser>();
            var loginUser = await loginUserCtrl.CreateAsync();

            loginUser.User.UserName = "ggehrer";
            loginUser.User.Email = "g.gehrer@htl-leonding.ac.at";
            loginUser.User.Password = "passme";
            loginUser.User.FirstName = "Gerhard";
            loginUser.User.LastName = "Gehrer";
            var role = loginUser.CreateRole();

            role.Designation = "Admin";
            loginUser.AddRole(role);
            role.Designation = "Manager";
            loginUser.AddRole(role);

            await loginUserCtrl.InsertAsync(loginUser);
        }
    }
}
