//@QnSBaseCode
//MdStart
namespace QuickNSmart.Transfer
{
    public class TransferObject : Contracts.IIdentifiable
    {
        public virtual int Id { get; set; }

        public virtual byte[] Timestamp { get; set; }
    }
}
//MdEnd