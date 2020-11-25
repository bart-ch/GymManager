$(document).ready(function () {
    var table = $("#supplements").DataTable({
        ajax: {
            url: "/api/supplements",
            dataSrc: ""
        },
        "order": [[4, "asc"]],
        columns: [
            {
                data: "brand"
            },
            {
                data: "supplementType.name"
            },
            {
                data: "flavor.name"
            },
            {
                data: "initialAmount"
            },
            {
                data: "currentAmount",
                render: function (data) {
                    if (data <= 100)
                        return "<abbr title='Small amount' class='low-amount'>" + data + "</abbr>";
                    else
                        return data;
                }
            },
            {
                data: "consumedAmount"
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
                    return "<a href='/Supplements/Edit/" + data + "' class='pointer'><i class='fa fa-edit' title='Edit'></i></a>";
                }
            },
            {
                data: "id",
                "orderable": false,
                render: function (data) {
                    return "<a class='pointer js-delete' data-supplement-id=" + data + "><i class='fa fa-trash' title='Delete'></i></a>";
                }
            }
        ]
    });

    $("#supplements").on("click", ".js-delete", function () {
        var button = $(this);

        bootbox.confirm({
            title: 'Delete',
            message: "Are you sure you want to delete this supplement?",
            callback: function (result) {
                if (result) {

                    $.ajax({
                        url: "/api/supplements/" + button.attr("data-supplement-id"),
                        method: "DELETE"
                    })
                        .done(function () {
                            table.row(button.parents("tr")).remove().draw();
                            toastr.success("Supplement successfully deleted.");
                        })
                        .fail(function () {
                            toastr.error("Unexpected error.");
                        });
                }
            }
        });
    });
})