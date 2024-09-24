using MediatR;
using MiniBank.CustomersSrv.Application.Dtos.Responses;
using System.Text.Json.Serialization;

namespace MiniBank.CustomersSrv.Application.Dtos.Requests;

public record CreateCustomerAddressRequest : IRequest<Result<CreateCustomerResponse>>
{
    public Guid CustomerId { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }
    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("street_name")]
    public string StreetName { get; set; }
    [JsonPropertyName("street_number")]
    public int StreetNumber { get; set; }

    [JsonPropertyName("zip_code")]
    public string ZipCode { get; set; }

}
