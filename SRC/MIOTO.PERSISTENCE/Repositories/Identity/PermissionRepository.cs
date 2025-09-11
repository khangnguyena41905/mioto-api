using Microsoft.EntityFrameworkCore;
using MIOTO.DOMAIN.Abstractions.Repositories.Identity;
using MIOTO.DOMAIN.Entities.Identity;

namespace MIOTO.PERSISTENCE.Repositories.Identity;

public class PermissionRepository : IPermissionRepository
{
    private readonly ApplicationDbContext _context;

    public PermissionRepository(ApplicationDbContext context) => _context = context;
    
    public async Task<IEnumerable<Permission>> GetByAppRoleIdAsync(Guid roleId)
    {
        var query = _context.Permissions;
        return await _context.Permissions.Where(x => x.RoleId == roleId).ToListAsync();
    }

    public async Task RemoveByAppRoleIdAsync(Guid roleId)
    {
        var needRemoveList = await _context.Permissions.Where(x => x.RoleId == roleId).ToListAsync();
        _context.RemoveRange(needRemoveList);
    }
}