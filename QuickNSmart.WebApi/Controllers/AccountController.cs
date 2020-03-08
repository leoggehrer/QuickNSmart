//@QnSBaseCode
//MdStart
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuickNSmart.Contracts.Persistence.Account;

namespace QuickNSmart.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class AccountController : ControllerBase
    {
        private ILoginSession ConvertTo(ILoginSession loginSession)
        {
            var result = new Transfer.Persistence.Account.LoginSession();

            result.CopyProperties(loginSession);
            return result;
        }
        [HttpGet("/api/[controller]/Logon/{jwt}")]
        public async Task<ILoginSession> LogonAsync(string jwt)
        {
            return ConvertTo(await Logic.Modules.Account.AccountManager.LogonAsync(jwt).ConfigureAwait(false));
        }
        [HttpGet("/api/[controller]/Logon/{email}/{password}")]
        public async Task<ILoginSession> LogonAsync(string email, string password)
        {
            return ConvertTo(await Logic.Modules.Account.AccountManager.LogonAsync(email, password).ConfigureAwait(false));
        }
        [HttpGet("/api/[controller]/Logout/{sessionToken}")]
        public Task LogoutAsync(string sessionToken)
        {
            return Logic.Modules.Account.AccountManager.LogoutAsync(sessionToken);
        }
    }
}
//MdEnd