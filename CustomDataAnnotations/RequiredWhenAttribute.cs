using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MVCLearning.CustomDataAnnotations
{
    public class RequiredWhenAttribute : ValidationAttribute, IClientValidatable
    {
        public RequiredWhenAttribute(string dependentProperty, object[] expectedValue)
        {
            DependentProperty = dependentProperty;
            ExpectedValues = expectedValue;
        }

        public object[] ExpectedValues { get; private set; }

        public string DependentProperty { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null || ExpectedValues == null)
            {
                return null;
            }

            object obj = validationContext.ObjectInstance;

            foreach (var prop in DependentProperty.Split('.').Select(s => obj.GetType().GetProperty(s)))
                obj = prop.GetValue(obj, null);

            if (obj == null)
                return null;

            if (ExpectedValues != null)
            {
                if (ExpectedValues.Contains(obj)) {
                    if (value == null) {
                        return new ValidationResult(base.ErrorMessageString);
                    }
                }
            }

            return null;
        }

        #region * IClientValidatable Members

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = base.ErrorMessageString,
                ValidationType = "requiredwhen",
            };

            rule.ValidationParameters["dependentproperty"] = "*." + DependentProperty;
            rule.ValidationParameters["expectedvaluefordependentproperty"] = (ExpectedValues == null) ? null : string.Join(",", ExpectedValues);
            yield return rule;
        }

        #endregion
    }
}
