//@QnSBaseCode
//MdStart
using QuickNSmart.Contracts.Persistence.Account;

namespace QuickNSmart.Contracts.Business.Account
{
    public partial interface IIdentityUser : IOneToOne<IIdentity, IUser>, ICopyable<IIdentityUser>
    {
    }
}
//MdEnd