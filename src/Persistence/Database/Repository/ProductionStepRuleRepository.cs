﻿using Application.Interfaces.Repository;
using AutoMapper;
using LinqToDB;
using Persistence.Database.DbContextFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database.Repository
{
    public class ProductionStepRuleRepository :BaseRepository, IProductionStepRuleRepository
    {
        public ProductionStepRuleRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory){}

        public async Task<List<short>> GetValidSteps(short currentStepId)
        {
            using var db = _dbContextFactory.Create();

            var steps = await db.ProductionStepRule  
                .Where(x => x.CurrentStepId == currentStepId)
                .Select(x => x.ValidNextStepId)
                .ToListAsync();

            if (steps == null)
            {
                return new List<short>();
            }

            return steps;
        }
    }
}
