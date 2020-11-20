$(document).ready(function () {

    var table = $("#flavors").DataTable({
        ajax: {
            url: "/api/flavors",
            dataSrc: ""
        },
        columns: [
            {
                data: "name"
            },
            {
                data: "id",
                "orderable": false,
                render: function (data) {
                    return "<a class='js-edit pointer' data-flavor-id=" + data + " data-toggle='modal' data-target='#editFlavorModal'><i class='fa fa-edit' title='Edit'></i></a>";
                }
            },
            {
                data: "id",
                "orderable": false,
                render: function (data) {
                    return "<a class='pointer js-delete' data-flavor-id=" + data + "><i class='fa fa-trash'  title='Delete'></i></a>";
                }
            }
        ]
    });


    $("#flavors").on("click", ".js-delete", function () {
        var button = $(this);


        bootbox.confirm({
            title: 'Delete',
            message: "Are you sure you want to delete this? Every supplement of the flavor will change its flavor to 'Other'",
            callback: function (result) {
                if (result) {

                    $.ajax({
                        url: "/api/flavors/" + button.attr("data-flavor-id"),
                        method: "DELETE"
                    })
                        .done(function () {
                            table.row(button.parents("tr")).remove().draw();
                            toastr.success("Flavor successfully deleted.");
                        })
                        .fail(function () {
                            toastr.error("You cannot delete 'Other'");
                        });
                }
            }
        });
    });

    $("#flavors").on("click", ".js-edit", function () {
        var button = $(this);


        $.ajax({
            url: "/api/flavors/" + button.attr("data-flavor-id"),
            method: "GET"
        })
            .done(function (data) {
                $("#flavor").val(data.name);
                $("#flavorId").val(data.id);
            })
            .fail(function () {
                toastr.error("Unexpected error.");
            });
    });

    $("#editFlavorForm").validate({
        errorPlacement: function ($error, $element) {
            var name = $element.attr("name");
            $("#error" + name).append($error);
        },
        rules: {
            editName: {
                maxlength: 30,
            }
        },
        submitHandler: function () {
            $("#flavor").attr('name', 'name');
            var formData = $('#editFlavorForm').serializeArray().reduce(function (obj, item) {
                obj[item.name] = item.value;
                return obj;
            }, {});
            $.ajax({
                url: "/api/flavors/" + $("#flavorId").val(),
                method: "PUT",
                data: formData
            })
                .done(function () {
                    toastr.options.onHidden = function () { window.location.replace("/Flavors") }
                    toastr.options.timeOut = 1300;
                    toastr.success("Flavor successfully updated.");
                })
                .fail(function () {
                    toastr.error("You cannot add flavor which already exists or edit 'Other'");
                    $("#flavor").attr('name', 'editName');
                });
            return false;
        }
    });

    $("#flavorForm").validate({
        errorPlacement: function ($error, $element) {
            var name = $element.attr("name");
            $("#error" + name).append($error);
        },
        rules: {
            name: {
                maxlength: 30,
            }
        },
        submitHandler: function () {
            var formData = $('#flavorForm').serializeArray().reduce(function (obj, item) {
                obj[item.name] = item.value;
                return obj;
            }, {});
            $.ajax({

                url: "/api/flavors",
                method: "POST",
                data: formData
            })
                .done(function () {
                    toastr.options.onHidden = function () { window.location.replace("/Flavors") }
                    toastr.options.timeOut = 1300;
                    toastr.success("Flavor successfully added.");
                })
                .fail(function () {
                    toastr.error("The flavor already exists.");
                });

            return false;
        }
    });
})