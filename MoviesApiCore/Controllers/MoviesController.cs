﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApiCore.NewFolder;
using MoviesApiCore.Services;

namespace MoviesApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
  
         private readonly IMapper _autoMapper;

        private readonly IMovieService _movieService;

        private readonly IGenresService _genreService;

        private new List<string> _allowExetension =new List<string>{ ".jpg",".jpeg",".png"};
        private long _maxSizeOfPoster = 1048578;

        public MoviesController(IMovieService movieService, IGenresService genreService, IMapper autoMapper)
        {
            _movieService = movieService;
            _genreService = genreService;
            _autoMapper = autoMapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _movieService.GetAll();

            var data = _autoMapper.Map<IEnumerable<MovieDetailsDTO>>(movies);

            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _movieService.GetById(id);
            if (movie == null)
                return NotFound();

            var dto = _autoMapper.Map<MovieDetailsDTO>(movie); 


            return Ok(dto);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(int id)
        {
            var movies = await _movieService.GetAll(id);

            var data = _autoMapper.Map<IEnumerable<MovieDetailsDTO>>(movies);

            return Ok(data);
        }

        [HttpPost]

        public async Task<IActionResult> CreateAsync([FromForm]MovieDTO movieDTO)
        {

            if (movieDTO.Poster == null)
                return BadRequest("Poster is required");


            if (!_allowExetension.Contains(Path.GetExtension(movieDTO.Poster.FileName).ToLower()))
                return BadRequest("Only jpg or png");
            
            if(_maxSizeOfPoster < movieDTO.Poster.Length)
            {
                return BadRequest("Your poster is Greater than 1 MB");
            }

            var isValidGenre = await _genreService.isValidGenre(movieDTO.GenreId);
            
            if (!isValidGenre) {
                return BadRequest("Invalid Genre id");
            }

            using var dataStream = new MemoryStream();
            await movieDTO.Poster.CopyToAsync(dataStream);

            var movie = _autoMapper.Map<Movie>(movieDTO);

            movie.Poster = dataStream.ToArray();

             _movieService.Add(movie);

            return Ok(movie);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _movieService.GetById(id);

            if(movie == null)
            {
                return NotFound();
            }
            _movieService.Delete(movie);

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id , [FromForm] MovieDTO movieDTO)
        {
            var movie = await _movieService.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

          if(movieDTO.Poster != null)
            {
                if (!_allowExetension.Contains(Path.GetExtension(movieDTO.Poster.FileName).ToLower()))
                    return BadRequest("Only jpg or png");

                if (_maxSizeOfPoster < movieDTO.Poster.Length)
                {
                    return BadRequest("Your poster is Greater than 1 MB");
                }

                using var dataStream = new MemoryStream();
                await movieDTO.Poster.CopyToAsync(dataStream);

                movie.Poster=dataStream.ToArray();
            }

            var isValidGenre = await _genreService.isValidGenre( movieDTO.GenreId);

            if (!isValidGenre)
            {
                return BadRequest("Invalid Genre id");
            }

            movie.Title = movieDTO.Title;
            movie.Year = movieDTO.Year;
            movie.StoryLine = movieDTO.StoryLine;
            movie.Rate = movieDTO.Rate;
            movie.GenreId = movieDTO.GenreId;

          

         _movieService.Update(movie);

            return Ok(movie);
        }
    }
}
