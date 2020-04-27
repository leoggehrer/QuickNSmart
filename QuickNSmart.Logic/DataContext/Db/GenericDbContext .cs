//@QnSBaseCode
//MdStart
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickNSmart.Contracts;
using QuickNSmart.Logic.Entities;
using System.Linq.Dynamic.Core;

namespace QuickNSmart.Logic.DataContext.Db
{
    abstract partial class GenericDbContext : DbContext, IContext
    {
        static GenericDbContext()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        public GenericDbContext()
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        DbSet<E> IContext.ContextSet<I, E>()
        {
            return Set<I, E>();
        }
        IQueryable<E> IContext.QueryableSet<I, E>()
        {
            return Set<I, E>();
        }
        public abstract DbSet<E> Set<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, I;

        public Task<int> CountAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, I
        {
            return Set<I, E>().CountAsync();
        }
        public Task<int> CountByAsync<I, E>(string predicate)
            where I : IIdentifiable
            where E : IdentityObject, I
        {
            return Set<I, E>().Where(predicate).CountAsync();
        }

        public Task<E> CreateAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new()
        {
            return Task.Run(() => new E());
        }

        public Task<E> InsertAsync<I, E>(E entity)
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new()
        {
            return Task.Run(() =>
            {
                Set<I, E>().Add(entity);
                return entity;
            });
        }
        public Task<E> UpdateAsync<I, E>(E entity)
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new()
        {
            return Task.Run(() =>
            {
                Set<I, E>().Update(entity);
                return entity;
            });
        }
        public Task<E> DeleteAsync<I, E>(int id)
            where I : IIdentifiable
            where E : IdentityObject, I
        {
            return Task.Run(() =>
            {
                E result = Set<E>().SingleOrDefault(i => i.Id == id);

                if (result != null)
                {
                    Set<I, E>().Remove(result);
                }
                return result;
            });
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
        public Task<int> RejectChangesAsync()
        {
            return Task.Run(() =>
            {
                int count = 0;
                foreach (var item in ChangeTracker.Entries())
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            count++;
                            item.State = EntityState.Modified; //Revert changes made to deleted entity.
                            item.State = EntityState.Unchanged;
                            break;
                        case EntityState.Added:
                            count++;
                            item.State = EntityState.Detached;
                            break;
                    }
                }
                return count;
            });
        }
    }
}
//MdEnd
