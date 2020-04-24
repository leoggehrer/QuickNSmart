//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonBase.Extensions;
using CSharpCodeGenerator.ConApp.Extensions;

namespace CSharpCodeGenerator.ConApp.Generation
{
    partial class Generator
    {
        static Generator()
        {
            Constructing();
            Constructed();
        }
        static partial void Constructing();
        static partial void Constructed();

        public enum InterfaceType
        {
            Unknown,
            Business,
            Module,
            Persistence,
        }

        public SolutionProperties SolutionProperties { get; private set; }
        public Generator(SolutionProperties solutionProperties)
        {
            solutionProperties.CheckArgument(nameof(solutionProperties));

            SolutionProperties = solutionProperties;
        }

        public static string EntitiesLabel => "Entities";
        public static string ModulesLabel => "Modules";
        public static string BusinessLabel => "Business";
        public static string PersistenceLabel => "Persistence";

        public static string DelegatePropertyName => "DelegateObject";
        public static string IIdentifiableName => "IIdentifiable";
        public static string IOneToManyName => "IOneToMany`2";
        public static string FirstItemName => "FirstItem";
        public static string SecondItemsName => "SecondItems";

        internal static IEnumerable<string> EnvelopeWithANamespace(IEnumerable<string> source, string nameSpace, params string[] usings)
        {
            List<string> result = new List<string>();

            if (nameSpace.HasContent())
            {
                result.Add($"namespace {nameSpace}");
                result.Add("{");
                result.AddRange(usings);
            }
            result.AddRange(source);
            if (nameSpace.HasContent())
            {
                result.Add("}");
            }
            return result;
        }

        #region Helpers
        #region Assemply-Helpers
        internal static IEnumerable<Type> GetInterfaceTypes(Assembly assembly)
        {
            assembly.CheckArgument(nameof(assembly));

            return assembly.GetTypes().Where(t => t.IsInterface && t.IsPublic);
        }
        internal static IEnumerable<Type> GetModulesTypes(Assembly assembly)
        {
            return GetInterfaceTypes(assembly)
                        .Where(t => t.IsInterface
                                 && t.FullName.Contains(".Modules."));
        }
        internal static IEnumerable<Type> GetPersistenceTypes(Assembly assembly)
        {
            return GetInterfaceTypes(assembly)
                        .Where(t => t.IsInterface
                                 && t.FullName.Contains(".Persistence."));
        }
        internal static IEnumerable<Type> GetBusinessTypes(Assembly assembly)
        {
            return GetInterfaceTypes(assembly)
                        .Where(t => t.IsInterface
                                 && t.FullName.Contains(".Business."));
        }
        #endregion Assembly-Helpers

        #region Interface helpers
        internal static InterfaceType GetInterfaceType(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = InterfaceType.Unknown;

            if (type.Namespace.Contains(ContractsProject.BusinessSubName))
                result = InterfaceType.Business;
            else if (type.Namespace.Contains(ContractsProject.ModulesSubName))
                result = InterfaceType.Module;
            else if (type.Namespace.Contains(ContractsProject.PersistenceSubName))
                result = InterfaceType.Persistence;

            return result;
        }
        internal static bool HasIdentifiableBase(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = false;

            if (type.IsInterface)
            {
                result = type.GetInterfaces().Any(i => i.Name.Equals(IIdentifiableName));
            }
            return result;
        }
        internal static bool HasOneToManyBase(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = false;

            if (type.IsInterface)
            {
                result = type.GetInterfaces().Any(i => i.Name.Equals(IOneToManyName));
            }
            return result;
        }
        internal static Type GetBaseInterface(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = default(Type);

            if (type.IsInterface)
            {
                var interfaceType = GetInterfaceType(type);

                if (interfaceType == InterfaceType.Business)
                {
                    result = type.GetInterfaces().FirstOrDefault(i => i.Namespace.Contains(ContractsProject.BusinessSubName));
                }
                else if (interfaceType == InterfaceType.Module)
                {
                    result = type.GetInterfaces().FirstOrDefault(i => i.Namespace.Contains(ContractsProject.ModulesSubName));
                }
                else if (interfaceType == InterfaceType.Persistence)
                {
                    result = type.GetInterfaces().FirstOrDefault(i => i.Namespace.Contains(ContractsProject.PersistenceSubName));
                }
            }
            return result;
        }
        internal static IEnumerable<Type> GetBaseInterfaces(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = new List<Type>();

            void GetBaseInterfacesRec(Type type, List<Type> baseTypes)
            {
                var baseItfc = GetBaseInterface(type);

                if (baseItfc != null)
                    baseTypes.Add(baseItfc);

                foreach (var item in type.GetInterfaces().Where(i => baseItfc != null && i.FullName.Equals(baseItfc.FullName) == false))
                {
                    GetBaseInterfacesRec(item, baseTypes);
                }
            }
            GetBaseInterfacesRec(type, result);
            return result;
        }
        #endregion Interface helpers

