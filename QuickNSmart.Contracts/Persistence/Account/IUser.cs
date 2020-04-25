//@QnSBaseCode
//MdStart
using QuickNSmart.Contracts.Modules.Common;

namespace QuickNSmart.Contracts.Persistence.Account
{
    public partial interface IUser : IIdentifiable, ICopyable<IUser>
    {
        int IdentityId { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Fullname { get; }
        State State { get; set; }
    }
}
//MdEnd