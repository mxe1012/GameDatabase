
namespace GameDatabase.Entites
{
    public interface IAdutiable
    {
        public string? CreatedBy {get; set;}
        public bool IsDeleted {get; set;}
        public string? DeletedBy {get; set;}
        public DateTime? DeletedAt {get; set;}
    }
}