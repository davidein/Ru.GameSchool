$(document).ready(function () {

    $('.countdown').each(function () {
        var datevalue = $(this).data('countdown');
        var options = {
            date: datevalue,
            htmlTemplate: "%{d}:%{h}:%{m}:%{s}",
            leadingZero: true,
            onComplete: function (e) {
                window.location.href = window.location.href;
            }
        }
        $(this).countdown(options);
    });

});