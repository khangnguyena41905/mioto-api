using MIOTO.DOMAIN.Abstractions.Repositories;
using MIOTO.DOMAIN.Abstractions.Repositories.Identity;
using Action = MIOTO.DOMAIN.Entities.Identity.Action;

namespace MIOTO.PERSISTENCE.Repositories.Identity;

public class ActionRepository : RepositoryBase<Action, string>,IActionRepository
{
    public ActionRepository(ApplicationDbContext context) : base(context)
    {
    }
}