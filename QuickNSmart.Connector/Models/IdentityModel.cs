//@QnSCodeCopy
//MdStart

using QuickNSmart.Connector.Contracts;

namespace QuickNSmart.Connector.Models
{
    public abstract partial class IdentityModel : WrapperModel, IIdentifiable
    {
        //QuickNSmart.Contracts.Persistence.Account.IActionLog Action { get; set; }

        public virtual int Id { get; set; }
        public virtual byte[] Timestamp { get; set; }
	}
}
//MdEnd
