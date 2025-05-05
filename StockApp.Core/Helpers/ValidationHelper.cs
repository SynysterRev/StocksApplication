using System.ComponentModel.DataAnnotations;

namespace StockApp.Core.Helpers
{
    internal class ValidationHelper
    {
        public static void ValidateObject(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    throw new ValidationException(validationResult.ErrorMessage);
                }
            }
        }
    }
}
