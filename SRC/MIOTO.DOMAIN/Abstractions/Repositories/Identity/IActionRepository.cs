using Action = MIOTO.DOMAIN.Entities.Identity.Action;

namespace MIOTO.DOMAIN.Abstractions.Repositories.Identity;

public interface IActionRepository : IRepositoryBase<Action, string>
{
    
}