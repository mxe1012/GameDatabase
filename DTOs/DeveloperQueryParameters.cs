namespace GameDatabase.DTOs
{
    public class DeveloperQueryParameters
    {
        public string? GeneralSearch {get; set;}
        public string? DeveloperName {get; set;}
        public string? City {get; set;}
        public string? State {get; set;}
        public string? CountryCode {get; set;}
        public int? YearFounded {get; set;}
        public bool? IsActive {get; set;}
        public int PageNumber {get; set;} = 1;
        public int PageSize {get; set;} = 20;
    }
}