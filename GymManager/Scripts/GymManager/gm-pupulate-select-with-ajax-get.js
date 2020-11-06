function getResourcesFromAPIAndInsertInSelect(selectId, selectPlaceholder, resourceName) {
    $.ajax({
        type: "GET",
        url: "/api/" + resourceName,
        data: "{}",
        success: function (data) {
            var dropdownItems = '<option disabled selected value="">-- ' + selectPlaceholder + ' --</option>';
            for (var i = 0; i < data.length; i++) {
                dropdownItems += '<option style="cursor: pointer;" class="dropdown-item" value="' + data[i].id + '">' + data[i].name + '</option>';
            }
            $(selectId).html(dropdownItems);
        }
    });
}