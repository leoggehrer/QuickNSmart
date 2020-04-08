//@QnSBaseCode
//MdStart
using System.Collections.Generic;
using CommonBase.Extensions;

namespace QuickNSmart.AspMvc.Modules.Language
{
    public partial class Translator
    {
        private static readonly Dictionary<string, string> translations = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> noTranslations = new Dictionary<string, string>();
        static Translator()
        {
            ClassConstructing();
            Init();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public static Contracts.Modules.Language.LanguageCode KeyLanguage { get; } = Contracts.Modules.Language.LanguageCode.En;
        public static Contracts.Modules.Language.LanguageCode ValueLanguage { get; } = Contracts.Modules.Language.LanguageCode.De;

        public static IEnumerable<KeyValuePair<string, string>> Translations => translations;
        public static IEnumerable<KeyValuePair<string, string>> NoTranslations = noTranslations;
        private static string CreatePredicate() => $"AppName.Equals(\"{nameof(QuickNSmart)}\") && {nameof(KeyLanguage)} == {(int)KeyLanguage} && {nameof(ValueLanguage)} == {(int)ValueLanguage}";

        public static void Init()
        {
            BeginInit();
            translations.Clear();
            noTranslations.Clear();
            LoadTranslations();
            EndInit();
        }
        static partial void BeginInit();
        static partial void LoadTranslations();
        static partial void EndInit();

        public static string Translate(string key)
        {
            if (translations.TryGetValue(key, out string value) == false)
            {
                value = key;
                if (noTranslations.ContainsKey(key) == false)
                {
                    noTranslations.Add(key, string.Empty);
                }
            }
            return value;
        }
        public static string Translate(string key, string defaultValue)
        {
            if (translations.TryGetValue(key, out string value) == false)
            {
                value = defaultValue;
                if (noTranslations.ContainsKey(key) == false)
                {
                    noTranslations.Add(key, string.Empty);
                }
            }
            return value;
        }

        public static void UpdateTranslations(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            keyValuePairs.CheckArgument(nameof(keyValuePairs));

            BeginUpdateTranslations();
            foreach (var item in keyValuePairs)
            {
                if (translations.ContainsKey(item.Key))
                {
                    translations[item.Key] = item.Value;
                }
            }
            EndUpdateTranslations();
        }
        static partial void BeginUpdateTranslations();
        static partial void EndUpdateTranslations();

        public static void UpdateNoTranslations(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            keyValuePairs.CheckArgument(nameof(keyValuePairs));

            BeginUpdateNoTranslations();
            foreach (var item in keyValuePairs)
            {
                if (noTranslations.ContainsKey(item.Key))
                {
                    noTranslations[item.Key] = item.Value;
                }
            }
            EndUpdateNoTranslations();
        }
        static partial void BeginUpdateNoTranslations();
        static partial void EndUpdateNoTranslations();
    }
}
//MdEnd