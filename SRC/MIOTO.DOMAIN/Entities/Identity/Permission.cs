using System.ComponentModel.DataAnnotations;
namespace MIOTO.DOMAIN.Entities.Identity;

public class Permission
{
    public Guid RoleId { get; set; }
    [MaxLength(50)]
    public string FunctionId { get; set; }
    [MaxLength(50)]
    public string ActionId { get; set; }
}