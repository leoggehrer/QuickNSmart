//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonBase.Extensions;

namespace CSharpCodeGenerator.ConApp.Generation
{
    partial class ClassGenerator : Generator
    {
        protected ClassGenerator(SolutionProperties solutionProperties)
            : base(solutionProperties)
        {

        }
        public static ClassGenerator Create(SolutionProperties solutionProperties)
        {
            return new ClassGenerator(solutionProperties);
        }

        #region Create constructors
        public static IEnumerable<string> CreateStaticConstrutor(string className, IEnumerable<string> initStatements = null)
        {
            var lines = new List<string>
            {
                $"static {className}()",
                "{",
            };
            if (initStatements != null)
            {
                foreach (var item in initStatements)
                {
                    lines.Add($"{item}");
                }
            }
            lines.Add("}");
            lines.Add(string.Empty);
            return lines;
        }
        public static IEnumerable<string> CreatePartialStaticConstrutor(string className, IEnumerable<string> initStatements = null)
        {
            var lines = new List<string>
            {
                $"static {className}()",
                "{",
                "ClassConstructing();"
            };
            if (initStatements != null)
            {
                foreach (var item in initStatements)
                {
                    lines.Add($"{item}");
                }
            }
            lines.Add($"ClassConstructed();");
            lines.Add("}");
            lines.Add("static partial void ClassConstructing();");
            lines.Add("static partial void ClassConstructed();");
            lines.Add(string.Empty);
            return lines;
        }
        public static IEnumerable<string> CreatePartialConstrutor(string visibility, string className, string argumentList = null, string baseConstructor = null, IEnumerable<string> initStatements = null, bool withPartials = true)
        {
            var lines = new List<string>
            {
                $"{visibility} {className}({argumentList})"
            };

            if (string.IsNullOrEmpty(baseConstructor) == false)
                lines.Add($":{baseConstructor}");

            lines.Add("{");
            lines.Add("Constructing();");
            if (initStatements != null)
            {
                foreach (var item in initStatements)
                {
                    lines.Add($"{item}");
                }
            }
            else
            {
                lines.Add(string.Empty);
            }
            lines.Add($"Constructed();");
            lines.Add("}");
            if (withPartials)
            {
                lines.Add("partial void Constructing();");
                lines.Add("partial void Constructed();");
            }
            return lines;
        }
        #endregion Create constructors

        #region Create partial model
        public static IEnumerable<string> CreateModelFromInterface(Type type, 
                                                                   Action<Type, List<string>> createAttributes = null, 
                                                                   Action<Type, PropertyInfo, List<string>> createPropertyAttributes = null)
        {
            type.CheckArgument(nameof(type));

            List<string> result = new List<string>();
            var baseItfcs = GetBaseInterfaces(type).ToArray();
            var entityName = CreateEntityNameFromInterface(type);
            var properties = GetAllInterfaceProperties(type, baseItfcs);

            createAttributes?.Invoke(type, result);
            result.Add($"public partial class {entityName} : {type.FullName}");
            result.Add("{");
            result.AddRange(CreatePartialStaticConstrutor(entityName));
            result.AddRange(CreatePartialConstrutor("public", entityName));
            foreach (var item in properties.Where(p => p.DeclaringType.Name.Equals(IIdentifiableName) == false
                                                    && p.DeclaringType.Name.Equals(IRelationName) == false))
            {
                createPropertyAttributes?.Invoke(type, item, result);
                result.AddRange(CreatePartialProperty(item));
            }
            result.AddRange(CreateCopyProperties(type));
            result.Add("}");
            return result;
        }
        public static IEnumerable<string> CreateDelegateModelFromInterface(Type type,
                                                                    Action<Type, List<string>> createAttributes = null,
                                                                    Action<Type, PropertyInfo, List<string>> createPropertyAttributes = null)
        {
            type.CheckArgument(nameof(type));

            List<string> result = new List<string>();
            var baseItfcs = GetBaseInterfaces(type).ToArray();
            var entityName = CreateEntityNameFromInterface(type);
            var properties = GetAllInterfaceProperties(type, baseItfcs);

            createAttributes?.Invoke(type, result);
            result.Add($"partial class {entityName} : {type.FullName}");
            result.Add("{");
            result.AddRange(CreatePartialStaticConstrutor(entityName));
            result.AddRange(CreatePartialConstrutor("public", entityName));
            if (baseItfcs.Length == 0)
            {
                //result.Add($"internal virtual {type.FullName} {ClassGenerator.DelegatePropertyName} " + "{ get; set; }");
            }
            else
            {
                //result.Add($"internal virtual {type.FullName} {ClassGenerator.DelegatePropertyName} " + "{ get; set; }");
            }
            result.Add($"public {type.FullName} {ClassGenerator.DelegatePropertyName} " + "{ get; set; }");
            foreach (var item in properties.Where(p => p.DeclaringType.Name.Equals(IIdentifiableName) == false))
            {
                result.AddRange(CreatePartialDelegateProperty(item));
            }
            result.AddRange(CreateDelegateCopyProperties(type, type.FullName));

            foreach (var item in type.GetMethods().Where(mi => mi.Name.Contains("_") == false))
            {
                //result.AddRange(CreateDelegateMethod(item));
            }
            result.Add("}");
            return result;
        }


