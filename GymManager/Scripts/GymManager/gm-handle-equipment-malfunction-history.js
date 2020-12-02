$(document).ready(function () {
    var url = $(location).attr('href');
    var id = url.substring(url.lastIndexOf('/') + 1);
    if (id > 0) {
        var table = $("#malfunctionHistory").DataTable({
            ajax: {
                url: "/api/equipment/" + id + "/malfunctions",
                dataSrc: ""
            },
            "order": [[2, "asc"]],
            columns: [
                {
                    data: "title"
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
                {
                    data: "malfunctionDate",
                    render: function (data) {
                        var date = new Date(data);
                        var dateString = date.getFullYear() + '/'
                            + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
                            + ('0' + date.getDate()).slice(-2);

                        return dateString;
                    }
                }
            ]
        });
    }

    table.on('click', 'tbody > tr > td', function () {
        var malfunctionId = table.row(this).data().id;
        window.location = "/Malfunctions/Details/" + malfunctionId;
    });

    $('#malfunctionHistory tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');

        }
    });
})