namespace MoviesApiCore.DTOS
{
    public class MovieDetailsDTO
    {
        public int id { get; set; }

     
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

       
        public string StoryLine { get; set; }

        public byte[] Poster { get; set; }

        public int GenreId {  get; set; }
        public string GenreName { get; set; }
    }
}
