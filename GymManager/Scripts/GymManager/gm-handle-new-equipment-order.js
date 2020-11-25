$(document).ready(function () {
    getResourcesFromAPIAndInsertInSelect("#typeId", "Equipment Type", "types");
    $('#desiredDeliveryDate').val(getTodaysDate());

    function resetForm($form) {
        $form.find('input:text, input:password, input:file, select, textarea').val('');
        $("input[type=date]").val("");
        $("input[type=number]").val("");
    };

    $("#equipmentOrderForm").validate({
        errorPlacement: function ($error, $element) {
            var name = $element.attr("name");
            $("#error" + name).append($error);
        },
        rules: {
            brand: {
                maxlength: 30,
            },
            model: {
                maxlength: 30,
            }
        },
        submitHandler: function () {
            var formData = $('#equipmentOrderForm').serializeArray().reduce(function (obj, item) {
                obj[item.name] = item.value;
                return obj;
            }, {});
            $.ajax({

                url: "/api/equipmentOrders",
                method: "POST",
                data: formData
            })
                .done(function () {
                    toastr.success("Equipment order successfully added.");
                    resetForm($('#equipmentOrderForm'));
                    $('#desiredDeliveryDate').val(getTodaysDate());

                })
                .fail(function () {
                    toastr.error("Unexpected error.");
                });

            return false;
        }
    });
})