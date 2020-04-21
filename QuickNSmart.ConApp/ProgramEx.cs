//@QnSIgnore
using CommonBase.Helpers;
using System;
using System.Threading.Tasks;
using AccountManager = QuickNSmart.Adapters.Modules.Account.AccountManager;

namespace QuickNSmart.ConApp
{
    partial class Program
    {
        static partial void EndExecuteMain()
        {
            var appAccountManager = new AccountManager
            {
                BaseUri = "https://localhost:5001/api",
                Adapter = Adapters.AdapterType.Controller,
            };

            AsyncHelper.RunSync(() => AddInvoiceDetailsAsync());
        }
        private static async Task AddInvoiceDetailsAsync()
        {
            var accMngr = new AccountManager();
            var login = await accMngr.LogonAsync(SaEmail, SaPwd);
            using var ctrl = Adapters.Factory.Create<Contracts.Business.TestRelation.IInvoiceDetails>(login.SessionToken);
            var entity = await ctrl.CreateAsync();

            entity.Master.Date = DateTime.Now;
            entity.Master.Subject = $"Rechnung {DateTime.Now}";
            entity.Master.Street = "Europastraße 67";
            entity.Master.ZipCode = "4020";
            entity.Master.City = "Linz";

            for (int i = 0; i < 5; i++)
            {
                var detail = entity.CreateDetail();

                detail.Order = i;
                detail.Text = $"Position: {i + 1}";
                detail.Quantity = 1;
                detail.Price = 43.50;
                detail.Tax = 20;
                entity.AddDetail(detail);
            }
            await ctrl.InsertAsync(entity);
            await accMngr.LogoutAsync(login.SessionToken);
        }
    }
}
