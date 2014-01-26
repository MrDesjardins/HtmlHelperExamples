using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace HtmlHelperExamples.HtmlHelpers
{
    public static class HtmlHelperNameSpaces
    {
        public static HelperExtensionInputFactory Input(this HtmlHelper helper)
        {
            return new HelperExtensionInputFactory(helper);
        }


        public static HelperExtensionInputFactory<TModel> Input<TModel>(this HtmlHelper<TModel> helper)
        {
            return new HelperExtensionInputFactory<TModel>(helper);
        }
    }


    /// <summary>
    /// Generic
    /// </summary>
    public class HelperExtensionInputFactory<TModel> : HelperExtensionInputFactory
    {
        private HtmlHelper<TModel> HtmlHelper { get; set; }

        public HelperExtensionInputFactory(HtmlHelper<TModel> htmlHelper) : base(htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }

        public MvcHtmlString SelectorFor<TValue>(Expression<Func<TModel, TValue>> property, IEnumerable<string> items)
        {
            var meta = ModelMetadata.FromLambdaExpression(property, this.HtmlHelper.ViewData);
            string fullPropertyName = HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(property));

            var selectBuilder = new TagBuilder("select");
            selectBuilder.MergeAttribute("name", fullPropertyName);
            selectBuilder.MergeAttribute("id", fullPropertyName);
            selectBuilder.MergeAttribute("class", "selector");

            foreach (var item in items)
            {
                var optionBuilder = new TagBuilder("option");
                optionBuilder.MergeAttribute("value", item);
                optionBuilder.SetInnerText(item);
                selectBuilder.InnerHtml += optionBuilder.ToString();
            }
            return new MvcHtmlString(selectBuilder.ToString());
        }
    }

    /// <summary>
    /// Non-Generic
    /// </summary>
    public class HelperExtensionInputFactory
    {
        private HtmlHelper HtmlHelper { get; set; }

        public HelperExtensionInputFactory(HtmlHelper htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
        }

        public MvcHtmlString SelectorFor(string fullPropertyName, IEnumerable<string> items)
        {

            var selectBuilder = new TagBuilder("select");
            selectBuilder.MergeAttribute("name", fullPropertyName);
            selectBuilder.MergeAttribute("id", fullPropertyName);
            selectBuilder.MergeAttribute("class", "selector");

            foreach (var item in items)
            {
                var optionBuilder = new TagBuilder("option");
                optionBuilder.MergeAttribute("value", item);
                optionBuilder.SetInnerText(item);
                selectBuilder.InnerHtml += optionBuilder.ToString();
            }
            return new MvcHtmlString(selectBuilder.ToString());
        }
    }
}