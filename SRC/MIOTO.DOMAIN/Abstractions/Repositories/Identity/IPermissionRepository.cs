using MIOTO.DOMAIN.Entities.Identity;

namespace MIOTO.DOMAIN.Abstractions.Repositories.Identity;

public interface IPermissionRepository
{
    Task<IEnumerable<Permission>> GetByAppRoleIdAsync(Guid roleId);
    Task RemoveByAppRoleIdAsync(Guid roleId);
}