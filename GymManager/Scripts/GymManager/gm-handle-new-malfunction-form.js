$(document).ready(function () {

    function resetForm($form) {
        $form.find('input:text, input:password, input:file, select, textarea').val('');
        $("input[type=date]").val("");
    };

    $('#malfunctionDate').val(getTodaysDate());

    $("#malfunctionForm").validate({
        errorPlacement: function ($error, $element) {
            var name = $element.attr("name");
            $("#error" + name).append($error);
        },
        rules: {
            title: {
                maxlength: 50
            },
            description: {
                maxlength: 255
            }
        },
        submitHandler: function () {

            var formData = $('#malfunctionForm').serializeArray().reduce(function (obj, item) {
                obj[item.name] = item.value;
                return obj;
            }, {});

            $.ajax({
                url: "/api/malfunctions",
                method: "POST",
                data: formData
            })
                .done(function () {
                    toastr.success("Malfunction successfully added.");
                    resetForm($('#malfunctionForm'));
                    $('#malfunctionDate').val(getTodaysDate());

                })
                .fail(function () {
                    toastr.error("Unexpected error.");
                });

            return false;
        }
    });

})