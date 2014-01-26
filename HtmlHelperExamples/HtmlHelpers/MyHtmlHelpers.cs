using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HtmlHelperExamples.HtmlHelpers
{
    public static class MyHtmlHelpers
    {
        public static MvcHtmlString OutputName(this HtmlHelper helper, string name)
        {
            return new MvcHtmlString(string.Format("<h1>{0}</h1>",name));
        }
    }
}