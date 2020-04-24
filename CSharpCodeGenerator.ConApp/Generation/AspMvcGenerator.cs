//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using CommonBase.Extensions;

namespace CSharpCodeGenerator.ConApp.Generation
{
    partial class AspMvcGenerator : ClassGenerator
    {
        protected AspMvcGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }
        public new static AspMvcGenerator Create(SolutionProperties solutionProperties)
        {
            return new AspMvcGenerator(solutionProperties);
        }

        public string AspMvcNameSpace => $"{SolutionProperties.AspMvcProjectName}.{SolutionProperties.ModelsFolder}";

        public string CreateNameSpace(Type type)
        {
            type.CheckArgument(nameof(type));

            return $"{AspMvcNameSpace}.{Generator.GetSubNamespaceFromInterface(type)}";
        }
        private bool CanCreate(Type type)
        {
            bool create = true;

            CanCreateModel(type, ref create);
            return create;
        }
        partial void CanCreateModel(Type type, ref bool create);
        partial void CreateModelAttributes(Type type, List<string> codeLines);
        partial void CreateModelPropertyAttributes(Type type, string propertyName, List<string> codeLines);

        public IEnumerable<string> CreateBusinessModels()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.BusinessTypes)
            {
                if (CanCreate(type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreateModelFromInterface(type), CreateNameSpace(type)));
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

        public IEnumerable<string> CreateModulesModels()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.ModuleTypes)
            {
                if (CanCreate(type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreateModelFromInterface(type), CreateNameSpace(type)));
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

        public IEnumerable<string> CreatePersistenceModels()
        {
            List<string> result = new List<string>();
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);

            foreach (var type in contractsProject.PersistenceTypes)
            {
                if (CanCreate(type))
                {
                    result.AddRange(EnvelopeWithANamespace(CreateModelFromInterface(type), CreateNameSpace(type)));
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

        private IEnumerable<string> CreateModelFromInterface(Type type)
        {
            type.CheckArgument(nameof(type));

            List<string> result = new List<string>();
            var baseItfcs = GetBaseInterfaces(type).ToArray();
            var entityName = CreateEntityNameFromInterface(type);
            var properties = GetAllInterfaceProperties(type, baseItfcs);

            CreateModelAttributes(type, result);
            result.Add($"public partial class {entityName} : {type.FullName}");
            result.Add("{");
            result.AddRange(CreatePartialStaticConstrutor(entityName));
            result.AddRange(CreatePartialConstrutor("public", entityName));
            foreach (var item in properties.Where(p => p.DeclaringType.Name.Equals(IIdentifiableName) == false
                                                    && p.DeclaringType.Name.Equals(IOneToManyName) == false))
            {
                CreateModelPropertyAttributes(type, item.Name, result);
                result.AddRange(CreatePartialProperty(item));
            }
            result.AddRange(CreateCopyProperties(type));
            result.Add("}");
            return result;
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
                result = HasIdentifiableBase(type) ? "IdentityModel" : "ModelObject";
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
                result = result.Replace(".Contracts", ".AspMvc.Models");
            }
            return result;
        }
    }
}
//MdEnd