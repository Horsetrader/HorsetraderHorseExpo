$(document).ready(function () {

    /*****************
    Page load
    ******************/
    
    $(".exampleyudu").colorbox({ iframe: true, innerWidth: 800, innerHeight: 600 });
    $(".examplelogin").colorbox({ iframe: true, innerWidth: 600, innerHeight: 600 });
    $(".exampleemail").colorbox({ iframe: true, innerWidth: 495, innerHeight: 425 });
    $(".example55").colorbox({ iframe: true, innerWidth: 620, innerHeight: 420 });
    $(".example5").colorbox();
    $(".popupRefundInfo").colorbox({ innerWidth: 525 });
    $('input[id$=tbxSearch]').watermark('Type here and search what you\'re looking for...');

    /*****************
    Page Functionality
    ******************/

    //If user presses 'Enter' while on FastAd
    //input, then fire btnGoToAd's click event
    $('input[id$=txtFastAd]').keydown(function (e) {
        if (e.keyCode == 13) {
            $('input[id$=btnGoAd]').click();
            return false;
        }
    });

    //If user presses 'Enter' while on Search
    //input, then fire btnSearch's click event
    $('input[id$=tbxSearch]').keydown(function (e) {
        if (e.keyCode == 13) {
            $('input[id$=btnSearch]').click();
            return false;
        }
    });

});