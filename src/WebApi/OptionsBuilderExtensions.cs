using FluentValidation;
using Microsoft.Extensions.Options;
using WebApi.Models;
using WebApi.Validators;
using WebApi.Validators.FluentValidators;

namespace WebApi
{
    public static class OptionsBuilderExtensions
    {
        public static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>(this OptionsBuilder<TOptions> builder)
            where TOptions : class
        {
            builder.Services.AddSingleton<IValidateOptions<TOptions>>(sp =>
            {
                return new GenericFluentValidator<TOptions>(builder.Name, sp);
            });

            return builder;
        }
    }
}
