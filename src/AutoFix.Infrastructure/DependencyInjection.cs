using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoFix.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure( this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAppDbContext>(sp=>sp.GetRequiredService<AppDbContext>());
            return services;

        }
    }
}
