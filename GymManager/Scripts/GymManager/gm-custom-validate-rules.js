$.validator.addMethod("greaterThan",
    function (value, element, param) {
        var $otherElement = $(param);
        return parseInt($otherElement.val(), 10) > parseInt(value, 10);
    });