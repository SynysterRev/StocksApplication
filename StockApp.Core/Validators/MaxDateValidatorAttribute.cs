using System.ComponentModel.DataAnnotations;

namespace StockApp.Core.Validators
{
    public class MaxDateValidatorAttribute : ValidationAttribute
    {
        public string MaxDate { get; set; }
        public string DefaultErrorMessage { get; set; } = "Maximum date allowed is {0}";

        public MaxDateValidatorAttribute(string maxDate)
        {
            MaxDate = maxDate;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime to_date = Convert.ToDateTime(value);

                DateTime maxDateTime = DateTime.Parse(MaxDate);

                if (to_date < maxDateTime)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MaxDate));
                }
                return ValidationResult.Success;
            }
            return null;
        }
    }
}
