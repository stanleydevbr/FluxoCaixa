using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FluxoCaixa.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddDevBrSwaggerConfiguration(this IServiceCollection services)
        {
            var config = SwaggerSettings.GetInstance();
            services.AddControllers();
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.ReportApiVersions = true;
                x.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddVersionedApiExplorer(x =>
            {
                x.GroupNameFormat = "'v'VVV";
                x.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen().ConfigureSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.FullName);
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigurationSwaggerOptions>();

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();
                c.EnableAnnotations();
                c.AddSecurityDefinition(config.Seguranca.Identificador, new OpenApiSecurityScheme
                {
                    Description = config.Seguranca.Descricao,
                    Name = config.Seguranca.Autorizacao,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,

                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =  new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            },
                        },
                        Array.Empty<string>()
                    }
                });
                foreach (var filePath in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)), "*.xml"))
                    c.IncludeXmlComments(filePath);
                c.ExampleFilters();
            });

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            services.AddFluentValidationRulesToSwagger();
            services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }

        public static IApplicationBuilder UseDevBrSwaggerConfiguration(
            this IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider,
            string applicationPrefixRoute)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseMvc()
                .UseApiVersioning()
                .UseMvcWithDefaultRoute();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = $"/swagger/{{documentName}}/swagger.json";
            });
            app.UseSwagger();
            app.UseDefaultFiles();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = $"{applicationPrefixRoute}/swagger";
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description?.GroupName?.ToLowerInvariant());
                }
                options.DocExpansion(DocExpansion.List);

            });

            return app;
        }

    }

    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context != default && context.MethodInfo != default && context.MethodInfo.DeclaringType != default)
            {
                IEnumerable<AuthorizeAttribute> authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                    .Union(context.MethodInfo.GetCustomAttributes(true))
                    .OfType<AuthorizeAttribute>();

                if (authAttributes.Any() && !operation.Responses.Any(r => r.Key == "401"))
                    operation.Responses.Add("401", new OpenApiResponse { Description = "User not authenticated." });

                // if (authAttributes.Any() && !operation.Responses.Any(r => r.Key == "403"))
                //     operation.Responses.Add("403", new OpenApiResponse { Description = "User not authorized to access this endpoint." });
            }
        }
    }

}
