namespace QuickNSmart.Logic.DataContext.Db
{
	using Microsoft.EntityFrameworkCore;
	partial class DbQuickNSmartContext : GenericDbContext
	{
		public override DbSet<E> Set<I, E>()
		{
			DbSet<E> result = null;
			return result;
		}
	}
}
