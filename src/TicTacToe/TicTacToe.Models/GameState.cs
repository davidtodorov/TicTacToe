namespace TicTacToe.Models
{
    public enum GameState
    {
        WaitingForASecondPlayer = 1,
        Xturn,
        Oturn,
        Xvictory,
        Ovictory,
        Draw
    }
}