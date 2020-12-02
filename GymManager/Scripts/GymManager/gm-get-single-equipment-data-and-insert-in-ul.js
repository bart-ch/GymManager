$(document).ready(function () {
    var url = $(location).attr('href');
    var id = url.substring(url.lastIndexOf('/') + 1);

    $.ajax({
        type: "GET",
        url: "/api/equipment/" + id,
    })
        .done(function (equipment) {
            $("#serialNumber").append(equipment.serialNumber);
            $("#brand").append(equipment.brand);
            $("#model").append(equipment.model);
            $("#type").append(equipment.type.name);
            $("#area").append(equipment.area.name);
            var date = new Date(equipment.deliveryDate);
            var dateString = date.getFullYear() + '-'
                + ('0' + (date.getMonth() + 1)).slice(-2) + '-'
                + ('0' + date.getDate()).slice(-2);
            $("#deliveryDate").append(dateString);
            $("#equipmentId").val(equipment.id);
        })
        .fail(function () {
            window.location.pathname = '/404.html';
        });
})