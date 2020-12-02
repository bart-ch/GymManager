$(document).ready(function () {
    var table = $("#equipment").DataTable({
        ajax: {
            url: "/api/equipment",
            dataSrc: ""
        },
        "order": [[1, "asc"]],
        columns: [
            {
                render: function (data, type, full, meta) {
                    if (!full.isOperational) {
                        return "<span class='non-operational'>" + full.serialNumber + "</span>";
                    }
                    return full.serialNumber;
                }
            },
            {
                data: "isOperational",
                render: function (data) {
                    if (data)
                        return "Yes";
                    else
                        return "<abbr title='The equipment has at least one unrepaired malfunction' class='non-operational'> No </abbr>";
                }
            },
            {
                data: "id",
                "orderable": false,
                render: function (data) {
                    return "<a href ='/Malfunctions/History/" + data + "' class='pointer'><i class='fa fa-history' title='Malfunction history of the equipment'></i></a>";
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