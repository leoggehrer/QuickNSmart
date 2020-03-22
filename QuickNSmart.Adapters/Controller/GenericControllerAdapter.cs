//@QnSBaseCode
//MdStart
using System.Collections.Generic;
using System.Linq;
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

        public string SessionToken { set => controller.SessionToken = value; }
        public int MaxPageSize => controller.MaxPageSize;

        #region Async-Methods
        public Task<int> CountAsync()
        {
            return controller.CountAsync();
        }
        public Task<int> CountByAsync(string predicate)
        {
            return controller.CountByAsync(predicate);
        }

        public Task<TContract> GetByIdAsync(int id)
        {
            return controller.GetByIdAsync(id);
        }
        public async Task<IEnumerable<TContract>> GetPageListAsync(int pageIndex, int pageSize)
        {
            return (await controller.GetPageListAsync(pageIndex, pageSize).ConfigureAwait(false)).ToArray();
        }
        public async Task<IEnumerable<TContract>> GetAllAsync()
        {
            return (await controller.GetAllAsync().ConfigureAwait(false)).ToArray();
        }

        public async Task<IEnumerable<TContract>> QueryPageListAsync(string predicate, int pageIndex, int pageSize)
        {
            return (await controller.QueryPageListAsync(predicate, pageIndex, pageSize).ConfigureAwait(false)).ToArray();
        }
        public async Task<IEnumerable<TContract>> QueryAllAsync(string predicate)
        {
            return (await controller.QueryAllAsync(predicate).ConfigureAwait(false)).ToArray();
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