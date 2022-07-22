using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Library.ViewModels.CustomValidators
{
    public class MultiLineValidator : ValidationAttribute
    {
        public int Count { get; set; } = 4;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(value?
                        .ToString()))
                return new ValidationResult(  ErrorMessage??"Must Have Value");
            else
            {
                if(value?.ToString()?
                    .Split(new []{ Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                    .Count() < Count)
                    return new ValidationResult(ErrorMessage??$"Must Have At Least {Count} Lines");
            }

            return ValidationResult.Success;
        }
    }
}
