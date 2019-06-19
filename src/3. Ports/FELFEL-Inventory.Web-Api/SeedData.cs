using FELFEL.Domain;
using FELFEL.External.EntityFrameworkDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FELFEL.WebApi
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            FELFELContext context = app.ApplicationServices
            .GetRequiredService<FELFELContext>();
            context.Database.Migrate();
            if (!context.Batches.Any())
            {
                context.Batches.AddRange(
                new Batch
                {
                    ProductType = new Product
                    {
                        Name = "Spaghetti"
                    },
                    Expiration = new DateTime(2018, 8, 1),
                    Arrival = new DateTime(2018, 6, 1),
                    OriginalUnitAmount = 400,
                    RemainingUnits = 200,
                    History = new HashSet<BatchChange>()
                }, new Batch
                {
                    ProductType = new Product
                    {
                        Name = "Spaghetti"
                    },
                    Expiration = new DateTime(2018, 8, 1),
                    Arrival = new DateTime(2018, 6, 1),
                    OriginalUnitAmount = 400,
                    RemainingUnits = 200,
                    History = new HashSet<BatchChange>()
                }, new Batch
                {
                    ProductType = new Product
                    {
                        Name = "Oats"
                    },
                    Expiration = new DateTime(2018, 8, 1),
                    Arrival = new DateTime(2018, 6, 1),
                    OriginalUnitAmount = 400,
                    RemainingUnits = 200,
                    History = new HashSet<BatchChange>()
                }, new Batch
                {
                    ProductType = new Product
                    {
                        Name = "Tamatoes"
                    },
                    Expiration = new DateTime(2018, 8, 1),
                    Arrival = new DateTime(2018, 6, 1),
                    OriginalUnitAmount = 400,
                    RemainingUnits = 200,
                    History = new HashSet<BatchChange>()
                });
                context.SaveChanges();
            }
        }
    }
}

    

