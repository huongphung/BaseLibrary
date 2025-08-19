using Core.Library.Configures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Library.DependencyInjections
{
    public static class AddSwagger
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            #region API Versioning
            services.AddEndpointsApiExplorer();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            //var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //   {
                //       Scheme = "Bearer",
                //       BearerFormat = "JWT",
                //       In = ParameterLocation.Header,
                //       Type = SecuritySchemeType.Http, //ApiKey,
                //       Name = "Authorization",
                //       Description = "Bearer Authentication with JWT Token",
                //   });

                //   options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //   {
                //       {
                //           new OpenApiSecurityScheme
                //           {
                //               Reference = new OpenApiReference
                //               {
                //                   Id = "Bearer",
                //                   Type = ReferenceType.SecurityScheme
                //               }
                //           },
                //           new List<string>()
                //       }
                //   });

                // Include XML comments
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }

                // Cấu hình để hiển thị group name trong Swagger UI
                //options.DocInclusionPredicate((docName, apiDesc) =>
                //{
                //	if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;
                //	var groupName = methodInfo.DeclaringType?.GetCustomAttribute<ApiExplorerSettingsAttribute>()?.GroupName;
                //	return string.IsNullOrEmpty(groupName) || groupName == docName;
                //});
            });
            #endregion

            return services;
        }

        public static WebApplication AddSwaggerApp(this WebApplication app)
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToLowerInvariant());
                    }
                }
            );

            return app;
        }
    }
}
