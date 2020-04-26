//@QnSBaseCode
//MdStart
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickNSmart.Contracts;
using QuickNSmart.Logic.Entities;

namespace QuickNSmart.Logic.DataContext
{
    internal interface IContext : IDisposable
    {
        DbSet<E> ContextSet<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, I;

        IQueryable<E> QueryableSet<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, I;

        #region Async-Methods
        Task<int> CountAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, I;

        Task<int> CountByAsync<I, E>(string predicate)
            where I : IIdentifiable
            where E : IdentityObject, I;

        Task<E> CreateAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new();

        Task<E> InsertAsync<I, E>(E entity)
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new();

        Task<E> UpdateAsync<I, E>(E entity)
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new();

        Task<E> DeleteAsync<I, E>(int id)
            where I : IIdentifiable
            where E : IdentityObject, I;

        Task<int> SaveChangesAsync();
        #endregion Async-Methods
    }
}
//MdEnd
