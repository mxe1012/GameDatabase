using System.ComponentModel.DataAnnotations;

namespace GameDatabase.DTOs
{
    public class DeveloperRequestDto
    {
        public string DeveloperName {get; set;}
        public string City {get; set;}
        [MaxLength(3)]
        public string State {get; set;}
        [MaxLength(3)]
        public string CountryCode {get; set;}
        public int YearFounded {get; set;}
        public bool IsActive {get; set;} = true;
    }
}