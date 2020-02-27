//@QnSBaseCode
//MdStart

namespace QuickNSmart.Contracts.Persistence.Account
{
    public partial interface IIdentityXApplication : IIdentifiable, ICopyable<IIdentityXApplication>
    {
        int IdentityId { get; set; }
        int ApplicationId { get; set; }
    }
}
//MdEnd