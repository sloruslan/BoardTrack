using Application.Configuration;
using Application.Interfaces;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Database;
using Persistence.Database.DbContextFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Persistence.Service
{
    public class DatabaseMaintenanceService : IDatabaseMaintenanceService
    {

        private static readonly Regex TableNamePattern = new(@"^(.*\.)?""(.*)""$", RegexOptions.Compiled);

        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<DatabaseMaintenanceService> _logger;

        private const string schemaName = "public";
        private readonly int _monthForDelete;

        public DatabaseMaintenanceService(IDbContextFactory dbContextFactory,
            ILogger<DatabaseMaintenanceService> logger,
            IOptions<PartitionOptions> _partitionOptions)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _monthForDelete = _partitionOptions.Value.MonthForDelete;
        }

        public async Task CreatePartitionsIfNeedAsync()
        {
            await using var db = _dbContextFactory.Create();
            await CreatePartitionsAsync(db, GetTableName(db.BoardHistory));

        }

        private static string GetTableName<T>(ITable<T> table) where T : notnull
        {
            var tableName = table.GetTableName();
            var cleanTableName = TableNamePattern.Replace(tableName, "$2");
            return cleanTableName;
        }

        private async Task CreatePartitionsAsync(DatabaseContext db, string tableNamePrefix)
        {
            var now = DateTime.Now;
            var dates = new[] { now.AddMonths(-1), now, now.AddMonths(1) };
            var sp = db.DataProvider.GetSchemaProvider();
            var dbSchema = sp.GetSchema(db);
            foreach (var date in dates)
            {
                var tableName = $"{tableNamePrefix}_{date.Year:0000}_{date.Month:00}";
                if (dbSchema.Tables.Any(t => t.TableName == tableName && t.SchemaName == schemaName.ToLower()))
                {
                    continue;
                }

                var command =
                    $"CREATE TABLE {schemaName}.\"{tableName}\" PARTITION OF {schemaName}.\"{tableNamePrefix}\" FOR VALUES FROM ('{date:yyy-MM}-01') TO ('{date.AddMonths(1):yyyy-MM}-01')";
                await db.ExecuteAsync(command, CancellationToken.None);
                _logger.LogInformation("Создана новая партиция {Schema}.{TableName}", schemaName, tableName);
            }
        }

        public async Task DeletePartitionsIfNeedAsync()
        {
            await using var db = _dbContextFactory.Create();

            var tableNamePrefix = GetTableName(db.BoardHistory);

            var now = DateTime.Now;
            var dates = new[] { now.AddMonths(-_monthForDelete) };
            var sp = db.DataProvider.GetSchemaProvider();
            var dbSchema = sp.GetSchema(db);

            foreach (var date in dates)
            {
                var tableName = $"{tableNamePrefix}_{date.Year:0000}_{date.Month:00}";
                if (dbSchema.Tables.Any(t => t.TableName == tableName && t.SchemaName == schemaName.ToLower()))
                {
                    var command =
                    $"DROP TABLE {schemaName}.\"{tableName}\";";
                    await db.ExecuteAsync(command, CancellationToken.None);
                    _logger.LogInformation("Удалена партиция {Schema}.{TableName}", schemaName, tableName);
                }
                
            }
        }
    }
}
