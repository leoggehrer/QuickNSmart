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

        public IEnumerable<string> CreateBusinessTransfers()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.BusinessTypes)
            {
                if (CanCreate(type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreateTransferFromInterface(type), CreateNameSpace(type), "using System.Text.Json.Serialization;"));
                    result.AddRange(EnvelopeWithANamespace(CreateBusinessModel(type), CreateNameSpace(type)));
                }
            }
            return result;
        }
        private IEnumerable<string> CreateBusinessModel(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = new List<string>
            {
                $"partial class {CreateEntityNameFromInterface(type)} : {GetBaseClassByInterface(type)}",
                "{",
                "}"
            };
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
        private IEnumerable<string> CreateModuleModel(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = new List<string>
            {
                $"partial class {CreateEntityNameFromInterface(type)} : {GetBaseClassByInterface(type)}",
                "{",
                "}"
            };
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
                    result.AddRange(EnvelopeWithANamespace(CreatePersistenceModel(type), CreateNameSpace(type)));
                }
            }
            return result;
        }
        private IEnumerable<string> CreatePersistenceModel(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = new List<string>
            {
                $"partial class {CreateEntityNameFromInterface(type)} : {GetBaseClassByInterface(type)}",
                "{",
                "}"
            };
            return result;
        }

        private IEnumerable<string> CreateTransferFromInterface(Type type)
        {
            return CreateModelFromInterface(type,
                                            (t, r) => CreateTransferAttributes(t, r),
                                            (t, p, r) =>
                                            {
                                                if (p.PropertyType.IsInterface)
                                                {
                                                    r.Add("[JsonIgnore]");
                                                }
                                                else if (p.PropertyType.IsGenericType 
                                                         && p.PropertyType.GetGenericArguments()[0].IsInterface)
                                                {
                                                    r.Add("[JsonIgnore]");
                                                }
                                                CreateTransferPropertyAttributes(t, p.Name, r);
                                            });
        }
        private static string GetBaseClassByInterface(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = string.Empty;

            if (type.FullName.Contains(ContractsProject.BusinessSubName))
            {
                result = "IdentityModel";
                var itfcs = type.GetInterfaces();

                if (itfcs.Length > 0 && itfcs[0].Name.Equals(IOneToManyName))
                {
                    var genericArgs = itfcs[0].GetGenericArguments();

                    if (genericArgs.Length == 2)
                    {
                        var firstModel = $"{CreateModelFullNameFromInterface(genericArgs[0])}";
                        var secondModel = $"{CreateModelFullNameFromInterface(genericArgs[1])}";

                        result = $"OneToManyModel<{genericArgs[0].FullName}, {firstModel}, {genericArgs[1].FullName}, {secondModel}>";
                    }
                }
            }
            else if (type.FullName.Contains(ContractsProject.ModulesSubName))
                result = HasIdentifiableBase(type) ? "IdentityModel" : "TransferModel";
            else if (type.FullName.Contains(ContractsProject.PersistenceSubName))
                result = "IdentityModel";

            var baseItfc = GetBaseInterface(type);
            if (baseItfc != null)
            {
                result = CreateEntityNameFromInterface(baseItfc);
            }
            return result;
        }
        public static string CreateModelFullNameFromInterface(Type type)
        {
            CheckInterfaceType(type);

            var result = string.Empty;

            if (type.IsInterface)
            {
                var entityName = type.Name.Substring(1);

                result = type.FullName.Replace(type.Name, entityName);
                result = result.Replace(".Contracts", ".Transfer");
            }
            return result;
        }
    }
}
//MdEnd