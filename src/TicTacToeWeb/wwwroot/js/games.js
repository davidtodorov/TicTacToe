$(document).ready(function () {
    $('.joinGameButton').on('click', function () {
        var that = $(this);
        var gameId = that.attr('game-id');
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.post("/game/join", { GameId: gameId, __RequestVerificationToken: token}, function (result) {
            if (result.success === true) {
                window.location.replace("/game/play/" + gameId);

            } else {
                toastr["error"](result.exception, "Error");
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": true,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": true,
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
            }
        });
    });


    // TODO: on change enum = protected, display:none
    function myFunction() {
        $('.visibility-type').on('change',
            function() {
                

            });
    }
});
