//@QnSIgnore
using System;
using System.Linq;
using System.Reflection;
using CommonBase.Helpers;

namespace QuickNSmart.AspMvc.Modules.Language
{
    partial class Translator
    {
        //        private static string BaseUri = "http://localhost:32985/api";
        private static string BaseUri = "https://localhost:5001/api";

        static partial void LoadTranslations()
        {
            try
            {
                Adapters.Connector.BaseUri = "http://localhost:32985/api";
                Adapters.Connector.BaseUri = BaseUri;
                var predicate = CreatePredicate();
                var connector = Adapters.Connector.Create<Contracts.Modules.Language.ITranslation, Models.Modules.Language.Translation>();
                var tranlations = AsyncHelper.RunSync(() => connector.QueryAllAsync(predicate));

                foreach (var translation in tranlations)
                {
                    translations.Add(translation.Key, translation.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
            }
        }
        static partial void EndUpdateTranslations()
        {
            Adapters.Connector.BaseUri = BaseUri;
            var connector = Adapters.Connector.Create<Contracts.Modules.Language.ITranslation, Models.Modules.Language.Translation>();

            foreach (var item in translations)
            {
                var predicate = CreatePredicate() + $"&& Key.Equals(\"{item.Key}\")";
                var query = AsyncHelper.RunSync(() =>  connector.QueryAllAsync(predicate));

                if (query.Any())
                {
                    var entity = query.ElementAt(0);

                    entity.Value = item.Value;
                    AsyncHelper.RunSync(() => connector.UpdateAsync(entity));
                }
            }
            Init();
        }

        static partial void EndUpdateNoTranslations()
        {
            Adapters.Connector.BaseUri = BaseUri;
            var connector = Adapters.Connector.Create<Contracts.Modules.Language.ITranslation, Models.Modules.Language.Translation>();

            foreach (var item in noTranslations)
            {
                var entity = AsyncHelper.RunSync(() => connector.CreateAsync());

                entity.AppName = nameof(QuickNSmart);
                entity.KeyLanguage = KeyLanguage;
                entity.Key = item.Key;
                entity.ValueLanguage = ValueLanguage;
                entity.Value = item.Value;
                AsyncHelper.RunSync(() => connector.InsertAsync(entity));
            }
            Init();
        }
    }
}
