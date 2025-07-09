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
        public DatabasePartitionJob(ILogger<DatabasePartitionJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Выполняется джоба по созданию партиций в БД");

            return Task.CompletedTask;
        }
    }
}
