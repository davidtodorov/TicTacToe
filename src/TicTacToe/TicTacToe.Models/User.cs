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
        }

        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string PhotoUrl { get; set; }

        public DateTime RegistrationDate { get; set; }

        public ICollection<Score> Scores { get; set; }
    }
}