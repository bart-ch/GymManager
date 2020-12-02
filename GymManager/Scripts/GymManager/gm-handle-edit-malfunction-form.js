$(document).ready(function () {
    var url = $(location).attr('href');
    var id = url.substring(url.lastIndexOf('/') + 1);
    if (!Number.isInteger(parseInt(id)))
        window.location.pathname = '/404.html';

    $.ajax({
        type: "GET",
        url: "/api/malfunctions/" + id,
    })
        .done(function (malfunction) {
            $("#id").val(malfunction.id);
            $("#equipmentSerialNumber").append(malfunction.equipment.serialNumber);
            $("#title").val(malfunction.title);
            $("#description").val(malfunction.description);
            $("#equipmentId").val(malfunction.equipmentId);

            if (malfunction.isRepaired)
                $("#isRepaired").attr("checked", "checked");

            var date = new Date(malfunction.malfunctionDate);
            var dateString = date.getFullYear() + '-'
                + ('0' + (date.getMonth() + 1)).slice(-2) + '-'
                + ('0' + date.getDate()).slice(-2);
            $("#malfunctionDate").val(dateString);
        })
        .fail(function () {
            window.location.pathname = '/404.html';
        });

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
                url: "/api/malfunctions/" + id,
                method: "PUT",
                data: formData
            })
                .done(function () {
                    toastr.success("Malfunction successfully updated.");

                })
                .fail(function () {
                    toastr.error("Unexpected error.");
                });

            return false;
        }
    });
})