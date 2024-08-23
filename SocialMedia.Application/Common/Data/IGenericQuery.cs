using SocialMedia.Domain.Common;

namespace SocialMedia.Application.Common.Data;

public interface IGenericQuery
{
    Task<T> GetByIdAsync<T>(params object[] id) where T : class;
    Task<IList<T>> GetByIdAsync<T>(IEnumerable<Guid> ids) where T : EntityWithGuid;
    Task<T> GetByIdOrThrowAsync<T>(params object[] id) where T : class;
    Task<bool> ExistsByIdAsync<T>(params object[] id) where T : class;
    Task<IList<T>> GetAllAsync<T>() where T : class;
    Task<long> CountAll<T>() where T : class;
    Task<T> GetOneEntityByPropertyAsync<T>(string propertyName, object propertyValue) where T : class;
    Task<IList<T>> GetEntitiesByPropertyAsync<T>(string propertyName, object propertyValue) where T : class;
}