        /// <summary>
        /// Diese Methode ueberprueft, ob der Typ ein Schnittstellen-Typ ist. Wenn nicht,
        /// dann wirft die Methode eine Ausnahme.
        /// </summary>
        /// <param name="type">Der zu ueberpruefende Typ.</param>
        internal static void CheckInterfaceType(Type type)
        {
            type.CheckArgument(nameof(type));

            if (type.GetTypeInfo().IsInterface == false)
                throw new ArgumentException($"The parameter '{nameof(type)}' must be an interface.");
        }
        /// <summary>
        /// Diese Methode ermittelt den Solutionname aus seinem Schnittstellen Typ.
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <returns>Schema der Entitaet.</returns>
        internal static string GetSolutionNameFromInterface(Type type)
        {
            CheckInterfaceType(type);

            var result = string.Empty;
            var data = type.GetTypeInfo().Namespace.Split('.');

            if (data.Length > 0)
            {
                result = data[0];
            }
            return result;
        }
        /// <summary>
        /// Diese Methode ermittelt den Modulenamen aus seinem Schnittstellen Typ.
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <returns>Schema der Entitaet.</returns>
        internal static string GetModuleNameFromInterface(Type type)
        {
            CheckInterfaceType(type);

            var result = string.Empty;
            var data = type.GetTypeInfo().Namespace.Split('.');
            var idx = data.FindIndex(i => i.Equals(BusinessLabel));

            if (idx == -1)
            {
                idx = data.FindIndex(i => i.Equals(PersistenceLabel));
            }

            if (idx == -1)
            {
                idx = data.FindIndex(i => i.Equals(ModulesLabel));
            }

            if (idx > -1)
            {
                var separator = string.Empty;

                for (var i = idx + 1; i < data.Length; i++)
                {
                    result = $"{result}{separator}{data[i]}";
                    separator = ".";
                }
            }
            return result;
        }
        /// <summary>
        /// Diese Methode ermittelt den Teilnamensraum aus seinem Schnittstellen Typ.
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <returns>Schema der Entitaet.</returns>
        internal static string GetSubNamespaceFromInterface(Type type)
        {
            CheckInterfaceType(type);

            var result = string.Empty;
            var data = type.GetTypeInfo().Namespace.Split('.');
            var idx = data.FindIndex(i => i.Equals(BusinessLabel));

            for (var i = 2; i < data.Length; i++)
            {
                if (string.IsNullOrEmpty(result))
                {
                    result = $"{data[i]}";
                }
                else
                {
                    result = $"{result}.{data[i]}";
                }
            }
            return result;
        }

