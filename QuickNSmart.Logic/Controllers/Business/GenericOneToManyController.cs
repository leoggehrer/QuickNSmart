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
    abstract partial class GenericOneToManyController<I, E, TFirst, TFirstEntity, TSecond, TSecondEntity> : ControllerObject, Contracts.Client.IControllerAccess<I>
		where I : Contracts.IOneToMany<TFirst, TSecond>
		where E : Entities.OneToManyObject<TFirst, TFirstEntity, TSecond, TSecondEntity>, I, Contracts.ICopyable<I>, new()
		where TFirst : Contracts.IIdentifiable, Contracts.ICopyable<TFirst>
		where TFirstEntity : Entities.IdentityObject, TFirst, Contracts.ICopyable<TFirst>, new()
		where TSecond : Contracts.IIdentifiable, Contracts.ICopyable<TSecond>
		where TSecondEntity : Entities.IdentityObject, TSecond, Contracts.ICopyable<TSecond>, new()
	{
		static GenericOneToManyController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();

		private GenericController<TFirst, TFirstEntity> oneEntityController;
		private GenericController<TSecond, TSecondEntity> manyEntityController;

		public virtual int MaxPageSize => oneEntityController.MaxPageSize;

		public GenericOneToManyController(DataContext.IContext context) : base(context)
		{
			Constructing();
			oneEntityController = CreateOneEntityController(this);
			manyEntityController = CreateManyEntityController(this);
			ChangedSessionToken += GenericOneToManyController_ChangedSessionToken;
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public GenericOneToManyController(ControllerObject controller) : base(controller)
		{
			Constructing();
			oneEntityController = CreateOneEntityController(this);
			manyEntityController = CreateManyEntityController(this);
			ChangedSessionToken += GenericOneToManyController_ChangedSessionToken;
			Constructed();
		}

		private void GenericOneToManyController_ChangedSessionToken(object sender, EventArgs e)
		{
			oneEntityController.SessionToken = SessionToken;
			manyEntityController.SessionToken = SessionToken;
		}

		protected abstract GenericController<TFirst, TFirstEntity> CreateOneEntityController(ControllerObject controller);
		protected abstract GenericController<TSecond, TSecondEntity> CreateManyEntityController(ControllerObject controller);

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
			return oneEntityController.CountAsync();
		}
		public virtual Task<int> CountByAsync(string predicate)
		{
			return oneEntityController.CountByAsync(predicate);
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
		protected virtual async Task LoadDetailsAsync(E entity, int masterId)
		{
			var predicate = $"{typeof(TFirstEntity).Name}Id == {masterId}";

			entity.ClearSecondItems();
			foreach (var item in (await manyEntityController.QueryAllAsync(predicate).ConfigureAwait(false)).ToList())
			{
				entity.AddSecondItem(item);
			}
		}
		protected virtual async Task<IEnumerable<TSecondEntity>> QueryDetailsAsync(int masterId)
		{
			var result = new List<TSecondEntity>();
			var predicate = $"{typeof(TFirstEntity).Name}Id == {masterId}";

			foreach (var item in (await manyEntityController.QueryAllAsync(predicate).ConfigureAwait(false)).ToList())
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
			var firstEntity = await oneEntityController.GetByIdAsync(id).ConfigureAwait(false);

			if (firstEntity != null)
			{
				var predicate = $"{typeof(TFirstEntity).Name}Id == {firstEntity.Id}";

				result = new E();
				result.FirstItem.CopyProperties(firstEntity);
				await LoadDetailsAsync(result, firstEntity.Id).ConfigureAwait(false);
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

				foreach (var item in (await oneEntityController.GetPageListAsync(pageIndex, pageSize).ConfigureAwait(false)).ToList())
				{
					E entity = new E();

					entity.FirstItem.CopyProperties(item);
					await LoadDetailsAsync(entity, item.Id).ConfigureAwait(false);

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

				foreach (var item in (await oneEntityController.GetAllAsync().ConfigureAwait(false)).ToList())
				{
					E entity = new E();

					entity.FirstItem.CopyProperties(item);
					await LoadDetailsAsync(entity, item.Id).ConfigureAwait(false);

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

				foreach (var item in (await oneEntityController.QueryPageListAsync(predicate, pageIndex, pageSize).ConfigureAwait(false)).ToList())
				{
					E entity = new E();

					entity.FirstItem.CopyProperties(item);
					await LoadDetailsAsync(entity, item.Id).ConfigureAwait(false);

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

				foreach (var item in (await oneEntityController.QueryAllAsync(predicate).ConfigureAwait(false)).ToList())
				{
					E entity = new E();

					entity.FirstItem.CopyProperties(item);
					await LoadDetailsAsync(entity, item.Id).ConfigureAwait(false);

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
			entity.SecondItems.CheckArgument(nameof(entity.SecondItems));

			var result = new E();

			result.FirstEntity.CopyProperties(entity.FirstItem);
			await oneEntityController.InsertAsync(result.FirstEntity);

			foreach (var item in entity.SecondItems)
			{
				var secondEntity = new TSecondEntity();

				secondEntity.CopyProperties(item);

				var pi = GetNavigationToOne();

				if (pi != null)
				{
					pi.SetValue(secondEntity, result.FirstEntity);
				}
				await manyEntityController.InsertAsync(secondEntity).ConfigureAwait(false);
				result.AddSecondItem(secondEntity);
			}
			return result;
		}

		public virtual async Task<I> UpdateAsync(I entity)
		{
			entity.CheckArgument(nameof(entity));
			entity.FirstItem.CheckArgument(nameof(entity.FirstItem));
			entity.SecondItems.CheckArgument(nameof(entity.SecondItems));

			//Delete all costs that are no longer included in the list.
			foreach (var item in (await QueryDetailsAsync(entity.Id).ConfigureAwait(false)).ToList())
			{
				var exitsItem = entity.SecondItems.SingleOrDefault(i => i.Id == item.Id);

				if (exitsItem == null)
				{
					await manyEntityController.DeleteAsync(item.Id).ConfigureAwait(false);
				}
			}

			var result = new E();
			var firstEntity = await oneEntityController.UpdateAsync(entity.FirstItem).ConfigureAwait(false);

			result.FirstItem.CopyProperties(firstEntity);
			foreach (var item in entity.SecondItems)
			{
				if (item.Id == 0)
				{
					var pi = GetForeignKeyToOne();

					if (pi != null)
					{
						pi.SetValue(item, firstEntity.Id);
					}
					var insDetail = await manyEntityController.InsertAsync(item).ConfigureAwait(false);

					item.CopyProperties(insDetail);
				}
				else
				{
					var updDetail = await manyEntityController.UpdateAsync(item).ConfigureAwait(false);

					item.CopyProperties(updDetail);
				}
			}
			return result;
		}

		public virtual async Task DeleteAsync(int id)
		{
			var entity = await GetByIdAsync(id).ConfigureAwait(false);

			if (entity != null)
			{
				foreach (var item in entity.SecondItems)
				{
					await manyEntityController.DeleteAsync(item.Id).ConfigureAwait(false);
				}
				await oneEntityController.DeleteAsync(entity.Id).ConfigureAwait(false);
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
				oneEntityController.Dispose();
				manyEntityController.Dispose();

				oneEntityController = null;
				manyEntityController = null;
			}
		}
	}
}
//MdEnd