using System;
using System.Collections.Generic;

namespace TicTacToe.Models
{
    public class User
    {
        public User()
        {
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<Score> Scores { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}