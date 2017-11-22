using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Models
{
    public enum State
    {
        WaitingForASecondPlayer,
        Xturn,
        Oturn,
        Xvictory,
        Ovictory,
        Draw

    }
}
