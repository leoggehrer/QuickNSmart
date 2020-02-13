//@QnSBaseCode
//MdStart

namespace QuickNSmart.Contracts.Persistence.Account
{
    public partial interface IUserXRole : IIdentifiable, ICopyable<IUserXRole>
    {
        int UserId { get; set; }
        int RoleId { get; set; }
    }
}
//MdEnd