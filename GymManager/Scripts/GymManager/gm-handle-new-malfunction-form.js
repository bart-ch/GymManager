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
        ignore: [],
        rules: {
            title: {
                maxlength: 50
            },
            description: {
                maxlength: 255
            }
        },
        messages: {
            equipmentId: {
                required: "Select the equipment."
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

    var table = $("#equipment").DataTable({
        ajax: {
            url: "/api/equipment",
            dataSrc: ""
        },
        columns: [
            {
                data: "serialNumber",
            }
        ]
    });

    $('#equipment tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            var equipmentId = table.row(this).data().id;
            $("#equipmentId").val(equipmentId);
        }
    });



})