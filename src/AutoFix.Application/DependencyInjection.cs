using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AutoFix.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddValidatorsFromAssembly((typeof(DependencyInjection).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
           services.AddTransient(typeof(IPipelineBehavior<,>),typeof(CachingBehavior<,>));
            return services;
        }
    }
}
