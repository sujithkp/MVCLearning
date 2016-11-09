using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVCLearning.Helpers
{
    public static class AutoCompleteBox
    {
        public static MvcHtmlString AutoCompleteBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, object[] codeTableArray, Expression<Func<TModel, TProperty>> property, object htmlAttributes)
        {
            const string suffix = "Id";

            if (codeTableArray == null)
                throw new ArgumentNullException("Source of autoCompleteBox");

            var fullName = ExpressionHelper.GetExpressionText(property);

            object result = null;
            try
            {
                result = ((LambdaExpression)property).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            }
            catch (System.Reflection.TargetInvocationException)
            {
            }

            object attemptedValue = result;

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            tagBuilder.MergeAttribute("name", fullName.Replace(suffix, ""));
            var codeTable = attemptedValue;

            string caption = string.Empty;

            tagBuilder.MergeAttribute("value", caption);

            tagBuilder.GenerateId(fullName);
            var id = tagBuilder.Attributes["id"].ToString();
            tagBuilder.MergeAttribute("id", id.Replace(suffix, ""), true);

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            var modelMetaData = ModelMetadata.FromLambdaExpression(property, htmlHelper.ViewData);

            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(fullName.Substring(0,fullName.Length), null));

            var scripts = new StringBuilder();
            scripts.Append("<script type=\"text/javascript\">");
            scripts.Append("$(document).ready(function () {");
            scripts.AppendFormat("$.{1}source = {0};$('#{1}').makeAsAutocomplete($.{1}source);", htmlHelper.Raw(Json.Encode(codeTableArray)), id.Replace(suffix, ""));
            scripts.Append("});");
            scripts.Append("</script>");

            var hiddenField = string.Format("<input type=\"hidden\" id=\"{0}\" name=\"{1}\" value=\"{2}\" />", id, fullName, result);

            return new MvcHtmlString(string.Format("{0}{1}{2}", tagBuilder.ToString(TagRenderMode.SelfClosing), hiddenField, scripts.ToString()));
        }
    }
}