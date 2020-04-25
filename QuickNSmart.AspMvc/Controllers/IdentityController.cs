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
using Model = QuickNSmart.AspMvc.Models.Business.Account.AppAccess;
using Contract = QuickNSmart.Contracts.Business.Account.IAppAccess;
using IdentityUser = QuickNSmart.Contracts.Business.Account.IIdentityUser;

namespace QuickNSmart.AspMvc.Controllers
{
    public partial class IdentityController : AccessController
    {
        private readonly ILogger<IdentityController> _logger;
        public IdentityController(ILogger<IdentityController> logger, IFactoryWrapper factoryWrapper)
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
            await LoadRolesAsync(model).ConfigureAwait(false);
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
            await LoadRolesAsync(model).ConfigureAwait(false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("IdentityEdit")]
        public async Task<IActionResult> IdentityEditAsync(int id, Identity identityModel, IFormCollection collection)
        {
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);
            async Task<IActionResult> CreateFailedAsync(Identity identity, string error)
            {
                var entity = await ctrl.CreateAsync().ConfigureAwait(false);

                entity.Identity.CopyProperties(identity);

                var model = ConvertTo<Model, Contract>(entity);

                model.ActionError = error;
                await LoadRolesAsync(model).ConfigureAwait(false);
                return View("Edit", model);
            }
            async Task<IActionResult> EditFailedAsync(Identity identity, string error)
            {
                var entity = await ctrl.GetByIdAsync(identity.Id).ConfigureAwait(false);

                entity.Identity.CopyProperties(identity);

                var model = ConvertTo<Model, Contract>(entity);

                model.ActionError = error;
                await LoadRolesAsync(model).ConfigureAwait(false);
                return View("Edit", model);
            }
            async Task UpdateRolesAsync(Model model)
            {
                using var ctrlRole = Factory.Create<Contracts.Persistence.Account.IRole>(SessionWrapper.SessionToken);
                var roles = await ctrlRole.GetAllAsync().ConfigureAwait(false);

                model.ClearRoles();
                foreach (var item in collection.Where(l => l.Key.StartsWith("Assigned")))
                {
                    var roleId = item.Key.ToInt();
                    var role = roles.SingleOrDefault(r => r.Id == roleId);

                    if (role != null)
                    {
                        model.AddRole(role);
                    }
                }
            }

            if (ModelState.IsValid == false)
            {
                if (identityModel.Id == 0)
                {
                    return await CreateFailedAsync(identityModel, GetModelStateError()).ConfigureAwait(false);
                }
                else
                {
                    return await EditFailedAsync(identityModel, GetModelStateError()).ConfigureAwait(false);
                }
            }
            try
            {
                if (identityModel.Id == 0)
                {
                    var entity = await ctrl.CreateAsync().ConfigureAwait(false);

                    entity.Identity.CopyProperties(identityModel);
                    var model = ConvertTo<Model, Contract>(entity);

                    await UpdateRolesAsync(model).ConfigureAwait(false);
                    var result = await ctrl.InsertAsync(model).ConfigureAwait(false);

                    id = result.Id;
                }
                else
                {
                    var entity = await ctrl.GetByIdAsync(id).ConfigureAwait(false);

                    var model = ConvertTo<Model, Contract>(entity);
                    model.Identity.CopyProperties(identityModel);
                    await UpdateRolesAsync(model).ConfigureAwait(false);
                    await ctrl.UpdateAsync(model).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                if (identityModel.Id == 0)
                {
                    return await CreateFailedAsync(identityModel, GetExceptionError(ex)).ConfigureAwait(false);
                }
                else
                {
                    return await EditFailedAsync(identityModel, GetExceptionError(ex)).ConfigureAwait(false);
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
        protected override string[] CsvHeader => new string[] { "Id", "Name", "Email", "Password", "Firstname", "Lastname", "AccessFaildCount", "State" };

        [ActionName("Export")]
        public async Task<FileResult> ExportAsync()
        {
            var fileName = $"{typeof(Model).Name}.csv";
            using var ctrl = Factory.Create<IdentityUser>(SessionWrapper.SessionToken);
            var entities = (await ctrl.GetAllAsync().ConfigureAwait(false)).Select(e => new 
            {
                e.Id,
                e.FirstItem.Name,
                e.FirstItem.Email,
                e.FirstItem.Password,
                e.SecondItem.Firstname,
                e.SecondItem.Lastname,
                e.FirstItem.AccessFailedCount,
                e.SecondItem.State
            });

            return ExportDefault(CsvHeader, entities, fileName);
        }

        [ActionName("Import")]
        public ActionResult ImportAsync(string error = null)
        {
            var model = new Models.Modules.Export.ImportProtocol() { ActionError = error };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Import")]
        public async Task<IActionResult> ImportAsync()
        {
            var index = 0;
            var model = new Models.Modules.Export.ImportProtocol();
            var logInfos = new List<Models.Modules.Export.ImportLog>();
            var importModels = ImportDefault<Model>(CsvHeader);
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);

            foreach (var item in importModels)
            {
                index++;
                try
                {
                    if (item.Action == Models.Modules.Export.ImportAction.Delete)
                    {
                        await ctrl.DeleteAsync(item.Id);
                    }
                    else if (item.Action == Models.Modules.Export.ImportAction.Update)
                    {
                        await ctrl.UpdateAsync(item.Model);
                    }
                    else if (item.Action == Models.Modules.Export.ImportAction.Insert)
                    {
                        await ctrl.InsertAsync(item.Model);
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
                        Text = ex.Message,
                    });
                }
            }
            model.LogInfos = logInfos;
            return View(model);
        }
        #endregion Export and Import

        #region Helpers
        private async Task LoadRolesAsync(Model model)
        {
            model.CheckArgument(nameof(model));

            using var ctrlRole = Factory.Create<Contracts.Persistence.Account.IRole>(SessionWrapper.SessionToken);
            var roles = await ctrlRole.GetAllAsync().ConfigureAwait(false);

            foreach (var item in roles)
            {
                var assigned = model.RoleEntities.SingleOrDefault(r => r.Id == item.Id);

                if (assigned != null)
                {
                    assigned.Assigned = true;
                }
                else
                {
                    var role = new Models.Persistence.Account.Role();

                    role.CopyProperties(item);
                    model.RoleEntities.Add(role);
                }
            }
        }
        #endregion Helpers
    }
}
//MdEnd