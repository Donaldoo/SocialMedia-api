using System.Linq.Expressions;
using X.PagedList;

namespace SocialMedia.Infrastructure.Persistence.Common.Extensions;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> OrderByMember<T>(this IQueryable<T> source, string memberPath,
        bool descending)
    {
        var parameter = Expression.Parameter(typeof(T), "item");
        var member = memberPath.Split('.')
            .Aggregate((Expression)parameter, Expression.PropertyOrField);
        var keySelector = Expression.Lambda(member, parameter);
        var methodCall = Expression.Call(
            typeof(Queryable), descending ? "OrderByDescending" : "OrderBy",
            new[] { parameter.Type, member.Type },
            source.Expression, Expression.Quote(keySelector));
        return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
    }

    public static IOrderedQueryable<TSource> OrderByMember<TSource, TKey>(this IQueryable<TSource> source,
        Expression<Func<TSource, TKey>> keySelector,
        bool descending)
    {
        return descending
            ? source.OrderByDescending(keySelector)
            : source.OrderBy(keySelector);
    }

    public static async Task<IPagedList<T>> ToOrderedPagedListAsync<T>(this IQueryable<T> query,
        int pageNumber, int pageSize, string orderColumn, bool orderDescending = false)
    {
        if (string.IsNullOrWhiteSpace(orderColumn))
            orderColumn = "CreatedAt";

        query = query.OrderByMember(orderColumn, orderDescending);
        
        return await query.ToPagedListAsync(pageNumber, pageSize);
    }
}