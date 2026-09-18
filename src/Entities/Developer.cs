namespace GameDatabase.Entites 
{
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
}