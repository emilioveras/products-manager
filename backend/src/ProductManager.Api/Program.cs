using ProductManager.Persistence.SqlServer.Contexts;
using ProductManager.Persistence.SqlServer.Repositories;
using ProductManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ProductManager.Domain.Contracts;
using ProductManager.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        
        //.AddNewtonsoftJson(options =>
        //{
        //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        //}); ;

        builder.Services.AddDbContext<DataBaseContext>(e =>

        e.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING"))
            .UseSeeding(static (context, _) =>
            {
                context.Set<ProductModel>().AddRange(
                    new ProductModel { Name = "Laptop", Category = "Electronics", Price = 999.99m, Stock = 50 },
                    new ProductModel { Name = "Smartphone", Category = "Electronics", Price = 699.99m, Stock = 150 },
                    new ProductModel { Name = "Desk Chair", Category = "Furniture", Price = 89.99m, Stock = 200 },
                    new ProductModel { Name = "Book: C# Programming", Category = "Books", Price = 39.99m, Stock = 300 }
                );

                context.SaveChanges();
            }));

        builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        builder.Services.AddScoped<IServiceProduct, ProductService>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();

            dbContext.Database.EnsureCreated();
        }

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();


        app.Run();
    }
}