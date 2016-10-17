(function ($) {
    $.fn.makeAsAutocomplete = function (source) {

        var ctrl = $(this);

        $(ctrl).autocomplete('destroy');
        $(ctrl).unbind('blur');

        if (source == 'destroy')
            return;

        $(ctrl).data('lastValue', $(ctrl).val());

        //We need to create an Id Control if there is none for this control.
        var id = $(ctrl).attr('id');
        var idCtrl = $("#" + id + "Id");
        if ($(idCtrl).length == 0) {
            $('<input type = "hidden" id = ' + id + "Id" + ' />').insertAfter('#' + id);
            $('#' + id + "Id").val($(ctrl).val());
        }

        $(ctrl).autocomplete({
            delay: 40,
            select: function (event, ui) {
                $(this).val(ui.item.label);
                var id = $(this).attr("id") + "Id";
                $("#" + id).val(ui.item.value);
                return false;
            },
            focus: function (event, ui) {
                $(this).val(ui.item.label);
                return false;
            },
            source: function (request, response) {
                var arr = null;
                if ($.trim(request.term).length == 0) {
                    arr = source;
                }
                else {
                    arr = $.grep(source, function (obj) {
                        return (obj.substr(0,
                            $.trim(request.term).length).toLowerCase() == $.trim(request.term.toLowerCase())) ||
                            (obj.ExtRefCode != null &&
                            obj.ExtRefCode.substr(0, request.term.length).toLowerCase() == $.trim(request.term.toLowerCase()))
                    });
                }

                if (arr.length > 50)
                    arr = $(arr).slice(0, 50);

                if (arr.length == 1) {
                    var id = $(ctrl).attr("id");
                    $("#" + id).val(arr[0].CodeId);
                    $(ctrl).val(_getCaption(arr[0]));
                    $(ctrl).autocomplete("close");
                }
                else {
                    response(
                        $.map(arr, function (item) {
                            return { label: _getCaption(item), value: item
                            };
                        }));
                }
            }
        });

        _getCaption = function (item) {
            return item;
        }

        /*
        //show autocmplete when fous in
        //
        $(ctrl).focus(function () {
        $(ctrl).autocomplete("widget").show();
        });
        */

        $(ctrl).bind("itemSelected", function () {
            onBlur(ctrl);
        });

        $(ctrl).keydown(function (event) {

            //backspace, tab, del, end, home, right, down, left, up
            var allowedKeys = [8, 9, 46, 35, 36, 37, 38, 39, 40];
            var isReadOnly = $(this).attr('readonly') == "readonly" ? true : false;

            var id = $(this).attr('id');
            var codeId = $("#" + id + "Id").val();

            var hasValue = (codeId != 0 && codeId != "");
            if (hasValue == true && $.inArray(event.keyCode, allowedKeys) === -1) {
                event.preventDefault();
                return;
            }
            //if key is backspace or delete, clear the contents if not readonly.
            if ((event.keyCode == 8 || event.keyCode == 46) && hasValue == true && !isReadOnly) {

                $(this).val("");
                var id = $(this).attr('id');
                $("#" + id + "Id").val(null);
                event.preventDefault();
            }
        });

        $(ctrl).blur(function () {
            onBlur(this);
        });

        function onBlur(ctrl) {

            var term = $(ctrl).val();
            var record = $.grep(source, function (obj) {
                return _getCaption(obj).toLowerCase() == term.toLowerCase() || (obj.ExtRefCode != null && obj.ExtRefCode.toLowerCase() == term.toLowerCase())
            });

            if (record.length == 0) {
                record = $.grep(source, function (obj) {
                    return obj.toLowerCase() == term.toLowerCase() || (obj.ExtRefCode != null && obj.ExtRefCode.toLowerCase() == term.toLowerCase())
                });
            }

            var id = $(ctrl).attr("id") + "Id";
            if (record.length == 0) {
                $(ctrl).val(null);
            }

            if ($(ctrl).data('lastValue') != term) {
                $(ctrl).trigger('onChange');
            }

            $(ctrl).data('lastValue', term);
        }


    }
})(jQuery);
