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
            using var ctrl = Adapters.Factory.Create<Contracts.Business.TestOneToMany.IInvoiceDetails>(login.SessionToken);
            var entity = await ctrl.CreateAsync();

            entity.FirstItem.Date = DateTime.Now;
            entity.FirstItem.Subject = $"Rechnung {DateTime.Now}";
            entity.FirstItem.Street = "Europastraße 67";
            entity.FirstItem.ZipCode = "4020";
            entity.FirstItem.City = "Linz";

            for (int i = 0; i < 5; i++)
            {
                var detail = entity.CreateSecondItem();

                detail.Order = i;
                detail.Text = $"Position: {i + 1}";
                detail.Quantity = 1;
                detail.Price = 43.50;
                detail.Tax = 20;
                entity.AddSecondItem(detail);
            }
            await ctrl.InsertAsync(entity);
            await accMngr.LogoutAsync(login.SessionToken);
        }
    }
}
