using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MIOTO.DOMAIN.Entities.Identity;
using Action = MIOTO.DOMAIN.Entities.Identity.Action;

namespace MIOTO.PERSISTENCE;

public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder builder)
        => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<Action> Actions { get; set; }

    public DbSet<Function> Functions { get; set; }

    public DbSet<ActionInFunction> ActionInFunctions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
}