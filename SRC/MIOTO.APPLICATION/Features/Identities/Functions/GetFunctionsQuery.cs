using MIOTO.CONTRACT.Abstractions.Message;
using MIOTO.CONTRACT.Abstractions.Shared;
using MIOTO.DOMAIN.Abstractions.Repositories.Identity;
using MIOTO.DOMAIN.Entities.Identity;

namespace MIOTO.APPLICATION.Features.Identities.Functions;

public record GetFunctionsQuery() : IQuery<IEnumerable<Function>>;

internal class GetFunctionsQueryHandler : IQueryHandler<GetFunctionsQuery, IEnumerable<Function>>
{
    private readonly IFunctionRepository _functionRepository;

    public GetFunctionsQueryHandler(IFunctionRepository functionRepository)
    {
        _functionRepository = functionRepository;
    }

    public async Task<Result<IEnumerable<Function>>> Handle(GetFunctionsQuery request, CancellationToken cancellationToken)
    {
        var functions = await _functionRepository.FindAllAsync();
        return Result.Success(functions);
    }
}
