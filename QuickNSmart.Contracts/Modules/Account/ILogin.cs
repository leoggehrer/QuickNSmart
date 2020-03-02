//@QnSBaseCode
//MdStart
using System;

namespace QuickNSmart.Contracts.Modules.Account
{
    public interface ILogin : ICopyable<ILogin>
    {
        int IdentityId { get; }
        string Guid { get; }
        string Name { get; }
        string Email { get; }
        DateTime LoginTime { get; }
        string AuthenticationToken { get; }
    }
}
//MdEnd