﻿using FELFEL.Domain;
using FELFEL.UseCases.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FELFEL.External.EntityFrameworkDataAccess.Repositories
{
    public class BatchRepository : Repository<Batch>, IBatchRepository
    {
        public BatchRepository(FELFELContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Batch>> GetBatchesByProduct(int productId)
        {
            return await _entities
                .Where(batch => batch.ProductType.Id == productId)
                .Include( _ => _.ProductType)
                .ToListAsync();
        }

        public int GetCount(Expression<Func<Batch, bool>> predicate)
        {
            return _entities.Count(predicate);
        }

        public async Task<IEnumerable<Batch>> GetBatchesDeatiledAsync()
        {
             return await _entities
                .Include(x => x.ProductType)
                .Include(x => x.History)
                .ToListAsync();
        }

        public async Task<Batch> GetBatchAsync(uint batchId)
        {
            return await _entities
               .Include(x => x.ProductType)
               .SingleOrDefaultAsync(batch => batch.Id == batchId);
        }

        public async Task<int> GetStock(int batchId)
        {
            return await _entities
               .Where(batch => batch.Id == batchId)
               .Select(_ => _.RemainingUnits)
               .FirstAsync(); 
        }

        public async Task<IEnumerable<object>> GetAllStock()
        {
            return await _entities
               .Select(_ => new { _.Id, _.RemainingUnits })
               .ToListAsync();


        }

        public async Task<Batch> GetBatchWithHistoryAsync(uint batchId)
        {
            return await _entities
               .Include(x => x.ProductType)
               .Include(x => x.History)
               .SingleOrDefaultAsync(batch => batch.Id == batchId);
        }
    }
}
