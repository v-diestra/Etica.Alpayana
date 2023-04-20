$(function () {

    $('.number').on('keypress', function (event) {
        var keycode = event.which;
        if (keycode < 48 || keycode > 57) {
            event.preventDefault();
        }
    });
})
var CURRENT_CONTROLLER_URL = window.location.origin + window.location.pathname.substring(0, window.location.pathname.lastIndexOf("/") + 1);