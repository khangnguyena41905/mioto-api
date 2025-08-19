namespace MIOTO.DOMAIN.Abstractions.Entities;

public interface IAuditableEntity<TKey> : IEntity<TKey> 
{
    DateTime CreatedDate { get; set; }
    Guid CreatedBy { get; set; }
    DateTime? UpdatedDate { get; set; }
    Guid? UpdatedBy { get; set; }
}
