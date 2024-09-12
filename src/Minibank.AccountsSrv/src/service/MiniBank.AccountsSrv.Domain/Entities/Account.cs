namespace MiniBank.AccountsSrv.Domain.Entities;

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

public class Account : AuditableEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}

public class Address
{
    public string Street { get; set; }    
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}
