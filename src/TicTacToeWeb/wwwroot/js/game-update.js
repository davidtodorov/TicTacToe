$(document).ready(function () {
    setInterval(updateGame, 250);
});

function updateGame() {
    var gameId = $('#hiddenGameId').val();

    $.get('/game/status',
        { Id: gameId },
        function (result) {
            if (result.success === true) {
                updateBoard(result.status.board);
                updateStatus(result.status.state);
            } 
        });

    //$.get('/game/index', function(result) {
    //    if (result.success === true) {
    //        var status = result;
    //    }
    //});
}

function updateBoard(board) {
    var cells = $('.game-grid-table td');

    for (var i = 0; i < board.length; i++) {
        var cell = $(cells[i]);
        cell.text(board[i]);
    }
}

function updateStatus(status) {
    var gameStatus = $('#gameStatus h3');

    gameStatus.text = status;
}