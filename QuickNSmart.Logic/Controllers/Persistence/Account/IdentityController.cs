//@QnSBaseCode
//MdStart
using System.Threading.Tasks;
using QuickNSmart.Adapters.Exceptions;
using QuickNSmart.Logic.Entities.Persistence.Account;

namespace QuickNSmart.Logic.Controllers.Persistence.Account
{
    partial class IdentityController
    {
        private void CheckEntity(Identity entity)
        {
            if (Modules.Account.AccountManager.CheckMailAddressSyntax(entity.Email) == false)
            {
                throw new LogicException(ErrorType.InvalidEmail, "Invalid email syntax.");
            }
            if (Modules.Account.AccountManager.CheckPasswordSyntax(entity.Password) == false)
            {
                throw new LogicException(ErrorType.InvalidPassword, "Invalid password syntax.");
            }
        }

        protected override Task BeforeInsertingAsync(Identity entity)
        {
            CheckEntity(entity);
            entity.Guid = System.Guid.NewGuid().ToString();
            entity.State = Contracts.State.Active;

            return base.BeforeInsertingAsync(entity);
        }
        protected override Task BeforeUpdatingAsync(Identity entity)
        {
            CheckEntity(entity);

            return base.BeforeUpdatingAsync(entity);
        }
    }
}
//MdEnd