using GymManager.Core.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Attributes
{
    public class DateMustBeOlderThanTodaysDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var malfunction = (Malfunction) validationContext.ObjectInstance;

            if (malfunction.MalfunctionDate <= DateTime.Today)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date must be older than today's date");
            }
        }
    }
}