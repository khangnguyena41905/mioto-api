using MIOTO.CONTRACT.Abstractions.Message;
using MIOTO.CONTRACT.Abstractions.Shared;
using MIOTO.DOMAIN.Entities.Identity;
using MIOTO.DOMAIN.Abstractions.Repositories.Identity;
using Action = MIOTO.DOMAIN.Entities.Identity.Action;

namespace MIOTO.APPLICATION.Features.Identities.Actions;

public record GetActionsQuery() : IQuery<IEnumerable<Action>>;

internal class GetActionsQueryHandler : IQueryHandler<GetActionsQuery, IEnumerable<Action>>
{
    private readonly IActionRepository _actionRepository;

    public GetActionsQueryHandler(IActionRepository actionRepository)
    {
        _actionRepository = actionRepository;
    }

    public async Task<Result<IEnumerable<Action>>> Handle(GetActionsQuery request, CancellationToken cancellationToken)
    {
        var actions = await _actionRepository.FindAllAsync();
        return Result.Success(actions);
    }
}