namespace MiniBank.Domain;


public interface IEntity
{

}


public abstract class EntityBase : IEntity
{
    public Guid EntityId { get; set; }
}

public abstract class AuditableEntity : EntityBase
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

