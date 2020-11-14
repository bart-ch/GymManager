$(document).ready(function () {
    getResourcesFromAPIAndInsertInSelect("#supplementTypeId", "Supplement Type", "supplementTypes");
    getResourcesFromAPIAndInsertInSelect("#flavorId", "Flavor", "flavors");

    $('#deliveryDate').val(getTodaysDate());

    function resetForm($form) {
        $form.find('input:text, input:password, input:file, select, textarea').val('');
        $("input[type=date]").val("");
        $("input[type=number]").val("");
    };

    $("#supplementForm").validate({
        errorPlacement: function ($error, $element) {
            var name = $element.attr("name");
            $("#error" + name).append($error);
        },
        rules: {
            brand: {
                maxlength: 30
            },
            consumedAmount: {
                greaterThan: "#initialAmount"
            }
        },
        messages: {
            consumedAmount: {
                greaterThan: "Consumed Amount must be less than Initial Amount."
            }
        },
        submitHandler: function () {
            var formData = $('#supplementForm').serializeArray().reduce(function (obj, item) {
                obj[item.name] = item.value;
                return obj;
            }, {});
            $.ajax({

                url: "/api/supplements",
                method: "POST",
                data: formData
            })
                .done(function () {
                    toastr.success("Supplement successfully added.");
                    resetForm($('#supplementForm'));
                    $('#deliveryDate').val(getTodaysDate());

                })
                .fail(function () {
                    toastr.error("Unexpected error.");
                });

            return false;
        }
    });
})