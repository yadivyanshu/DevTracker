using System;
using System.ComponentModel.DataAnnotations;

namespace DevTracker.Common.Validators
{
    public class EnumValidationAttribute<T> : ValidationAttribute where T : Enum
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            
            if (!Enum.IsDefined(typeof(T), value))
            {
                ErrorMessage = $"{value} is not a valid value for {typeof(T).Name}.";
                return false;
            }

            return true;
        }
    }
}