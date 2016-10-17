(function ($) {

    minimumvalue = 0;

    $.validator.addMethod("jqRequiredwhen", function (value, element, params) {

        var actualValueofdependentproperty = $(params["dependentelement"]).val();

        //throw "Error";

        if (actualValueofdependentproperty == undefined)
            actualValueofdependentproperty = $(params["dependentelement"]).getValue();

        if (actualValueofdependentproperty === null || actualValueofdependentproperty === "")
            return true;

        var expectedvaluefordependentproperty = params["expectedvaluefordependentproperty"];
        var isNeedToValidate = ($.inArray(actualValueofdependentproperty, expectedvaluefordependentproperty) >= 0);

        if (!isNeedToValidate)
            return true;

        if (value == null || value == "")
            return false;

        return true;
    });

    $.validator.unobtrusive.adapters.add("requiredwhen", ["dependentproperty", "expectedvaluefordependentproperty"], function (options) {
        var expectedvaluefordependentproperty = null;

        if (options.params.expectedvaluefordependentproperty.length != 0)
            expectedvaluefordependentproperty = options.params.expectedvaluefordependentproperty.split(',');

        var prefix = getModelPrefix(options.element.name);
        dependentproperty = options.params.dependentproperty,
        fullOtherName = appendModelPrefix(dependentproperty, prefix),
        element = $(options.form).find(":input[name='" + fullOtherName + "']");

        options.rules["jqRequiredwhen"] = { dependentelement: element, expectedvaluefordependentproperty: expectedvaluefordependentproperty };

        if (options.message) {
            options.messages["jqRequiredwhen"] = options.message;
        }
    });

    function getModelPrefix(fieldName) {
        return fieldName.substr(0, fieldName.lastIndexOf(".") + 1);
    }

    function appendModelPrefix(value, prefix) {
        if (value.indexOf("*.") === 0) {
            value = value.replace("*.", prefix);
        }
        return value;
    }

}(jQuery))