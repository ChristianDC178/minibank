using MediatR;
using System.Text.Json.Serialization;

namespace MiniBank.CustomersSrv.Application.Dtos.Requests;

public record CustomerIdRequest : IRequest<string>
{
    [JsonPropertyName("customer_id")]
    public Guid CustomerId { get; set; }
}