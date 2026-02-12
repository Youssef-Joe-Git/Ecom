using System.Diagnostics;
using Ecom.API.Middleware;
using Ecom.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMemoryCache();


        // Add services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddAutoMapper(cfg =>
        {
            // هنا ممكن تضيف Config إضافي لو حبيت
        }, AppDomain.CurrentDomain.GetAssemblies());
        var app = builder.Build();

        // Middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            // افتح المتصفح تلقائي بعد تشغيل التطبيق
            var url = "https://localhost:7001/swagger";
            try
            {
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            }
            catch
            {
                // لو أي مشكلة، ممكن تتجاهلها
            }
        }
        app.UseMiddleware<ExceptionsMiddleware>();
        app.UseStatusCodePagesWithReExecute("/errors/{0}");

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

            

        app.Run();
        
    }
}