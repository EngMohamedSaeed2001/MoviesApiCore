using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApiCore.NewFolder2;

namespace MoviesApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
       private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var generes = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            return Ok(generes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDTO dTO)
        {
            var genre = new Genre { Name =dTO.Name};

            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();

            return Ok(genre);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(int id,[FromBody]CreateGenreDTO dTo)
        {
            var genre= await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if(genre==null)
                return NotFound("Not found");

            genre.Name = dTo.Name;

            _context.SaveChanges();

            return Ok(genre);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g =>g.Id == id);

            if (genre == null)
                return NotFound("Not found");

            _context.Remove(genre); 

            _context.SaveChanges(); 

            return Ok();
        }
    }
}
