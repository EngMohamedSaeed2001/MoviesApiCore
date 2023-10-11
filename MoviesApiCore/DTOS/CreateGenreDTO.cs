namespace MoviesApiCore.NewFolder2
{
    public class CreateGenreDTO
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
