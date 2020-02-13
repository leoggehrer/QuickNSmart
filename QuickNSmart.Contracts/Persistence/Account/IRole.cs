//@QnSBaseCode
//MdStart
namespace QuickNSmart.Contracts.Persistence.Account
{
    public partial interface IRole : IIdentifiable, ICopyable<IRole>
    {
        string Designation { get; set; }
        string Description { get; set; }
    }
}
//MdEnd