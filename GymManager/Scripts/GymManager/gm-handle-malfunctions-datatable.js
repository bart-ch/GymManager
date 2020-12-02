$(document).ready(function () {
    var table = $("#malfuinctions").DataTable({
        ajax: {
            url: "/api/malfunctions",
            dataSrc: ""
        },
        "order": [[3, "asc"]],
        columns: [
            {
                data: "id"
            },
            {
                data: "title"
            },

            {
                data: "equipment.serialNumber"
            },

            {
                data: "malfunctionDate",
                render: function (data) {
                    var date = new Date(data);
                    var dateString = date.getFullYear() + '/'
                        + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
                        + ('0' + date.getDate()).slice(-2);

                    return dateString;
                }
            },
            {
                data: "isRepaired",
                render: function (data) {
                    if (data)
                        return "Yes";
                    else
                        return "<span class='non-operational'> No </span>";
                }
            },
        ]
    });

    table.on('click', 'tbody > tr > td', function () {
        var malfunctionId = table.row(this).data().id;
        window.location = "Malfunctions/Details/" + malfunctionId;
    });

    $('#malfuinctions tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');

        }
    });
})