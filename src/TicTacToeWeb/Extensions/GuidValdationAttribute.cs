using System;
using System.ComponentModel.DataAnnotations;

namespace TicTacToeWeb.Extensions
{
    public class GuidValdationAttribute
    {
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
        sealed public class NoEmptyGuid : ValidationAttribute
        {
            public override bool IsValid(Object value)
            {
                bool result = true;

                if ((Guid)value == Guid.Empty)
                    result = false;

                return result;
            }
        }
    }
}
