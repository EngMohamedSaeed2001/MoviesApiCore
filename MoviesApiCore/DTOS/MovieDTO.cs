﻿namespace MoviesApiCore.DTOS
{
    public class MovieDTO
    {
        [MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        [MaxLength(2500)]
        public string StoryLine { get; set; }

        public IFormFile? Poster { get; set; }

        public int GenreId { get; set; }

       
    }
}
