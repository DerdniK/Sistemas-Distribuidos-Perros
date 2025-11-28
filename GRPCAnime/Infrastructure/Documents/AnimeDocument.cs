using System.Text.Json.Serialization;


namespace GRPCAnime.Infrastructure.Documents;

public class AnimeDocument
{
    public AnimeDocument()
    {
        Id = Ulid.NewUlid().ToString(); // ← autogenera al crear
    }

    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
    [JsonPropertyName("author")]
    public string Author { get; set; } = default!;
    [JsonPropertyName("genre")]
    public string Genre { get; set; } = default!;
    [JsonPropertyName("first_broadcast")]
    public DateTime First_Broadcast { get; set; }
    [JsonPropertyName("last_broadcast")]
    public DateTime Last_Broadcast { get; set; }
    [JsonPropertyName("has_manga")]
    public bool Has_Manga { get; set; }
}