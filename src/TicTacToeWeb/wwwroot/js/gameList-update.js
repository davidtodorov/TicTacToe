$(document).ready(function () {
    setInterval(updateList, 1500);
});

function updateList() {
   $.get('/game/gamelist', function(result) {
        if (result) {
            $('#game-list-container').html(result);
        }
    });
}