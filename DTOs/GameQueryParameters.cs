namespace GameDatabase.DTOs
{
    public class GameQueryParameters
    {
        public string? GeneralSearch {get; set;}
        public string? GameName {get; set;}
        public decimal? RetailPrice {get; set;}
        public decimal? PriceLessThan {get; set;}
        public decimal? PriceGreaterThan {get; set;}
        public DateOnly? ReleaseDate {get; set;}
        public DateOnly? ReleasedOnOrBefore {get; set;}
        public DateOnly? ReleasedOnOrAfter {get; set;}
        public bool? IsForSale {get; set;}
        public int? DeveloperId {get; set;}
        public int? GenreId {get; set;}
        public int? EngineId {get; set;}
        public int PageNumber {get; set;} = 1;
        public int PageSize {get; set;} = 20;
    }
}