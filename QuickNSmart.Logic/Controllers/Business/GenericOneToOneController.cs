//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommonBase.Extensions;
using CommonBase.Helpers;
using QuickNSmart.Logic.Exceptions;

namespace QuickNSmart.Logic.Controllers.Business
{
    abstract partial class GenericOneToOneController<I, E, TFirst, TFirstEntity, TSecond, TSecondEntity> : ControllerObject, Contracts.Client.IControllerAccess<I>
        where I : Contracts.IOneToOne<TFirst, TSecond>
        where E : Entities.OneToOneObject<TFirst, TFirstEntity, TSecond, TSecondEntity>, I, Contracts.ICopyable<I>, new()
        where TFirst : Contracts.IIdentifiable, Contracts.ICopyable<TFirst>
        where TFirstEntity : Entities.IdentityObject, TFirst, Contracts.ICopyable<TFirst>, new()
        where TSecond : Contracts.IIdentifiable, Contracts.ICopyable<TSecond>
        where TSecondEntity : Entities.IdentityObject, TSecond, Contracts.ICopyable<TSecond>, new()
    {
        static GenericOneToOneController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        private GenericController<TFirst, TFirstEntity> firstEntityController;
        private GenericController<TSecond, TSecondEntity> secondEntityController;

        public virtual int MaxPageSize => firstEntityController.MaxPageSize;

        public GenericOneToOneController(DataContext.IContext context) : base(context)
        {
            Constructing();
            firstEntityController = CreateFirstEntityController(this);
            secondEntityController = CreateSecondEntityController(this);
            ChangedSessionToken += GenericOneToOneController_ChangedSessionToken;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        public GenericOneToOneController(ControllerObject controller) : base(controller)
        {
            Constructing();
            firstEntityController = CreateFirstEntityController(this);
            secondEntityController = CreateSecondEntityController(this);
            ChangedSessionToken += GenericOneToOneController_ChangedSessionToken;
            Constructed();
        }

        private void GenericOneToOneController_ChangedSessionToken(object sender, EventArgs e)
        {
            firstEntityController.SessionToken = SessionToken;
            secondEntityController.SessionToken = SessionToken;
        }

        protected abstract GenericController<TFirst, TFirstEntity> CreateFirstEntityController(ControllerObject controller);
        protected abstract GenericController<TSecond, TSecondEntity> CreateSecondEntityController(ControllerObject controller);

        protected E ConvertTo(I contract)
        {
            contract.CheckArgument(nameof(contract));

            E result = new E();

            result.CopyProperties(contract);
            return result;
        }
        protected IEnumerable<E> ConvertTo(IEnumerable<I> contracts)
        {
            contracts.CheckArgument(nameof(contracts));

            List<E> result = new List<E>();

            foreach (var item in contracts)
            {
                result.Add(ConvertTo(item));
            }
            return result;
        }

        public virtual Task<int> CountAsync()
        {
            return firstEntityController.CountAsync();
        }
        public virtual Task<int> CountByAsync(string predicate)
        {
            return firstEntityController.CountByAsync(predicate);
        }
        #region Async-Methods
        protected virtual PropertyInfo GetNavigationToOne()
        {
            return typeof(TSecondEntity).GetProperty(typeof(TFirstEntity).Name);
        }
        protected virtual PropertyInfo GetForeignKeyToOne()
        {
            return typeof(TSecondEntity).GetProperty($"{typeof(TFirstEntity).Name}Id");
        }
        protected virtual async Task LoadSecondAsync(E entity, int masterId)
        {
            var predicate = $"{typeof(TFirstEntity).Name}Id == {masterId}";
            var qyr = await secondEntityController.QueryAllAsync(predicate).ConfigureAwait(false);

            if (qyr.Any())
            {
                entity.SecondEntity.CopyProperties(qyr.ElementAt(0));
            }
            else
            {
                entity.SecondEntity.CopyProperties(new TSecondEntity());
            }
        }
        protected virtual async Task<IEnumerable<TSecondEntity>> QueryDetailsAsync(int masterId)
        {
            var result = new List<TSecondEntity>();
            var predicate = $"{typeof(TFirstEntity).Name}Id == {masterId}";

            foreach (var item in (await secondEntityController.QueryAllAsync(predicate).ConfigureAwait(false)).ToList())
            {
                var e = new TSecondEntity();

                e.CopyProperties(item);
                result.Add(e);
            }
            return result;
        }
        public virtual async Task<I> GetByIdAsync(int id)
        {
            var result = default(E);
            var firstEntity = await firstEntityController.GetByIdAsync(id).ConfigureAwait(false);

            if (firstEntity != null)
            {
                var predicate = $"{typeof(TFirstEntity).Name}Id == {firstEntity.Id}";

                result = new E();
                result.FirstItem.CopyProperties(firstEntity);
                await LoadSecondAsync(result, firstEntity.Id).ConfigureAwait(false);
            }
            else
            {
                throw new LogicException(ErrorType.InvalidId);
            }
            return result;
        }
        public virtual Task<IQueryable<I>> GetPageListAsync(int pageIndex, int pageSize)
        {
            return Task.Run(async () =>
            {
                List<I> result = new List<I>();

                foreach (var item in (await firstEntityController.GetPageListAsync(pageIndex, pageSize).ConfigureAwait(false)).ToList())
                {
                    E entity = new E();

                    entity.FirstItem.CopyProperties(item);
                    await LoadSecondAsync(entity, item.Id).ConfigureAwait(false);

                    result.Add(entity);
                }
                return result.AsQueryable();
            });
        }
        public virtual Task<IQueryable<I>> GetAllAsync()
        {
            return Task.Run(async () =>
            {
                List<I> result = new List<I>();

                foreach (var item in (await firstEntityController.GetAllAsync().ConfigureAwait(false)).ToList())
                {
                    E entity = new E();

                    entity.FirstItem.CopyProperties(item);
                    await LoadSecondAsync(entity, item.Id).ConfigureAwait(false);

                    result.Add(entity);
                }
                return result.AsQueryable();
            });
        }

        public virtual Task<IQueryable<I>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            return Task.Run(async () =>
            {
                List<I> result = new List<I>();

                foreach (var item in (await firstEntityController.QueryPageListAsync(predicate, pageIndex, pageSize).ConfigureAwait(false)).ToList())
                {
                    E entity = new E();

                    entity.FirstItem.CopyProperties(item);
                    await LoadSecondAsync(entity, item.Id).ConfigureAwait(false);

                    result.Add(entity);
                }
                return result.AsQueryable();
            });
        }
        public virtual Task<IQueryable<I>> QueryAllAsync(string predicate)
        {
            return Task.Run(async () =>
            {
                List<I> result = new List<I>();

                foreach (var item in (await firstEntityController.QueryAllAsync(predicate).ConfigureAwait(false)).ToList())
                {
                    E entity = new E();

                    entity.FirstItem.CopyProperties(item);
                    await LoadSecondAsync(entity, item.Id).ConfigureAwait(false);

                    result.Add(entity);
                }
                return result.AsQueryable();
            });
        }

        public virtual Task<I> CreateAsync()
        {
            return Task.Run<I>(() => new E());
        }

        public virtual async Task<I> InsertAsync(I entity)
        {
            entity.CheckArgument(nameof(entity));
            entity.FirstItem.CheckArgument(nameof(entity.FirstItem));
            entity.SecondItem.CheckArgument(nameof(entity.SecondItem));

            var result = new E();

            result.FirstEntity.CopyProperties(entity.FirstItem);
            await firstEntityController.InsertAsync(result.FirstEntity).ConfigureAwait(false);

            result.SecondEntity.CopyProperties(entity.SecondItem);
            var pi = GetNavigationToOne();

            if (pi != null)
            {
                pi.SetValue(result.SecondEntity, result.FirstEntity);
            }
            await secondEntityController.InsertAsync(result.SecondEntity).ConfigureAwait(false);
            return result;
        }

        public virtual async Task<I> UpdateAsync(I entity)
        {
            entity.CheckArgument(nameof(entity));
            entity.FirstItem.CheckArgument(nameof(entity.FirstItem));
            entity.SecondItem.CheckArgument(nameof(entity.SecondItem));

            var result = new E();

            result.FirstEntity.CopyProperties(entity.FirstItem);
            await firstEntityController.UpdateAsync(result.FirstEntity).ConfigureAwait(false);

            result.SecondEntity.CopyProperties(entity.SecondItem);
            if (result.SecondEntity.Id == 0)
            {
                var pi = GetForeignKeyToOne();

                if (pi != null)
                {
                    pi.SetValue(result.SecondEntity, result.FirstEntity.Id);
                }
                await secondEntityController.InsertAsync(result.SecondEntity).ConfigureAwait(false);
            }
            else
            {
                await secondEntityController.UpdateAsync(result.SecondEntity).ConfigureAwait(false);
            }
            return result;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id).ConfigureAwait(false);

            if (entity != null)
            {
                if (entity.SecondItem.Id > 0)
                {
                    await secondEntityController.DeleteAsync(entity.SecondItem.Id).ConfigureAwait(false);
                }
                await firstEntityController.DeleteAsync(entity.Id).ConfigureAwait(false);
            }
            else
            {
                throw new LogicException(ErrorType.InvalidId);
            }
        }

        public virtual Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
        #endregion Async-Methods

        #region Invoke handler
        public virtual Task InvokeActionAsync(string name, params object[] parameters)
        {
            var helper = new InvokeHelper();

            return helper.InvokeActionAsync(this, name, parameters);
        }
        public virtual Task<object> InvokeFunctionAsync(string name, params object[] parameters)
        {
            var helper = new InvokeHelper();

            return helper.InvokeFunctionAsync(this, name, parameters);
        }
        #endregion Invoke handler

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                firstEntityController.Dispose();
                secondEntityController.Dispose();

                firstEntityController = null;
                secondEntityController = null;
            }
        }
    }
}
//MdEnd