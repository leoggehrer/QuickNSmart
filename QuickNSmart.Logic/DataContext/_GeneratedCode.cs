namespace QuickNSmart.Logic.DataContext.Db
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	partial class QuickNSmartDbContext : GenericDbContext
	{
		protected DbSet<Entities.Persistence.Account.Application> ApplicationSet
		{
			get;
			set;
		}
		protected DbSet<Entities.Persistence.Account.LoginUser> LoginUserSet
		{
			get;
			set;
		}
		public override DbSet<E> Set<I, E>()
		{
			DbSet<E> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IApplication))
			{
				result = ApplicationSet as DbSet<E>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginUser))
			{
				result = LoginUserSet as DbSet<E>;
			}
			return result;
		}
		partial void DoModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Entities.Persistence.Account.Application>().ToTable(nameof(Entities.Persistence.Account.Application), nameof(Entities.Persistence.Account)).HasKey(nameof(Entities.Persistence.Account.Application.Id));
			modelBuilder.Entity<Entities.Persistence.Account.Application>().Property(p => p.Timestamp).IsRowVersion();
			ConfigureEntityType(modelBuilder.Entity<Entities.Persistence.Account.Application>());
			modelBuilder.Entity<Entities.Persistence.Account.LoginUser>().ToTable(nameof(Entities.Persistence.Account.LoginUser), nameof(Entities.Persistence.Account)).HasKey(nameof(Entities.Persistence.Account.LoginUser.Id));
			modelBuilder.Entity<Entities.Persistence.Account.LoginUser>().Property(p => p.Timestamp).IsRowVersion();
			ConfigureEntityType(modelBuilder.Entity<Entities.Persistence.Account.LoginUser>());
		}
		partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.Application> entityTypeBuilder);
		partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.LoginUser> entityTypeBuilder);
	}
}