        #endregion Create partial model

        #region Create partial property
        static partial void SetPropertyAttributes(Type type, PropertyInfo propertyInfo, List<string> codeLines);
        static partial void SetPropertyGetAttributes(Type type, PropertyInfo propertyInfo, List<string> codeLines);
        static partial void SetPropertySetAttributes(Type type, PropertyInfo propertyInfo, List<string> codeLines);
        static partial void GetPropertyDefaultValue(Type type, PropertyInfo propertyInfo, ref string defaultValue);

        /// <summary>
        /// Diese Methode erstellt den Programmcode einer Eigenschaft aus dem Eigenschaftsinfo-Objekt.
        /// </summary>
        /// <param name="propertyInfo">Das Eigenschaftsinfo-Objekt.</param>
        /// <returns>Die Eigenschaft als Text.</returns>
        internal static IEnumerable<string> CreatePartialProperty(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = new List<string>();
            var defaultValue = string.Empty;
            var fieldType = Generator.GetPropertyType(propertyInfo);
            var fieldName = CreateFieldName(propertyInfo, "_");
            var paramName = CreateFieldName(propertyInfo, "_");

            result.Add(string.Empty);
            SetPropertyAttributes(propertyInfo.DeclaringType, propertyInfo, result);
            result.Add($"public {fieldType} {propertyInfo.Name}");
            result.Add("{");
            result.AddRange(CreatePartialGetProperty(propertyInfo));
            result.AddRange(CreatePartialSetProperty(propertyInfo));
            result.Add("}");

            GetPropertyDefaultValue(propertyInfo.DeclaringType, propertyInfo, ref defaultValue);
            result.Add(string.IsNullOrEmpty(defaultValue)
                ? $"private {fieldType} {fieldName};"
                : $"private {fieldType} {fieldName} = {defaultValue};");

            result.Add($"partial void On{propertyInfo.Name}Reading();");
            result.Add($"partial void On{propertyInfo.Name}Changing(ref bool handled, ref {fieldType} {paramName});");
            result.Add($"partial void On{propertyInfo.Name}Changed();");
            return result;
        }
        /// <summary>
        /// Diese Methode erstellt den Programmcode einer Getter-Eigenschaft aus dem Eigenschaftsinfo-Objekt.
        /// </summary>
        /// <param name="propertyInfo">Das Eigenschaftsinfo-Objekt.</param>
        /// <param name="generationType">The type of the generation that should be performed</param>
        /// <returns>Die Getter-Eigenschaft als Text.</returns>
        internal static IEnumerable<string> CreatePartialGetProperty(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = new List<string>();
            var fieldName = CreateFieldName(propertyInfo, "_");

            SetPropertyGetAttributes(propertyInfo.DeclaringType, propertyInfo, result);
            result.Add("get");
            result.Add("{");
            result.Add($"On{propertyInfo.Name}Reading();");
            result.Add($"return {fieldName};");
            result.Add("}");
            return result;
        }
        /// <summary>
        /// Diese Methode erstellt den Programmcode einer Setter-Eigenschaft aus dem Eigenschaftsinfo-Objekt.
        /// </summary>
        /// <param name="propertyInfo">Das Eigenschaftsinfo-Objekt.</param>
        /// <returns>Die Setter-Eigenschaft als Text.</returns>
        internal static IEnumerable<string> CreatePartialSetProperty(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = new List<string>();
            var propName = propertyInfo.Name;
            var fieldName = CreateFieldName(propertyInfo, "_");

            SetPropertySetAttributes(propertyInfo.DeclaringType, propertyInfo, result);

            result.Add("set");
            result.Add("{");
            result.Add("bool handled = false;");
            result.Add($"On{propName}Changing(ref handled, ref {fieldName});");
            result.Add("if (handled == false)");
            result.Add("{");
            result.Add($"this.{fieldName} = value;");
            result.Add("}");
            result.Add($"On{propName}Changed();");
            result.Add("}");
            return result.ToArray();
        }
        #endregion Create partial properties

