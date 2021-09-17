using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CleanArchitectureBase.Application;
using CleanArchitectureBase.Application.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nextended.Core.Extensions;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace CleanArchitectureBase.WebAPI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSessionProvider(this IServiceCollection services)
        {
            services.AddSession();
            services.AddHttpContextAccessor();
            services.RemoveAll<ISessionProvider>().AddTransient<ISessionProvider, SessionProvider>();
            return services;
        }

        public static IServiceCollection AddApiVersions(this IServiceCollection services)
        {
            return services.AddApiVersioning(options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = ApiVersions.Newest;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddVersionedApiExplorer(options =>
                {
                    options.DefaultApiVersion = ApiVersions.Newest;
                    options.GroupNameFormat = ApiVersions.GroupNameFormat;
                    options.SubstituteApiVersionInUrl = true;
                });
        }

        public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("ApiDocumentation");
            foreach (var version in ApiVersions.All.Reverse())
            {
                services.AddOpenApiDocument(options =>
                {
                    
                    options.Title = configSection.GetValue<string>(nameof(options.Title));
                    options.Description = configSection.GetValue<string>(nameof(options.Description));

                    options.DocumentName = ApiVersions.DocumentVersionPrefix + version.MajorVersion;
                    options.ApiGroupNames = new[] { ApiVersions.DocumentVersionPrefix + version.MajorVersion };
                    options.Version = ApiVersions.VersionString(version);

                    // Patch document for Azure API Management
                    options.AllowReferencesWithProperties = true;
                    options.PostProcess = document => configSection.ConfigureDocument(document, version); 
                    options.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                       {
                           Type = OpenApiSecuritySchemeType.ApiKey,
                           Name = "Authorization",
                           In = OpenApiSecurityApiKeyLocation.Header,
                           Description = "Type into the textbox: Bearer {your JWT token}."
                       }).OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
                });
            }
            
            return services;
        }

        private static void ConfigureDocument(this IConfigurationSection configSection, OpenApiDocument document, ApiVersion version)
        {
            configSection.GetSection("Contact").Bind(document.Info.Contact ?? (document.Info.Contact = new OpenApiContact()));
            configSection.GetSection("License").Bind(document.Info.License ?? (document.Info.License = new OpenApiLicense()));
            var prefix = $"{Constants.ApiBasePath.EnsureEndsWith("/").EnsureStartsWith("/")}" + ApiVersions.DocumentVersionPrefix + version.MajorVersion;
            foreach (var pair in document.Paths.ToArray())
            {
                document.Paths.Remove(pair.Key);
                document.Paths[pair.Key.Substring(prefix.Length)] = pair.Value;
            }
        }


        public static IServiceCollection AddAppLocalization(this IServiceCollection services, IMvcBuilder mvcBuilder)
        {
            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new("de-DE"),
                        new("en-US")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en-US");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });

            mvcBuilder.AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();
            return services;
        }
    }

    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseEnvironment(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseDatabaseErrorPage();
                // app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseSpaStaticFiles();
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            return app;
        }

        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            return app;
        }

        public static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app)
        {
            app.UseSwaggerUi3();
            app.UseOpenApi(options =>
            {
                options.PostProcess = (document, request) =>
                {
                    // Patch server URL for Swagger UI
                    var prefix = $"{Constants.ApiBasePath.EnsureEndsWith("/").EnsureStartsWith("/")}v" + document.Info.Version.Split('.')[0];
                    document.Servers.First().Url += prefix;
                };
            });
            return app;
        }

        public static IApplicationBuilder UseSessionId(this IApplicationBuilder app)
        {
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var id = context.Session.GetString(Constants.SessionIdKey);
                if (string.IsNullOrEmpty(id))
                    context.Session.SetString(Constants.SessionIdKey, Guid.NewGuid().ToString());

                await next.Invoke();
            });
            return app;
        }
    }
}
