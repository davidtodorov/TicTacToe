using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TicTacToe.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.RegistrationDate = DateTime.UtcNow;            
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

        [MaxLength(1000)]
        public string PhotoUrl { get; set; }

        public DateTime RegistrationDate { get; set; }

        public ICollection<Score> Scores { get; set; }
    }
}