using Cinema.Utility.DBInitializer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Cinema
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = false;
                option.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores <ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication()
             .AddGoogle("google", opt =>
             {
                 var googleAuth = builder.Configuration.GetSection("Authentication:Google");
                 opt.ClientId = googleAuth["ClientId"]??" ";
                 opt.ClientSecret = googleAuth["ClientSecret"]?? " ";
                 opt.SignInScheme = IdentityConstants.ExternalScheme;
             });

            builder.Services.ConfigureApplicationCookie(option => 
            {
                option.LoginPath = "/Identity/Account/Login";
                option.AccessDeniedPath = "/Customer/Home/NotFoundPage";
            });

            var configuration = builder.Configuration;

            builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
            });
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IRepository<Categories>, Repository<Categories>>();
            builder.Services.AddScoped<IRepository<Cinemas>, Repository<Cinemas>>();
            builder.Services.AddScoped<IRepository<Actors>, Repository<Actors>>();
            builder.Services.AddScoped<IRepository<UserOTP>, Repository<UserOTP>>();
            builder.Services.AddScoped<IRepository<Ticket>, Repository<Ticket>>();
            builder.Services.AddScoped<IRepository<Promotion>, Repository<Promotion>>();
            builder.Services.AddScoped<IDBInitializer, DBInitializer>();

            builder.Services.AddSession(option => 
            {
                option.IdleTimeout = TimeSpan.FromMinutes(50);
            });

            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider.GetService<IDBInitializer>();
            service.Initialize();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{Area=Customer}/{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
