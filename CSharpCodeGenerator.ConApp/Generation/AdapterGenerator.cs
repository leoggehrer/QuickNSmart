//@QnSBaseCode
//MdStart
using CommonBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpCodeGenerator.ConApp.Generation
{
    partial class AdapterGenerator : ClassGenerator
    {
        protected AdapterGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }
        public new static AdapterGenerator Create(SolutionProperties solutionProperties)
        {
            return new AdapterGenerator(solutionProperties);
        }

        public string AdapterNameSpace => $"{SolutionProperties.AdaptersProjectName}";

        public string CreateControllerNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{AdapterNameSpace}.Controllers";
        }
        public string CreateServiceNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{AdapterNameSpace}.Services";
        }
        partial void CanCreateController(Type type, ref bool create);

        public bool CanCreateController(Type type)
        {
            bool create = true;

            CanCreateFactoryController(type, ref create);
            return create;
        }
        partial void CanCreateFactoryController(Type type, ref bool create);
        partial void CreateControllerAttributes(Type type, List<string> codeLines);

        public bool CanCreateServices(Type type)
        {
            bool create = true;

            CanCreateFactoryService(type, ref create);
            return create;
        }
        partial void CanCreateFactoryService(Type type, ref bool create);

        public IEnumerable<string> CreateFactory()
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
            foreach (var type in types.Where(t => CanCreateController(t)))
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
                result.Add($"result = new Controller.GenericControllerAdapter<{type.FullName}>() as Contracts.Client.IAdapterAccess<I>;");
                result.Add("}");
                first = false;
            }
            result.Add("}");
            result.Add("else if (Adapter == AdapterType.Service)");
            result.Add("{");
            first = true;
            foreach (var type in types.Where(t => CanCreateServices(t)))
            {
                string modelName = CreateEntityNameFromInterface(type);
                string modelNameSpace = $"Transfer.{GetSubNamespaceFromInterface(type)}";

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
            result.Add("}");
            return EnvelopeWithANamespace(result, AdapterNameSpace);
        }

    }
}
//MdEnd