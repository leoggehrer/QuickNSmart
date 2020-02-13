//@QnSBaseCode
//MdStart
using System.Collections.Generic;
using QuickNSmart.Contracts.Persistence.Account;

namespace QuickNSmart.Contracts.Business.Account
{
    public partial interface ILoginUser : IIdentifiable, ICopyable<ILoginUser>
    {
        IUser User { get; }
        IEnumerable<IRole> Roles { get; }

        IRole CreateRole();
        void AddRole(IRole role);
        void RemoveRole(IRole role);
    }
}
//MdEnd