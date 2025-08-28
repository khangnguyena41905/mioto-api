using MIOTO.DOMAIN.Abstractions.Repositories;
using MIOTO.DOMAIN.Abstractions.Repositories.Identity;
using MIOTO.DOMAIN.Entities.Identity;

namespace MIOTO.PERSISTENCE.Repositories.Identity;

public class FunctionRepository : RepositoryBase<Function, string>, IFunctionRepository
{
    public FunctionRepository(ApplicationDbContext context) : base(context)
    {
    }
}