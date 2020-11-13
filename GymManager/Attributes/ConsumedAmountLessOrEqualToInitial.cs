using GymManager.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Attributes
{
    public class ConsumedAmountLessOrEqualToInitial : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var supplement = (Supplement) validationContext.ObjectInstance;

            if (supplement.InitialAmount >= supplement.ConsumedAmount)
            {
                return ValidationResult.Success;
            } 
            else
            {
                return new ValidationResult("Consumed amount must be less or equal to initial amount");
            }
        }
    }
}