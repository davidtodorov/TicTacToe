using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class User
    {
        public User()
        {
            this.RegistrationDate = DateTime.UtcNow;
            this.PhotoUrl = null;
            this.Scores = new List<Score>();
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime RegistrationDate { get; set; }

        public ICollection<Score> Scores { get; set; }
    }
}