using FELFEL.External.EntityFrameworkDataAccess;
using FELFEL.Persistence;
using FELFEL.UseCases;
using FELFEL.UseCases.RegisterNewBatch;
using FELFEL.UseCases.ModifyBatchStock;
using FELFEL.UseCases.Repositories;
using FELFEL.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FELFEL.External.EntityFrameworkDataAccess.Repositories;
using FELFEL.UseCases.GetFreshnessOverview;

namespace FELFEL_Inventory.Web_Api
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
            services.AddDbContext<FELFELContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("FELFEL-Inventory.Web-Api")));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IRegisterNewBatch, RegisterNewBatch>();
            services.AddScoped<IModifyBatchStock, ModifyBatchStock>();
            services.AddScoped<IGetFreshnessOverview, GetFreshnessOverview>();
            services.AddScoped<IBatchRepository, BatchRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            SeedData.EnsurePopulated(app);
        }
    }
}
