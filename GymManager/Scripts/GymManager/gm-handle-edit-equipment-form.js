$(document).ready(function () {

    getResourcesFromAPIAndInsertInSelect("#areaId", "Gym Area", "areas");
    getResourcesFromAPIAndInsertInSelect("#typeId", "Equipment Type", "types");

    var url = $(location).attr('href');
    var id = url.substring(url.lastIndexOf('/') + 1);

    if (!Number.isInteger(parseInt(id))) 
        window.location.pathname = '/404.html';

        $.ajax({
            type: "GET",
            url: "/api/equipment/" + id,
        })
            .done(function (equipment) {
                $("#serialNumber").val(equipment.serialNumber);
                $("#brand").val(equipment.brand);
                $("#model").val(equipment.model);
                $("#typeId").val(equipment.type.id);
                $("#areaId").val(equipment.area.id);
                var date = new Date(equipment.deliveryDate);
                var dateString = date.getFullYear() + '-'
                    + ('0' + (date.getMonth() + 1)).slice(-2) + '-'
                    + ('0' + date.getDate()).slice(-2);
                $("#deliveryDate").val(dateString);

            })
            .fail(function () {
                window.location.pathname = '/404.html'
            });

    $("#equipmentForm").validate({
        errorPlacement: function ($error, $element) {
            var name = $element.attr("name");
            $("#error" + name).append($error);
        },
        rules: {
            brand: {
                maxlength: 30
            },
            model: {
                maxlength: 30
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
                url: "/api/equipment/" + id,
                method: "PUT",
                data: formData
            })
                .done(function () {
                    toastr.success("Equipment successfully updated.");
                })
                .fail(function () {
                    toastr.error("Unexpected error.");
                });

            return false;
        }
    });
});