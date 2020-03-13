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

		public void CopyProperties(IIdentifiable other)
		{
			other.CheckArgument(nameof(other));

			bool handled = false;
			BeforeCopyProperties(other, ref handled);
			if (handled == false)
			{
				Id = other.Id;
				Timestamp = other.Timestamp;
			}
			AfterCopyProperties(other);
		}
		partial void BeforeCopyProperties(IIdentifiable other, ref bool handled);
		partial void AfterCopyProperties(IIdentifiable other);
	}
}
//MdEnd