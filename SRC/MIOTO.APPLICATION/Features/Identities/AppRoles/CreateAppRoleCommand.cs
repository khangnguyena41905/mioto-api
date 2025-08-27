using FluentValidation;
using MIOTO.CONTRACT.Abstractions.Message;
using MIOTO.CONTRACT.Abstractions.Shared;
using MIOTO.DOMAIN;
using MIOTO.DOMAIN.Entities.Identity;
using MIOTO.DOMAIN.Abstractions.Repositories.Identity;

namespace MIOTO.APPLICATION.Features.Identities.AppRoles;

// Command để tạo AppRole
public record CreateAppRoleCommand(
    string Name,
    string RoleCode,
    string Description
) : ICommand<AppRole>;

// Validator để kiểm tra dữ liệu đầu vào
internal class CreateAppRoleCommandValidator : AbstractValidator<CreateAppRoleCommand>
{
    public CreateAppRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên vai trò không được để trống")
            .MaximumLength(256);

        RuleFor(x => x.RoleCode)
            .NotEmpty().WithMessage("Mã vai trò không được để trống")
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}

// Handler để xử lý logic tạo mới
internal class CreateAppRoleCommandHandler : ICommandHandler<CreateAppRoleCommand, AppRole>
{
    private readonly IAppRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppRoleCommandHandler(IUnitOfWork unitOfWork,IAppRoleRepository roleRepository)
    {
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
    }

    public async Task<Result<AppRole>> Handle(CreateAppRoleCommand request, CancellationToken cancellationToken)
    {
        // Kiểm tra trùng mã vai trò
        var existing = await _roleRepository
            .FindSingleAsync(x => x.RoleCode == request.RoleCode);

        if (existing is not null)
        {
            return Result.Failure<AppRole>(new Error("400","Existed an existing app role"));
        }

        var role = new AppRole
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            NormalizedName = request.Name.ToUpperInvariant(),
            RoleCode = request.RoleCode,
            Description = request.Description
        };
        
        var result = await  _roleRepository.AddAsync(role);
        await _unitOfWork.CommitAsync();
        return Result.Success(result);
    }
}
