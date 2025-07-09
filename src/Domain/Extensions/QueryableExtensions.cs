using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;
using LinqToDB;
using LinqToDB.Mapping;

namespace Domain.Extensions;

public static class QueryableExtensions
{
    public static async Task<List<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int limit,
        int offset)
    {
        if (limit < 1)
            throw new ArgumentException("Limit cannot be less than 1");

        var data = await source.Skip(offset)
            .Take(limit)
            .ToListAsync();

        return data;
    }

    public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
        string orderDirection)
    {
        return source.OrderBy(orderByProperty, orderDirection.ToLower() == "desc");
    }

    private static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
        bool desc)
    {
        var command = desc ? "OrderByDescending" : "OrderBy";

        var type = typeof(TEntity);
        var properties = type.GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public |
                                            BindingFlags.Instance);

        var property = properties.FirstOrDefault(x =>
            Attribute.IsDefined(x, typeof(ColumnAttribute)) &&
            string.Equals(x.GetCustomAttribute<ColumnAttribute>()?.Name,
                orderByProperty, StringComparison.CurrentCultureIgnoreCase) ||
            Attribute.IsDefined(x, typeof(JsonPropertyNameAttribute)) &&
            string.Equals(x.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name,
                orderByProperty, StringComparison.CurrentCultureIgnoreCase));
        if (property == null)
        {
            throw new InvalidOperationException($"Sort.FieldName '{orderByProperty}' property does not exist");
        }

        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        var resultExpression = Expression.Call(typeof(Queryable), command, [type, property.PropertyType],
            source.Expression, Expression.Quote(orderByExpression));

        return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(resultExpression);
    }

    public static IQueryable<TEntity> WhereIfParameterNotNull<TEntity, TParameterType>(this IQueryable<TEntity> source,
        TParameterType? parameterValue,
        Expression<Func<TEntity, bool>> predicate)
    {
        if (parameterValue == null)
        {
            return source;
        }

        return source.Where(predicate);
    }

    public static IQueryable<TEntity> WhereIfParameterNotNull<TEntity>(this IQueryable<TEntity> source,
        string? stringParameterValue,
        Expression<Func<TEntity, bool>> predicate)
    {
        if (string.IsNullOrEmpty(stringParameterValue))
        {
            return source;
        }

        return source.Where(predicate);
    }
}