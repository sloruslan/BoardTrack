using Microsoft.Extensions.Options;
using Npgsql;
using Persistence.Configurations;

namespace Persistence.Database.DbContextFactory;

public class DbContextFactory : IDbContextFactory
{
    private readonly DatabaseOptions _databaseOptions;

    public DbContextFactory(IOptions<DatabaseOptions> options)
    {
        _databaseOptions = options.Value;
    }

    public DatabaseContext Create()
    {
        var connStringBuilder = new NpgsqlConnectionStringBuilder()
        {
            Host = _databaseOptions.Host,
            Port = _databaseOptions.Port,
            Database = _databaseOptions.Name,
            Username = _databaseOptions.User,
            Password = _databaseOptions.Password,
            CommandTimeout = _databaseOptions.CommandTimeout,
            ConnectionIdleLifetime = _databaseOptions.ConnectionIdLifetime,
            Pooling = _databaseOptions.Pooling,
            KeepAlive = _databaseOptions.KeepAlive,
            TcpKeepAlive = _databaseOptions.TcpKeepAlive,
            IncludeErrorDetail = _databaseOptions.IncludeErrorDetail
        };

        return new DatabaseContext(connStringBuilder.ToString());
    }

}