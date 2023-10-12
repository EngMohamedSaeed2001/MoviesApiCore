using Microsoft.EntityFrameworkCore;

namespace MoviesApiCore.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChangesAsync();
            return movie;
        }

        public void Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Movie>> GetAll(int genreId = 0)
        {
           return await _context.Movies
                .Where(m=>m.GenreId == genreId || genreId==0)
                .OrderByDescending(r => r.Rate)
                .Include(m => m.Genre)
                .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id); 
        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChangesAsync();
            return movie;
        }
    }
}
