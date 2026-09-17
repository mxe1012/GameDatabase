public class Developer
{
    public int DeveloperId {get; set;}
    public string DeveloperName {get; set;}
    public string City {get; set;}
    public string State {get; set;}
    public string CountryCode {get; set;}
    public int YearFounded {get; set;}
    public bool IsActive {get; set;} = true;
}

public class Genre
{
    public int GenreId {get; set;}
    public string GenreName {get; set;}
}

public class Engine
{
    public int EngineId {get; set;}
    public string EngineName {get; set;}
    public bool IsOpenSource {get; set;} = true;
}

public class Game
{
    public int GameId {get; set;}
    public string GameName {get; set;}
    public decimal Price {get; set;}
    public DateOnly ReleaseDate {get; set;}
    public int DeveloperId {get; set;}
    public Developer Developer {get; set;}
    public int GenreId {get; set;}
    public Genre Genre {get; set;}
    public int EngineId {get; set;}
    public Engine Engine {get; set;}
}