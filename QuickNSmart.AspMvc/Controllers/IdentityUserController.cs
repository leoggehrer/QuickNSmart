//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using CommonBase.Extensions;
using QuickNSmart.AspMvc.Models.Persistence.Account;
using Model = QuickNSmart.AspMvc.Models.Business.Account.IdentityUser;
using Contract = QuickNSmart.Contracts.Business.Account.IIdentityUser;

namespace QuickNSmart.AspMvc.Controllers
{
    public partial class IdentityUserController : AccessController
    {
        private readonly ILogger<IdentityUserController> _logger;
        public IdentityUserController(ILogger<IdentityUserController> logger, IFactoryWrapper factoryWrapper)
            : base(factoryWrapper)
        {
            Constructing();
            _logger = logger;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        [ActionName("Index")]
        public async Task<IActionResult> IndexAsync()
        {
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);
            var entities = await ctrl.GetAllAsync().ConfigureAwait(false);

            return View(entities.Select(e => ConvertTo<Model, Contract>(e)));
        }
        [ActionName("Create")]
        public async Task<IActionResult> CreateAsync(string error = null)
        {
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);
            var entity = await ctrl.CreateAsync().ConfigureAwait(false);
            var model = ConvertTo<Model, Contract>(entity);

            model.ActionError = error;
            return View("Edit", model);
        }

        [ActionName("Edit")]
        public async Task<IActionResult> EditAsync(int id, string error = null)
        {
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);
            using var ctrlRole = Factory.Create<Contracts.Persistence.Account.IRole>(SessionWrapper.SessionToken);
            var entity = await ctrl.GetByIdAsync(id).ConfigureAwait(false);
            var model = ConvertTo<Model, Contract>(entity);

            model.ActionError = error;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditAsync(int id, Identity identity, User user, IFormCollection formCollection)
        {
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);
            async Task<IActionResult> CreateFailedAsync(Model viewModel, string error)
            {
                var entity = await ctrl.CreateAsync().ConfigureAwait(false);

                entity.CopyProperties(viewModel);

                var model = ConvertTo<Model, Contract>(entity);

                model.ActionError = error;
                return View("Edit", model);
            }
            async Task<IActionResult> EditFailedAsync(Model viewModel, string error)
            {
                var entity = await ctrl.GetByIdAsync(viewModel.Id).ConfigureAwait(false);

                entity.CopyProperties(viewModel);

                var model = ConvertTo<Model, Contract>(entity);

                model.ActionError = error;
                return View("Edit", model);
            }

            Model model = new Model();
            
            user.Id = formCollection["UserId"][0].ToInt();
            model.FirstItem.CopyProperties(identity);
            model.SecondItem.CopyProperties(user);

            if (ModelState.IsValid == false)
            {
                if (model.Id == 0)
                {
                    return await CreateFailedAsync(model, GetModelStateError()).ConfigureAwait(false);
                }
                else
                {
                    return await EditFailedAsync(model, GetModelStateError()).ConfigureAwait(false);
                }
            }
            try
            {
                if (model.Id == 0)
                {
                    var result = await ctrl.InsertAsync(model).ConfigureAwait(false);

                    id = result.Id;
                }
                else
                {
                    await ctrl.UpdateAsync(model).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                if (model.Id == 0)
                {
                    return await CreateFailedAsync(model, GetExceptionError(ex)).ConfigureAwait(false);
                }
                else
                {
                    return await EditFailedAsync(model, GetExceptionError(ex)).ConfigureAwait(false);
                }
            }
            return RedirectToAction("Edit", new { id });
        }

        [ActionName("Details")]
        public async Task<IActionResult> DetailsAsync(int id)
        {
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);
            var entity = await ctrl.GetByIdAsync(id).ConfigureAwait(false);

            return View(entity != null ? ConvertTo<Model, Contract>(entity) : entity);
        }

        // GET: /Delete/5
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(int id, string error = null)
        {
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);
            using var ctrlRole = Factory.Create<Contracts.Persistence.Account.IRole>(SessionWrapper.SessionToken);
            var entity = await ctrl.GetByIdAsync(id).ConfigureAwait(false);
            var model = ConvertTo<Model, Contract>(entity);

            model.ActionError = error;
            return View(model);
        }

        // POST: /Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);

                await ctrl.DeleteAsync(id).ConfigureAwait(false);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Delete", new { error = GetExceptionError(ex) });
            }
        }


        #region Export and Import
        protected override string[] CsvHeader => new string[] { "Id", "SecondItem.Id", "FirstItem.Name", "FirstItem.Email", "SecondItem.Firstname", "SecondItem.Lastname",  "FirstItem.Password", "FirstItem.AccessFailedCount", "FirstItem.EnableJwtAuth" };

        [ActionName("Export")]
        public async Task<FileResult> ExportAsync()
        {
            var fileName = "IdentityUser.csv";
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);
            var entities = (await ctrl.GetAllAsync().ConfigureAwait(false)).Select(e => ConvertTo<Model, Contract>(e));

            return ExportDefault(CsvHeader, entities, fileName);
        }

        [ActionName("Import")]
        public ActionResult ImportAsync(string error = null)
        {
            var model = new Models.Modules.Export.ImportProtocol() { BackController = ControllerName, ActionError = error };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Import")]
        public async Task<IActionResult> ImportAsync()
        {
            var index = 0;
            var model = new Models.Modules.Export.ImportProtocol() { BackController = ControllerName };
            var logInfos = new List<Models.Modules.Export.ImportLog>();
            var importModels = ImportDefault<Model>(CsvHeader);
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);

            foreach (var item in importModels)
            {
                index++;
                try
                {
                    if (item.Action == Models.Modules.Export.ImportAction.Insert)
                    {
                        var entity = await ctrl.CreateAsync();

                        CopyModels(CsvHeader, item.Model, entity);
                        await ctrl.InsertAsync(entity);
                    }
                    else if (item.Action == Models.Modules.Export.ImportAction.Update)
                    {
                        var entity = await ctrl.GetByIdAsync(item.Id);

                        CopyModels(CsvHeader, item.Model, entity);
                        await ctrl.UpdateAsync(entity);
                    }
                    else if (item.Action == Models.Modules.Export.ImportAction.Delete)
                    {
                        await ctrl.DeleteAsync(item.Id);
                    }
                    logInfos.Add(new Models.Modules.Export.ImportLog
                    {
                        IsError = false,
                        Prefix = $"Line: {index} - {item.Action}",
                        Text = "OK",
                    });
                }
                catch (Exception ex)
                {
                    logInfos.Add(new Models.Modules.Export.ImportLog
                    {
                        IsError = true,
                        Prefix = $"Line: {index} - {item.Action}",
                        Text = GetExceptionError(ex),
                    });
                }
            }
            model.LogInfos = logInfos;
            return View(model);
        }
        #endregion Export and Import
    }
}
//MdEnd