//@QnSIgnore
using QuickNSmart.Contracts.Persistence.TestRelation;

namespace QuickNSmart.Contracts.Business.TestRelation
{
    public partial interface IInvoiceDetails : IRelation<IInvoice, IInvoiceDetail>, ICopyable<IInvoiceDetails>
    {
    }
}
