$(document).ready(function () {
    var url = $(location).attr('href');
    var id = url.substring(url.lastIndexOf('/') + 1);
    $.ajax({
        type: "GET",
        url: "/api/malfunctions/" + id,
    })
        .done(function (malfunction) {
            $("#id").append(malfunction.id);
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
            window.location.pathname = '/Malfunctions'
        });
})