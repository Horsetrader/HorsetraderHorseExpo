$(document).ready(function () {

    $('[data-toggle=offcanvas]').click(function () {
        $('.row-offcanvas').toggleClass('active');
    });

    $('[data-toggle=mapoffcanvas]').click(function () {
        $('.row-map-offcanvas').toggleClass('active');
    });

    $('[add-to-list]').click(function () {
        //Assign the order number to variable
        eventOrderNumber = $(this).attr('add-to-list');
        //Assign value to hidden input
        $('#eventOrderNumber').val(eventOrderNumber);
        //The hidden input's value will be handled in code behind
        $('#btnAddToList').click();
    });

    //If user presses 'Enter' while on Search
    //input, then fire btnSearch's click event
    $('input[id$=tbxSearch]').keydown(function (e) {
        if (e.keyCode == 13) {
            $('input[id$=btnSearch]').click();
            return false;
        }
    });

    $('a.modalButton').on('click', function (e) {
        var title = $(this).attr('data-title');
        var src = $(this).attr('data-src');
        var height = $(this).attr('data-height') || 300;
        var width = $(this).attr('data-width') || 400;

        $('#emailModalLabel').html(title);

        $("#emailModal iframe").attr({ 'src': src,
            'height': height,
            'width': width
        });
    });

    //Validate Booth Booster in Exhibitor list
    $('[booth-booster=False]').click(function () {
        alert('Sorry...This exhibitor doesn\'t have a BoothBooster');
    });

    //Stop YouTube videos on modal closing
    $('#emailModal button').click(function () {
        $('#emailModal iframe').removeAttr('src');
    });
});