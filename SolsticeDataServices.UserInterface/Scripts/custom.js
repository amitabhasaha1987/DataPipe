$(document).ready(function () {
    $('.bxslider').bxSlider({
        auto: true,
    });

    $('.slider1').bxSlider({
        auto: true,
    });

    $('.slider2').bxSlider({
        auto: true,
    });

    //select all the a tag with name equal to modal
    $('a[name=modal]').click(function (e) {
        //Cancel the link behavior
        e.preventDefault();

        //Get the A tag
        var id = $(this).attr('href');

        //Get the screen height and width
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();

        //Set heigth and width to mask to fill up the whole screen
        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

        //transition effect		
        $('#mask').fadeIn(1000);
        $('#mask').fadeTo("slow", 0.8);

        //Get the window height and width
        var winH = $(window).height();
        var winW = $(window).width();

        //Set the popup window to center
        $(id).css('top', winH / 2 - $(id).height() / 2);
        $(id).css('left', winW / 2 - $(id).width() / 2);

        //transition effect
        $(id).fadeIn(2000);

    });

    //if close button is clicked
    $('.window .closebtn').click(function (e) {
        //Cancel the link behavior
        e.preventDefault();

        $('#mask').hide();
        $('.window').hide();
    });

    //if mask is clicked
    $('#mask').click(function () {
        $(this).hide();
        $('.window').hide();
    });


});

$(function () {
    $("#example-form").submit(function () {
        var inputVal = $(".simulator-input, .simulator-input2, .simulator-textarea .btn-group").val();
        $(document).trigger("clear-alert-id.example");
        if (inputVal.length < 3) {
            $(document).trigger("set-alert-id-example", [
              {
                  message: "This field is required",
                  priority: "error"
              },
              /*{
                message: "This is an info alert",
                priority: "info"
              }*/
            ]);
        }

        return false;
    });



    //$("#login-example-form").submit(function () {
    //    debugger;
    //    var inputVal = $(".popinputbox").val();
    //    $(document).trigger("clear-alert-id.example1");
    //    if (inputVal.length == 0) {
    //        $(document).trigger("set-alert-id-example1", [
    //            {
    //                message: "This field is required",
    //                priority: "error"
    //            },
    //        ]);
    //    }
    //    return false;
    //});


    //$("#login-example-form").submit(function () {
    //    debugger;
    //    var inputUVal = $("#txtemail").val();
    //    $(document).trigger("clear-alert-id.example1");
    //    if (inputUVal.length == 0) {
    //        $(document).trigger("set-alert-id-example1", [
    //            {
    //                message: "This field is required",
    //                priority: "error"
    //            },
    //        ]);
    //    }

    //    var inputEVal = $("#txtpassword").val();
    //    $(document).trigger("clear-alert-id.example2");
    //    if (inputEVal.length == 0) {
    //        $(document).trigger("set-alert-id-example2", [
    //            {
    //                message: "This field is required",
    //                priority: "error"
    //            },
    //        ]);
    //    }

    //    $.ajax(
    //        {
    //            type: "get",
    //            url: "/Index/Login",
    //            data: { "EmailId": inputUVal, "Password": inputEVal },
    //            success: function (data) {
    //                window.location.href = "/Dashboard/Index";
    //            }
    //        });
    //    return false;
    //});

});
