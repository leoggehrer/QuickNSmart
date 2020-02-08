//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
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
                lines.Add($":{baseConstructor}".SetIndent(1));

            lines.Add("{");
            lines.Add("Constructing();".SetIndent(1));
            if (initStatements != null)
            {
                foreach (var item in initStatements)
                {
                    lines.Add($"{item}".SetIndent(1));
                }
            }
            else
            {
                lines.Add(string.Empty);
            }
            lines.Add($"Constructed();".SetIndent(1));
            lines.Add("}");
            if (withPartials)
            {
                lines.Add("partial void Constructing();");
                lines.Add("partial void Constructed();");
            }
            return lines;
        }
        #endregion Create constructors

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
            result.Add("get".SetIndent(1));
            result.Add("{".SetIndent(1));
            result.Add($"On{propertyInfo.Name}Reading();".SetIndent(2));
            result.Add($"return {fieldName};".SetIndent(2));
            result.Add("}".SetIndent(1));
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

            result.Add("set".SetIndent(1));
            result.Add("{".SetIndent(1));
            result.Add("bool handled = false;".SetIndent(2));
            result.Add($"On{propName}Changing(ref handled, ref {fieldName});".SetIndent(2));
            result.Add("if (handled == false)".SetIndent(2));
            result.Add("{".SetIndent(2));
            result.Add($"this.{fieldName} = value;".SetIndent(3));
            result.Add("}".SetIndent(2));
            result.Add($"On{propName}Changed();".SetIndent(2));
            result.Add("}".SetIndent(1));
            return result.ToArray();
        }
        #endregion Create partial properties

        #region CreateCopyProperties
        internal static IEnumerable<string> CreateCopyProperties(Type type)
        {
            type.CheckArgument(nameof(type));

            var result = new List<string>
            {
                $"public void CopyProperties({type.FullName} other)",
                "{",
                "if (other == null)",
                "{",
                "throw new System.ArgumentNullException(nameof(other));",
                "}",
                string.Empty,
                "bool handled = false;",
                $"BeforeCopyProperties(other, ref handled);",
                "if (handled == false)",
                "{",
            };
            foreach (var item in GetPublicProperties(type))
            {
                if (item.CanRead)
                {
                    result.Add($"{item.Name} = other.{item.Name};".SetIndent(2));
                }
            }
            result.Add("}");
            result.Add("AfterCopyProperties(other);");
            result.Add("}");

            result.Add($"partial void BeforeCopyProperties({type.FullName} other, ref bool handled);");
            result.Add($"partial void AfterCopyProperties({type.FullName} other);");

            return result;
        }
        #endregion CreateCopyProperties
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
                $"if (!(obj is {type.FullName} instance))".SetIndent(1),
                "{".SetIndent(1),
                "return false;".SetIndent(2),
                "}".SetIndent(1),
                "return base.Equals(instance) && Equals(instance);".SetIndent(1),
                "}",
                string.Empty,
                $"protected bool Equals({type.FullName} other)",
                "{",
                "if (other == null)".SetIndent(1),
                "{".SetIndent(1),
                "return false;".SetIndent(2),
                "}".SetIndent(1)
            };

            var counter = 0;

            foreach (var pi in GetPublicProperties(type))
            {
                if (pi.CanRead)
                {
                    var codeLine = counter == 0 ? "return ".SetIndent(1) : "       && ".SetIndent(1);

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
                result[result.Count - 1] = $"{result[result.Count - 1]};";
            }
            else
            {
                result.Add("return true;".SetIndent(1));
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
                result.Add($"return {codeLine};".SetIndent(1));
            }
            else
            {
                result.Add($"return base.GetHashCode();".SetIndent(1));
            }
            result.Add("}");
            return result.ToArray();
        }
    }
}
//MdEnd