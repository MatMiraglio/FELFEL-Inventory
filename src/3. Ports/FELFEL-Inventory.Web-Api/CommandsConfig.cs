using FELFEL.External.EntityFrameworkDataAccess.Repositories;
using FELFEL.Persistence;
using FELFEL.UseCases;
using FELFEL.UseCases.GetFreshnessOverview;
using FELFEL.UseCases.ModifyBatchStock;
using FELFEL.UseCases.RegisterNewBatch;
using FELFEL.UseCases.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FELFEL.WebApi
{
    public static class CommandsConfig
    {

        public static IServiceCollection AddDIConfig(this IServiceCollection services) =>
            services
                .AddTransient<IRegisterNewBatch, RegisterNewBatch>()
                .AddTransient(x => new Lazy<IRegisterNewBatch>(() => x.GetRequiredService<IRegisterNewBatch>()))

                .AddTransient<IModifyBatchStock, ModifyBatchStock>()
                .AddTransient(x => new Lazy<IModifyBatchStock>(() => x.GetRequiredService<IModifyBatchStock>()))

                .AddTransient<IGetFreshnessOverview, GetFreshnessOverview>()
                .AddTransient(x => new Lazy<IGetFreshnessOverview>(() => x.GetRequiredService<IGetFreshnessOverview>()))

                .AddTransient<IBatchRepository, BatchRepository>()
                .AddTransient(provider => new Lazy<IBatchRepository>( provider.GetService<IBatchRepository>()))

                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient(x => new Lazy<IUnitOfWork>(() => x.GetRequiredService<IUnitOfWork>()));
    }
}
