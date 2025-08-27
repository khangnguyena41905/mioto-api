using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIOTO.DOMAIN.Entities.Identity;
using MIOTO.PERSISTENCE.Contants;

namespace MIOTO.PERSISTENCE.Configurations;

internal sealed class ActionInFunctionConfiguration : IEntityTypeConfiguration<ActionInFunction>
{
    public void Configure(EntityTypeBuilder<ActionInFunction> builder)
    {
        builder.ToTable(TableNames.ActionInFunctions);

        // Thiết lập khóa chính là composite key gồm ActionId và FunctionId
        builder.HasKey(x => new { x.ActionId, x.FunctionId });
    }
}