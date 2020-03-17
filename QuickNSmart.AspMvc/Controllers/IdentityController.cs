//@QnSBaseCode
//MdStart
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model = QuickNSmart.AspMvc.Models.Business.Account.AppAccess;
using Contract = QuickNSmart.Contracts.Business.Account.IAppAccess;
using QuickNSmart.AspMvc.Models.Persistence.Account;
using System;

namespace QuickNSmart.AspMvc.Controllers
{
    public partial class IdentityController : MvcController
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
            var entities = await ctrl.GetAllAsync();

            return View(entities.Select(e => ConvertTo<Model, Contract>(e))); 
        }
        [ActionName("Edit")]
        public async Task<IActionResult> EditAsync(int id, string error = null)
        {
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);
            var entity = await ctrl.GetByIdAsync(id);
            var model = ConvertTo<Model, Contract>(entity);

            model.ActionError = error;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("IdentityEdit")]
        public async Task<IActionResult> IdentityEditAsync(int id, Identity model)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Edit", new { id, error = GetModelStateError() });
            }
            try
            {
                using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);
                var entity = await ctrl.GetByIdAsync(id);

                entity.Identity.CopyProperties(model);
                await ctrl.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Edit", new { id, error = GetExceptionError(ex) });
            }
            return RedirectToAction("Edit", new { id });
        }

        [ActionName("Details")]
        public async Task<IActionResult> DetailsAsync(int id)
        {
            using var ctrl = Factory.Create<Contract>(SessionWrapper.SessionToken);
            var entity = await ctrl.GetByIdAsync(id);

            return View(entity != null ? ConvertTo<Model, Contract>(entity) : entity);
        }
    }
}
//MdEnd