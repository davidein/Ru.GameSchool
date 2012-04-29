$(document).ready(function () {
    $('.dropdown-toggle').dropdown()

    // Popover 
    $('#registerHere input').hover(function () {
        $(this).popover('show')
    });

});