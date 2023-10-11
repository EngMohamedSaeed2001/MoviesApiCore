using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MoviesApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<IActionResult> CreateAsync([FromForm]MovieDTO movieDTO)
        {
            using var dataStream = new MemoryStream();
            await movieDTO.Poster.CopyToAsync(dataStream);

            var movie =new Movie
            {
                Title = movieDTO.Title,
                Year = movieDTO.Year,
                Poster = dataStream.ToArray(),
                Rate = movieDTO.Rate,
                StoryLine = movieDTO.StoryLine,
                GenreId = movieDTO.GenreId,
               
            };


            await  _context.AddAsync(movie);
            _context.SaveChangesAsync();

            return Ok(movie);
        }
    }
}
