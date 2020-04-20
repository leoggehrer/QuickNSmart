//@QnSIgnore
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickNSmart.Logic.Entities.Persistence.TestRelation;

namespace QuickNSmart.Logic.DataContext.Db
{
    partial class QuickNSmartDbContext
    {
        partial void ConfigureEntityType(EntityTypeBuilder<Invoice> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasIndex(p => p.Subject)
                .IsUnique();

            entityTypeBuilder
                .Property(p => p.Subject)
                .IsRequired()
                .HasMaxLength(256);
            entityTypeBuilder
                .Property(p => p.Street)
                .IsRequired()
                .HasMaxLength(128);
            entityTypeBuilder
                .Property(p => p.ZipCode)
                .IsRequired()
                .HasMaxLength(10);
            entityTypeBuilder
                .Property(p => p.City)
                .IsRequired()
                .HasMaxLength(64);
        }
        partial void ConfigureEntityType(EntityTypeBuilder<InvoiceDetail> entityTypeBuilder)
        {
            entityTypeBuilder
                .Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
