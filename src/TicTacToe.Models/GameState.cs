namespace TicTacToe.Models
{
    public enum GameState
    {
        WaitingForASecondPlayer = 1,
        CreatorTurn,
        OpponentTurn,
        CreatorVictory,
        OpponentVictory,
        Draw
    }
}