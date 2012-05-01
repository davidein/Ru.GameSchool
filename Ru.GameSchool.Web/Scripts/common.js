$(document).ready(function () {

    $('.dropdown-toggle').dropdown()

    // Popover 
    $('#registerHere input').hover(function () {
        $(this).popover('show')
    });
    // Open bootstrap modal windows with remote content - needs a partial view with bootstrap modal markup
    $("a[data-toggle=modal]").click(function () {
        var target, url;
        target = $(this).attr('data-target');
        url = $(this).attr('href');
        return $(target).load(url);
    });
    $('#vid-list li a').hover(function () {
        $(this).tooltip('show')
    });
});



/* Update datepicker plugin so that DD/MM/YYYY format is used. */
$.extend($.fn.datepicker.defaults, {
    parse: function (string) {
        var matches;
        if ((matches = string.match(/^(\d{2,2})\/(\d{2,2})\/(\d{4,4})$/))) {
            return new Date(matches[3], matches[1] - 1, matches[2]);
        } else {
            return null;
        }
    },
    format: function (date) {
        var 
            month = (date.getMonth() + 1).toString(),
            dom = date.getDate().toString();
        if (month.length === 1) {
            month = "0" + month;
        }
        if (dom.length === 1) {
            dom = "0" + dom;
        }
        return dom + "/" + month + "/" + date.getFullYear();
    }

});
