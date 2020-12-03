$(document).ready(function () {

    var table = $("#equipmentOrders").DataTable({
        dom: 'Blfrtip',
        buttons: [
            {
                extend: 'pdfHtml5',
                text: 'Generate PDF',
                className: "btn btn-primary mb-2",
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5,6]
                }
            }
        ],
        ajax: {
            url: "/api/equipmentOrders",
            dataSrc: ""
        },
        "order": [[6, "desc"]],
        columns: [
            {
                data: "id"
            },
            {
                render: function (data, type, full, meta) {
                    return full.user.name + " " + full.user.surname;
                }
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
                data: "quantity"
            },
            {
                data: "desiredDeliveryDate",
                render: function (data) {
                    var date = new Date(data);
                    var dateString = date.getFullYear() + '/'
                        + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
                        + ('0' + date.getDate()).slice(-2);

                    return dateString;
                }
            },
            {
                "orderable": false,
                render: function (data, type, full, meta) {
                    return "<form>"
                        + "<div class='form-group'>"
                        + "<select class='form-control' id='orderSelect" + full.id + "'>"
                        + "<option value='' selected disabled hidden>" + full.orderStatus.name + "</option>"
                        + "<option class='pointer' value='1'>In process</option>"
                        + "<option class='pointer' value='2'>Shipped</option>"
                        + "<option class='pointer' value='3'>Completed</option>"
                        + "</select > "
                        + "</div>"
                        + "<button type='button' data-equipment-order-id='" + full.id + "' class='btn btn-primary btn-block js-change'>Update</button ></form > ";
                }
            },
            {
                data: "id",
                "orderable": false,
                render: function (data) {
                    return "<a href ='/Orders/Equipment/Edit/" + data + "' class='pointer'><i class='fa fa-edit' title='Edit'></i></a>";
                }
            },
            {
                data: "id",
                "orderable": false,
                render: function (data) {
                    return "<a class='pointer js-delete' data-equipment-order-id=" + data + "><i class='fa fa-trash' title='Delete'></i></a>";
                }
            }
        ]
    });

    $("#equipmentOrders").on("click", ".js-change", function () {
        var button = $(this);
        var orderStatusId = $("#orderSelect" + button.attr("data-equipment-order-id")).val();
        if (orderStatusId == null) {
            toastr.warning("Choose a different order status.");
            return 0;
        }

        $.ajax({
            url: "/api/equipmentOrders/" + button.attr("data-equipment-order-id") + "/" + orderStatusId,
            method: "PUT"
        })
            .done(function () {
                toastr.success("Order status successfully changed.");
            })
            .fail(function () {
                toastr.error("Unexpected error.");
            });

    });

    $("#equipmentOrders").on("click", ".js-delete", function () {
        var button = $(this);

        bootbox.confirm({
            title: 'Delete',
            message: "Are you sure you want to delete this order?",
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: "/api/equipmentOrders/" + button.attr("data-equipment-order-id"),
                        method: "DELETE"
                    })
                        .done(function () {
                            table.row(button.parents("tr")).remove().draw();
                            toastr.success("Order successfully deleted.");
                        })
                        .fail(function () {
                            toastr.error("Unexpected error.");
                        });
                }
            }
        });
    });

})