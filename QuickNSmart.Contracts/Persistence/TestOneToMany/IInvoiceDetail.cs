//@QnSIgnore

namespace QuickNSmart.Contracts.Persistence.TestOneToMany
{
    public partial interface IInvoiceDetail : IIdentifiable, ICopyable<IInvoiceDetail>
    {
        int InvoiceId { get; set; }
        int Order { get; set; }
        string Text { get; set; }
        double Quantity { get; set; }
        double Tax { get; set; }
        double Price { get; set; }
    }
}
