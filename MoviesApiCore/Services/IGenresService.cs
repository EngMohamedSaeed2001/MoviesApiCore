namespace MoviesApiCore.NewFolder
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();

        Task<Genre> GetById(int id);
        Task<Genre> Add(Genre genre);
        Genre Update(Genre genre);

        void Delete(Genre genre);

        Task<bool> isValidGenre(int id);
    }
}
