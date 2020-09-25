using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MyLayercake.Core.Extentions {
    public static class IEntityExtentions {
        public static IEnumerable<ValidationResult> GetValidationErrors<T>(this T domainObject) where T : new() {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(domainObject);

            Validator.TryValidateObject(domainObject, context, validationResults, true);

            return validationResults;
        }
    }
}