        #region Delegate property helpers
        /// <summary>
        /// Diese Methode erstellt den Programmcode einer Eigenschaft aus dem Eigenschaftsinfo-Objekt.
        /// </summary>
        /// <param name="propertyInfo">Das Eigenschaftsinfo-Objekt.</param>
        /// <returns>Die Eigenschaft als Text.</returns>
        internal static IEnumerable<string> CreatePartialDelegateProperty(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = new List<string>();
            var propName = propertyInfo.Name;
            var fieldType = Generator.GetPropertyType(propertyInfo);

            result.Add($"public {fieldType} {propName}");
            result.Add("{");
            if (propertyInfo.CanRead)
            {
                result.AddRange(CreatePartialGetDelegateProperty(propertyInfo));
            }
            if (propertyInfo.CanWrite)
            {
                result.AddRange(CreatePartialSetDelegateProperty(propertyInfo));
            }
            result.Add("}");

            if (propertyInfo.CanRead)
            {
                result.Add($"partial void On{propName}Reading();");
            }
            if (propertyInfo.CanWrite)
            {
                result.Add($"partial void On{propName}Changed();");
            }
            return result;
        }
        /// <summary>
        /// Diese Methode erstellt den Programmcode einer Getter-Eigenschaft und leitet diese an das Delegate-Objekt weiter.
        /// </summary>
        /// <param name="propertyInfo">Das Eigenschaftsinfo-Objekt.</param>
        /// <returns>Die Getter-Eigenschaft als Text.</returns>
        internal static IEnumerable<string> CreatePartialGetDelegateProperty(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = new List<string>
            {
                "get",
                "{",
                $"On{propertyInfo.Name}Reading();",
                $"return {DelegatePropertyName} != null ? {DelegatePropertyName}.{propertyInfo.Name} : default({propertyInfo.PropertyType});",
                "}"
            };
            return result;
        }
        /// <summary>
        /// Diese Methode erstellt den Programmcode einer Setter-Eigenschaft und leitet diese an das Delegate-Objekt weiter.
        /// </summary>
        /// <param name="propertyInfo">Das Eigenschaftsinfo-Objekt.</param>
        /// <returns>Die Setter-Eigenschaft als Text.</returns>
        internal static IEnumerable<string> CreatePartialSetDelegateProperty(PropertyInfo propertyInfo)
        {
            propertyInfo.CheckArgument(nameof(propertyInfo));

            var result = new List<string>
            {
                "set",
                "{",
                $"{DelegatePropertyName}.{propertyInfo.Name} = value;",
                $"On{propertyInfo.Name}Changed();",
                "}"
            };
            return result;
        }
        #endregion Delegate property helpers

