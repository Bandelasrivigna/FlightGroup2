using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Group2Flight.Models.Validations
{
    public class LegitimateDateAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxYears;

        public LegitimateDateAttribute(int years)
        {
            _maxYears = years;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is not DateTime date)
                return GetValidationResult(context);

            DateTime today = DateTime.Today;
            DateTime maxDate = today.AddYears(_maxYears);

            return date > today && date <= maxDate
                ? ValidationResult.Success
                : GetValidationResult(context);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-legitimatedate-years", _maxYears.ToString());
            MergeAttribute(context.Attributes, "data-val-legitimatedate",
                FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        }

        private ValidationResult GetValidationResult(ValidationContext context)
        {
            return new ValidationResult(
                FormatErrorMessage(context.DisplayName ?? "Date")
            );
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage
                ?? $"{name} must be a valid future date within {_maxYears} years.";
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
                return false;

            attributes.Add(key, value);
            return true;
        }
    }
}