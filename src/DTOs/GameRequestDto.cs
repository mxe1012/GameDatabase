namespace GameDatabase.DTOs
{
    public class GameRequestDto
    {
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public DateOnly ReleaseDate {get; set;}
        public int DeveloperId { get; set; }
        public int GenreId { get; set; }
        public int EngineId { get; set; }
    }
}