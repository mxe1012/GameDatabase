using System.ComponentModel.DataAnnotations;

namespace GameDatabase.Entites 
{
    public class Developer : IAdutiable
    {
        public int DeveloperId {get; set;}
        public string DeveloperName {get; set;}
        public string City {get; set;}
        [MaxLength(2)]
        public string State {get; set;}
        [MaxLength(3)]
        public string CountryCode {get; set;}
        public int YearFounded {get; set;}
        public bool IsActive {get; set;} = true;
        public string? CreatedBy {get; set;}
        public bool IsDeleted {get; set;}
        public string? DeletedBy {get; set;}
        public DateTime? DeletedAt {get; set;}
    }
}