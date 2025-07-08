namespace Persistence.Database.DbContextFactory;

public interface IDbContextFactory
{
    DatabaseContext Create();

}