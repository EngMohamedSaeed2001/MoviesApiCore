using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApiCore.NewFolder;
using MoviesApiCore.NewFolder2;

namespace MoviesApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
       private readonly IGenresService _genresService;

        public GenresController( IGenresService genresService)
        {
            
            _genresService = genresService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var generes = await _genresService.GetAll();
            return Ok(generes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDTO dTO)
        {
            var genre = new Genre { Name =dTO.Name};

            _genresService.Add(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(int id,[FromBody]CreateGenreDTO dTo)
        {
            var genre= await _genresService.GetById(id);

            if (genre==null)
                return NotFound("Not found");

            genre.Name = dTo.Name;
            _genresService.Update(genre);

            return Ok(genre);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
                return NotFound("Not found");

            _genresService.Delete(genre);

            return Ok();
        }
    }
}
