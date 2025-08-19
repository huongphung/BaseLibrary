using Core.Library.Behavior;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Library.DependencyInjections
{
    public static class AddCqrs
    {
        public static IServiceCollection AddCqrsService(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly[] assemblies = Assembly.GetEntryAssembly()!.GetReferencedAssemblies()
                        .Select(Assembly.Load).Append(Assembly.GetCallingAssembly()).ToArray();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies);
            });
            services.AddFluentValidationAutoValidation();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(HandleBehaviour<,>));

            services.AddFluentValidation(fv =>
            {
                fv.AutomaticValidationEnabled = false;
                fv.DisableDataAnnotationsValidation = true;
                fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
