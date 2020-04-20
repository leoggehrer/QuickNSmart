//@QnSIgnore
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickNSmart.Logic.Entities.Persistence.TestRelation;

namespace QuickNSmart.Logic.DataContext.Db
{
    partial class QuickNSmartDbContext
    {
        partial void ConfigureEntityType(EntityTypeBuilder<Invoice> entityTypeBuilder)
        {

        }
        partial void ConfigureEntityType(EntityTypeBuilder<InvoiceDetail> entityTypeBuilder)
        {
        }
    }
}
