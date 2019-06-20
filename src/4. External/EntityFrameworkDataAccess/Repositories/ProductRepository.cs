using FELFEL.Domain;
using FELFEL.UseCases.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FELFEL.External.EntityFrameworkDataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(FELFELContext context) : base(context) { }

        //Exposes methods to avoid repeting queries
    }
}
