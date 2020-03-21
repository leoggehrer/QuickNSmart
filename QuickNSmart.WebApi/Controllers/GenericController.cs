//@QnSBaseCode
//MdStart
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CommonBase.Extensions;

namespace QuickNSmart.WebApi.Controllers
{
    public abstract class GenericController<I, M> : ApiControllerBase
        where I : Contracts.IIdentifiable
        where M : Transfer.TransferObject, I, Contracts.ICopyable<I>, new()
    {
        protected Contracts.Client.IControllerAccess<I> CreateController()
        {
            var result = Logic.Factory.Create<I>();
            string authHeader = HttpContext.Request.Headers["Authorization"];
            string sessionToken = GetSessionToken(authHeader);

            if (sessionToken.HasContent())
            {
                result.SessionToken = sessionToken;
            }
            return result;
        }
        protected M ToModel(I entity)
        {
            var result = new M();

            result.CopyProperties(entity);
            return result;
        }

        protected async Task<int> CountAsync()
        {
            using var ctrl = CreateController();

            return await ctrl.CountAsync();
        }

        protected async Task<M> GetModelByIdAsync(int id)
        {
            using var ctrl = CreateController();

            var entity = (await ctrl.GetByIdAsync(id));
            return ToModel(entity);
        }
        protected async Task<IEnumerable<M>> GetPageModelsAsync(int index, int size)
        {
            using var ctrl = CreateController();

            return (await ctrl.GetPageListAsync(index, size)).ToList().Select(i => ToModel(i));
        }
        protected async Task<IEnumerable<M>> GetAllModelsAsync()
        {
            using var ctrl = CreateController();

            return (await ctrl.GetAllAsync()).ToList().Select(i => ToModel(i));
        }

        protected async Task<IEnumerable<M>> QueryPageModelsAsync(string predicate, int index, int size)
        {
            using var ctrl = CreateController();

            return (await ctrl.QueryPageListAsync(predicate, index, size)).ToList().Select(i => ToModel(i));
        }
        protected async Task<IEnumerable<M>> QueryAllModelsAsync(string predicate)
        {
            using var ctrl = CreateController();

            return (await ctrl.QueryAllAsync(predicate)).ToList().Select(i => ToModel(i));
        }

        protected async Task<M> CreateModelAsync()
        {
            using var ctrl = CreateController();

            var entity = await ctrl.CreateAsync();
            return ToModel(entity);
        }
        protected async Task<M> InsertModelAsync(M model)
        {
            using var ctrl = CreateController();

            var entity = await ctrl.InsertAsync(model);

            await ctrl.SaveChangesAsync();
            return ToModel(entity);
        }
        protected async Task<M> UpdateModelAsync(M model)
        {
            using var ctrl = CreateController();

            var entity = await ctrl.UpdateAsync(model);

            await ctrl.SaveChangesAsync();
            return ToModel(entity);
        }
        protected async Task DeleteModelAsync(int id)
        {
            using var ctrl = CreateController();

            await ctrl.DeleteAsync(id);
            await ctrl.SaveChangesAsync();
        }
    }
}
//MdEnd
