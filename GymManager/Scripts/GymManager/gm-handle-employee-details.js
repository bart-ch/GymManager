$(document).ready(function () {
    var url = $(location).attr('href');
    var id = url.substring(url.lastIndexOf('/') + 1);
    $.ajax({
        type: "GET",
        url: "/api/employees/" + id,
    })
        .done(function (employee) {
            $("#name").append(employee.name);
            $("#surname").append(employee.surname);
            $("#email").append(employee.email);
            $("#jobTitle").append(employee.jobTitle);
            $("#deleteButton").attr("data-employee-id", employee.id);
            $("#editButton").attr("href", "/Employees/Edit/" + employee.id);
        })
        .fail(function () {
            window.location.pathname = '/404.html';
        });

    $("#deleteButton").on("click", function () {
        var button = $(this);

        bootbox.confirm({
            title: 'Delete',
            message: "Are you sure you want to delete this employee?",
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: "/api/employees/" + button.attr("data-employee-id"),
                        method: "DELETE"
                    })
                        .done(function () {
                            window.location.replace("/Employees");
                            toastr.success("Employee successfully deleted.");
                        })
                        .fail(function () {
                            toastr.error("You cannot delete your own account.");
                        });
                }
            }
        });
    })
})