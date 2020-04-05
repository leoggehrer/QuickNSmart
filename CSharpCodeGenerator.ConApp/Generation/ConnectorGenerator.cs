//@QnSBaseCode
//MdStart
using CommonBase.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace CSharpCodeGenerator.ConApp.Generation
{
    internal partial class ConnectorGenerator : ClassGenerator
    {
        public string ProjectName => $"{SolutionProperties.SolutionName}{SolutionProperties.ConnectorPostfix}";
        public string ProjectPath => Path.Combine(SolutionProperties.SolutionPath, ProjectName);

        public string _connectorPath = null;
        public string ConnectorPath
        {
            get
            {
                if (_connectorPath.IsNullOrEmpty())
                {
                    var binPath = Path.Combine(ProjectPath, "bin");

                    if (Directory.Exists(binPath))
                    {
                        var fileName = $"{ProjectName}.dll";
                        var fileInfos = new DirectoryInfo(binPath).GetFiles(fileName, SearchOption.AllDirectories)
                                                                  .Where(f => f.FullName.EndsWith(fileName))
                                                                  .OrderByDescending(f => f.LastWriteTime);

                        var fileInfo = fileInfos.FirstOrDefault();

                        if (fileInfo != null)
                        {
                            _connectorPath = Path.GetDirectoryName(fileInfo.FullName);
                            Logger(fileInfo.FullName);
                        }
                        else
                        {
                            ErrorLogger("No dll available.");
                        }
                    }
                    else
                    {
                        ErrorLogger("No dll available.");
                    }
                }
                return _connectorPath;
            }
            set => _connectorPath = value;
        }
        public string ConnectorFilePath
        {
            get
            {
                string result = string.Empty;

                if (ConnectorPath.IsNullOrEmpty() == false)
                {
                    var fileName = $"{ProjectName}.dll";

                    result = Path.Combine(ConnectorPath, fileName);
                }
                return result;
            }
        }

        public IEnumerable<Assembly> ReferencedContracts
        {
            get
            {
                var result = new List<Assembly>();

                if (ConnectorPath.HasContent())
                {
                    var fileInfos = new DirectoryInfo(ConnectorPath).GetFiles($"*{SolutionProperties.ContractsPostfix}.dll")
                                          .OrderByDescending(f => f.LastWriteTime);
                    result.AddRange(fileInfos.Select(fi => Assembly.LoadFile(fi.FullName)));
                }
                return result;
            }
        }
        public IEnumerable<Assembly> ReferencedAdapters
        {
            get
            {
                var result = new List<Assembly>();

                if (ConnectorPath.HasContent())
                {
                    var fileInfos = new DirectoryInfo(ConnectorPath).GetFiles($"*{SolutionProperties.AdaptersPostfix}.dll")
                                          .OrderByDescending(f => f.LastWriteTime);
                    result.AddRange(fileInfos.Select(fi => Assembly.LoadFile(fi.FullName)));
                }
                return result;
            }
        }
        public IEnumerable<Assembly> ReferencedAssemblies
        {
            get
            {
                var result = new List<Assembly>();

                if (ConnectorFilePath.HasContent())
                {
                    var assembly = AssemblyLoadContext
                                        .Default
                                        .LoadFromAssemblyPath(ConnectorFilePath);

                    if (assembly != null)
                    {
                        result.AddRange(assembly.GetReferencedAssemblies()
                                        .Select(a => Assembly.Load(a)));
                    }
                }
                return result;
            }
        }

        private ConnectorGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {

        }
        public new static ConnectorGenerator Create(SolutionProperties solutionProperties)
        {
            return new ConnectorGenerator(solutionProperties);
        }

        private string CreateNamespace(Type type, string folderName)
        {
            string solutionName = GetSolutionNameFromInterface(type);
            string subNameSpace = GetSubNamespaceFromInterface(type);
            
            return $"{SolutionProperties.ConnectorProjectName}.{folderName}.{solutionName}.{subNameSpace}";
        }

        public IEnumerable<string> CreateContracts()
        {
            List<string> result = new List<string>();

            foreach (var assemblyContracts in ReferencedContracts)
            {
                var contracts = GetModulesTypes(assemblyContracts)
                                    .Union(GetBusinessTypes(assemblyContracts))
                                    .Union(GetPersistenceTypes(assemblyContracts));

                foreach (var contract in contracts)
                {
                    result.Add($"namespace {CreateNamespace(contract, SolutionProperties.ContractsFolder)}");
                    result.Add("{");
                    result.Add($"public partial interface {contract.Name} : {contract.FullName}");
                    result.Add("{");
                    result.Add("}");
                    result.Add("}");
                }
            }
            return result;
        }
        public IEnumerable<string> CreateModulesContracts()
        {
            List<string> result = new List<string>();

            foreach (var assemblyContracts in ReferencedContracts)
            {
                var contracts = GetModulesTypes(assemblyContracts);

                foreach (var contract in contracts)
                {
                    result.Add($"namespace {CreateNamespace(contract, SolutionProperties.ContractsFolder)}");
                    result.Add("{");
                    result.Add($"public partial interface {contract.Name}");
                    result.Add("{");
                    result.AddRange(CreateInterfaceProperties(contract));
                    result.Add("}");
                    result.Add("}");
                }
            }
            return result;
        }
        public IEnumerable<string> CreateBusinessContracts()
        {
            List<string> result = new List<string>();

            foreach (var assemblyContracts in ReferencedContracts)
            {
                var contracts = GetBusinessTypes(assemblyContracts);

                foreach (var contract in contracts)
                {
                    result.Add($"namespace {CreateNamespace(contract, SolutionProperties.ContractsFolder)}");
                    result.Add("{");
                    result.Add($"public partial interface {contract.Name} : IIdentifiable, ICopyable<{contract.Name}>");
                    result.Add("{");
                    result.AddRange(CreateInterfaceProperties(contract));
                    result.Add("}");
                    result.Add("}");
                }
            }
            return result;
        }
        public IEnumerable<string> CreatePersistenceContracts()
        {
            List<string> result = new List<string>();

            foreach (var assemblyContracts in ReferencedContracts)
            {
                var contracts = GetPersistenceTypes(assemblyContracts);

                foreach (var contract in contracts)
                {
                    result.Add($"namespace {CreateNamespace(contract, SolutionProperties.ContractsFolder)}");
                    result.Add("{");
                    result.Add($"public partial interface {contract.Name} : IIdentifiable, ICopyable<{contract.Name}>");
                    result.Add("{");
                    result.AddRange(CreateInterfaceProperties(contract));
                    result.Add("}");
                    result.Add("}");
                }
            }
            return result;
        }

        public IEnumerable<string> CreateModulesModels()
        {
            List<string> result = new List<string>();

            foreach (var assemblyContracts in ReferencedContracts)
            {
                var contracts = GetModulesTypes(assemblyContracts);

                foreach (var contract in contracts)
                {
                    string contractNameSpace = CreateNamespace(contract, SolutionProperties.ContractsFolder);
                    string contractName = $"{contractNameSpace}.{contract.Name}";

                    result.Add($"namespace {CreateNamespace(contract, SolutionProperties.ModelsFolder)}");
                    result.Add("{");
                    result.AddRange(CreateModel(contract, contractName));
                    result.AddRange(CreateModuleModel(contract));
                    result.Add("}");
                }
            }
            return result;
        }
        public IEnumerable<string> CreateBusinessModels()
        {
            List<string> result = new List<string>();

            foreach (var assemblyContracts in ReferencedContracts)
            {
                var contracts = GetBusinessTypes(assemblyContracts);

                foreach (var contract in contracts)
                {
                    string contractNameSpace = CreateNamespace(contract, SolutionProperties.ContractsFolder);
                    string contractName = $"{contractNameSpace}.{contract.Name}";

                    result.Add($"namespace {CreateNamespace(contract, SolutionProperties.ModelsFolder)}");
                    result.Add("{");
                    result.AddRange(CreateModel(contract, contractName));
                    result.AddRange(CreateBusinessModel(contract));
                    result.Add("}");
                }
            }
            return result;
        }
        public IEnumerable<string> CreatePersistenceModels()
        {
            List<string> result = new List<string>();

            foreach (var assemblyContracts in ReferencedContracts)
            {
                var contracts = GetPersistenceTypes(assemblyContracts);

                foreach (var contract in contracts)
                {
                    string contractNameSpace = CreateNamespace(contract, SolutionProperties.ContractsFolder);
                    string contractName = $"{contractNameSpace}.{contract.Name}";

                    result.Add($"namespace {CreateNamespace(contract, SolutionProperties.ModelsFolder)}");
                    result.Add("{");
                    result.AddRange(CreateModel(contract, contractName));
                    result.AddRange(CreatePersistenceModel(contract));
                    result.Add("}");
                }
            }
            return result;
        }

        private IEnumerable<string> CreateModel(Type type, string contractName)
        {
            type.CheckArgument(nameof(type));
            contractName.CheckArgument(nameof(contractName));

            List<string> result = new List<string>();
            var entityName = CreateEntityNameFromInterface(type);

            result.Add($"partial class {entityName} : {contractName}");
            result.Add("{");
            result.AddRange(CreatePartialStaticConstrutor(entityName));
            result.AddRange(CreatePartialConstrutor("public", entityName));
            result.Add($"public {type.FullName} {ClassGenerator.DelegatePropertyName} " + "{ get; set; }");
            foreach (var item in GetPublicProperties(type).Where(p => p.DeclaringType.Name.Equals("IIdentifiable") == false))
            {
                result.AddRange(CreatePartialDelegateProperty(item));
            }
            result.AddRange(CreateDelegateCopyProperties(type, contractName));

            foreach (var item in type.GetMethods().Where(mi => mi.Name.Contains("_") == false))
            {
                result.AddRange(CreateDelegateMethod(item));
            }

            result.Add("}");
            return result;
        }
        private IEnumerable<string> CreateModuleModel(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = new List<string>
            {
                $"partial class {CreateEntityNameFromInterface(type)} : Models.WrapperModel",
                "{",
                "}"
            };
            return result;
        }
        private IEnumerable<string> CreateBusinessModel(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = new List<string>
            {
                $"partial class {CreateEntityNameFromInterface(type)} : Models.IdentityModel",
                "{",
                "}"
            };
            return result;
        }
        private IEnumerable<string> CreatePersistenceModel(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = new List<string>
            {
                $"partial class {CreateEntityNameFromInterface(type)} : Models.IdentityModel",
                "{",
                "}"
            };
            return result;
        }

        #region Property helpers
        internal IEnumerable<string> CreateDelegateMethod(MethodInfo methodInfo)
        {
            methodInfo.CheckArgument(nameof(methodInfo));

            var result = new List<string>();
            var parameterList = string.Empty;
            var parameterNames = string.Empty;
            var delegateCall = new List<string>();

            foreach (var item in methodInfo.GetParameters())
            {
                if (parameterList.Length > 0)
                    parameterList += ", ";

                parameterList += $"{item.ParameterType} {item.Name}";

                if (parameterNames.Length > 0)
                    parameterNames += ", ";

                parameterNames += $"{item.Name}";
            }
            string returnType;
            if (methodInfo.ReturnType == typeof(void))
            {
                returnType = "void";
                delegateCall.Add($"{DelegatePropertyName}.{methodInfo.Name}({parameterNames});");
            }
            else if (methodInfo.ReturnType.IsInterface && methodInfo.ReturnType.FullName.Contains(".Contracts."))
            {
                var entityName = CreateEntityNameFromInterface(methodInfo.ReturnType);
                var subNameSpace = GetSubNamespaceFromInterface(methodInfo.ReturnType);
                var contractNameSpace = $"{SolutionProperties.ConnectorProjectName}.{SolutionProperties.ContractsFolder}.{subNameSpace}";
                var modelNameSpace = $"{SolutionProperties.ConnectorProjectName}.{SolutionProperties.ModelsFolder}.{subNameSpace}";

                returnType = $"{contractNameSpace}.{methodInfo.ReturnType.Name}";
                delegateCall.Add($"var model = new {modelNameSpace}.{entityName}();");
                delegateCall.Add($"model.{DelegatePropertyName} = {DelegatePropertyName}.{methodInfo.Name}({parameterNames});");
                delegateCall.Add("return model;");
            }
            else
            {
                returnType = methodInfo.ReturnType.ToString();
                delegateCall.Add($"return {DelegatePropertyName}.{methodInfo.Name}({parameterNames});");
            }

            result.Add($"public {returnType} {methodInfo.Name}({parameterList})");
            result.Add("{");
            result.AddRange(delegateCall);
            result.Add("}");

            return result;
        }
        internal IEnumerable<string> CreateInterfaceProperties(Type type)
        {
            type.CheckArgument(nameof(type));

            string GetPropertyType(PropertyInfo pi)
            {
                var result = Generator.GetPropertyType(pi); 

                if (pi.PropertyType.IsInterface && pi.PropertyType.FullName.Contains(".Contacts."))
                {
                    var subNameSpace = GetSubNamespaceFromInterface(pi.PropertyType);
                    var contractNameSpace = $"{SolutionProperties.ConnectorProjectName}.{SolutionProperties.ContractsFolder}.{subNameSpace}";

                    result = $"{contractNameSpace}.{pi.PropertyType.Name}";
                }
                return result;
            }
            var result = new List<string>();

            foreach (var pi in GetPublicProperties(type).Where(p => p.DeclaringType.Name.Equals("IIdentifiable") == false))
            {
                if (pi.CanRead && pi.CanWrite)
                    result.Add($"{GetPropertyType(pi)} {pi.Name} " + "{ get; set; }");
                else if (pi.CanRead)
                    result.Add($"{GetPropertyType(pi)} {pi.Name} " + "{ get; }");
                else if (pi.CanWrite)
                    result.Add($"{GetPropertyType(pi)} {pi.Name} " + "{ set; }");
            }
            return result;
        }
        #endregion Property helper

        #region Helpers
        private static void Logger(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
        private static void ErrorLogger(string message)
        {
            var saveColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error: ");
            Console.ForegroundColor = saveColor;
            Console.WriteLine(message);
        }
        #endregion Helpers
    }
}
//MdEnd