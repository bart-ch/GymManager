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
                    render: function (data, type, full, meta) {
                        return "<a href='/Malfunctions/Details/" + full.id + "' >" + full.title + "</a>";
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
})