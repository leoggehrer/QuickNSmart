//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using CommonBase.Extensions;

namespace CSharpCodeGenerator.ConApp.Generation
{
    partial class ControllerGenerator : ClassGenerator
    {
        protected ControllerGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }
        public new static ControllerGenerator Create(SolutionProperties solutionProperties)
        {
            return new ControllerGenerator(solutionProperties);
        }

        #region General
        private bool CanCreate(string generationName, Type type)
        {
            bool create = true;

            CanCreateController(generationName, type, ref create);
            return create;
        }
        partial void CanCreateController(string generationName, Type type, ref bool create);
        #endregion General

        #region LogicController
        public string LogicControllerNameSpace => $"{SolutionProperties.LogicProjectName}.{SolutionProperties.ControllersFolder}";
        public string CreateLogicControllerNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{LogicControllerNameSpace}.{Generator.GetSubNamespaceFromInterface(type)}";
        }
        partial void CreateLogicControllerAttributes(Type type, List<string> codeLines);

        public IEnumerable<string> CreatePersistenceController(Type type)
        {
            type.CheckArgument(nameof(type));

            List<string> result = new List<string>();
            string entityName = CreateEntityNameFromInterface(type);
            string subNameSpace = GetSubNamespaceFromInterface(type);
            string entityType = $"{SolutionProperties.EntitiesFolder}.{subNameSpace}.{entityName}";
            string controllerName = $"{entityName}Controller";

            CreateLogicControllerAttributes(type, result);
            result.Add($"sealed partial class {controllerName} : GenericController<{type.FullName}, {entityType}>");
            result.Add("{");

            result.AddRange(CreatePartialStaticConstrutor(controllerName));
            result.AddRange(CreatePartialConstrutor("public", controllerName, $"{SolutionProperties.DataContextFolder}.IContext context", "base(context)"));
            result.AddRange(CreatePartialConstrutor("public", controllerName, "ControllerObject controller", "base(controller)", null, false));
            result.Add("}");
            return result;
        }
        public IEnumerable<string> CreateBusinessController(Type type)
        {
            type.CheckArgument(nameof(type));

            List<string> result = new List<string>();
            string entityName = CreateEntityNameFromInterface(type);
            string subNameSpace = GetSubNamespaceFromInterface(type);
            string entityType = $"{SolutionProperties.EntitiesFolder}.{subNameSpace}.{entityName}";
            string controllerName = $"{entityName}Controller";

            CreateLogicControllerAttributes(type, result);
            result.Add($"sealed partial class {controllerName} : ControllerObject, Contracts.Client.IControllerAccess<{type.FullName}>");
            result.Add("{");

            result.AddRange(CreatePartialStaticConstrutor(controllerName));
            result.AddRange(CreatePartialConstrutor("public", controllerName, $"{SolutionProperties.DataContextFolder}.IContext context", "base(context)"));
            result.AddRange(CreatePartialConstrutor("public", controllerName, "ControllerObject controller", "base(controller)", null, false));
            result.Add("}");
            return result;
        }
        public IEnumerable<string> CreatePersistenceControllers()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.PersistenceTypes)
            {
                if (CanCreate(nameof(CreatePersistenceControllers), type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreatePersistenceController(type), CreateLogicControllerNameSpace(type)));
                }
            }
            return result;
        }
        public IEnumerable<string> CreateBusinessControllers()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.BusinessTypes)
            {
                if (CanCreate(nameof(CreateBusinessControllers), type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreateBusinessController(type), CreateLogicControllerNameSpace(type)));
                }
            }
            return result;
        }
        #endregion LogicController

        #region WebApiController
        public string WebApiNameSpace => $"{SolutionProperties.WebApiProjectName}";
        public string CreateWebApiNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{WebApiNameSpace}.Controllers";
        }
        partial void CreateWebApiControllerAttributes(Type type, List<string> codeLines);
        partial void CreateWebApiActionAttributes(Type type, string action, List<string> codeLines);

        private IEnumerable<string> CreateWebApiController(Type type)
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
            CreateWebApiControllerAttributes(type, result);
            result.Add($"public partial class {entityName}Controller : GenericController<Contract, Model>");
            result.Add("{");

            result.Add($"[HttpGet(\"{routeBase}/MaxPage\")]");
            CreateWebApiActionAttributes(type, "getmaxpage", result);
            result.Add("public Task<int> GetMaxPageAsync()");
            result.Add("{");
            result.Add("return GetMaxPageAsync();");
            result.Add("}");

            result.Add($"[HttpGet(\"{routeBase}/Count\")]");
            CreateWebApiActionAttributes(type, "getcount", result);
            result.Add("public Task<int> GetCountAsync()");
            result.Add("{");
            result.Add("return CountAsync();");
            result.Add("}");

            result.Add($"[HttpGet(\"{routeBase}/Get\")]");
            CreateWebApiActionAttributes(type, "get", result);
            result.Add($"public Task<IEnumerable<Model>> GetAsync()");
            result.Add("{");
            result.Add("return GetModelsAsync();");
            result.Add("}");

            result.Add($"[HttpGet(\"{routeBase}/Get" + "/{index}/{size}\")]");
            CreateWebApiActionAttributes(type, "getpage", result);
            result.Add($"public Task<IEnumerable<Model>> GetPageListAsync(int index, int size)");
            result.Add("{");
            result.Add("return GetPageModelsAsync(index, size);");
            result.Add("}");

            result.Add($"[HttpGet(\"{routeBase}/Get" + "/{index}/{size}\")]");
            CreateWebApiActionAttributes(type, "querypage", result);
            result.Add($"public Task<IEnumerable<Model>> QueryPageListAsync(string predicate, int index, int size)");
            result.Add("{");
            result.Add("return QueryPageModelsAsync(predicate, index, size);");
            result.Add("}");

            result.Add($"[HttpGet(\"{routeBase}/Get" + "/{id}\")]");
            CreateWebApiActionAttributes(type, "getbyid", result);
            result.Add($"public Task<Model> GetAsync(int id)");
            result.Add("{");
            result.Add("return GetModelByIdAsync(id);");
            result.Add("}");

            result.Add($"[HttpGet(\"{routeBase}/Create\")]");
            CreateWebApiActionAttributes(type, "getcreate", result);
            result.Add($"public Task<Model> GetCreateAsync(int id)");
            result.Add("{");
            result.Add("return CreateModelAsync();");
            result.Add("}");

            result.Add($"[HttpPost(\"{routeBase}\")]");
            CreateWebApiActionAttributes(type, "post", result);
            result.Add($"public Task<Model> PostAsync(Model model)");
            result.Add("{");
            result.Add("return InsertModelAsync(model);");
            result.Add("}");

            result.Add($"[HttpPut(\"{routeBase}\")]");
            CreateWebApiActionAttributes(type, "put", result);
            result.Add($"public Task<Model> PutAsync(Model model)");
            result.Add("{");
            result.Add("return UpdateModelAsync(model);");
            result.Add("}");

            result.Add($"[HttpDelete(\"{routeBase}" + "/{id}\")]");
            CreateWebApiActionAttributes(type, "delete", result);
            result.Add($"public Task DeleteAsync(int id)");
            result.Add("{");
            result.Add("return DeleteModelAsync(id);");
            result.Add("}");

            result.Add("}");
            return result;
        }
        public IEnumerable<string> CreateWebApiControllers()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);
            var types = contractsProject.PersistenceTypes.Union(contractsProject.BusinessTypes);

            foreach (var type in types)
            {
                if (CanCreate(nameof(CreateWebApiControllers), type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreateWebApiController(type), CreateWebApiNameSpace(type)));
                }
            }
            return result;
        }
        #endregion WebApiController
    }
}
//MdEnd