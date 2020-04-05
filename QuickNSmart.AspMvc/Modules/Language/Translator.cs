//@QnSBaseCode
//MdStart
using System.Collections.Generic;

namespace QuickNSmart.AspMvc.Modules.Language
{
    public partial class Translator
    {
        private static Dictionary<string, string> Translations { get; } = new Dictionary<string, string>();
        static Translator()
        {
            ClassConstructing();
            Init();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        public static void Init()
        {
            BeginInit();
            EndInit();
        }
        static partial void BeginInit();
        static partial void EndInit();

        public static string Translate(string key)
        {
            if (Translations.TryGetValue(key, out string value) == false)
            {
                value = key;
            }
            return value;
        }
        public static string Translate(string key, string defaultValue)
        {
            if (Translations.TryGetValue(key, out string value) == false)
            {
                value = defaultValue;
            }
            return value;
        }
    }
}
//MdEnd