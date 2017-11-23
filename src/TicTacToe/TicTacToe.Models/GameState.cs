using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Models
{
    public enum GameState
    {
        WaitingForASecondPlayer,
        Xturn,
        Oturn,
        Xvictory,
        Ovictory,
        Draw

    }
}
