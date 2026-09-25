namespace GameDatabase.DTOs
{
    public class GenreQueryParameters
    {
        public string? GenreName {get; set;}
        public string? Search {get; set;}
        public int PageNumber {get; set;} = 1;
        public int PageSize {get; set;} = 20;
    }
}