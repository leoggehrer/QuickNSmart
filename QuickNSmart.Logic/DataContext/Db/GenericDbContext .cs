//@QnSBaseCode
//MdStart
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickNSmart.Contracts;
using QuickNSmart.Logic.Entities;
using System.Collections.Generic;

namespace QuickNSmart.Logic.DataContext.Db
{
    abstract partial class GenericDbContext : DbContext, IContext
    {
        IEnumerable<E> IContext.Set<I, E>()
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

        public Task SaveAsync()
        {
            return SaveChangesAsync();
        }
    }
}
//MdEnd
