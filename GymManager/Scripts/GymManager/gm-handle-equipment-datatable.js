$(document).ready(function () {
    var table = $("#equipment").DataTable({
        ajax: {
            url: "/api/equipment",
            dataSrc: ""
        },
        columns: [
            {
                data: "serialNumber"
            },
            {
                data: "brand"
            },
            {
                data: "model"
            },
            {
                data: "type.name"
            },
            {
                data: "area.name"
            },
            {
                data: "deliveryDate",
                render: function (data) {
                    var date = new Date(data);
                    var dateString = date.getFullYear() + '/'
                        + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
                        + ('0' + date.getDate()).slice(-2);

                    return dateString;
                }
            },
            {
                data: "id",
                "orderable": false,
                render: function (data) {
                    return "<a href ='/Equipment/Edit/" + data + "' class='pointer'><i class='fa fa-edit' title='Edit'></i></a>";
                }
            },
            {
                data: "id",
                "orderable": false,
                render: function (data) {
                    return "<a class='pointer js-delete' data-equipment-id=" + data + "><i class='fa fa-trash' title='Delete'></i></a>";
                }
            }
        ]
    });

    $("#equipment").on("click", ".js-delete", function () {
        var button = $(this);

        bootbox.confirm({
            title: 'Delete',
            message: "Are you sure you want to delete this equipment?",
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: "/api/equipment/" + button.attr("data-equipment-id"),
                        method: "DELETE"
                    })
                        .done(function () {
                            table.row(button.parents("tr")).remove().draw();
                            toastr.success("Equipment successfully deleted.");
                        })
                        .fail(function () {
                            toastr.error("Unexpected error.");
                        });
                }
            }
        });
    });
})