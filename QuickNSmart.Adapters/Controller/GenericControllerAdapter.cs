//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuickNSmart.Adapters.Controller
{
    partial class GenericControllerAdapter<TContract> : Contracts.Client.IAdapterAccess<TContract>
        where TContract : Contracts.IIdentifiable
    {
        static GenericControllerAdapter()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public Contracts.Client.IControllerAccess<TContract> controller;

        public GenericControllerAdapter()
        {
            Constructing();
            controller = Logic.Factory.Create<TContract>();
            Constructed();
        }
        public GenericControllerAdapter(string authenticationToken)
        {
            Constructing();
            controller = Logic.Factory.Create<TContract>(authenticationToken);
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();

        public string AuthenticationToken { set => controller.AuthenticationToken = value; }
        public int MaxPageSize => controller.MaxPageSize;

        #region Async-Methods
        public Task<int> CountAsync()
        {
            return controller.CountAsync();
        }

        public Task<IEnumerable<TContract>> GetAllAsync()
        {
            return controller.GetAllAsync();
        }

        public Task<IEnumerable<TContract>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            return controller.QueryPageListAsync(predicate, pageIndex, pageSize);
        }

        public Task<IEnumerable<TContract>> GetPageListAsync(int pageIndex, int pageSize)
        {
            return controller.GetPageListAsync(pageIndex, pageSize);
        }

        public Task<TContract> GetByIdAsync(int id)
        {
            return controller.GetByIdAsync(id);
        }

        public Task<TContract> CreateAsync()
        {
            return controller.CreateAsync();
        }

        public async Task<TContract> InsertAsync(TContract entity)
        {
            var result = await controller.InsertAsync(entity);

            await controller.SaveChangesAsync();
            return result;
        }

        public async Task<TContract> UpdateAsync(TContract entity)
        {
            var result = await controller.UpdateAsync(entity);

            await controller.SaveChangesAsync();
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await controller.DeleteAsync(id);
            await controller.SaveChangesAsync();
        }
        #endregion Async-Methods

        public void Dispose()
        {
            controller?.Dispose();
            controller = null;
        }
    }
}
//MdEnd