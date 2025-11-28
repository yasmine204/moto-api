using System.ComponentModel.DataAnnotations;

namespace MotoRentAPI.Common.Attributes
{
    public class NotEmptyOrWhiteSpaceAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            if (value is string str)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return new ValidationResult(
                        $"{validationContext.MemberName} não pode ser vazio ou apenas espaços."
                    );
                }
            }

            return ValidationResult.Success;
        }
    }
}
