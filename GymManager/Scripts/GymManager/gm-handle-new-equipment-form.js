$(document).ready(function () {

    getResourcesFromAPIAndInsertInSelect("#areaId", "Gym Area", "areas");
    getResourcesFromAPIAndInsertInSelect("#typeId", "Equipment Type", "types");

    function resetForm($form) {
        $form.find('input:text, input:password, input:file, select, textarea').val('');
        $("input[type=date]").val("");
    };

    $('#deliveryDate').val(getTodaysDate());

    $("#equipmentForm").validate({
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
            },
            serialNumber: {
                maxlength: 30
            }
        },
        submitHandler: function () {
            var formData = $('#equipmentForm').serializeArray().reduce(function (obj, item) {
                obj[item.name] = item.value;
                return obj;
            }, {});
            $.ajax({

                url: "/api/equipment",
                method: "POST",
                data: formData
            })
                .done(function () {
                    toastr.success("Equipment successfully added.");
                    resetForm($('#equipmentForm'));
                    $('#deliveryDate').val(getTodaysDate());

                })
                .fail(function () {
                    toastr.error("Unexpected error.");
                });

            return false;
        }
    });

});