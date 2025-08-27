using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIOTO.DOMAIN.Entities.Identity;
using MIOTO.PERSISTENCE.Contants;

namespace MIOTO.PERSISTENCE.Configurations;

public class PermissionConfiguration: IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permissions);

        // Composite key: RoleId + FunctionId + ActionId
        builder.HasKey(x => new { x.RoleId, x.FunctionId, x.ActionId });

        // Optional: Add constraints if needed
        builder.Property(x => x.FunctionId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ActionId)
            .IsRequired()
            .HasMaxLength(50);
    }
}