//@QnSBaseCode
//MdStart
namespace QuickNSmart.Contracts.Persistence.Account
{
    public partial interface IIdentity : IIdentifiable, ICopyable<IIdentity>
    {
        string Guid { get; }
        string UserName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        State State { get; set; }
    }
}
//MdEnd