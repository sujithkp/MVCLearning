using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVCLearning.CustomDataAnnotations
{
    public class DateRangeAttribute : ValidationAttribute, IClientValidatable
    {
        public DateRangeAttribute()
        {
            DependantProperty = string.Empty;
        }

        public DateTime CurrentDate { get; set; }

        public int Years { get; set; }

        public string DependantProperty { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (DependantProperty != null && DependantProperty != string.Empty)
            {
                var obj = validationContext.ObjectInstance;
                var bDate = (DateTime?)obj.GetType().GetProperty(DependantProperty).GetValue(obj, null);
                var enteredDate = (DateTime?)value;

                if (bDate == null) return null;

                if (bDate.Value.Subtract(enteredDate.Value).Minutes > 0)
                    return null;
            }
            else
            {
                var val = Convert.ToDateTime(value).AddYears(-1 * Years);

                if (DateTime.Now > val)
                    return null;
            }

            return new ValidationResult(base.ErrorMessageString);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = base.ErrorMessageString,
                ValidationType = "daterange",
            };

            rule.ValidationParameters["years"] = Years;
			rule.ValidationParameters["dependantproperty"] = (DependantProperty == null) ? null : (String.Concat("*.", DependantProperty));
            yield return rule;
        }
    }
}
