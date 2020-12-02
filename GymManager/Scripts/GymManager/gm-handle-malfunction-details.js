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
            $("#id").append(malfunction.id);
            $("#deleteButton").attr("data-malfunction-id", malfunction.id);
            $("#equipmentSerialNumber").append(malfunction.equipment.serialNumber);
            $("#title").append(malfunction.title);
            $("#description").append(malfunction.description);

            if (malfunction.isRepaired)
                $("#isRepaired").append("Yes");
            else
                $("#isRepaired").append("No");

            var date = new Date(malfunction.malfunctionDate);
            var dateString = date.getFullYear() + '-'
                + ('0' + (date.getMonth() + 1)).slice(-2) + '-'
                + ('0' + date.getDate()).slice(-2);
            $("#malfunctionDate").append(dateString);

            $("#editButton").attr("href", "/Malfunctions/Edit/" + malfunction.id);
        })
        .fail(function () {
            window.location.pathname = '/404.html';
        });

    $("#deleteButton").on("click", function () {
        var button = $(this);

        bootbox.confirm({
            title: 'Delete',
            message: "Are you sure you want to delete this malfunction?",
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: "/api/malfunctions/" + button.attr("data-malfunction-id"),
                        method: "DELETE"
                    })
                        .done(function () {
                            window.location.replace("/Malfunctions");
                            toastr.success("Malfunction successfully deleted.");
                        })
                        .fail(function () {
                            toastr.error("Unexpected error.");
                        });
                }
            }
        });
    })
})