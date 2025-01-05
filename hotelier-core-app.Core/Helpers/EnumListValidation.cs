using System.ComponentModel.DataAnnotations;

namespace hotelier_core_app.Core.Helpers
{
    public class EnumListValidationAttribute : ValidationAttribute
    {
        private readonly Type _enumType;
        public EnumListValidationAttribute(Type enumType)
        {
            if (enumType == null || !enumType.IsEnum)
            {
                throw new ArgumentNullException(nameof(enumType), "The type must be an enum type");
            }
            _enumType = enumType;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IEnumerable<string> enumNames)
            {
                var invalidNames = enumNames.Where(enumName => !Enum.IsDefined(_enumType, enumName)).ToList();

                if (invalidNames.Count != 0)
                {
                    var invalidNamesString = string.Join(", ", invalidNames);
                    return new ValidationResult($"{invalidNamesString} are not valid values in {_enumType.Name}");
                }

                return ValidationResult.Success ?? new ValidationResult("Invalid input data type.");
            }

            return new ValidationResult("Invalid input data type. Expected an enumerable of strings.");
        }
    }
}
