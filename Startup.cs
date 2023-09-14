using AplikacjaDoNaukiJęzyków.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace AplikacjaDoNaukiJęzyków
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        private IWebHostEnvironment CurrentEnvironment { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // var connString = "";
            // if (CurrentEnvironment.IsDevelopment())
            //     connString = Configuration.GetConnectionString("DefaultConnection");
            // else
            // {
            //     var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            //     connUrl = connUrl.Replace("postgres://", string.Empty);
            //     var pgUserPass = connUrl.Split("@")[0];
            //     var pgHostPortDb = connUrl.Split("@")[1];
            //     var pgHostPort = pgHostPortDb.Split("/")[0];
            //     var pgDb = pgHostPortDb.Split("/")[1];
            //     var pgUser = pgUserPass.Split(":")[0];
            //     var pgPass = pgUserPass.Split(":")[1];
            //     var pgHost = pgHostPort.Split(":")[0];
            //     var pgPort = pgHostPort.Split(":")[1];

            //     connString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}";
            // }
            // services.AddDbContext<DatabaseContext>(opt => 
            // {
            //     opt.UseNpgsql(connString);
            // });

            string connectionString = String.Empty;
            if (CurrentEnvironment.IsDevelopment())
                connectionString = Configuration.GetConnectionString("DefaultConnection");
            else
            {
                // Use connection string provided at runtime by Fly.
                var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                
                // Parse connection URL to connection string for Npgsql
                connUrl = connUrl.Replace("postgres://", string.Empty);
                var pgUserPass = connUrl.Split("@")[0];
                var pgHostPortDb = connUrl.Split("@")[1];
                var pgHostPort = pgHostPortDb.Split("/")[0];
                var pgDb = pgHostPortDb.Split("/")[1];
                var pgUser = pgUserPass.Split(":")[0];
                var pgPass = pgUserPass.Split(":")[1];
                var pgHost = pgHostPort.Split(":")[0];
                var pgPort = pgHostPort.Split(":")[1];
                var updatedHost = pgHost.Replace("flycast", "internal");

                connectionString = $"Server={updatedHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};";
            }
            services.AddDbContext<DatabaseContext>(opt => 
            {
                opt.UseNpgsql(connectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DatabaseContext>();
                context.Database.Migrate();
                Seed.SeedWords(context);

            }
            catch (Exception ex)
            {
                var logger = services.GetService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");
            }
        }
    }
}
