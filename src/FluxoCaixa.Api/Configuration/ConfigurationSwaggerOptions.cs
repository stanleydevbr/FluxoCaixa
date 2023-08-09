using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace FluxoCaixa.Api.Configuration
{
    public class ConfigurationSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        protected readonly IApiVersionDescriptionProvider provider;
        public ConfigurationSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var config = SwaggerSettings.GetInstance();
            var info = new OpenApiInfo()
            {
                Title = config.Titulo,
                Version = description.ApiVersion.ToString(),
                Description = config.Descricao,
                Contact = new OpenApiContact() { Name = "DevBr API", Email = "stanleydevbr@gmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://github.com/stanleydevbr") }

            };
            if (description.IsDeprecated)
            {
                info.Description += "Versão de API está depreciada";
            }
            return info;
        }
    }

}
