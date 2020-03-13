//@QnSBaseCode
//MdStart
using CommonBase.Extensions;
using QuickNSmart.Contracts;

namespace QuickNSmart.Transfer
{
    public partial class TransferObject : Contracts.IIdentifiable
    {
        public virtual int Id { get; set; }
        public virtual byte[] Timestamp { get; set; }
	}
}
//MdEnd