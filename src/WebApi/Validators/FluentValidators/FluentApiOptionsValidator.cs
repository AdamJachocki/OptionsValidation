using FluentValidation;
using WebApi.Models;

namespace WebApi.Validators.FluentValidators
{
    public class FluentApiOptionsValidator: AbstractValidator<FluentApiOptions>
    {
        public FluentApiOptionsValidator()
        {
            RuleFor(x => x.ApiUrl)
                .NotEmpty();

            RuleFor(x => x.ClientUri)
                .NotEmpty()
                .When(x => string.IsNullOrWhiteSpace(x.ClientId) && string.IsNullOrWhiteSpace(x.ClientSecret))
                .WithMessage("Jeśli nie podajesz clientId i sekretu, musisz podać ClientUri")
                .MinimumLength(5)
                .WithMessage("ClientUri jest za krótkie");

            RuleFor(x => x.ClientId)
                .NotEmpty()
                .When(x => string.IsNullOrWhiteSpace(x.ClientUri))
                .WithMessage("Musisz podać ClientId i sekret, jeśli nie podajesz ClientUri");

            RuleFor(x => x.ClientSecret)
                .NotEmpty()
                .When(x => !string.IsNullOrWhiteSpace(x.ClientId))
                .WithMessage("Brak client secret");
        }
    }
}
