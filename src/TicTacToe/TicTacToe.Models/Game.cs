using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid  Id { get; set; }
        public string Name { get; set; }
        public string Board { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public Visibility Visibility { get; set; }
        public GameState State { get; set; } 
    }
}
