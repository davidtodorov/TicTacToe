using System;
using System.ComponentModel.DataAnnotations;

namespace TicTacToeWeb.Extensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NoEmptyGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            return (Guid)value == Guid.Empty;
        }
    }
}