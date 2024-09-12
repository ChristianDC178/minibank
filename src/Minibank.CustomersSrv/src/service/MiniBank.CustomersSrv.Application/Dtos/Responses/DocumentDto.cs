using System.Text.Json.Serialization;

namespace MiniBank.CustomersSrv.Application.Dtos.Responses;

public class DocumentDto
{
    [JsonPropertyName("document_id")]
    public int DocumentId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}


