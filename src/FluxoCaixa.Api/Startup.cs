using FluxoCaixa.Api.Configuration;
using FluxoCaixa.Domain.Notifications;
using FluxoCaixa.Infra.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluxoCaixa.Api
{
    public class Startup
    {
        private const string PREFIXO_ROTA = "FluxoCaixa";
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt => {
                opt.EnableEndpointRouting = false;
                opt.Filters.Add<NotificationFilter>();
                });

            services.AddControllers(opt => opt.Filters.Add<NotificationFilter>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(opt =>
                {
                    var serializerOptions = opt.JsonSerializerOptions;
                    serializerOptions.IgnoreNullValues = true;
                    serializerOptions.IgnoreReadOnlyProperties = false;
                    serializerOptions.WriteIndented = true;
                    serializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString;
                    serializerOptions.Converters.Add(new JsonStringEnumConverter());
                    serializerOptions.Converters.Add(new DateTimeConverter());
                    //serializerOptions.Converters.Add(new DecimalJsonConverter());
                    
                });

            services.AddDevBrSwaggerConfiguration();
            var connectionString = "Data source=devbr.db";
            services.AddDbContext<FluxoCaixaContext>(opt =>
            {
                opt.UseSqlite(connectionString);
            });

            services.RegisterServices();
            services.AutoMapperRegister();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseDevBrSwaggerConfiguration(env, provider, PREFIXO_ROTA);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime));
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            //writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
            writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd"));
        }
    }
    public class DecimalJsonConverter : JsonConverter<decimal>
    {
        public override decimal Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                reader.GetDecimal();

        public override void Write(
            Utf8JsonWriter writer,
            decimal value,
            JsonSerializerOptions options) =>
                writer.WriteNumberValue(value);
    }
}
