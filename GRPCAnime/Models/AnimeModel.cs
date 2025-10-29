using GRPCAnime.Repositories;

namespace GRPCAnime.Models;

public class AnimeModel
{
    public string Id { get; set; } = Ulid.NewUlid().ToString();
    public string Name { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Genre { get; set; } = default!;
    public DateTime First_Broadcast { get; set; }
    public DateTime Last_Broadcast { get; set; }
    public bool Has_Manga { get; set; }
}