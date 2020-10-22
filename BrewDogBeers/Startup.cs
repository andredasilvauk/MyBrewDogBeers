using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrewDogBeersData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BrewDogBeers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            var settings = new AppSettings()
            {
                APIUrl = Configuration["APIUrl"],
                MaxNumberOfFavorites = Convert.ToInt32(Configuration["MaxNumberOfFavorites"])
            };
            services.AddSingleton(settings);

            services.AddTransient<IBrewDogBeersAPI, BrewDogBeersAPI>();
            services.AddTransient<IBrewDogBeersDataManager, BrewDogBeersDataManager>();

            services.AddSingleton<IBrewDogBeersFavorites>(InitializeBrewDogBeersClientInstanceAsync(Configuration.GetSection("BrewDogBeersDb"), settings).GetAwaiter().GetResult());

            services.AddHttpClient();

            services.AddControllersWithViews();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static async Task<BrewDogBeersFavorites> InitializeBrewDogBeersClientInstanceAsync(IConfigurationSection configurationSection, AppSettings settings)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;            
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            BrewDogBeersFavorites dbService = new BrewDogBeersFavorites(client, databaseName, containerName, settings);
            Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return dbService;
        }
    }
}