        #region CopyProperties
        internal static IEnumerable<string> CreateCopyProperties(Type type)
        {
            type.CheckArgument(nameof(type));

            return CreateCopyProperties(type, type.FullName);
        }
        internal static IEnumerable<string> CreateCopyProperties(Type type, string copyType)
        {
            type.CheckArgument(nameof(type));
            copyType.CheckArgument(nameof(copyType));

            var result = new List<string>
            {
                $"public void CopyProperties({copyType} other)",
                "{",
                "if (other == null)",
                "{",
                "throw new System.ArgumentNullException(nameof(other));",
                "}",
                string.Empty,
                "bool handled = false;",
                "BeforeCopyProperties(other, ref handled);",
                "if (handled == false)",
                "{",
            };
            foreach (var item in GetPublicProperties(type))
            {
                if (item.Name.Equals(MasterName) && item.DeclaringType.Name.Equals(IRelationName))
                {
                    result.Add($"{MasterName}.CopyProperties(other.{MasterName});");
                }
                else if (item.Name.Equals(DetailsName) && item.DeclaringType.Name.Equals(IRelationName))
                {
                    result.Add("ClearDetails();");
                    result.Add("foreach (var detail in other.Details)");
                    result.Add("{");
                    result.Add("AddDetail(detail);");
                    result.Add("}");
                }
                else if (item.CanRead)
                {
                    result.Add($"{item.Name} = other.{item.Name};");
                }
            }
            result.Add("}");
            result.Add("AfterCopyProperties(other);");
            result.Add("}");

            result.Add($"partial void BeforeCopyProperties({copyType} other, ref bool handled);");
            result.Add($"partial void AfterCopyProperties({copyType} other);");

            return result;
        }
        internal static IEnumerable<string> CreateDelegateCopyProperties(Type type)
        {
            type.CheckArgument(nameof(type));

            return CreateDelegateCopyProperties(type, type.FullName);
        }
        internal static IEnumerable<string> CreateDelegateCopyProperties(Type type, string copyType)
        {
            type.CheckArgument(nameof(type));

            var result = new List<string>
            {
                $"public void CopyProperties({copyType} other)",
                "{",
                "if (other == null)",
                "{",
                "throw new System.ArgumentNullException(nameof(other));",
                "}",
                string.Empty,
                "bool handled = false;",
                "BeforeCopyProperties(other, ref handled);",
                "if (handled == false)",
                "{",
            };
            result.Add($"{ClassGenerator.DelegatePropertyName}.CopyProperties(other as {type.FullName});");
            result.Add("}");
            result.Add("AfterCopyProperties(other);");
            result.Add("}");

            result.Add($"partial void BeforeCopyProperties({copyType} other, ref bool handled);");
            result.Add($"partial void AfterCopyProperties({copyType} other);");

            return result;
        }
        #endregion CopyProperties

        /// <summary>
        /// Diese Methode erstellt den Programmcode fuer das Vergleichen der Eigenschaften.
        /// </summary>
        /// <param name="type">Die Schnittstellen-Typ Information.</param>
        /// <returns>Die Equals-Methode als Text.</returns>
        internal static string[] CreateEquals(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = new List<string>
            {
                $"public override bool Equals(object obj)",
                "{",
                $"if (!(obj is {type.FullName} instance))",
                "{",
                "return false;",
                "}",
                "return base.Equals(instance) && Equals(instance);",
                "}",
                string.Empty,
                $"protected bool Equals({type.FullName} other)",
                "{",
                "if (other == null)",
                "{",
                "return false;",
                "}"
            };

            var counter = 0;

            foreach (var pi in GetPublicProperties(type))
            {
                if (pi.CanRead)
                {
                    var codeLine = counter == 0 ? "return " : "       && ";

                    if (pi.PropertyType.GetTypeInfo().IsValueType)
                    {
                        codeLine += $"{pi.Name} == other.{pi.Name}";
                    }
                    else
                    {
                        codeLine += $"IsEqualsWith({pi.Name}, other.{pi.Name})";
                    }
                    result.Add(codeLine);
                    counter++;
                }
            }
            if (counter > 0)
            {
                result[^1] = $"{result[^1]};";
            }
            else
            {
                result.Add("return true;");
            }
            result.Add("}");
            return result.ToArray();
        }
        /// <summary>
        /// Diese Methode erstellt den Programmcode fuer die Berechnung des Hash-Codes.
        /// </summary>
        /// <param name="type">Die Schnittstellen-Typ Information.</param>
        /// <returns>Die GetHashCode-Methode als Text.</returns>
        internal static string[] CreateGetHashCode(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = new List<string>
            {
                $"public override int GetHashCode()",
                "{"
            };

            var braces = 0;
            var counter = 0;
            var codeLine = string.Empty;

            foreach (var pi in GetPublicProperties(type))
            {
                if (pi.CanRead)
                {
                    if (counter == 0)
                    {
                        braces++;
                        codeLine = "HashCode.Combine(";
                    }
                    else if (counter % 6 == 0)
                    {
                        braces++;
                        codeLine += ", HashCode.Combine(";
                    }
                    else
                    {
                        codeLine += ", ";
                    }
                    codeLine += pi.Name;
                    counter++;
                }
            }
            for (int i = 0; i < braces; i++)
            {
                codeLine += ")";
            }

            if (counter > 0)
            {
                result.Add($"return {codeLine};");
            }
            else
            {
                result.Add($"return base.GetHashCode();");
            }
            result.Add("}");
            return result.ToArray();
        }
    }
}
//MdEnd