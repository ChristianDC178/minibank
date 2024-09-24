using System.Text.Json.Serialization;

namespace MiniBank.CustomersSrv.Application.Dtos.Responses;

public class CreateCustomerResponse
{

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("birth_date")]
    public DateTime BirthDate { get; set; }

    [JsonPropertyName("document")]
    public DocumentDto Document { get; set; }

}


