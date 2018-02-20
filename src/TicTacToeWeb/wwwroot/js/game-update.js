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
}

function updateBoard(board) {
    var cells = $('.game-grid-table td');

    for (var i = 0; i < board.length; i++) {
        var cell = $(cells[i]);
        cell.text(board[i]);
    }
}

function updateStatus(status) {
    var gameStatus = $('h3#gameStatus ');

    if (status === 1) {
        gameStatus.text("Status: Waiting for a second player");
    }
    else if (status === 2) {
        gameStatus.text("Status: X turn");
    }
    else if (status === 3) {
        gameStatus.text("Status: O turn");
    }
    else if (status === 4) {
        gameStatus.text("Status: X won");
    }
    else if (status === 5) {
        gameStatus.text("Status: O won");
    }
    else if (status === 6) {
        gameStatus.text("Status: Draw");
    }
}