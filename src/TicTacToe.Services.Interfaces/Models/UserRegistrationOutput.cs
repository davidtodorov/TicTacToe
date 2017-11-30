using System;

namespace TicTacToe.Services.Interfaces.Models
{
    public class UserRegistrationOutput
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}