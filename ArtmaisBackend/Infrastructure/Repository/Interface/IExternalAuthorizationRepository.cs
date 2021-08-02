using ArtmaisBackend.Core.Entities;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IExternalAuthorizationRepository
    {
        ExternalAuthorization Create(string externalAuthorizationId, long userId);
        ExternalAuthorization GetExternalAuthorizationByExternalAuthorizationId(string externalAuthorizationId);
    }
}
