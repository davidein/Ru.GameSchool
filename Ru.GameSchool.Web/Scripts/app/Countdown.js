$(document).ready(function () {

    $('.countdown').each(function () {
        var datevalue = $(this).data('countdown');
        var reload = $(this).data('reload');
        var options = {
            date: datevalue,
            htmlTemplate: "%{d}:%{h}:%{m}:%{s}",
            leadingZero: true,
            onComplete: function (e) {
                if (reload == '1')
                    window.location.href = window.location.href;
            }
        }
        $(this).countdown(options);
    });

});