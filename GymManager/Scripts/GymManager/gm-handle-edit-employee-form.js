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
            window.location.pathname = '/404.html';
        });

    $("#employeeForm").validate({
        errorPlacement: function ($error, $element) {
            var name = $element.attr("name");
            $("#error" + name).append($error);
        },
        rules: {
            name: {
                maxlength: 255
            },
            surname: {
                maxlength: 255
            },
            jobTitle: {
                maxlength: 255
            },
            email: {
                email: true
            }
        },
        submitHandler: function () {
            var formData = $('#employeeForm').serializeArray().reduce(function (obj, item) {
                obj[item.name] = item.value;
                return obj;
            }, {});
            $.ajax({
                url: "/api/employees/" + id,
                method: "PUT",
                data: formData
            })
                .done(function () {
                    toastr.success("Profile data successfully updated.");
                })
                .fail(function () {
                    toastr.error("Unexpected error.");
                });

            return false;
        }
    });
})