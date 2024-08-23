using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Data;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Internationalization;
using SocialMedia.Domain.Common;

namespace SocialMedia.Infrastructure.Persistence.Common.Data;

public class EfGenericQuery : IGenericQuery
{
    private readonly AppDbContext _db;
    private readonly ILanguageResource _resource;

    public EfGenericQuery(AppDbContext db, ILanguageResource resource)
    {
        _db = db;
        _resource = resource;
    }

    public async Task<T> GetByIdAsync<T>(params object[] id) where T : class
    {
        return await _db.Set<T>().FindAsync(id);
    }

    public async Task<IList<T>> GetByIdAsync<T>(IEnumerable<Guid> ids) where T : EntityWithGuid
    {
        return await _db.Set<T>()
            .Where(x => ids.Any(xx => xx == x.Id))
            .ToListAsync();
    }

    public async Task<T> GetByIdOrThrowAsync<T>(params object[] id) where T : class
    {
        var result = await GetByIdAsync<T>(id);
        return result ??
               throw new EntityNotFoundException(_resource.EntityWithIdNotFound(nameof(T), id.ToString()));
    }

    public async Task<bool> ExistsByIdAsync<T>(params object[] id) where T : class
    {
        var item = await GetByIdAsync<T>(id);
        return item != null;
    }

    public async Task<IList<T>> GetAllAsync<T>() where T : class
    {
        return await _db.Set<T>().ToListAsync();
    }

    public async Task<long> CountAll<T>() where T : class
    {
        return await _db.Set<T>().LongCountAsync();
    }

    public Task<T> GetOneEntityByPropertyAsync<T>(string propertyName, object propertyValue) where T : class
    {
        return _db.Set<T>().FirstOrDefaultAsync(e => Equals(EF.Property<object>(e, propertyName), propertyValue));
    }

    public async Task<IList<T>> GetEntitiesByPropertyAsync<T>(string propertyName, object propertyValue) where T : class
    {
        return await _db.Set<T>().Where(e => Equals(EF.Property<object>(e, propertyName), propertyValue)).ToListAsync();

    }
}