        /// <summary>
        /// Diese Methode ermittelt den Schema Namen aus seinem Schnittstellen Typ und seinem Namespace.
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <returns>Schema der Entitaet.</returns>
        internal static string GetSchemaNameFromInterface(Type type)
        {
            CheckInterfaceType(type);

            var result = string.Empty;

            if (type.GetTypeInfo().IsInterface)
            {
                var data = type.GetTypeInfo().Namespace.Split('.');

                if (data.Contains(PersistenceLabel))
                {
                    if (data[^1].Equals(PersistenceLabel) == false)
                    {
                        result = data[^1];
                    }
                    else
                    {
                        result = "dbo";
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Diese Methode ermittelt den Entity Namen aus seinem Schnittstellen Typ.
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <returns>MethodName der Entitaet.</returns>
        public static string CreateEntityNameFromInterface(Type type)
        {
            CheckInterfaceType(type);

            var result = string.Empty;

            if (type.GetTypeInfo().IsInterface)
            {
                result = type.Name.Substring(1);
            }
            return result;
        }
        /// <summary>
        /// Diese Methode ermittelt den Entity Namen aus seinem Schnittstellen Typ.
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <returns>Name der Entitaet.</returns>
        public static string CreateEntityFullNameFromInterface(Type type)
        {
            CheckInterfaceType(type);

            var result = string.Empty;

            if (type.IsInterface)
            {
                var entityName = type.Name.Substring(1);

                result = type.FullName.Replace(type.Name, entityName);
                result = result.Replace(".Contracts", ".Logic.Entities");
            }
            return result;
        }
        /// <summary>
        /// Diese Methode ermittelt den Kontroller Namen aus seinem Schnittstellen Typ.
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <returns>Name des Kontrollers.</returns>
        public static string CreateControllerFullNameFromInterface(Type type)
        {
            CheckInterfaceType(type);

            var result = string.Empty;

            if (type.IsInterface)
            {
                var entityName = type.Name.Substring(1);

                result = type.FullName.Replace(type.Name, entityName);
                result = result.Replace(".Contracts", ".Logic.Controllers");
                result = $"{result}Controller";
            }
            return result;
        }

        #region Property-Helpers
        /// <summary>
        /// Diese Methode konvertiert den Eigenschaftstyp in eine Zeichenfolge.
        /// </summary>
        /// <param name="propertyInfo">Das Eigenschaftsinfo-Objekt.</param>
        /// <returns>Der Eigenschaftstyp als Zeichenfolge.</returns>
        internal static string GetPropertyType(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.GetCodeDefinition();
        }
        /// <summary>
        /// Diese Methode ermittelt den Feldnamen aus seinem Schnittstellen Typ.
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <param name="preFix">Prefix der dem Namen vorgestellt ist.</param>
        /// <param name="postfix">Postfix der dem Namen nachgestellt ist.</param>
        /// <returns>Der Feldname als Zeichenfolge.</returns>
        public static string CreateFieldName(Type type, string preFix = "", string postFix = "")
        {
            CheckInterfaceType(type);

            return $"{preFix}{char.ToLower(type.Name.Skip(1).First())}{type.Name.Substring(2)}{postFix}";
        }
        /// <summary>
        /// Diese Methode ermittelt den Feldnamen der Eigenschaft.
        /// </summary>
        /// <param name="propertyInfo">Das Eigenschaftsinfo-Objekt.</param>
        /// <param name="prefix">Prefix der dem Namen vorgestellt ist.</param>
        /// <returns>Der Feldname als Zeichenfolge.</returns>
        public static string CreateFieldName(PropertyInfo propertyInfo, string prefix)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            return $"{prefix}{char.ToLower(propertyInfo.Name.First())}{propertyInfo.Name.Substring(1)}";
        }
        /// <summary>
        /// Diese Methode ermittelt den Feldnamen aus seinem Schnittstellen Typ.
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <param name="postfix">Postfix der dem Namen nachgestellt ist.</param>
        /// <returns>Der Feldname als Zeichenfolge.</returns>
        public static string CreatePropertyName(Type type, string postFix = "")
        {
            CheckInterfaceType(type);

            return $"{type.Name.Substring(1)}{postFix}";
        }
        internal static IEnumerable<PropertyInfo> GetPublicProperties(Type type)
        {
            type.CheckArgument(nameof(type));

            if (type.GetTypeInfo().IsInterface)
            {
                var propertyInfos = new List<PropertyInfo>();

                var queue = new Queue<Type>();
                var considered = new List<Type>
                {
                    type
                };
                queue.Enqueue(type);
                while (queue.Count > 0)
                {
                    var subType = queue.Dequeue();
                    foreach (var subInterface in subType.GetInterfaces())
                    {
                        if (considered.Contains(subInterface)) continue;

                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(
                        BindingFlags.FlattenHierarchy
                        | BindingFlags.Public
                        | BindingFlags.Instance);

                    var newPropertyInfos = typeProperties
                        .Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                return propertyInfos.ToArray();
            }

            return type.GetProperties(
                BindingFlags.FlattenHierarchy
                | BindingFlags.Public
                | BindingFlags.Instance);
        }
        /// <summary>
        /// Liefert die Eigenschaften welche direkt in der Schnittstelle definiert sind.
        /// </summary>
        /// <param name="type">Die zu lesende Schnittstelle.</param>
        /// <returns>Alle Eigenschaften die in der Schnittstelle definiert sind.</returns>
        public static IEnumerable<PropertyInfo> GetInterfaceProperties(Type type)
        {
            type.CheckArgument(nameof(type));

            if (type.GetTypeInfo().IsInterface == false)
                throw new ArgumentException($"The parameter '{nameof(type)}' must be an interface.");

            var result = new List<PropertyInfo>();

            foreach (var item in type.GetProperties())
            {
                result.Add(item);
            }
            return result;
        }
        /// <summary>
        /// Liefert alle Eigenschaften die in der Schnittstelle direkt definiert sind und alle
        /// Eigenschaften die den Basisschnittstellen definiert sind. Es koennen Schnittstellen 
        /// ausgenommen werden.
        /// </summary>
        /// <param name="type">Die zu lesende Schnittstelle</param>
        /// <param name="ignoreInterfaces">Die Schnittstellen die von Lesen ausgenommen werden.</param>
        /// <returns>Alle Eigenschaften die in der Schnittstelle definiert sind und alle Eigenschaften die in den Basisschnittstellen definiert sind.</returns>
        public static IEnumerable<PropertyInfo> GetAllInterfaceProperties(Type type, params Type[] ignoreInterfaces)
        {
            type.CheckArgument(nameof(type));

            if (type.GetTypeInfo().IsInterface == false)
                throw new ArgumentException($"The parameter '{nameof(type)}' must be an interface.");

            var result = new List<PropertyInfo>();

            if (ignoreInterfaces.Contains(type) == false)
            {
                result.AddRange(GetInterfaceProperties(type));
                var interfaces = FlattenInterfaces(type.GetInterfaces());

                foreach (var item in interfaces)
                {
                    if (ignoreInterfaces.Contains(item) == false)
                    {
                        foreach (var pi in GetInterfaceProperties(item))
                        {
                            if (result.Find(p => p.Name.Equals(pi.Name)) == null)
                            {
                                result.Add(pi);
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Diese Methode loest die Vererbung auf und liefert alle Schnittstellen in einer Liste.
        /// </summary>
        /// <param name="types">Die aufzuloesenden Schnittstellen.</param>
        /// <returns>Eine Liste der Schnittstellen.</returns>
        public static IEnumerable<Type> FlattenInterfaces(IEnumerable<Type> types)
        {
            types.CheckArgument(nameof(types));

            var result = new List<Type>();

            foreach (var type in types)
            {
                if (type.GetTypeInfo().IsInterface
                    && result.Contains(type) == false)
                {
                    result.Add(type);
                    FlattenInterfacesRec(type, result);
                }
            }
            return result;
        }
        private static void FlattenInterfacesRec(Type type, List<Type> types)
        {
            type.CheckArgument(nameof(type));
            types.CheckArgument(nameof(types));

            foreach (var itf in type.GetInterfaces())
            {
                if (types.Contains(itf) == false)
                {
                    types.Add(itf);
                    FlattenInterfacesRec(itf, types);
                }
            }
        }
        #endregion Property-Helpers

        #endregion Helpers
    }
}
//MdEnd