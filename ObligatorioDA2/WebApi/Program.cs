using BusinessLogic;
using DataAccess;
using Domain;
using Domain.PaymentMethods;
using IBusinessLogic;
using IDataAccess;
using IImporters;
using IImportersServices;
using Importers;
using ImportersServices;
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
            builder.Services.AddDbContext<DbContext, Context>();
            builder.Services.AddTransient<IService<User>, UserService>();
            builder.Services.AddTransient<IService<Product>, ProductService>();
            builder.Services.AddTransient<IService<Purchase>, PurchaseService>();
            builder.Services.AddTransient<IService<PromotionEntity>, PromotionService>();
            builder.Services.AddTransient<IService<Session>, SessionService>();
            builder.Services.AddTransient<IService<PaymentMethodEntity>, PaymentMethodService>();


            builder.Services.AddTransient<IProductLogic, ProductLogic>();
            builder.Services.AddTransient<IPromotionLogic, PromotionLogic>();
            builder.Services.AddTransient<IPromotionImporter, PromotionImporter>();
            builder.Services.AddTransient<IPurchaseLogic, PurchaseLogic>();
            builder.Services.AddScoped<ISessionLogic, SessionLogic>();
            builder.Services.AddTransient<ISignupLogic, SignupLogic>();
            builder.Services.AddTransient<IUserLogic, UserLogic>();

            builder.Services.AddTransient<IImporterService, ImporterService>();

            builder.Services.AddScoped<IShoppingCart, ShoppingCart>();
            builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
            builder.Services.AddTransient<IShoppingCartServiceDatabaseHelper, ShoppingCartServiceDatabaseHelper>();
            builder.Services.AddTransient<IShoppingCartServiceReflectionHelper, ShoppingCartServiceReflectionHelper>();

            builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)));
            builder.Services.AddTransient<AuthenticationFilter>();
            //builder.Services.AddTransient<AuthorizationFilter>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200")
                        // .WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
            //

            var app = builder.Build();
            app.UseCors();

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