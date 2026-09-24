using GameDatabase.Entites;

namespace GameDatabase.DTOs
{
    public class EngineResponseDto : IAdutiable
    {
        public int EngineId {get; set;}
        public string EngineName {get; set;}
        public bool IsOpenSource {get; set;} = true;
        public string CreatedBy {get; set;}
        public bool IsDeleted {get; set;}
        public string? DeletedBy {get; set;}
        public DateTime? DeletedAt {get; set;}
    }
}