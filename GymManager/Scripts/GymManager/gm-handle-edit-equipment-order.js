$(document).ready(function () {

    getResourcesFromAPIAndInsertInSelect("#typeId", "Equipment Type", "types");

    var url = $(location).attr('href');
    var id = url.substring(url.lastIndexOf('/') + 1);
    if (!Number.isInteger(parseInt(id)))
        window.location.pathname = '/404.html';

    $.ajax({
        type: "GET",
        url: "/api/equipmentOrders/" + id,
    })
        .done(function (equipmentOrder) {
            $("#brand").val(equipmentOrder.brand);
            $("#model").val(equipmentOrder.model);
            $("#typeId").val(equipmentOrder.type.id);
            $("#quantity").val(equipmentOrder.quantity);
            var date = new Date(equipmentOrder.desiredDeliveryDate);
            var dateString = date.getFullYear() + '-'
                + ('0' + (date.getMonth() + 1)).slice(-2) + '-'
                + ('0' + date.getDate()).slice(-2);
            $("#desiredDeliveryDate").val(dateString);

        })
        .fail(function () {
            window.location.pathname = '/404.html';
        });


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
                url: "/api/equipmentOrders/" + id,
                method: "PUT",
                data: formData
            })
                .done(function () {
                    toastr.success("Equipment order successfully updated.");
                })
                .fail(function () {
                    toastr.error("Unexpected error.");
                });

            return false;
        }
    });
})