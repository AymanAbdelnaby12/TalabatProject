

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Api.Error;
using Talabat.Api.Extention;
using Talabat.Api.Helper;
using Talabat.Api.Middlewares;
using Talabat.Core.Interfaces_Or_Repository;
using Talabat.Core.Models;
using Talabat.Core.Models.Identity;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Services;


internal class Program
{
    private static async Task Main(string[] args)
    {
        #region Configure Services Add Services to the container
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<StoreContext>(option => //allow dependency injection for redis
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddSingleton<IConnectionMultiplexer>(Option =>
        {
            var configuration = builder.Configuration.GetConnectionString("RedisConnection");
            return ConnectionMultiplexer.Connect(configuration);
        }
        );

        builder.Services.AddDbContext<AppIdentityDbContext>(option => //allow dependency injection for redis
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
        });

        builder.Services.AddScoped<IBasketRepository, BasketRepository>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();

        builder.Services.AddApplicationServices();
        builder.Services.AddIdentityServices(builder.Configuration);

        // this is the way to register the generic repository
        //builder.Services.AddScoped<IGenericRepository<Product>,GenericRepository<Product>>();
        //builder.Services.AddScoped<IGenericRepository<ProductType>, GenericRepository<ProductType>>();
        //builder.Services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();

        #endregion
        var app = builder.Build();

        #region Update Database
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var context = services.GetRequiredService<StoreContext>();
            await StoreContextSeed.SeedAsync(context);
            await context.Database.MigrateAsync();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            await AppIdentityDbContextSeed.SeedUser(userManager);

        }
        catch (Exception ex)
        {
            // Log the error
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred during migration");

        }
        #endregion

        #region Configure the HTTP request pipeline
        // Configure the HTTP request pipeline.
        app.UseMiddleware<ExceptionMiddleWare>();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStatusCodePagesWithRedirects("/error/{0}");
        app.UseHttpsRedirection();
        app.UseStaticFiles();//MiddelWare To Log Static Files Such as Images 

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        #endregion

        app.Run();
    }
}


