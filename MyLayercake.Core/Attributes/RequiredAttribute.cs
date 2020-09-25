using System;
using System.ComponentModel.DataAnnotations;

namespace MyLayercake.Core.Attributes {
    /// <summary>
    /// Validates that non-zero values are supplied for int, decimal or non-default values for dates and guids
    /// </summary>
    public class RequiredAttribute : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var errorMessage = $"The {validationContext.DisplayName} field is required.";
            var validationResult = new ValidationResult(errorMessage, new string[] { validationContext.DisplayName });

            if (value == null) return validationResult;

            if (value is string && string.IsNullOrWhiteSpace(value.ToString())) {
                return validationResult;
            }

            if (value is Guid && Guid.Parse(value.ToString()) == Guid.Empty) {
                return validationResult;
            }

            var incoming = value.ToString();

            if (Decimal.TryParse(incoming, out decimal val) && val == 0) {
                return validationResult;
            }

            if (DateTime.TryParse(incoming, out DateTime date) && date == DateTime.MinValue) {
                return validationResult;
            }

            return ValidationResult.Success;
        }
    }
}
