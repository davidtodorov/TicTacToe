using System;
using System.Collections.Generic;

namespace TicTacToe.Models
{
    public class User
    {
        public User()
        {
            this.UsersNotifications = new List<UserNotification>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<UserNotification> UsersNotifications { get; set; }
    }
}