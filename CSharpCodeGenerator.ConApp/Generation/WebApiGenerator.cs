//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using CommonBase.Extensions;

namespace CSharpCodeGenerator.ConApp.Generation
{
    partial class WebApiGenerator : ClassGenerator
    {
        protected WebApiGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }
        public new static WebApiGenerator Create(SolutionProperties solutionProperties)
        {
            return new WebApiGenerator(solutionProperties);
        }

        public string WebApiNameSpace => $"{SolutionProperties.WebApiProjectName}";

        public string CreateNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{WebApiNameSpace}.Controllers";
        }
        private bool CanCreate(Type type)
        {
            bool create = true;

            CanCreateController(type, ref create);
            return create;
        }
        partial void CanCreateController(Type type, ref bool create);
        partial void CreateControllerAttributes(Type type, List<string> codeLines);
        partial void CreateActionAttributes(Type type, string action, List<string> codeLines);

        private IEnumerable<string> CreateController(Type type)
        {
            List<string> result = new List<string>();
            string entityName = CreateEntityNameFromInterface(type);
            string subNameSpace = GetSubNamespaceFromInterface(type);
            string modelType = $"Transfer.{subNameSpace}.{entityName}";
            string routeBase = $"/api/[controller]";

            result.Add("using Microsoft.AspNetCore.Mvc;");
            result.Add("using System.Collections.Generic;");
            result.Add("using System.Threading.Tasks;");
            result.Add($"using Contract = {type.FullName};");
            result.Add($"using Model = {modelType};");

            result.Add("[ApiController]");
            result.Add("[Route(\"Controller\")]");
            CreateControllerAttributes(type, result);
            result.Add($"public partial class {entityName}Controller : GenericController<Contract, Model>");
            result.Add("{");

            result.Add($"[HttpGet(\"{routeBase}/Count\")]");
            CreateActionAttributes(type, "getcount", result);
            result.Add("public Task<int> GetCountAsync()");
            result.Add("{");
            result.Add("return CountAsync();");
            result.Add("}");

            result.Add($"[HttpGet(\"{routeBase}/Get\")]");
            CreateActionAttributes(type, "get", result);
            result.Add($"public Task<IEnumerable<Model>> GetAsync()");
            result.Add("{");
            result.Add("return GetModelsAsync();");
            result.Add("}");

            result.Add($"[HttpGet(\"{routeBase}/Get" + "/{id}\")]");
            CreateActionAttributes(type, "getbyid", result);
            result.Add($"public Task<Model> GetAsync(int id)");
            result.Add("{");
            result.Add("return GetModelByIdAsync(id);");
            result.Add("}");

            result.Add($"[HttpGet(\"{routeBase}/Get" + "/{index}/{size}\")]");
            CreateActionAttributes(type, "getpage", result);
            result.Add($"public Task<IEnumerable<Model>> GetPageListAsync(int index, int size)");
            result.Add("{");
            result.Add("return GetPageModelsAsync(index, size);");
            result.Add("}");

            result.Add($"[HttpGet(\"{routeBase}/Create\")]");
            CreateActionAttributes(type, "getcreate", result);
            result.Add($"public Task<Model> GetCreateAsync(int id)");
            result.Add("{");
            result.Add("return CreateModelAsync();");
            result.Add("}");

            result.Add($"[HttpPost(\"{routeBase}\")]");
            CreateActionAttributes(type, "post", result);
            result.Add($"public Task<Model> PostAsync(Model model)");
            result.Add("{");
            result.Add("return InsertModelAsync(model);");
            result.Add("}");

            result.Add($"[HttpPut(\"{routeBase}\")]");
            CreateActionAttributes(type, "put", result);
            result.Add($"public Task<Model> PutAsync(Model model)");
            result.Add("{");
            result.Add("return UpdateModelAsync(model);");
            result.Add("}");

            result.Add($"[HttpDelete(\"{routeBase}" + "/{ id}\")]");
            CreateActionAttributes(type, "delete", result);
            result.Add($"public Task DeleteAsync(int id)");
            result.Add("{");
            result.Add("return DeleteModelAsync(id);");
            result.Add("}");

            result.Add("}");
            return result;
        }
        public IEnumerable<string> CreateControllers()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);
            var types = contractsProject.PersistenceTypes.Union(contractsProject.BusinessTypes);

            foreach (var type in types)
            {
                if (CanCreate(type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreateController(type), CreateNameSpace(type)));
                }
            }
            return result;
        }
    }
}
//MdEnd