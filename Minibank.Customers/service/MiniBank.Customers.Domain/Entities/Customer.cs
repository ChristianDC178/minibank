using MiniBank.Domain;

namespace MiniBank.CustomersSrv.Domain.Entities;

public class Customer : AuditableEntity
{
    public Customer(
        string firstName,
        string lastName,
        DateTime birthDate,
        Document document
        )
    {
        FirstName = firstName;
        LastName = lastName;
        Document = document;
        
        EntityId = Guid.NewGuid();
    }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Address Address { get; set; } 
    public Document Document { get; set; }

}