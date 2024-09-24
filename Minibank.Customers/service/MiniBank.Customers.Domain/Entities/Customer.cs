namespace MiniBank.CustomersSrv.Domain.Entities;

public class Customer //: AuditableEntity
{
    public Customer(
        string firstName,
        string lastName,
        Document document
        )
    {
        FirstName = firstName;
        LastName = lastName;
        Document = document;
        EntityId = Guid.NewGuid();
    }

    public Guid EntityId { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Address Address { get; set; }
    public Document Document { get; set; }

}

public class Document
{
    public int DocumentId { get; set; }
    public string Type { get; set; }
}

public class Address
{
    public string City { get; set; }
    public string State { get; set; }
    public string StreetName { get; set; }
    public int StreetNumber { get; set; }
    public string ZipCode { get; set; }

}

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}

