$(document).ready(function () {
    $('.joinGameButton').on('click', function () {
        var that = $(this);
        var gameId = that.attr('game-id');

        // TODO: 1) Check for success status -> redirect to play game view

        // TODO: 2) Check for error -> try to install toastr.js via bower.js and show error

        // TODO: 3) Add __RequestVerificationToken field to the request body

        $.post("/game/join", { GameId: gameId }, function (result) {
            if (result.success === true) {
                window.location.replace("/game/play/" + gameId);
                
            }
            
        });
    });
});