namespace GameDatabase.Entites
{
    public class Genre : IAdutiable
    {
        public int GenreId {get; set;}
        public string GenreName {get; set;}
        public string? CreatedBy {get; set;}
        public bool IsDeleted {get; set;}
        public string? DeletedBy {get; set;}
        public DateTime DeletedAt {get; set;}
    }
}