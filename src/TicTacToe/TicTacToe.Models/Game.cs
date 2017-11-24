using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Game
    {
        public Game()
        {
            this.Users = new List<User>();
        }


        public int  Id { get; set; }
        public string Name { get; set; }
        public string Board { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public Visibility Visibility { get; set; }
        public GameState State { get; set; }

        public int ScoreId { get; set; }
        public Score Score { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
