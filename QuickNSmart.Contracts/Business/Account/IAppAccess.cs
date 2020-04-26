//@QnSBaseCode
//MdStart
using QuickNSmart.Contracts.Persistence.Account;

namespace QuickNSmart.Contracts.Business.Account
{
    public partial interface IAppAccess : IOneToMany<IIdentity, IRole>, ICopyable<IAppAccess>
    {

    }
}
//MdEnd