using FluentValidation;

namespace CurrencyAppApi.Models.Validators
{
    public class CurrentExchangeRatesDtoValidator : AbstractValidator<CurrentExchangeRatesDto>
    {
        public CurrentExchangeRatesDtoValidator()
        {
            RuleFor(r => r.TableName).Must(x => NbpConsts.TABLE_NAMES.Contains(x.ToUpper())).WithMessage("Only A and B value is valid.");
        }
    }
}
