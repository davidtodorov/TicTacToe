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
                updateStatus(result.status);
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

    if (status.state !== 1)
    {
        var players = $('h3#pvsp');
        players.text(status.creatorUsername + "[X] vs " + status.opponentUsername + "[O]");
    }

    if (status.state === 1) {
        gameStatus.text("Status: Waiting for a second player");
    }
    else if (status.state === 2) {
        gameStatus.text("Status: X turn");
    }
    else if (status.state === 3) {
        gameStatus.text("Status: O turn");
    }
    else if (status.state === 4) {
        gameStatus.text("Status: X won");
    }
    else if (status.state === 5) {
        gameStatus.text("Status: O won");
        return null;
    }
    else if (status.state === 6) {
        gameStatus.text("Status: Draw");
        return null;
    }
}