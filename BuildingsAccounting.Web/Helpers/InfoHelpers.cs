using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace BuildingsAccounting.Web.Helpers
{
    public static class InfoHelpers
    {
        public static MvcHtmlString StringInfo(this HtmlHelper html, string name, string value)
        {
            return MvcHtmlString.Create($"<p class=\"mb-2\"><strong>{name}:</strong> <em>{value}</em></p>");
        }

        public static MvcHtmlString ValueInfo<T>(this HtmlHelper html, string name, T value)
        {
            string str = value.ToString() != String.Empty ? value.ToString() : "(інформація відсутня)";
            return MvcHtmlString.Create($"<p class=\"mb-2\"><strong>{name}:</strong> <em class=\"float-right\">{str}</em></p>");
        }
    }
}