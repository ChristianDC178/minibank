using MediatR;
using MiniBank.CustomersSrv.Application.Dtos.Responses;
using MiniBank.ResultPattern;
using System.Text.Json.Serialization;

namespace MiniBank.CustomersSrv.Application.Dtos.Requests;

public record CreateCustomerRequest : IRequest<Result<CustomerResponse>>
{

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("birth_date")]
    public DateTime BirthDate { get; set; }

    [JsonPropertyName("document")]
    public DocumentDto Document { get; set; }

}
