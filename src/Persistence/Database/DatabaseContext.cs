using Domain.DTO.Database.Models;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.PostgreSQL;

namespace Persistence.Database;

public class DatabaseContext : DataConnection
{
    public DatabaseContext(string connectionString) : base(
        PostgreSQLTools.GetDataProvider(PostgreSQLVersion.v95), connectionString)
    {

    }
    public ITable<Board> Board => this.GetTable<Board>();
    public ITable<BoardType> BoardType => this.GetTable<BoardType>();
    public ITable<ProductionStep> ProductionStep => this.GetTable<ProductionStep>();
    public ITable<ProductionStepRule> ProductionStepRule => this.GetTable<ProductionStepRule>();
    public ITable<BoardHistory> BoardHistory => this.GetTable<BoardHistory>();
    public ITable<User> User => this.GetTable<User>();
    public ITable<Role> Role => this.GetTable<Role>();
    

}