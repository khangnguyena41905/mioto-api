using MIOTO.DOMAIN.Entities.Identity;

namespace MIOTO.APPLICATION.Models.Requests.Funtions;

public class UpdateFunctionRequest
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string? ParrentId { get; set; }
    public int? SortOrder { get; set; }
    public string? CssClass { get; set; }
    public bool? IsActive { get; set; }
    public ICollection<ActionInFunction>? ActionInFunctions { get; set; }
}