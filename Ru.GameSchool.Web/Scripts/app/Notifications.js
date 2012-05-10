$(function () {
    var markup = '<li {{if !IsRead}} class="unread"{{/if}} ><a href="${Url}">${Description}</a></li>';

    var get = function () {
        $.ajax({
            type: "POST",
            url: "/Json/GetNotifications/",
            success: function (data) {
                $('.notificationdropdown').empty();
                if (data.length > 0) {
                    var counter = newItems(data);
                    if (counter > 0) {
                        $('#notify').addClass('btn-danger');
                    } else {
                        $('#notify').removeClass('btn-danger');
                    }
                    $('#notify .count').text(counter);
                    $.tmpl(markup, data).appendTo('.notificationdropdown');
                }
                else
                    $('#notify').removeClass('btn-danger');
                timer();
            }
        });
    };

    var newItems = function (list) {
        var counter = 0;
        for (var i = 0; i < list.length; i++) {
            if (!list[i].IsRead)
                counter++;
        }
        return counter;
    };

    var timer = function () {
        setTimeout(get, 1000 * 60);
    };

    jQuery.fn.NotificationRunner = function () {
        var data = $('body').data('user');
        if (data.auth == 'true') {
            get();
        }
    };
});

$(document).ready(function () {
    $.fn.NotificationRunner();

    $("#notify").click(function (i, v) {
        $.ajax({
            type: "POST",
            url: "/Json/ClearNotifications/",
            success: function (data) {
                $('#notify .count').text("0");
                $('#notify').removeClass('btn-danger');
            }
        });
    });
});