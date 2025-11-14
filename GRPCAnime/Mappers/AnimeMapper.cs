using GRPCAnime.Infrastructure.Documents;
using GRPCAnime.Models;

namespace GRPCAnime.Mappers;

public static class AnimeMapper
{
    public static AnimeDocument ToDocument(this AnimeModel anime)
        {
            return new AnimeDocument
            {
                Id = anime.Id,
                Name = anime.Name,
                Author = anime.Author,
                Genre = anime.Genre,
                First_Broadcast = anime.First_Broadcast,
                Last_Broadcast = anime.Last_Broadcast,
                Has_Manga = anime.Has_Manga
            };
        }

        public static AnimeModel ToDomain(this AnimeDocument doc)
        {
            return new AnimeModel
            {
                Id = doc.Id,
                Name = doc.Name,
                Author = doc.Author,
                Genre = doc.Genre,
                First_Broadcast = doc.First_Broadcast,
                Last_Broadcast = doc.Last_Broadcast,
                Has_Manga = doc.Has_Manga
            };
        }
}