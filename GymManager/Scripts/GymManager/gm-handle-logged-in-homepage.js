$(function () {

    $.ajax({
        type: "GET",
        url: "/api/employees/loggedIn",
    })
        .done(function (employee) {
            $("#homePageMessage").append(employee.name + " ");
            $("#homePageMessage").append(employee.surname + "!");
        })
        .fail(function () {
            toastr.error("Unexpected error.")
        });
})