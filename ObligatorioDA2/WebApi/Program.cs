using DataAccess;
using Domain;
using IDataAccess;
using IBusinessLogic;
using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Filters;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductsAPI", Version = "v1" });
            });

            // Add services to the container.
            builder.Services.AddRazorPages();

            //
            //add transient, singleton, scoped, etc
            //builder.Services.AddDbContext<DbContext, Context>();
            builder.Services.AddDbContext<DbContext, Context>();
            builder.Services.AddTransient<IService<User>, UserService>();
            builder.Services.AddTransient<IService<Product>, ProductService>();
            builder.Services.AddTransient<IService<Purchase>, PurchaseService>();
            builder.Services.AddTransient<IService<Session>, SessionService>();
            builder.Services.AddSingleton<ISessionLogic, SessionLogic>();


            builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)));
            builder.Services.AddScoped<AuthenticationFilter>();
            //builder.Services.AddTransient<AuthorizationFilter>();
            builder.Services.AddHttpContextAccessor();
            //

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi"));

            app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.Run();
        }
    }
}