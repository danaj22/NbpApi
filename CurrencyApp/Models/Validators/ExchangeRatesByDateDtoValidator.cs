using FluentValidation;

namespace CurrencyAppApi.Models.Validators
{
    public class ExchangeRatesByDateDtoValidator : AbstractValidator<ExchangeRatesByDateDto>
    {
        public ExchangeRatesByDateDtoValidator()
        {
            RuleFor(r => r.TableName).Must(x => NbpConsts.TABLE_NAMES.Contains(x.ToUpper())).WithMessage("Only A and B value is valid.");
        }
    }
}
