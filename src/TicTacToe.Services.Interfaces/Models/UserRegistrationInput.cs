namespace TicTacToe.Services.Interfaces.Models
{
    public class UserRegistrationInput
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhotoUrl { get; set; }

        public string RegistrationDate { get; set; }
    }
}