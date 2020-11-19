$(document).ready(function () {
    var table = $("#equipment").DataTable({
        ajax: {
            url: "/api/malfunctions",
            dataSrc: ""
        },
        "order": [[3, "asc"]],
        columns: [
            {
                data: "id",                   
                render: function (data) {
                    return "<a href='/Malfunctions/Details/" + data + "' >" + data + "</a>";
                }
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
})