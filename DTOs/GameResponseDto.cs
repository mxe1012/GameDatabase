using GameDatabase.Entites;

namespace GameDatabase.DTOs
{
    public class GameResponseDto : IAdutiable
    {
        public int GameId {get; set;}
        public string GameName { get; set; }
        public decimal RetailPrice { get; set; }
        public DateOnly ReleaseDate {get; set;}
        public bool IsForSale {get; set;} = true;
        public int DeveloperId { get; set; }
        public int GenreId { get; set; }
        public int EngineId { get; set; }
        public string CreatedBy {get; set;}
        public bool IsDeleted {get; set;}
        public string? DeletedBy {get; set;}
        public DateTime DeletedAt {get; set;}
    }
}