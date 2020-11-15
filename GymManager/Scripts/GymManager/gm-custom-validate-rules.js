$.validator.addMethod("greaterThan",
    function (value, element, param) {
        var $otherElement = $(param);
        if (value == "") {
            return true;
        }
        return parseInt($otherElement.val(), 10) > parseInt(value, 10);
    });