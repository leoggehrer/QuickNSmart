//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonBase.Extensions;
using CommonBase.Helpers;
using QuickNSmart.AspMvc.Models.Modules.Language;
using QuickNSmart.Contracts.Modules.Language;
using QuickNSmart.Contracts.Persistence.Account;
using AccountManger = QuickNSmart.Adapters.Modules.Account.AccountManager;

namespace QuickNSmart.AspMvc.Modules.Language
{
    public partial class Translator
    {
        private static readonly Dictionary<string, TranslationEntry> translations = new Dictionary<string, TranslationEntry>();
        private static readonly Dictionary<string, TranslationEntry> noTranslations = new Dictionary<string, TranslationEntry>();
        static Translator()
        {
            ClassConstructing();
            Init();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        private static string BaseUri { get; set; } = string.Empty;
        public static Contracts.Modules.Language.LanguageCode KeyLanguage { get; } = Contracts.Modules.Language.LanguageCode.En;
        public static Contracts.Modules.Language.LanguageCode ValueLanguage { get; } = Contracts.Modules.Language.LanguageCode.De;

        public static IEnumerable<KeyValuePair<string, TranslationEntry>> Translations => translations;
        public static IEnumerable<KeyValuePair<string, TranslationEntry>> NoTranslations = noTranslations;
        private static string CreatePredicate() => $"AppName.Equals(\"{nameof(QuickNSmart)}\") && {nameof(KeyLanguage)} == {(int)KeyLanguage} && {nameof(ValueLanguage)} == {(int)ValueLanguage}";

        public static void Init()
        {
            var handled = false;
            var settingValue = Logic.Modules.Configuration.Settings.Get("Translator:BaseUri");
            if (settingValue.HasContent())
            {
                BaseUri = settingValue;
            }

            BeginLoadTranslations(ref handled);
            if (handled == false)
            {
                translations.Clear();
                noTranslations.Clear();
                LoadTranslations();
            }
            EndLoadTranslations();
        }
        static void LoadTranslations()
        {
            try
            {
                Adapters.Connector.BaseUri = BaseUri;
                var predicate = CreatePredicate();
                var connector = Adapters.Connector.Create<Contracts.Modules.Language.ITranslation, Models.Modules.Language.Translation>();
                var query = AsyncHelper.RunSync(() => connector.QueryAllAsync(predicate));

                foreach (var item in query)
                {
                    translations.Add(item.Key, new TranslationEntry { Id = item.Id, Value = item.Value });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
        }
        static partial void BeginLoadTranslations(ref bool handled);
        static partial void EndLoadTranslations();

        public static string Translate(string key)
        {
            var result = key;

            if (translations.TryGetValue(key, out TranslationEntry traVal))
            {
                result = traVal.Value;
            }
            else if (noTranslations.ContainsKey(key) == false)
            {
                int nextId = noTranslations.Any() ? noTranslations.Max(i => i.Value.Id) + 1 : 1;

                noTranslations.Add(key, new TranslationEntry { Id = nextId, Value = key });
            }
            return result;
        }
        public static string Translate(string key, string defaultValue)
        {
            var result = defaultValue;

            if (translations.TryGetValue(key, out TranslationEntry traVal))
            {
                result = traVal.Value;
            }
            else if (noTranslations.ContainsKey(key) == false)
            {
                int nextId = noTranslations.Any() ? noTranslations.Max(i => i.Value.Id) + 1 : 1;

                noTranslations.Add(key, new TranslationEntry { Id = nextId, Value = defaultValue });
            }
            return result;
        }

        public static ILoginSession TryLogon(string identityUri, string email, string password)
        {
            var trAccMngr = new AccountManger
            {
                BaseUri = BaseUri,
                Adapter = Adapters.AdapterType.Service,
            };
            var trLogin = default(ILoginSession);

            if (identityUri.HasContent())
            {
                var idAccMngr = new AccountManger
                {
                    BaseUri = identityUri,
                    Adapter = Adapters.AdapterType.Service,
                };
                var idLogin = AsyncHelper.RunSync(() => idAccMngr.LogonAsync(email, password));

                trLogin = AsyncHelper.RunSync(() => trAccMngr.LogonAsync(idLogin.JsonWebToken));
                AsyncHelper.RunSync(() => idAccMngr.LogoutAsync(idLogin.SessionToken));
            }
            else
            {
                trLogin = AsyncHelper.RunSync(() => trAccMngr.LogonAsync(email, password));
            }
            return trLogin;
        }
        public static void Logout(ILoginSession login)
        {
            login.CheckArgument(nameof(login));

            var trAccMngr = new AccountManger
            {
                BaseUri = BaseUri,
                Adapter = Adapters.AdapterType.Service,
            };
            AsyncHelper.RunSync(() => trAccMngr.LogoutAsync(login.SessionToken));
        }

        public static void UpdateTranslations(string identityUri, string email, string password, IEnumerable<KeyValuePair<string, TranslationEntry>> keyValuePairs)
        {
            keyValuePairs.CheckArgument(nameof(keyValuePairs));

            var handled = false;

            BeginUpdateTranslations(identityUri, email, password, keyValuePairs, ref handled);
            if (handled == false)
            {
                foreach (var item in keyValuePairs)
                {
                    if (item.Key.HasContent()
                        && item.Value != null 
                        && item.Value.Id > 0
                        && item.Value.Value.HasContent())
                    {
                        var query = translations.Where(e => e.Value.Id == item.Value.Id);

                        if (query.Any())
                        {
                            var translation = query.ElementAt(0);

                            var changed = translation.Key.Equals(item.Key) == false 
                                          || translation.Value.Value.Equals(item.Value.Value) == false;

                            translations.Remove(translation.Key);
                            translations.Add(item.Key, new TranslationEntry { Changed = changed, Id = item.Value.Id, Value = item.Value.Value });
                        }
                    }
                }
                // Update exits translations
                Adapters.Connector.BaseUri = BaseUri;
                var login = default(ILoginSession);
                try
                {
                    login = TryLogon(identityUri, email, password);
                    var connector = Adapters.Connector.Create<ITranslation, Translation>(login.SessionToken);

                    foreach (var item in translations.Where(i => i.Value.Changed))
                    {
                        var entity = AsyncHelper.RunSync(() => connector.GetByIdAsync(item.Value.Id));

                        if (entity != null)
                        {
                            entity.Key = item.Key;
                            entity.Value = item.Value.Value;
                            AsyncHelper.RunSync(() => connector.UpdateAsync(entity));
                        }
                    }
                    Init();
                }
                finally
                {
                    if (login != null)
                        Logout(login);
                }
            }
            EndUpdateTranslations();
        }
        static partial void BeginUpdateTranslations(string identityUri, string email, string password, IEnumerable<KeyValuePair<string, TranslationEntry>> keyValuePairs, ref bool handled);
        static partial void EndUpdateTranslations();

        public static void UpdateNoTranslations(string identityUri, string email, string password, IEnumerable<KeyValuePair<string, TranslationEntry>> keyValuePairs)
        {
            keyValuePairs.CheckArgument(nameof(keyValuePairs));

            var handled = false;

            BeginUpdateNoTranslations(identityUri, email, password, keyValuePairs, ref handled);
            if (handled == false)
            {
                foreach (var item in keyValuePairs)
                {
                    if (item.Key.HasContent()
                        && item.Value != null
                        && item.Value.Id > 0
                        && item.Value.Value.HasContent())
                    {
                        var query = noTranslations.Where(e => e.Value.Id == item.Value.Id);

                        if (query.Any())
                        {
                            var translation = query.ElementAt(0);

                            var changed = translation.Key.Equals(item.Key) == false
                                          || translation.Value.Value.Equals(item.Value.Value) == false;

                            noTranslations.Remove(translation.Key);
                            noTranslations.Add(item.Key, new TranslationEntry { Changed = changed, Id = item.Value.Id, Value = item.Value.Value });
                        }
                    }
                }

                // Insert missing translations
                Adapters.Connector.BaseUri = BaseUri;
                var login = default(ILoginSession);
                try
                {
                    login = TryLogon(identityUri, email, password);
                    var connector = Adapters.Connector.Create<ITranslation, Translation>(login.SessionToken);

                    foreach (var item in noTranslations.Where(i => i.Value.Changed))
                    {
                        var entity = AsyncHelper.RunSync(() => connector.CreateAsync());

                        if (entity != null)
                        {
                            entity.Key = item.Key;
                            entity.Value = item.Value.Value;
                            AsyncHelper.RunSync(() => connector.InsertAsync(entity));
                        }
                    }
                    Init();
                }
                finally
                {
                    if (login != null)
                        Logout(login);
                }
            }
            EndUpdateNoTranslations();
        }
        static partial void BeginUpdateNoTranslations(string identityUri, string email, string password, IEnumerable<KeyValuePair<string, TranslationEntry>> keyValuePairs, ref bool handled);
        static partial void EndUpdateNoTranslations();
    }
}
//MdEnd