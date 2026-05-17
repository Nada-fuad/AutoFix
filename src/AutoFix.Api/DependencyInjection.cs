using AutoFix.Api.services;
using AutoFix.Application.Common.Interfaces;

namespace AutoFix.Api
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddApiDocumentation();
            services.AddIdentityInfrastructure();

            return services;
        }

        public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
        {
            services.AddOpenApi();

            return services;
        }

        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUser, CurrentUser>();
            services.AddHttpContextAccessor();
            return services;
        }

    }

}
