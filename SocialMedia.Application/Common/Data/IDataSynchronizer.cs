using System.Linq.Expressions;

namespace SocialMedia.Application.Common.Data;

public interface IDataSynchronizer
{
    public Task<ISyncOp<TEntity>> InitAsync<TEntity>(IEnumerable<TEntity> source, Expression<Func<TEntity, bool>> pDbFilter) 
        where TEntity : class;
}

public interface ISyncOp<out TEntity> where TEntity : class
{
    public IReadOnlyList<TEntity> Deleted { get; }
    public IReadOnlyList<TEntity> Inserted { get; }
    public IReadOnlyList<TEntity> Updated { get; }
}