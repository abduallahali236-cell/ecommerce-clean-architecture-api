using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using global::ECommerce.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));

            // Note: AutoMapper profiles should be in the Application assembly. If profiles live elsewhere,
            // register assemblies that contain them, e.g. typeof(SomeProfile).Assembly; to avoid runtime DI errors.


            return services;
        }
    }
}
