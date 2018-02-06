using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TicTacToe.Models;

namespace TicTacToeWeb.ViewModels.Game
{
    public class CreateGameViewModel : IValidatableObject
    {
        [Required]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Range(1, 3)]
        public VisibilityType Visibility { get; set; } = VisibilityType.Public;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Visibility == VisibilityType.Protected && string.IsNullOrWhiteSpace(Password))
            {
                return new List<ValidationResult>()
                {
                    new ValidationResult("The password is required for protected games.", new []{nameof(Password)})
                };

            }
            return Enumerable.Empty<ValidationResult>();
        }
    }
}
