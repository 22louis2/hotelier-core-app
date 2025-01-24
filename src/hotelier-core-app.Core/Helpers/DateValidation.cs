using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace hotelier_core_app.Core.Helpers
{
    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var dateString = Convert.ToString(value);
            if (string.IsNullOrWhiteSpace(dateString))
                return ValidationResult.Success ?? new ValidationResult("Sorry! Invalid date format entered. Use YYYY-MM-DD format only"); ;

            bool ok = DateTime.TryParseExact(
               dateString,
               "yyyy-MM-dd",
               CultureInfo.InvariantCulture,
               DateTimeStyles.None,
               out _
            );

            if (ok)
                return ValidationResult.Success ?? new ValidationResult("Sorry! Invalid date format entered. Use YYYY-MM-DD format only"); ;
            return new ValidationResult("Sorry! Invalid date format entered. Use YYYY-MM-DD format only");
        }
    }
}
