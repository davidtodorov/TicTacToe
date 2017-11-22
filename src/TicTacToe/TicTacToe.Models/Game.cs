using System;

namespace TicTacToe.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //board
        public State State { get; set; }
        public Visibility Visibility { get; set; }
        protected string Password { get; set; }



    }
}
