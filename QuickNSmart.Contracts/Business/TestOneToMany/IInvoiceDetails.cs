//@QnSIgnore
using QuickNSmart.Contracts.Persistence.TestOneToMany;

namespace QuickNSmart.Contracts.Business.TestOneToMany
{
    public partial interface IInvoiceDetails : IOneToMany<IInvoice, IInvoiceDetail>, ICopyable<IInvoiceDetails>
    {
    }
}
