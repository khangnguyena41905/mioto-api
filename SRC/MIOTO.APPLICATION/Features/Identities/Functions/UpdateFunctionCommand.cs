using FluentValidation;
using MIOTO.CONTRACT.Abstractions.Message;
using MIOTO.CONTRACT.Abstractions.Shared;
using MIOTO.DOMAIN;
using MIOTO.DOMAIN.Abstractions.Repositories.Identity;
using MIOTO.DOMAIN.Entities.Identity;

namespace MIOTO.APPLICATION.Features.Identities.Functions;

public record UpdateFunctionCommand(
    string Id,
    string Name,
    string Url,
    string? ParrentId,
    int? SortOrder,
    string? CssClass,
    bool? IsActive,
    ICollection<ActionInFunction>? ActionInFunctions
) : ICommand<Function>;

public class UpdateFunctionCommandValidator : AbstractValidator<UpdateFunctionCommand>
{
    public UpdateFunctionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id không được để trống");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name không được để trống");
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url không được để trống");
    }
}

internal class UpdateFunctionCommandHandler : ICommandHandler<UpdateFunctionCommand, Function>
{
    private readonly IFunctionRepository _functionRepository;
    private readonly IActionInFunctionRepository _actionInFunctionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFunctionCommandHandler(
        IFunctionRepository functionRepository,
        IActionInFunctionRepository actionInFunctionRepository,
        IUnitOfWork unitOfWork)
    {
        _functionRepository = functionRepository;
        _actionInFunctionRepository = actionInFunctionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Function>> Handle(UpdateFunctionCommand request, CancellationToken cancellationToken)
    {
        // 1. Lấy Function hiện tại
        var function = await _functionRepository.FindByIdAsync(request.Id, f => f.ActionInFunctions);
        if (function is null)
            return Result.Failure<Function>(Error.None);

        // 2. Cập nhật thông tin cơ bản
        function.Name = request.Name;
        function.Url = request.Url;
        function.ParrentId = request.ParrentId;
        function.SortOrder = request.SortOrder;
        function.CssClass = request.CssClass;
        function.IsActive = request.IsActive;

        // 3. Xóa toàn bộ ActionInFunctions cũ
        if (function.ActionInFunctions?.Any() == true)
        {
            await _actionInFunctionRepository.RemoveListAsync(function.ActionInFunctions.ToList());
        }

        // 4. Thêm ActionInFunctions mới (nếu có)
        if (request.ActionInFunctions?.Any() == true)
        {
            foreach (var aif in request.ActionInFunctions)
            {
                aif.FunctionId = function.Id; // gán FK nếu cần
            }

            await _actionInFunctionRepository.AddRangeAsync(request.ActionInFunctions.ToList());
        }

        // 5. Cập nhật Function
        await _functionRepository.UpdateAsync(function);

        // 6. Commit transaction
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(function);
    }
}
