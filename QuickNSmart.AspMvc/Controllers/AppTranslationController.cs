//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace QuickNSmart.AspMvc.Controllers
{
    public class AppTranslationController : AccessController
    {
        public AppTranslationController(IFactoryWrapper factoryWrapper)
            : base(factoryWrapper)
        {

        }

        public IActionResult Index(string error = null)
        {
            return RedirectToAction("NoTranslations");
        }

        public IActionResult NoTranslations(string error = null)
        {
            var model = new Models.Modules.Language.AppTranslation
            {
                Action = "UpdateNoTranslation",
                ActionError = error,
            };
            model.NavLinks.Add(new Models.ActionItem
            {
                Text = "Texts with no translation",
                Action = "NoTranslations",
                Controller = ControllerName,
                Active = true,
            });
            model.NavLinks.Add(new Models.ActionItem
            {
                Text = "Texts with translations",
                Action = "Translations",
                Controller = ControllerName,
                Active = false,
            });
            Modules.Language.Translator.NoTranslations
                                       .OrderBy(i => i.Key)
                                       .ToList()
                                       .ForEach(e => model.Translations.Add(e.Key, e.Value));
            return View("Index", model);
        }
        public IActionResult Translations(string error = null)
        {
            var model = new Models.Modules.Language.AppTranslation
            {
                Action = "UpdateNoTranslation",
                ActionError = error,
            };
            model.NavLinks.Add(new Models.ActionItem
            {
                Text = "Texts with no translation",
                Action = "NoTranslations",
                Controller = ControllerName,
                Active = false,
            });
            model.NavLinks.Add(new Models.ActionItem
            {
                Text = "Texts with translations",
                Action = "Translations",
                Controller = ControllerName,
                Active = true,
            });
            Modules.Language.Translator.Translations
                                       .OrderBy(i => i.Key)
                                       .ToList()
                                       .ForEach(e => model.Translations.Add(e.Key, e.Value));
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateNoTranslation(IFormCollection formCollection)
        {
            var index = 0;
            var action = nameof(Translations);
            var error = string.Empty;
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            while (formCollection.TryGetValue($"key[{index}]", out StringValues keyValues)
                   && formCollection.TryGetValue($"value[{index++}]", out StringValues valueValues))
            {
                keyValuePairs.Add(new KeyValuePair<string, string>(keyValues[0], valueValues[0]));

            }
            try
            {
                Modules.Language.Translator.UpdateNoTranslations(keyValuePairs);
            }
            catch (Exception ex)
            {
                error = GetExceptionError(ex);
                action = nameof(NoTranslations);
            }
            return RedirectToAction(action, new { error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTranslation(IFormCollection formCollection)
        {
            var index = 0;
            var action = nameof(Translations);
            var error = string.Empty;
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            while (formCollection.TryGetValue($"key[{index}]", out StringValues keyValues)
                   && formCollection.TryGetValue($"value[{index++}]", out StringValues valueValues))
            {
                keyValuePairs.Add(new KeyValuePair<string, string>(keyValues[0], valueValues[0]));

            }
            try
            {
                Modules.Language.Translator.UpdateTranslations(keyValuePairs);
            }
            catch (Exception ex)
            {
                error = GetExceptionError(ex);
            }
            return RedirectToAction(action, new { error });
        }
    }
}
//MdEnd