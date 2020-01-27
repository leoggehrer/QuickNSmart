//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using CommonBase.Extensions;

namespace CSharpCodeGenerator.ConApp.Generation
{
    partial class DataContextGenerator : Generator
    {
        protected DataContextGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {
        }

        public static DataContextGenerator Create(SolutionProperties solutionProperties)
        {
            return new DataContextGenerator(solutionProperties);
        }

        public string DataContextNameSpace => $"{SolutionProperties.LogicProjectName}.{SolutionProperties.DataContextFolder}";
        public string CreateDbNameSpace()
        {
            return $"{DataContextNameSpace}.Db";
        }
        partial void CreateDataContextAttributes(Type type, List<string> codeLines);

        public IEnumerable<string> CreateDbContext()
        {
            return CreateDbContext(CreateDbNameSpace());
        }
        public IEnumerable<string> CreateDbContext(string nameSpace)
        {
            List<string> result = new List<string>();
            bool first = true;
            ContractsProject contractsProject = ContractsProject.Create(SolutionProperties);

            if (nameSpace.HasContent())
            {
                result.Add($"namespace {nameSpace}");
                result.Add("{");
            }
            result.Add("using Microsoft.EntityFrameworkCore;");
            result.Add($"partial class Db{SolutionProperties.SolutionName}Context : GenericDbContext");
            result.Add("{");

            foreach (var type in contractsProject.PersistenceTypes)
            {
                string entityName = CreateEntityNameFromInterface(type);
                string subNameSpace = GetSubNamespaceFromInterface(type);
                string entityNameSet = $"{entityName}Set";

                result.Add($"protected DbSet<Entities.{subNameSpace}.{entityName}> {entityNameSet}" + " { get; set; }");
            }

            result.Add("public override DbSet<E> Set<I, E>()");
            result.Add("{");
            result.Add("DbSet<E> result = null;");

            foreach (var type in contractsProject.PersistenceTypes)
            {
                string entityName = CreateEntityNameFromInterface(type);
                string entityNameSet = $"{entityName}Set";

                if (first)
                {
                    result.Add($"if (typeof(I) == typeof({type.FullName}))");
                }
                else
                {
                    result.Add($"else if (typeof(I) == typeof({type.FullName}))");
                }
                result.Add("{");
                result.Add($"result = {entityNameSet} as DbSet<E>;");
                result.Add("}");
                first = false;
            }
            result.Add("return result;");
            result.Add("}");

            result.Add("}");
            if (nameSpace.HasContent())
            {
                result.Add("}");
            }
            return result;
        }
    }
}
//MdEnd