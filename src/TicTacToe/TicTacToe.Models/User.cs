using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    public class User
    {
        public User()
        {
            this.UserId = Guid.NewGuid();
            this.RegistrationDate = DateTime.UtcNow;

            this.Scores = new List<Score>();
            this.Notifications = new List<Notification>();
            this.Games = new List<Game>();
        }

        [Key]
        public Guid UserId { get; set; }

        [Required]
        // TODO: Add validation
        public string FirstName { get; set; }

        [Required]
        // TODO: Add validation
        public string LastName { get; set; }

        // TODO: Add validation
        public string PhotoUrl { get; set; }

        public DateTime RegistrationDate { get; set; }

        public ICollection<Score> Scores { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}