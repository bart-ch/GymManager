$(document).ready(function () {
    var url = $(location).attr('href');
    var id = url.substring(url.lastIndexOf('/') + 1);
    $.ajax({
        type: "GET",
        url: "/api/employees/" + id,
    })
        .done(function (employee) {
            $("#name").val(employee.name);
            $("#surname").val(employee.surname);
            $("#email").val(employee.email);
            $("#jobTitle").val(employee.jobTitle);
            $("#deleteButton").attr("data-employee-id", employee.id);
            $("#editButton").attr("href", "/Employees/Edit/" + employee.id);
        })
        .fail(function () {
            window.location.pathname = '/Employees'
        });
})