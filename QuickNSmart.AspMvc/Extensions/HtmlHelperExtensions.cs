﻿//@QnSBaseCode
//MdStart
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using CommonBase.Extensions;

namespace QuickNSmart.AspMvc.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString ToSelect(this IHtmlHelper htmlHelper, string css, string id, string name, IEnumerable<KeyValuePair<Enum, string>> options, Enum selVal)
        {
            options.CheckArgument(nameof(options));

            StringBuilder sb = new StringBuilder();

            sb.Append("<select");
            if (css.HasContent())
                sb.Append($" class=\"{css}\"");

            if (id.HasContent())
                sb.Append($" id=\"{id}\"");

            if (name.HasContent())
                sb.Append($" name=\"{name}\"");

            sb.Append(">");
            foreach (var item in options)
            {
                if (selVal.Equals(item.Key))
                    sb.AppendLine($"<option value=\"{item.Key}\" selected>{item.Value}</option>");
                else
                    sb.AppendLine($"<option value=\"{item.Key}\">{item.Value}</option>");
            }
            sb.AppendLine("</select>");
            return new HtmlString(sb.ToString());
        }
    }
}
//MdEnd