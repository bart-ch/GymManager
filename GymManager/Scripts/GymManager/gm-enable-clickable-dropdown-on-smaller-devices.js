$(function () {

    if ($(window).width() < 992) {
        $(".dropdown-toggle").attr("data-toggle", "dropdown");
    }
    else {
        $(".dropdown-toggle").removeAttr("data-toggle");
    }

    function adjustSize() {
        if ($(window).width() < 992) {
            $(".dropdown-toggle").attr("data-toggle", "dropdown");
        }
        else {
            $(".dropdown-toggle").removeAttr("data-toggle");
        };
    };

    $(window).on('resize', adjustSize);

})

