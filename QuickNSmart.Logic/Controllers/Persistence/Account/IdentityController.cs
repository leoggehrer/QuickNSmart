//@QnSBaseCode
//MdStart
using System.Threading.Tasks;
using QuickNSmart.Logic.Entities.Persistence.Account;

namespace QuickNSmart.Logic.Controllers.Persistence.Account
{
    partial class IdentityController
    {
        protected override Task BeforeInsertingAsync(Identity entity)
        {
            entity.Guid = System.Guid.NewGuid().ToString();
            entity.State = Contracts.State.Active;

            return base.BeforeInsertingAsync(entity);
        }
    }
}
//MdEnd