using System;
using System.ComponentModel.DataAnnotations;

namespace TicTacToeWeb.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class NoEmptyGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            return (Guid)value != Guid.Empty;
        }
    }
}