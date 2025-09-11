using MIOTO.CONTRACT.Abstractions.Message;
using MIOTO.CONTRACT.Abstractions.Shared;
using MIOTO.DOMAIN.Abstractions.Repositories.Identity;
using MIOTO.DOMAIN.Entities.Identity;

namespace MIOTO.APPLICATION.Features.Identities.AppRoles;

public record GetAppRoleByIdQuery(Guid Id) : IQuery<AppRole>;

internal class GetAppRoleByIdQueryHandler 
    : IQueryHandler<GetAppRoleByIdQuery, AppRole>
{
    private readonly IAppRoleRepository _appRoleRepository;

    public GetAppRoleByIdQueryHandler(IAppRoleRepository appRoleRepository)
    {
        _appRoleRepository = appRoleRepository;
    }

    public async Task<Result<AppRole>> Handle(
        GetAppRoleByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var role = await _appRoleRepository.FindByIdAsync(request.Id);

        if (role is null)
            return Result.Failure<AppRole>(Error.NullValue);

        return Result.Success(role);
    }
}