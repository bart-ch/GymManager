$(document).ready(function () {
    getResourcesFromAPIAndInsertInSelect("#supplementTypeId", "Supplement Type", "supplementTypes");
    getResourcesFromAPIAndInsertInSelect("#flavorId", "Flavor", "flavors");

    var url = $(location).attr('href');
    var id = url.substring(url.lastIndexOf('/') + 1);
    if (!Number.isInteger(parseInt(id)))
        window.location.pathname = '/404.html';

    $.ajax({
        type: "GET",
        url: "/api/supplements/" + id,
    })
        .done(function (supplement) {
            $("#brand").val(supplement.brand);
            $("#supplementTypeId").val(supplement.supplementType.id);
            $("#flavorId").val(supplement.flavor.id);
            $("#initialAmount").val(supplement.initialAmount);
            $("#consumedAmount").val(supplement.consumedAmount);
            var date = new Date(supplement.deliveryDate);
            var dateString = date.getFullYear() + '-'
                + ('0' + (date.getMonth() + 1)).slice(-2) + '-'
                + ('0' + date.getDate()).slice(-2);
            $("#deliveryDate").val(dateString);

        })
        .fail(function () {
            window.location.pathname = '/404.html';
        });

    $("#supplementForm").validate({
        errorPlacement: function ($error, $element) {
            var name = $element.attr("name");
            $("#error" + name).append($error);
        },
        rules: {
            brand: {
                maxlength: 30
            },
            consumedAmount: {
                greaterThan: "#initialAmount"
            }
        },
        messages: {
            consumedAmount: {
                greaterThan: "Consumed Amount must be less than Initial Amount."
            }
        },
        submitHandler: function () {
            var formData = $('#supplementForm').serializeArray().reduce(function (obj, item) {
                obj[item.name] = item.value;
                return obj;
            }, {});
            $.ajax({
                url: "/api/supplements/" + id,
                method: "PUT",
                data: formData
            })
                .done(function () {
                    toastr.success("Supplement successfully updated.");
                })
                .fail(function () {
                    toastr.error("Unexpected error.");
                });

            return false;
        }
    });
})