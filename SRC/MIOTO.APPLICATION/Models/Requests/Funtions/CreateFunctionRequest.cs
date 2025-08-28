namespace MIOTO.APPLICATION.Models.Requests.Funtions;

public class CreateFunctionRequest
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string? ParrentId { get; set; }
    public int? SortOrder { get; set; }
    public string? CssClass { get; set; }
    public bool? IsActive { get; set; }
}