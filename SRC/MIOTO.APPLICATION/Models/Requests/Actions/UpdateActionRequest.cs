namespace MIOTO.APPLICATION.Models.Requests.Actions;

public class UpdateActionRequest
{
    public string Name { get; set; }
    public int? SortOrder { get; set; }
    public bool? IsActive { get; set; }
}