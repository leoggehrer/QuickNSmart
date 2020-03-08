//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace QuickNSmart.Logic.Controllers.Business
{
    abstract partial class BusinessControllerAdapter<I> : ControllerObject, Contracts.Client.IControllerAccess<I>
		where I : Contracts.IIdentifiable
	{
		public virtual int MaxPageSize => throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().Name}!");

		static BusinessControllerAdapter()
		{
			ClassConstructing();
			ClassConstructed();
		}
		static partial void ClassConstructing();
		static partial void ClassConstructed();
		public BusinessControllerAdapter(DataContext.IContext context) : base(context)
		{
			Constructing();
			Constructed();
		}
		partial void Constructing();
		partial void Constructed();
		public BusinessControllerAdapter(ControllerObject controller) : base(controller)
		{
			Constructing();
			Constructed();
		}

		public virtual Task<int> CountAsync()
		{
			throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().Name}!");
		}

		public virtual Task<IQueryable<I>> GetAllAsync()
		{
			throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().Name}!");
		}

		public virtual Task<IQueryable<I>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
		{
			throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().Name}!");
		}

		public virtual Task<IQueryable<I>> GetPageListAsync(int pageIndex, int pageSize)
		{
			throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().Name}!");
		}

		public virtual Task<I> GetByIdAsync(int id)
		{
			throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().Name}!");
		}

		public virtual Task<I> CreateAsync()
		{
			throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().Name}!");
		}

		public virtual Task<I> InsertAsync(I entity)
		{
			throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().Name}!");
		}

		public virtual Task<I> UpdateAsync(I entity)
		{
			throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().Name}!");
		}

		public virtual Task DeleteAsync(int id)
		{
			throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().Name}!");
		}

		public virtual Task SaveChangesAsync()
		{
			throw new NotSupportedException($"It is not supported: {MethodBase.GetCurrentMethod().Name}!");
		}
	}
}
//MdEnd