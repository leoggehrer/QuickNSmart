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
            var authCtrl = Adapters.Factory.Create<Contracts.Business.Account.IAuthentication>();
            var auth = await authCtrl.CreateAsync();

            auth.Identity.Name = "ggehrer";
            auth.Identity.Email = "g.gehrer@htl-leonding.ac.at";
            auth.Identity.Password = "passme";
            var role = auth.CreateRole();

            role.Designation = "Admin";
            auth.AddRole(role);
            role.Designation = "Manager";
            auth.AddRole(role);

            await authCtrl.InsertAsync(auth);

            auth = await authCtrl.CreateAsync();

            auth.Identity.Name = "tgehrer";
            auth.Identity.Email = "t.gehrer@htl-leonding.ac.at";
            auth.Identity.Password = "passme";
            role = auth.CreateRole();

            role.Designation = "Admin";
            auth.AddRole(role);
            role.Designation = "Manager";
            auth.AddRole(role);
            role.Designation = "controller";
            auth.AddRole(role);

            await authCtrl.InsertAsync(auth);
        }
    }
}
