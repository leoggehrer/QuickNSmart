//@QnSIgnore
using System;

namespace QuickNSmart.Contracts.Persistence.TestOneToMany
{
    public partial interface IInvoice : IIdentifiable, ICopyable<IInvoice>
    {
        DateTime Date { get; set; }
        string Subject { get; set; }
        string Street { get; set; }
        string ZipCode { get; set; }
        string City { get; set; }
    }
}
