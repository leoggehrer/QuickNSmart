//@QnSBaseCode
//MdStart
namespace QuickNSmart.Contracts.Persistence.Account
{
    public partial interface IClient : IIdentifiable, ICopyable<IClient>
    {
        string Guid { get; }
        string Name { get; set; }
        string Key { get; set; }
        State State { get; set; }
    }
}
//MdEnd