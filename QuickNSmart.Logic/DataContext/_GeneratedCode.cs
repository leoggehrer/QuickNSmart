namespace QuickNSmart.Logic.DataContext.Db
{
	using Microsoft.EntityFrameworkCore;
	partial class QuickNSmartDbContext : GenericDbContext
	{
		public override DbSet<E> Set<I, E>()
		{
			DbSet<E> result = null;
			return result;
		}
	}
}
