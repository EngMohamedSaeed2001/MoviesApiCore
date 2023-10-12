using Microsoft.EntityFrameworkCore;
using MoviesApiCore.DTOS;
using MoviesApiCore.Models;

namespace MoviesApiCore.NewFolder
{
    public class GenreService : IGenresService
    {
        private readonly ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> Add(Genre genre)
        {

            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();

            return genre;
        }

        public void Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();

          
        }

        public async Task<Genre> GetById(int id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> isValidGenre(int id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }

        public Genre Update(Genre genre)
        {
           _context.Update(genre);
            _context.SaveChanges();

            return genre;
        }
    }
}
