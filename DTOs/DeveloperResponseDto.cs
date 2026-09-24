using GameDatabase.Entites;

namespace GameDatabase.DTOs
{
    public class DeveloperResponseDto : IAdutiable
    {   
        public int DeveloperId {get; set;}
        public string DeveloperName {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string CountryCode {get; set;}
        public int YearFounded {get; set;}
        public bool IsActive {get; set;} = true;
        public string CreatedBy {get; set;}
        public bool IsDeleted {get; set;}
        public string? DeletedBy {get; set;}
        public DateTime? DeletedAt {get; set;}
    }
}