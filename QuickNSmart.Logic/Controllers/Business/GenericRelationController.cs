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
    abstract partial class GenericRelationController<I, E, IMaster, EMaster, IDetail, EDetail> : ControllerObject, Contracts.Client.IControllerAccess<I>
		where I : Contracts.IRelation<IMaster, IDetail>
		where E : Entities.RelationObject<IMaster, EMaster, IDetail, EDetail>, I, Contracts.ICopyable<I>, new()
		where IMaster : Contracts.IIdentifiable, Contracts.ICopyable<IMaster>
		where EMaster : Entities.IdentityObject, IMaster, Contracts.ICopyable<IMaster>, new()
		where IDetail : Contracts.IIdentifiable, Contracts.ICopyable<IDetail>
		where EDetail : Entities.IdentityObject, IDetail, Contracts.ICopyable<IDetail>, new()
	{
		static GenericRelationController()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();

		private GenericController<IMaster, EMaster> masterController;
		private GenericController<IDetail, EDetail> detailController;

		public virtual int MaxPageSize => masterController.MaxPageSize;

		public GenericRelationController(DataContext.IContext context) : base(context)
		{
			Constructing();
			masterController = CreateMasterController(this);
			detailController = CreateDetailController(this);
			ChangedSessionToken += GenericRelationController_ChangedSessionToken;
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public GenericRelationController(ControllerObject controller) : base(controller)
		{
			Constructing();
			masterController = CreateMasterController(this);
			detailController = CreateDetailController(this);
			ChangedSessionToken += GenericRelationController_ChangedSessionToken;
			Constructed();
		}

		private void GenericRelationController_ChangedSessionToken(object sender, EventArgs e)
		{
			masterController.SessionToken = SessionToken;
			detailController.SessionToken = SessionToken;
		}

		protected abstract GenericController<IMaster, EMaster> CreateMasterController(ControllerObject controller);
		protected abstract GenericController<IDetail, EDetail> CreateDetailController(ControllerObject controller);

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
			return masterController.CountAsync();
		}
		public virtual Task<int> CountByAsync(string predicate)
		{
			return masterController.CountByAsync(predicate);
		}
		#region Async-Methods
		protected virtual PropertyInfo GetMasterNavigation()
		{
			return typeof(EDetail).GetProperty(typeof(EMaster).Name);
		}
		protected virtual async Task LoadDetailsAsync(E entity, int masterId)
		{
			var predicate = $"{typeof(EMaster).Name}Id == {masterId}";

			entity.ClearDetails();
			foreach (var item in (await detailController.QueryAllAsync(predicate).ConfigureAwait(false)).ToList())
			{
				entity.AddDetail(item);
			}
		}
		protected virtual async Task<IEnumerable<EDetail>> QueryDetailsAsync(int masterId)
		{
			var result = new List<EDetail>();
			var predicate = $"{typeof(EMaster).Name}Id == {masterId}";

			foreach (var item in (await detailController.QueryAllAsync(predicate).ConfigureAwait(false)).ToList())
			{
				var e = new EDetail();

				e.CopyProperties(item);
				result.Add(e);
			}
			return result;
		}
		public virtual async Task<I> GetByIdAsync(int id)
		{
			var result = default(E);
			var masterEntity = await masterController.GetByIdAsync(id).ConfigureAwait(false);

			if (masterEntity != null)
			{
				var predicate = $"{typeof(EMaster).Name}Id == {masterEntity.Id}";

				result = new E();
				result.MasterEntity.CopyProperties(masterEntity);
				await LoadDetailsAsync(result, masterEntity.Id).ConfigureAwait(false);
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

				foreach (var item in (await masterController.GetPageListAsync(pageIndex, pageSize).ConfigureAwait(false)).ToList())
				{
					E entity = new E();

					entity.MasterEntity.CopyProperties(item);
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

				foreach (var item in (await masterController.GetAllAsync().ConfigureAwait(false)).ToList())
				{
					E entity = new E();

					entity.MasterEntity.CopyProperties(item);
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

				foreach (var item in (await masterController.QueryPageListAsync(predicate, pageIndex, pageSize).ConfigureAwait(false)).ToList())
				{
					E entity = new E();

					entity.MasterEntity.CopyProperties(item);
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

				foreach (var item in (await masterController.QueryAllAsync(predicate).ConfigureAwait(false)).ToList())
				{
					E entity = new E();

					entity.MasterEntity.CopyProperties(item);
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
			entity.Master.CheckArgument(nameof(entity.Master));
			entity.Details.CheckArgument(nameof(entity.Details));

			var result = new E();

			result.MasterEntity.CopyProperties(entity.Master);
			await masterController.InsertAsync(result.MasterEntity);

			foreach (var item in entity.Details)
			{
				var detail = new EDetail();

				detail.CopyProperties(item);

				var pi = GetMasterNavigation();

				if (pi != null)
				{
					pi.SetValue(detail, result.MasterEntity);
				}
				await detailController.InsertAsync(detail).ConfigureAwait(false);
				result.AddDetail(detail);
			}
			return result;
		}

		public virtual async Task<I> UpdateAsync(I entity)
		{
			entity.CheckArgument(nameof(entity));
			entity.Master.CheckArgument(nameof(entity.Master));
			entity.Details.CheckArgument(nameof(entity.Details));

			//Delete all costs that are no longer included in the list.
			foreach (var item in (await QueryDetailsAsync(entity.Id).ConfigureAwait(false)).ToList())
			{
				var exitDetail = entity.Details.SingleOrDefault(i => i.Id == item.Id);

				if (exitDetail == null)
				{
					await detailController.DeleteAsync(item.Id).ConfigureAwait(false);
				}
			}

			var result = new E();
			var master = await masterController.UpdateAsync(entity.Master).ConfigureAwait(false);

			result.MasterEntity.CopyProperties(master);
			foreach (var item in entity.Details)
			{
				if (item.Id == 0)
				{
					var pi = GetMasterNavigation();

					if (pi != null)
					{
						pi.SetValue(item, entity.Id);
					}
					var insDetail = await detailController.InsertAsync(item).ConfigureAwait(false);

					item.CopyProperties(insDetail);
				}
				else
				{
					var updDetail = await detailController.UpdateAsync(item).ConfigureAwait(false);

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
				foreach (var item in entity.Details)
				{
					await detailController.DeleteAsync(item.Id).ConfigureAwait(false);
				}
				await masterController.DeleteAsync(entity.Id).ConfigureAwait(false);
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
	}
}
//MdEnd