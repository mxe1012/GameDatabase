namespace GameDatabase.Entites
{
    public class Game
    {
        public int GameId {get; set;}
        public string GameName {get; set;}
        public decimal RetailPrice {get; set;}
        public DateOnly ReleaseDate {get; set;}
        public bool IsForSale {get; set;} = true;
        public int DeveloperId {get; set;}
        public Developer Developer {get; set;}
        public int GenreId {get; set;}
        public Genre Genre {get; set;}
        public int EngineId {get; set;}
        public Engine Engine {get; set;}
        public string? CreatedBy {get; set;}
    }
}