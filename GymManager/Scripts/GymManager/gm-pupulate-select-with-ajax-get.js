function getResourcesFromAPIAndInsertInSelect(selectId, selectPlaceholder, resourceName) {

    var url = $(location).attr('href');
    var id = url.substring(url.lastIndexOf('/') + 1);

    $.ajax({
        type: "GET",
        url: "/api/" + resourceName,
        data: "{}",
        success: function (data) {
            if (id == "New") {
                var dropdownItems = '<option disabled selected value="">-- ' + selectPlaceholder + ' --</option>';
            }
            for (var i = 0; i < data.length; i++) {
                dropdownItems += '<option style="cursor: pointer;" class="dropdown-item" value="' + data[i].id + '">' + data[i].name + '</option>';
            }
            $(selectId).html(dropdownItems);
        }
    });
}