using MIOTO.DOMAIN.Entities.Identity;

namespace MIOTO.DOMAIN.Abstractions.Repositories.Identity;

public interface IActionInFunctionRepository
{
    Task<IEnumerable<ActionInFunction>> FindByFunctionIdAsync(string functionId);
    
    Task RemoveListAsync(List<ActionInFunction> actionInFunctions);
    Task AddRangeAsync(List<ActionInFunction> list);
}