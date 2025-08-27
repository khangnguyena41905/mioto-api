using MIOTO.DOMAIN.Abstractions.Repositories.Identity;
using MIOTO.DOMAIN.Entities.Identity;

namespace MIOTO.PERSISTENCE.Repositories.Identity;

public class AppRoleRepository: RepositoryBase<AppRole, Guid>,IAppRoleRepository
{
    public AppRoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}