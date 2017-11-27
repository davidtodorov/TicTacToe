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
        }


        public int  GameId { get; set; }
        public string Name { get; set; }
        public string Board { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public Visibility Visibility { get; set; }
        public GameState State { get; set; }

        public int PlayerOneId { get; set; }
        public int? PlayerTwoId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ScoreId { get; set; }
        public Score Score { get; set; }

        public ICollection<Notification> Notifications { get; set; }
        
        


    }
}
