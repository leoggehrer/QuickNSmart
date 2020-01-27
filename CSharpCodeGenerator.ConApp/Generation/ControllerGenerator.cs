//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
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

        public string ControllerNameSpace => $"{SolutionProperties.LogicProjectName}.{SolutionProperties.ControllersFolder}";
        public string CreateNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{ControllerNameSpace}.{Generator.GetSubNamespaceFromInterface(type)}";
        }

        private bool CanCreate(Type type)
        {
            bool create = true;

            CanCreateController(type, ref create);
            return create;
        }
        partial void CanCreateController(Type type, ref bool create);
        partial void CreateControllerAttributes(Type type, List<string> codeLines);

        public IEnumerable<string> CreatePersistenceController(Type type)
        {
            type.CheckArgument(nameof(type));

            List<string> result = new List<string>();
            string entityName = CreateEntityNameFromInterface(type);
            string subNameSpace = GetSubNamespaceFromInterface(type);
            string entityType = $"{SolutionProperties.EntitiesFolder}.{subNameSpace}.{entityName}";
            string controllerName = $"{entityName}Controller";

            CreateControllerAttributes(type, result);
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

            CreateControllerAttributes(type, result);
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
                if (CanCreate(type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreatePersistenceController(type), CreateNameSpace(type)));
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
                if (CanCreate(type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreateBusinessController(type), CreateNameSpace(type)));
                }
            }
            return result;
        }
    }
}
//MdEnd