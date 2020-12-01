$(document).ready(function () {

    var table = $("#employees").DataTable({
        ajax: {
            url: "/api/employees",
            dataSrc: ""
        },
        "order": [[0, "asc"]],
        columns: [
            {
                render: function (data, type, full, meta) {
                    return full.name + " " + full.surname;
                //    return "<abbr title='Show more details'><a href='/Employees/Details/" + full.id + "' >" + full.name + " " + full.surname + "</a></abbr>";
                }
            },
            {
                data: "email"
            },
            {
                data: "jobTitle"
            }
        ]
    });

    table.on('click', 'tbody > tr > td', function () {
        var employeeId = table.row(this).data().id;
        window.location = "Employees/Details/" + employeeId;
    });

    $('#employees tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');

        }
    });
})