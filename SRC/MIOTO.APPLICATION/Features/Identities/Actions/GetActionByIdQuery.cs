using MIOTO.CONTRACT.Abstractions.Message;
using MIOTO.CONTRACT.Abstractions.Shared;
using MIOTO.DOMAIN.Entities.Identity;
using MIOTO.DOMAIN.Abstractions.Repositories.Identity;
using Action = MIOTO.DOMAIN.Entities.Identity.Action;

namespace MIOTO.APPLICATION.Features.Identities.Actions;

public record GetActionByIdQuery(string Id) : IQuery<Action>;

internal class GetActionByIdQueryHandler : IQueryHandler<GetActionByIdQuery, Action>
{
    private readonly IActionRepository _actionRepository;

    public GetActionByIdQueryHandler(IActionRepository actionRepository)
    {
        _actionRepository = actionRepository;
    }

    public async Task<Result<Action>> Handle(GetActionByIdQuery request, CancellationToken cancellationToken)
    {
        var action = await _actionRepository.FindByIdAsync(request.Id);

        if (action is null)
            return Result.Failure<Action>(new Error("404", "Action not found"));

        return Result.Success(action);
    }
}