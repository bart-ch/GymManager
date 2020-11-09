$(document).ready(function () {

    getResourcesFromAPIAndInsertInSelect("#areaId", "Gym Area", "areas");
    getResourcesFromAPIAndInsertInSelect("#typeId", "Equipment Type", "types");

    function getTodaysDate() {
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        return today = now.getFullYear() + "-" + (month) + "-" + (day);
    }

    function resetForm($form) {
        $form.find('input:text, input:password, input:file, select, textarea').val('');
        $("input[type=date]").val("");
    };

    $('#purchaseDate').val(getTodaysDate());

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
                    $('#purchaseDate').val(getTodaysDate());

                })
                .fail(function () {
                    toastr.error("Unexpected error.");
                });

            return false;
        }
    });

});