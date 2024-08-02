using ForexMarket.Data;
using ForexMarket.Middleware;
using ForexMarket.Models;
using ForexMarket.Services;
using ForexMarket.Services.LifeTimeServices;
using ForexMarket.Utility.AppSettingsClasses;
using ForexMarket.Utility.DI_AppSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ForexMarket {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();


            builder.Services.AddTransient<IMarketForcaster, MarketForcaster>();
            

            //it will add multiple validation
            //builder.Services.AddScoped<IValidationChecker, AddressValidationChecker>();
            //builder.Services.AddScoped<IValidationChecker, CreditValidationChecker>();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, AddressValidationChecker>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, CreditValidationChecker>());
            builder.Services.AddScoped<ICreditValidator, CreditValidator>();

            builder.Services.AddScoped<CreditApprovedLow>();
            builder.Services.AddScoped<CreditApprovedHigh>();

            builder.Services.AddScoped<Func<CreditApprovedEnum, ICreditApproved>>(ServiceProvider => range => {
                switch (range) {
                    case CreditApprovedEnum.High: return ServiceProvider.GetService<CreditApprovedHigh>();
                    case CreditApprovedEnum.Low: return ServiceProvider.GetService<CreditApprovedLow>();
                    default: return ServiceProvider.GetService<CreditApprovedLow>();
                }
            });



            builder.Services.AddServicesToConfig(builder.Configuration);

            builder.Services.AddTransient<TransientService>();
            builder.Services.AddScoped<ScopedService>();
            builder.Services.AddSingleton<SingletonService>();

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddRazorPages();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseMigrationsEndPoint();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<CustomeMiddleware>();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
