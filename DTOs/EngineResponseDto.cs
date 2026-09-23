namespace GameDatabase.DTOs
{
    public class EngineResponseDto
    {
        public int EngineId {get; set;}
        public string EngineName {get; set;}
        public bool IsOpenSource {get; set;} = true;
        public string CreatedBy {get; set;}
    }
}