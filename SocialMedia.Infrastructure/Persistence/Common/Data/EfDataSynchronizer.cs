using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Data;

namespace SocialMedia.Infrastructure.Persistence.Common.Data;

public class EfDataSynchronizer : IDataSynchronizer
{
    private readonly AppDbContext _dbContext;

    public EfDataSynchronizer(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ISyncOp<TEntity>> InitAsync<TEntity>(IEnumerable<TEntity> source,
        Expression<Func<TEntity, bool>> pDbFilter)
        where TEntity : class
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        EfSyncOp<TEntity> result = new(_dbContext, source, pDbFilter);
        await result.InitAsync();
        return result;
    }
}

internal class EfSyncOp<TEntity> : ISyncOp<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;
    private readonly IEnumerable<TEntity> _source;
    private readonly Expression<Func<TEntity, bool>> _pDbFilter;

    private readonly List<TEntity> _deleted = new();
    private readonly List<TEntity> _inserted = new();
    private readonly List<TEntity> _updated = new();

    public IReadOnlyList<TEntity> Deleted { get; private set; }
    public IReadOnlyList<TEntity> Inserted { get; private set; }
    public IReadOnlyList<TEntity> Updated { get; private set; }

    public EfSyncOp(AppDbContext dbContext, IEnumerable<TEntity> source,
        Expression<Func<TEntity, bool>> pDbFilter)
    {
        _dbContext = dbContext;
        _source = source;
        _pDbFilter = pDbFilter;
    }

    public async Task InitAsync()
    {
        List<TEntity> dbList = await _dbContext.Set<TEntity>().Where(_pDbFilter).ToListAsync();

        foreach (TEntity entity in _source)
        {
            var entityInDb = dbList.FirstOrDefault(e => e.Equals(entity));

            if (entityInDb == null)
            {
                _inserted.Add(entity);
            }
            else
            {
                _updated.Add(entity);
            }
        }

        foreach (TEntity entity in dbList)
        {
            bool exists = _source.FirstOrDefault(t => entity.Equals(t)) != null;
            if (!exists)
            {
                _deleted.Add(entity);
            }
        }

        Deleted = new ReadOnlyCollection<TEntity>(_deleted);
        Updated = new ReadOnlyCollection<TEntity>(_updated);
        Inserted = new ReadOnlyCollection<TEntity>(_inserted);
    }
}