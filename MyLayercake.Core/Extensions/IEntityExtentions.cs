using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyLayercake.Core.Extensions {
    public static class IEntityExtentions {
        public static IEnumerable<ValidationResult> GetValidationErrors<T>(this T domainObject) where T : IEntity, new() {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(domainObject);

            Validator.TryValidateObject(domainObject, context, validationResults, true);

            return validationResults;
        }

        public static bool IsValid<T>(this T domainObject) where T : IEntity, new() {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(domainObject);

            Validator.TryValidateObject(domainObject, context, validationResults, true);

            return validationResults.Count < 1; 
        }
    }
}