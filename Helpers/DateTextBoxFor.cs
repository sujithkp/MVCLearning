using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MVCLearning.Helpers
{
    public static class DateTextBox
    {
        public static MvcHtmlString DateTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var fullName = ExpressionHelper.GetExpressionText(expression);

            object result = null;
            try
            {
                result = ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            }
            catch (System.Reflection.TargetInvocationException)
            {
            }

            string attemptedValue = null;
            if (result != null)
                attemptedValue = Convert.ToDateTime(result, CultureInfo.CurrentCulture).ToString("dd/MM/yyyy");

            var htmlAttributeDic = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (htmlAttributeDic.Keys.Contains("class"))
            {
                htmlAttributeDic["class"] = htmlAttributeDic["class"] + " date-picker";
            }
            else
            {
                htmlAttributeDic.Add("class", "date-picker");
            }

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributeDic);
            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.MergeAttribute("value", attemptedValue);
            tagBuilder.GenerateId(fullName);

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }
            var modelMetaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var attr = htmlHelper.GetUnobtrusiveValidationAttributes(fullName, modelMetaData);
            tagBuilder.MergeAttributes(attr);

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

    }
}