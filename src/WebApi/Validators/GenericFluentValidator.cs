using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;

namespace WebApi.Validators
{
    public class GenericFluentValidator<TOptions> : IValidateOptions<TOptions>
        where TOptions : class
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _name;

        public GenericFluentValidator(string name, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _name = name;
        }

        public ValidateOptionsResult Validate(string? name, TOptions options)
        {
            if (_name != null && _name != name)
                return ValidateOptionsResult.Skip;

            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

            ValidationResult res = validator.Validate(options);
            if (res.IsValid)
                return ValidateOptionsResult.Success;

            var errorArray = res.Errors.Select(e => e.ErrorMessage).ToArray();
            var msg = string.Join(Environment.NewLine, errorArray);

            return ValidateOptionsResult.Fail(msg);              
        }
    }
}
