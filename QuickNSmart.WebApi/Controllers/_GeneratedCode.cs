namespace QuickNSmart.WebApi.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Contract = QuickNSmart.Contracts.Persistence.Account.IApplication;
	using Model = Transfer.Persistence.Account.Application;
	[ApiController]
	[Route("Controller")]
	public partial class ApplicationController : GenericController<Contract, Model>
	{
		[HttpGet("/api/[controller]/MaxPage")]
		public Task<int> GetMaxPageAsync()
		{
			return GetMaxPageAsync();
		}
		[HttpGet("/api/[controller]/Count")]
		public Task<int> GetCountAsync()
		{
			return CountAsync();
		}
		[HttpGet("/api/[controller]/Get")]
		public Task<IEnumerable<Model>> GetAsync()
		{
			return GetModelsAsync();
		}
		[HttpGet("/api/[controller]/Get/{index}/{size}")]
		public Task<IEnumerable<Model>> GetPageListAsync(int index, int size)
		{
			return GetPageModelsAsync(index, size);
		}
		[HttpGet("/api/[controller]/Get/{index}/{size}")]
		public Task<IEnumerable<Model>> QueryPageListAsync(string predicate, int index, int size)
		{
			return QueryPageModelsAsync(predicate, index, size);
		}
		[HttpGet("/api/[controller]/Get/{id}")]
		public Task<Model> GetAsync(int id)
		{
			return GetModelByIdAsync(id);
		}
		[HttpGet("/api/[controller]/Create")]
		public Task<Model> GetCreateAsync(int id)
		{
			return CreateModelAsync();
		}
		[HttpPost("/api/[controller]")]
		public Task<Model> PostAsync(Model model)
		{
			return InsertModelAsync(model);
		}
		[HttpPut("/api/[controller]")]
		public Task<Model> PutAsync(Model model)
		{
			return UpdateModelAsync(model);
		}
		[HttpDelete("/api/[controller]/{id}")]
		public Task DeleteAsync(int id)
		{
			return DeleteModelAsync(id);
		}
	}
}
namespace QuickNSmart.WebApi.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Contract = QuickNSmart.Contracts.Persistence.Account.ILoginUser;
	using Model = Transfer.Persistence.Account.LoginUser;
	[ApiController]
	[Route("Controller")]
	public partial class LoginUserController : GenericController<Contract, Model>
	{
		[HttpGet("/api/[controller]/MaxPage")]
		public Task<int> GetMaxPageAsync()
		{
			return GetMaxPageAsync();
		}
		[HttpGet("/api/[controller]/Count")]
		public Task<int> GetCountAsync()
		{
			return CountAsync();
		}
		[HttpGet("/api/[controller]/Get")]
		public Task<IEnumerable<Model>> GetAsync()
		{
			return GetModelsAsync();
		}
		[HttpGet("/api/[controller]/Get/{index}/{size}")]
		public Task<IEnumerable<Model>> GetPageListAsync(int index, int size)
		{
			return GetPageModelsAsync(index, size);
		}
		[HttpGet("/api/[controller]/Get/{index}/{size}")]
		public Task<IEnumerable<Model>> QueryPageListAsync(string predicate, int index, int size)
		{
			return QueryPageModelsAsync(predicate, index, size);
		}
		[HttpGet("/api/[controller]/Get/{id}")]
		public Task<Model> GetAsync(int id)
		{
			return GetModelByIdAsync(id);
		}
		[HttpGet("/api/[controller]/Create")]
		public Task<Model> GetCreateAsync(int id)
		{
			return CreateModelAsync();
		}
		[HttpPost("/api/[controller]")]
		public Task<Model> PostAsync(Model model)
		{
			return InsertModelAsync(model);
		}
		[HttpPut("/api/[controller]")]
		public Task<Model> PutAsync(Model model)
		{
			return UpdateModelAsync(model);
		}
		[HttpDelete("/api/[controller]/{id}")]
		public Task DeleteAsync(int id)
		{
			return DeleteModelAsync(id);
		}
	}
}
