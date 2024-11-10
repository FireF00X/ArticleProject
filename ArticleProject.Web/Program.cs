using ArticleProject.Data.IRepository;
using ArticleProject.Data.ProjectData.Contexts;
using ArticleProject.Data.Repository;
using ArticleProject.Web.Data;
using ArticleProject.Web.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace ArticleProject.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDbContext<ArticleDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IEmailSender, EmailSettings>();
            builder.Services.AddAuthorization(op =>
            {
                op.AddPolicy("User",p=>p.RequireClaim("User"));
                op.AddPolicy("Admin",p=>p.RequireClaim("Admin"));
            });
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

            var app = builder.Build();

            #region MyRegion
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContextIdentity = services.GetRequiredService<ApplicationDbContext>();
            var _dbContextArticle = services.GetRequiredService<ArticleDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContextIdentity.Database.MigrateAsync();
                await _dbContextArticle.Database.MigrateAsync();
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }
            finally
            {
                _dbContextIdentity.Dispose();
                _dbContextArticle.Dispose();

            }
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
