using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using WebApi.Models;

namespace WebApi.Validators
{
    public class ApiOptionsValidator : IValidateOptions<ApiOptions>
    {
        public ValidateOptionsResult Validate(string? name, ApiOptions options)
        {
            bool isIdAndSecret = IsIdAndSecret(options);
            bool isUri = IsUri(options);

            if (isIdAndSecret && isUri)
                return ValidateOptionsResult.Fail("Nie możesz jednocześnie podać ClientUri i sekretów");

            if (!isIdAndSecret && !isUri)
                return ValidateOptionsResult.Fail("Musisz podać jakieś dane do połączenia z API");

            if (isIdAndSecret && options.ClientSecret.Length < 5)
                return ValidateOptionsResult.Fail("Client secret jest za krótki");

            return ValidateOptionsResult.Success;
        }

        private bool IsIdAndSecret(ApiOptions options)
        {
            return !string.IsNullOrWhiteSpace(options.ClientId) && !string.IsNullOrWhiteSpace(options.ClientSecret);
        }

        private bool IsUri(ApiOptions options)
        {
            return !string.IsNullOrWhiteSpace(options.ClientUri);
        }
    }
}
