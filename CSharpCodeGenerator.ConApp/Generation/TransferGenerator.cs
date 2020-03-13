//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using CommonBase.Extensions;

namespace CSharpCodeGenerator.ConApp.Generation
{
    partial class TransferGenerator : ClassGenerator
    {
        protected TransferGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }
        public new static TransferGenerator Create(SolutionProperties solutionProperties)
        {
            return new TransferGenerator(solutionProperties);
        }

        public string TransferNameSpace => $"{SolutionProperties.TransferProjectName}";

        public string CreateNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{TransferNameSpace}.{Generator.GetSubNamespaceFromInterface(type)}";
        }
        private bool CanCreate(Type type)
        {
            bool create = true;

            CanCreateTransfer(type, ref create);
            return create;
        }
        partial void CanCreateTransfer(Type type, ref bool create);
        partial void CreateTransferAttributes(Type type, List<string> codeLines);
        partial void CreateTransferPropertyAttributes(Type type, string propertyName, List<string> codeLines);

        public IEnumerable<string> CreateTransferFromInterface(Type type)
        {
            type.CheckArgument(nameof(type));

            List<string> result = new List<string>();
            var entityName = CreateEntityNameFromInterface(type);

            CreateTransferAttributes(type, result);
            result.Add($"public partial class {entityName} : {type.FullName}");
            result.Add("{");
            result.AddRange(CreatePartialStaticConstrutor(entityName));
            result.AddRange(CreatePartialConstrutor("public", entityName));
            foreach (var item in GetPublicProperties(type).Where(p => p.DeclaringType.Name.Equals("IIdentifiable") == false))
            {
                if (item.PropertyType.IsInterface)
                {
                    result.Add("[JsonIgnore]");
                }
                else if (item.PropertyType.IsGenericType && item.PropertyType.GetGenericArguments()[0].IsInterface)
                {
                    result.Add("[JsonIgnore]");
                }
                CreateTransferPropertyAttributes(type, item.Name, result);
                result.AddRange(CreatePartialProperty(item));
            }
            result.AddRange(CreateSubCopyProperties(type));
            result.Add("}");
            return result;
        }

        public IEnumerable<string> CreateModulesTransfers()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.ModuleTypes)
            {
                if (CanCreate(type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreateTransferFromInterface(type), CreateNameSpace(type), "using System.Text.Json.Serialization;"));
                    result.AddRange(EnvelopeWithANamespace(CreateModuleModel(type), CreateNameSpace(type)));
                }
            }
            return result;
        }
        public IEnumerable<string> CreateBusinessTransfers()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.BusinessTypes)
            {
                if (CanCreate(type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreateTransferFromInterface(type), CreateNameSpace(type), "using System.Text.Json.Serialization;"));
                    result.AddRange(EnvelopeWithANamespace(CreateTransferModel(type), CreateNameSpace(type)));
                }
            }
            return result;
        }
        public IEnumerable<string> CreatePersistenceTransfers()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.PersistenceTypes)
            {
                if (CanCreate(type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreateTransferFromInterface(type), CreateNameSpace(type), "using System.Text.Json.Serialization;"));
                    result.AddRange(EnvelopeWithANamespace(CreateTransferModel(type), CreateNameSpace(type)));
                }
            }
            return result;
        }
        private IEnumerable<string> CreateTransferModel(Type type)
        {
            type.CheckArgument(nameof(type));

            List<string> result = new List<string>();

            result.Add($"partial class {CreateEntityNameFromInterface(type)} : TransferObject");
            result.Add("{");
            result.Add("}");
            return result;
        }
        private IEnumerable<string> CreateModuleModel(Type type)
        {
            type.CheckArgument(nameof(type));

            List<string> result = new List<string>();

            result.Add($"partial class {CreateEntityNameFromInterface(type)} : ModuleObject");
            result.Add("{");
            result.Add("}");
            return result;
        }
    }
}
//MdEnd