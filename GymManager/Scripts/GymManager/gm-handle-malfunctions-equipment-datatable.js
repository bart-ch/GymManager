$(document).ready(function () {
    var table = $("#equipment").DataTable({
        ajax: {
            url: "/api/equipment",
            dataSrc: ""
        },
        "order": [[1, "asc"]],
        columns: [
            {
                data: "serialNumber"
            },
            {
                data: "isOperational",
                render: function (data) {
                    if (data)
                        return "Yes";
                    else
                        return "No";
                }
            },
            {
                data: "id",
                "orderable": false,
                render: function (data) {
                    return "<a href ='/Malfunctions/New/" + data + "' class='pointer'><i class='fa fa-exclamation-triangle' title='Report a malfunction'></i></a>";
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