namespace QuickNSmart.Logic.DataContext.Db
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	partial class QuickNSmartDbContext : GenericDbContext
	{
		protected DbSet<Entities.Persistence.Account.LoginSession> LoginSessionSet
		{
			get;
			set;
		}
		protected DbSet<Entities.Persistence.Account.Role> RoleSet
		{
			get;
			set;
		}
		protected DbSet<Entities.Persistence.Account.User> UserSet
		{
			get;
			set;
		}
		protected DbSet<Entities.Persistence.Account.UserXRole> UserXRoleSet
		{
			get;
			set;
		}
		public override DbSet<E> Set<I, E>()
		{
			DbSet<E> result = null;
			if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.ILoginSession))
			{
				result = LoginSessionSet as DbSet<E>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IRole))
			{
				result = RoleSet as DbSet<E>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUser))
			{
				result = UserSet as DbSet<E>;
			}
			else if (typeof(I) == typeof(QuickNSmart.Contracts.Persistence.Account.IUserXRole))
			{
				result = UserXRoleSet as DbSet<E>;
			}
			return result;
		}
		partial void DoModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Entities.Persistence.Account.LoginSession>().ToTable(nameof(Entities.Persistence.Account.LoginSession), nameof(Entities.Persistence.Account)).HasKey(nameof(Entities.Persistence.Account.LoginSession.Id));
			modelBuilder.Entity<Entities.Persistence.Account.LoginSession>().Property(p => p.Timestamp).IsRowVersion();
			ConfigureEntityType(modelBuilder.Entity<Entities.Persistence.Account.LoginSession>());
			modelBuilder.Entity<Entities.Persistence.Account.Role>().ToTable(nameof(Entities.Persistence.Account.Role), nameof(Entities.Persistence.Account)).HasKey(nameof(Entities.Persistence.Account.Role.Id));
			modelBuilder.Entity<Entities.Persistence.Account.Role>().Property(p => p.Timestamp).IsRowVersion();
			ConfigureEntityType(modelBuilder.Entity<Entities.Persistence.Account.Role>());
			modelBuilder.Entity<Entities.Persistence.Account.User>().ToTable(nameof(Entities.Persistence.Account.User), nameof(Entities.Persistence.Account)).HasKey(nameof(Entities.Persistence.Account.User.Id));
			modelBuilder.Entity<Entities.Persistence.Account.User>().Property(p => p.Timestamp).IsRowVersion();
			ConfigureEntityType(modelBuilder.Entity<Entities.Persistence.Account.User>());
			modelBuilder.Entity<Entities.Persistence.Account.UserXRole>().ToTable(nameof(Entities.Persistence.Account.UserXRole), nameof(Entities.Persistence.Account)).HasKey(nameof(Entities.Persistence.Account.UserXRole.Id));
			modelBuilder.Entity<Entities.Persistence.Account.UserXRole>().Property(p => p.Timestamp).IsRowVersion();
			ConfigureEntityType(modelBuilder.Entity<Entities.Persistence.Account.UserXRole>());
		}
		partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.LoginSession> entityTypeBuilder);
		partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.Role> entityTypeBuilder);
		partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.User> entityTypeBuilder);
		partial void ConfigureEntityType(EntityTypeBuilder<Entities.Persistence.Account.UserXRole> entityTypeBuilder);
	}
}
