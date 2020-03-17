//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Reflection;
using CommonBase.Extensions;
using QuickNSmart.Contracts.Client;
using QuickNSmart.Logic.DataContext;
using QuickNSmart.Logic.Modules.Security;
using System.Linq.Expressions;
using QuickNSmart.Logic.Exceptions;

namespace QuickNSmart.Logic.Controllers
{
    /// <inheritdoc cref="IControllerAccess{T}"/>
    /// <summary>
    /// This generic class implements the base properties and operations defined in the interface. 
    /// </summary>
    /// <typeparam name="E">The entity type of element in the controller.</typeparam>
    /// <typeparam name="I">The interface type which implements the entity.</typeparam>
    [Authorize]
    internal abstract partial class GenericController<I, E> : ControllerObject, IControllerAccess<I>
        where I : Contracts.IIdentifiable
        where E : Entities.IdentityObject, I, Contracts.ICopyable<I>, new()
    {
        static GenericController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public int MaxPageSize => 500;

        internal IQueryable<E> Set() => Context.Set<I, E>();

        protected GenericController(IContext context)
            : base(context)
        {
            Constructing();
            Constructed();
        }
        protected GenericController(ControllerObject controllerObject)
            : base(controllerObject)
        {
            Constructing();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        #region Async-Methods
        public Task<int> CountAsync()
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());

            return Context.CountAsync<I, E>();
        }
        public virtual Task<I> GetByIdAsync(int id)
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());

            return Task.Run<I>(() => Set().SingleOrDefault(i => i.Id == id));
        }
        public async virtual Task<IQueryable<I>> GetAllAsync()
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());

            int idx = 0;
            int qryCount = 0;
            List<I> result = new List<I>();

            do
            {
                var qry = await GetPageListAsync(idx++, MaxPageSize).ConfigureAwait(false);
                
                qryCount = qry.Count();
                result.AddRange(qry);
            } while (qryCount == MaxPageSize);
            return result.AsQueryable();
        }
        public virtual Task<IQueryable<I>> GetPageListAsync(int pageIndex, int pageSize)
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());

            if (pageSize < 1 && pageSize > MaxPageSize)
                throw new LogicException(ErrorType.InvalidPageSize);

            return Task.FromResult<IQueryable<I>>(Set().Skip(pageIndex * pageSize).Take(pageSize));
        }
        public virtual Task<IQueryable<I>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());

            if (pageSize < 1 && pageSize > MaxPageSize)
                throw new LogicException(ErrorType.InvalidPageSize);

            return Task.FromResult<IQueryable<I>>(Set().AsQueryable()
                     .Where(predicate)
                     .Skip(pageIndex * pageSize)
                     .Take(pageSize));
        }
        public virtual Task<I> CreateAsync()
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());

            return Task.Run<I>(() => new E());
        }

        protected virtual Task BeforeInsertingUpdateingAsync(E entiy)
        {
            return Task.FromResult(0);
        }
        protected virtual Task AfterInsertedUpdatedAsync(E entiy)
        {
            return Task.FromResult(0);
        }
        protected virtual Task BeforeInsertingAsync(E entity)
        {
            return Task.FromResult(0);
        }
        public virtual async Task<I> InsertAsync(I entity)
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());
            entity.CheckArgument(nameof(entity));

            var entityModel = new E();

            entityModel.CopyProperties(entity);
            await BeforeInsertingUpdateingAsync(entityModel).ConfigureAwait(false);
            await BeforeInsertingAsync(entityModel).ConfigureAwait(false);
            var result = await Context.InsertAsync<I, E>(entityModel).ConfigureAwait(false);
            await AfterInsertedAsync(result).ConfigureAwait(false);
            await AfterInsertedUpdatedAsync(result).ConfigureAwait(false);
            return result;
        }
        internal virtual async Task<I> InsertAsync(E entity)
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());
            entity.CheckArgument(nameof(entity));

            await BeforeInsertingUpdateingAsync(entity).ConfigureAwait(false);
            await BeforeInsertingAsync(entity).ConfigureAwait(false);
            var result = await Context.InsertAsync<I, E>(entity).ConfigureAwait(false);
            await AfterInsertedAsync(result).ConfigureAwait(false);
            await AfterInsertedUpdatedAsync(result).ConfigureAwait(false);
            return result;
        }
        protected virtual Task AfterInsertedAsync(E entity)
        {
            return Task.FromResult(0);
        }

        protected virtual Task BeforeUpdatingAsync(E entity)
        {
            return Task.FromResult(0);
        }
        public virtual async Task<I> UpdateAsync(I entity)
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());
            entity.CheckArgument(nameof(entity));

            var entityModel = Set().SingleOrDefault(i => i.Id == entity.Id);

            if (entityModel != null)
            {
                entityModel.CopyProperties(entity);
                await BeforeInsertingUpdateingAsync(entityModel).ConfigureAwait(false);
                await BeforeUpdatingAsync(entityModel).ConfigureAwait(false);
                var result = await Context.UpdateAsync<I, E>(entityModel).ConfigureAwait(false);
                await AfterUpdatedAsync(result).ConfigureAwait(false);
                await AfterInsertedUpdatedAsync(result).ConfigureAwait(false);
                return result;
            }
            else
            {
                throw new LogicException(ErrorType.InvalidId);
            }
        }
        internal virtual async Task<I> UpdateAsync(E entity)
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());
            entity.CheckArgument(nameof(entity));

            await BeforeInsertingUpdateingAsync(entity).ConfigureAwait(false);
            await BeforeUpdatingAsync(entity).ConfigureAwait(false);
            var result = await Context.UpdateAsync<I, E>(entity).ConfigureAwait(false);
            await AfterUpdatedAsync(result).ConfigureAwait(false);
            await AfterInsertedUpdatedAsync(result).ConfigureAwait(false);
            return result;
        }
        protected virtual Task AfterUpdatedAsync(E entity)
        {
            return Task.FromResult(0);
        }

        protected virtual Task BeforeDeletingAsync(int id)
        {
            return Task.FromResult(0);
        }
        public async Task DeleteAsync(int id)
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());

            await BeforeDeletingAsync(id).ConfigureAwait(false);
            var item = await Context.DeleteAsync<I, E>(id).ConfigureAwait(false);

            if (item != null)
            {
                await AfterDeletedAsync(item).ConfigureAwait(false);
            }
        }
        protected virtual Task AfterDeletedAsync(E entity)
        {
            return Task.FromResult(0);
        }

        public Task SaveChangesAsync()
        {
            CheckAuthorization(GetType(), MethodBase.GetCurrentMethod());

            return Context.SaveAsync();
        }
        #endregion Async-Methods

        #region Internal-Methods
        internal virtual E QueryById(int id)
        {
            return Set().SingleOrDefault(i => i.Id == id);
        }
        internal virtual IQueryable<E> Query(Expression<Func<E, bool>> predicate)
        {
            return Set().Where(predicate);
        }
        internal virtual IQueryable<E> Query(string predicate, int pageIndex, int pageSize)
        {
            if (pageSize < 1 && pageSize > MaxPageSize)
                throw new LogicException(ErrorType.InvalidPageSize);

            return Set().Where(predicate)
                       .Skip(pageIndex * pageSize)
                       .Take(pageSize);
        }

        //internal virtual async Task<IEnumerable<I>> QueryAsyncFacade(Expression<Func<I, bool>> predicate)
        //{
        //    var newPredicate = ExpressionConverter.ConvertToObject<I, bool, E, bool>(predicate);

        //    return await QueryAsync(newPredicate).ToArrayAsync().ConfigureAwait(false);
        //}
        #endregion Internal-Methods
    }
}
//MdEnd
