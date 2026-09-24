using GameDatabase.Entites;

namespace GameDatabase.DTOs
{
    public class GenreResponseDto: IAdutiable
    {
        public int GenreId {get; set;}
        public string GenreName {get; set;}
        public string CreatedBy {get; set;}
        public bool IsDeleted {get; set;}
        public string? DeletedBy {get; set;}
        public DateTime? DeletedAt {get; set;}
    }
}