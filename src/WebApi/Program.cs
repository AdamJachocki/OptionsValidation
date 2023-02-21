using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System.Reflection;
using WebApi.Models;
using WebApi.Validators;
using WebApi.Validators.FluentValidators;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddOptions<SimpleOptions>()
                .Bind(builder.Configuration.GetSection("SimpleOptions"))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection("ApiOptions"));
            builder.Services.AddSingleton<IValidateOptions<ApiOptions>, ApiOptionsValidator>();

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddOptions<FluentApiOptions>()
                .BindConfiguration("FluentApiOptions")
                .ValidateFluentValidation()
                .ValidateOnStart();


            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}