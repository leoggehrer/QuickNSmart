//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using CommonBase.Extensions;

namespace CSharpCodeGenerator.ConApp.Generation
{
    partial class FactoryGenerator : ClassGenerator
    {
        protected FactoryGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }
        public new static FactoryGenerator Create(SolutionProperties solutionProperties)
        {
            return new FactoryGenerator(solutionProperties);
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

        #region Logic
        public string LogicNameSpace => $"{SolutionProperties.LogicProjectName}";
        public string CreateLogicNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{LogicNameSpace}.{Generator.GetSubNamespaceFromInterface(type)}";
        }
        public IEnumerable<string> CreateLogicFactory()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);
            var types = contractsProject.PersistenceTypes.Union(contractsProject.BusinessTypes);
            var first = true;

            result.Add("public static partial class Factory");
            result.Add("{");
            result.Add("public static Contracts.Client.IControllerAccess<I> Create<I>() where I : Contracts.IIdentifiable");
            result.Add("{");
            result.Add("Contracts.Client.IControllerAccess<I> result = null;");
            foreach (var type in types.Where(t => CanCreate(nameof(CreateLogicFactory), t)))
            {
                string entityName = CreateEntityNameFromInterface(type);
                string controllerNameSpace = $"Controllers.{GetSubNamespaceFromInterface(type)}";

                if (first)
                {
                    result.Add($"if (typeof(I) == typeof({type.FullName}))");
                }
                else
                {
                    result.Add($"else if (typeof(I) == typeof({type.FullName}))");
                }
                result.Add("{");
                result.Add($"result = new {controllerNameSpace}.{entityName}Controller(CreateContext()) as Contracts.Client.IControllerAccess<I>;");
                result.Add("}");
                first = false;
            }
            result.Add("return result;");
            result.Add("}");
            
            result.Add("public static Contracts.Client.IControllerAccess<I> Create<I>(object sharedController) where I : Contracts.IIdentifiable");
            result.Add("{");
            result.Add("Contracts.Client.IControllerAccess<I> result = null;");
            first = true;
            foreach (var type in types.Where(t => CanCreate(nameof(CreateLogicFactory), t)))
            {
                string entityName = CreateEntityNameFromInterface(type);
                string controllerNameSpace = $"Controllers.{GetSubNamespaceFromInterface(type)}";

                if (first)
                {
                    result.Add($"if (typeof(I) == typeof({type.FullName}))");
                }
                else
                {
                    result.Add($"else if (typeof(I) == typeof({type.FullName}))");
                }
                result.Add("{");
                result.Add($"result = new {controllerNameSpace}.{entityName}Controller(sharedController as Controllers.ControllerObject) as Contracts.Client.IControllerAccess<I>;");
                result.Add("}");
                first = false;
            }
            result.Add("return result;");
            result.Add("}");

            result.Add("public static Contracts.Client.IControllerAccess<I> Create<I>(string authenticationToken) where I : Contracts.IIdentifiable");
            result.Add("{");
            result.Add("Contracts.Client.IControllerAccess<I> result = null;");
            first = true;
            foreach (var type in types.Where(t => CanCreate(nameof(CreateLogicFactory), t)))
            {
                string entityName = CreateEntityNameFromInterface(type);
                string controllerNameSpace = $"Controllers.{GetSubNamespaceFromInterface(type)}";

                if (first)
                {
                    result.Add($"if (typeof(I) == typeof({type.FullName}))");
                }
                else
                {
                    result.Add($"else if (typeof(I) == typeof({type.FullName}))");
                }
                result.Add("{");
                result.Add($"result = new {controllerNameSpace}.{entityName}Controller(CreateContext()) " + "{ AuthenticationToken = authenticationToken } as Contracts.Client.IControllerAccess<I>;");
                result.Add("}");
                first = false;
            }
            result.Add("return result;");
            result.Add("}");
            result.Add("}");

            return EnvelopeWithANamespace(result, LogicNameSpace);
        }
        #endregion Logic

        #region Adapter
        public string AdapterNameSpace => $"{SolutionProperties.AdaptersProjectName}";
        public string CreateControllerNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"Controllers.{GetSubNamespaceFromInterface(type)}";
        }
        public string CreateTransferNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"Transfer.{GetSubNamespaceFromInterface(type)}";
        }
        public IEnumerable<string> CreateAdapterFactory()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);
            var types = contractsProject.PersistenceTypes.Union(contractsProject.BusinessTypes);
            var first = true;

            result.Add("public static partial class Factory");
            result.Add("{");
            result.Add("public static Contracts.Client.IAdapterAccess<I> Create<I>() where I : Contracts.IIdentifiable");
            result.Add("{");
            result.Add("Contracts.Client.IAdapterAccess<I> result = null;");
            result.Add("if (Adapter == AdapterType.Controller)");
            result.Add("{");
            foreach (var type in types.Where(t => CanCreate(nameof(CreateAdapterFactory), t)))
            {
                string entityName = CreateEntityNameFromInterface(type);
                string controllerNameSpace = CreateControllerNameSpace(type);

                if (first)
                {
                    result.Add($"if (typeof(I) == typeof({type.FullName}))");
                }
                else
                {
                    result.Add($"else if (typeof(I) == typeof({type.FullName}))");
                }
                result.Add("{");
                result.Add($"result = new Controller.GenericControllerAdapter<{type.FullName}>() as Contracts.Client.IAdapterAccess<I>;");
                result.Add("}");
                first = false;
            }
            result.Add("}");
            result.Add("else if (Adapter == AdapterType.Service)");
            result.Add("{");
            first = true;
            foreach (var type in types.Where(t => CanCreate(nameof(CreateAdapterFactory), t)))
            {
                string modelName = CreateEntityNameFromInterface(type);
                string modelNameSpace = CreateTransferNameSpace(type);

                if (first)
                {
                    result.Add($"if (typeof(I) == typeof({type.FullName}))");
                }
                else
                {
                    result.Add($"else if (typeof(I) == typeof({type.FullName}))");
                }
                result.Add("{");
                result.Add($"result = new Service.GenericServiceAdapter<{type.FullName}, {modelNameSpace}.{modelName}>(BaseUri, \"{modelName}\") as Contracts.Client.IAdapterAccess<I>;");
                result.Add("}");
                first = false;
            }
            result.Add("}");
            result.Add("return result;");
            result.Add("}");

            first = true;
            result.Add("public static Contracts.Client.IAdapterAccess<I> Create<I>(string authenticationToken) where I : Contracts.IIdentifiable");
            result.Add("{");
            result.Add("Contracts.Client.IAdapterAccess<I> result = null;");
            result.Add("if (Adapter == AdapterType.Controller)");
            result.Add("{");
            foreach (var type in types.Where(t => CanCreate(nameof(CreateAdapterFactory), t)))
            {
                string entityName = CreateEntityNameFromInterface(type);
                string controllerNameSpace = CreateControllerNameSpace(type);

                if (first)
                {
                    result.Add($"if (typeof(I) == typeof({type.FullName}))");
                }
                else
                {
                    result.Add($"else if (typeof(I) == typeof({type.FullName}))");
                }
                result.Add("{");
                result.Add($"result = new Controller.GenericControllerAdapter<{type.FullName}>(authenticationToken) as Contracts.Client.IAdapterAccess<I>;");
                result.Add("}");
                first = false;
            }
            result.Add("}");
            result.Add("else if (Adapter == AdapterType.Service)");
            result.Add("{");
            first = true;
            foreach (var type in types.Where(t => CanCreate(nameof(CreateAdapterFactory), t)))
            {
                string modelName = CreateEntityNameFromInterface(type);
                string modelNameSpace = CreateTransferNameSpace(type);

                if (first)
                {
                    result.Add($"if (typeof(I) == typeof({type.FullName}))");
                }
                else
                {
                    result.Add($"else if (typeof(I) == typeof({type.FullName}))");
                }
                result.Add("{");
                result.Add($"result = new Service.GenericServiceAdapter<{type.FullName}, {modelNameSpace}.{modelName}>(authenticationToken, BaseUri, \"{modelName}\") as Contracts.Client.IAdapterAccess<I>;");
                result.Add("}");
                first = false;
            }
            result.Add("}");
            result.Add("return result;");
            result.Add("}");

            result.Add("}");
            return EnvelopeWithANamespace(result, AdapterNameSpace);
        }
        #endregion
    }
}
//MdEnd