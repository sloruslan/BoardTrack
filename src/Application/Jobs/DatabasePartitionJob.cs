using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Jobs
{
    [DisallowConcurrentExecution]
    public class DatabasePartitionJob : IJob
    {
        private readonly ILogger<DatabasePartitionJob> _logger;
        private readonly IDatabaseMaintenanceService _databaseMaintenanceService;
        public DatabasePartitionJob(ILogger<DatabasePartitionJob> logger, 
            IDatabaseMaintenanceService databaseMaintenanceService)
        {
            _logger = logger;
            _databaseMaintenanceService = databaseMaintenanceService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Выполняется джоба по созданию партиций в БД");

            await _databaseMaintenanceService.CreatePartitionsIfNeedAsync();
            await _databaseMaintenanceService.DeletePartitionsIfNeedAsync();
        }
    }
}
