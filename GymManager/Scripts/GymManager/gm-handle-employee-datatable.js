$(document).ready(function () {

    $("#employees").DataTable({
        ajax: {
            url: "/api/employees",
            dataSrc: ""
        },
        "order": [[0, "asc"]],
        columns: [
            {
                render: function (data, type, full, meta) {
                    return "<abbr title='Show more details'><a href='/Employees/Details/" + full.id + "' >" + full.name + " " + full.surname + "</a></abbr>";
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
})