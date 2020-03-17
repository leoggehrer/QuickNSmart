//@QnSBaseCode
//MdStart
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickNSmart.AspMvc.Models.Modules.Account;
using QuickNSmart.AspMvc.Models.Persistence.Account;
using Model = QuickNSmart.AspMvc.Models.Persistence.Account.LoginSession;
using Contract = QuickNSmart.Contracts.Persistence.Account.ILoginSession;
using AccountManager = QuickNSmart.Adapters.Modules.Account.AccountManager;

namespace QuickNSmart.AspMvc.Controllers
{
    public partial class AccountController : MvcController
    {
        static AccountController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, IFactoryWrapper factoryWrapper)
            : base(factoryWrapper)
        {
            Constructing();
            _logger = logger;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        public IActionResult Logon(string returnUrl = null, string message = null)
        {
            var model = new LogonViewModel();

            SessionWrapper.ReturnUrl = returnUrl;
            SessionWrapper.Hint = message;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Logon")]
        public async Task<IActionResult> LogonAsync(LogonViewModel model, string returnUrl)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            BeforeLogon();
            try
            {
                var accMngr = new AccountManager();
                var loginResult = await accMngr.LogonAsync(model.Email, model.Password).ConfigureAwait(false);
                var loginSession = new LoginSession();

                loginSession.CopyProperties(loginResult);
                SessionWrapper.LoginSession = loginSession;
            }
            catch (Exception ex)
            {
                model.ActionError = ex.Message;
                return View(model);
            }
            AfterLogon();

            if (string.IsNullOrEmpty(returnUrl) == false)
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        partial void BeforeLogon();
        partial void AfterLogon();

        [ActionName("Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            if (SessionWrapper.LoginSession != null)
            {
                BeforeLogout();
                var accMngr = new AccountManager();
                await accMngr.LogoutAsync(SessionWrapper.LoginSession.SessionToken).ConfigureAwait(false);
                SessionWrapper.LoginSession = null;
                AfterLogout();
            }
            return RedirectToAction("Index", "Home");
        }

        partial void BeforeLogout();
        partial void AfterLogout();

        public IActionResult ChangePassword()
        {
            if (SessionWrapper.LoginSession == null
                || SessionWrapper.LoginSession.LogoutTime.HasValue)
            {
                return RedirectToAction("Logon", new { returnUrl = "ChangePassword" });
            }

            var model = new ChangePasswordViewModel()
            {
                UserName = SessionWrapper.LoginSession.Name,
                Email = SessionWrapper.LoginSession.Email,
            };
            return View("ChangePassword", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (SessionWrapper.LoginSession == null
                || SessionWrapper.LoginSession.LogoutTime.HasValue)
            {
                return RedirectToAction("Logon", new { returnUrl = "ChangePassword" });
            }

            try
            {
                var accMngr = new AccountManager();

                await accMngr.ChangePasswordAsync(SessionWrapper.LoginSession.SessionToken, model.OldPassword, model.NewPassword).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                model.ActionError = GetExceptionError(ex);
                return View("ChangePassword", model);
            }
            return View("ConfirmationChangePassword");
        }

        public IActionResult ResetPassword()
        {
            if (SessionWrapper.LoginSession == null
                || SessionWrapper.LoginSession.LogoutTime.HasValue)
            {
                return RedirectToAction("Logon", new { returnUrl = "ChangePassword" });
            }

            var model = new ResetPasswordViewModel();

            return View("ResetPassword", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ResetPassword")]
        public async Task<IActionResult> ChangePasswordAsync(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            if (SessionWrapper.LoginSession == null
                || SessionWrapper.LoginSession.LogoutTime.HasValue)
            {
                return RedirectToAction("Logon", new { returnUrl = "ResetPassword" });
            }

            try
            {
                var accMngr = new AccountManager();

                await accMngr.ChangePasswordForAsync(SessionWrapper.LoginSession.SessionToken, model.Email, model.ConfirmPassword).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                model.ActionError = GetExceptionError(ex);
                return View("ResetPassword", model);
            }
            return View("ConfirmationChangePassword");
        }
    }
}
//MdEnd