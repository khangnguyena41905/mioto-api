using MIOTO.CONTRACT.Abstractions.Message;
using MIOTO.CONTRACT.Abstractions.Shared;
using MIOTO.DOMAIN.Abstractions.Repositories.Identity;
using MIOTO.DOMAIN.Entities.Identity;

namespace MIOTO.APPLICATION.Features.Identities.Functions;

public record GetFunctionByIdQuery(string Id) : IQuery<Function>;

internal class GetFunctionByIdQueryHandler : IQueryHandler<GetFunctionByIdQuery, Function>
{
    private readonly IFunctionRepository _functionRepository;

    public GetFunctionByIdQueryHandler(IFunctionRepository functionRepository)
    {
        _functionRepository = functionRepository;
    }

    public async Task<Result<Function>> Handle(GetFunctionByIdQuery request, CancellationToken cancellationToken)
    {
        var func = await _functionRepository.FindByIdAsync(request.Id);

        if (func is null)
            return Result.Failure<Function>(new Error("404", "Function not found"));

        return Result.Success(func);
    }
}
