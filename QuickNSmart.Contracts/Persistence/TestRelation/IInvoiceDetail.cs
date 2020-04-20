//@QnSIgnore

namespace QuickNSmart.Contracts.Persistence.TestRelation
{
    public partial interface IInvoiceDetail : IIdentifiable, ICopyable<IInvoiceDetail>
    {
        int Order { get; set; }
        string Text { get; set; }
        double Quantity { get; set; }
        double Tax { get; set; }
        double Price { get; set; }
    }
}
