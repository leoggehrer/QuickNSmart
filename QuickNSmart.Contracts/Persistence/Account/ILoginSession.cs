//@QnSBaseCode
//MdStart
using System;

namespace QuickNSmart.Contracts.Persistence.Account
{
    public partial interface ILoginSession : IIdentifiable, ICopyable<ILoginSession>
    {
        int IdentityId { get; }
        string JsonWebToken { get; }
        string SessionToken { get; }
        DateTime LoginTime { get; }
        DateTime LastAccess { get; }
        DateTime? LogoutTime { get; }
    }
}
//MdEnd