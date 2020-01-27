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

        public string FactoryNameSpace => $"{SolutionProperties.LogicProjectName}";
        public string CreateNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{FactoryNameSpace}.{Generator.GetSubNamespaceFromInterface(type)}";
        }
        partial void CreateFactoryAttributes(Type type, List<string> codeLines);
        public IEnumerable<string> CreateFactory()
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
            foreach (var type in types)
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
            foreach (var type in types)
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
            result.Add("}");
            return EnvelopeWithANamespace(result, FactoryNameSpace);
        }
    }
}
//MdEnd