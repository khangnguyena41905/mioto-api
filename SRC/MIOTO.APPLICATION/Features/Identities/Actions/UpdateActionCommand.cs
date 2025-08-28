using FluentValidation;
using MIOTO.CONTRACT.Abstractions.Message;
using MIOTO.CONTRACT.Abstractions.Shared;
using MIOTO.DOMAIN;
using MIOTO.DOMAIN.Entities.Identity;
using MIOTO.DOMAIN.Abstractions.Repositories.Identity;
using Action = MIOTO.DOMAIN.Entities.Identity.Action;

namespace MIOTO.APPLICATION.Features.Identities.Actions;

public record UpdateActionCommand(
    string Id,
    string Name,
    int? SortOrder,
    bool? IsActive
) : ICommand<Action>;

internal class UpdateActionCommandValidator : AbstractValidator<UpdateActionCommand>
{
    public UpdateActionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id không được để trống");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên action không được để trống")
            .MaximumLength(256);

        RuleFor(x => x.SortOrder)
            .GreaterThanOrEqualTo(0).When(x => x.SortOrder.HasValue);
    }
}

internal class UpdateActionCommandHandler : ICommandHandler<UpdateActionCommand, Action>
{
    private readonly IActionRepository _actionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateActionCommandHandler(IActionRepository actionRepository, IUnitOfWork unitOfWork)
    {
        _actionRepository = actionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Action>> Handle(UpdateActionCommand request, CancellationToken cancellationToken)
    {
        var action = await _actionRepository.FindByIdAsync(request.Id);

        if (action is null)
            return Result.Failure<Action>(new Error("404", "Action not found"));

        // Kiểm tra trùng tên (ngoại trừ chính nó)
        var existing = await _actionRepository.FindSingleAsync(x => x.Name == request.Name && x.Id != request.Id);
        if (existing is not null)
            return Result.Failure<Action>(new Error("400", "Another action with same name already exists"));

        action.Name = request.Name;
        action.SortOrder = request.SortOrder;
        action.IsActive = request.IsActive;

        await _actionRepository.UpdateAsync(action);
        await _unitOfWork.CommitAsync();

        return Result.Success(action);
    }
}
