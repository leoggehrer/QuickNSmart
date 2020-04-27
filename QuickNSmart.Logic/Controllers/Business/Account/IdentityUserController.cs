//@QnSBaseCode
//MdStart
using System.Threading.Tasks;
using QuickNSmart.Contracts.Business.Account;

namespace QuickNSmart.Logic.Controllers.Business.Account
{
    partial class IdentityUserController
    {
        public override async Task<IIdentityUser> CreateAsync()
        {
            var result = await base.CreateAsync().ConfigureAwait(false);

            result.FirstItem.State = Contracts.Modules.Common.State.Active;
            result.SecondItem.State = Contracts.Modules.Common.State.Active;
            return result;
        }
    }
}
//MdEnd