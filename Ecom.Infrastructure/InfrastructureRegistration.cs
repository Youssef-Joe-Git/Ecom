using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositries;
using Ecom.Infrastructure.Repositries.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRpository<>), typeof(GenericRpository<>));
            // add unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IImageManagementService, ImageManagementService>();
            services.AddSingleton<IFileProvider>( new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));


            // add DbContext
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                )
            );


            return services;
        }
    }
}
