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
    bool? IsActive
) : ICommand<Function>;

internal class UpdateFunctionCommandValidator : AbstractValidator<UpdateFunctionCommand>
{
    public UpdateFunctionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
        RuleFor(x => x.Url).NotEmpty().MaximumLength(500);
        RuleFor(x => x.CssClass).MaximumLength(100);
    }
}

internal class UpdateFunctionCommandHandler : ICommandHandler<UpdateFunctionCommand, Function>
{
    private readonly IFunctionRepository _functionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFunctionCommandHandler(IFunctionRepository functionRepository, IUnitOfWork unitOfWork)
    {
        _functionRepository = functionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Function>> Handle(UpdateFunctionCommand request, CancellationToken cancellationToken)
    {
        var func = await _functionRepository.FindByIdAsync(request.Id);
        if (func is null)
            return Result.Failure<Function>(new Error("404", "Function not found"));

        var existing = await _functionRepository.FindSingleAsync(x => x.Name == request.Name && x.Id != request.Id);
        if (existing is not null)
            return Result.Failure<Function>(new Error("400", "Another function with same name already exists"));

        func.Name = request.Name;
        func.Url = request.Url;
        func.ParrentId = request.ParrentId;
        func.SortOrder = request.SortOrder;
        func.CssClass = request.CssClass;
        func.IsActive = request.IsActive;

        await _functionRepository.UpdateAsync(func);
        await _unitOfWork.CommitAsync();

        return Result.Success(func);
    }
}
