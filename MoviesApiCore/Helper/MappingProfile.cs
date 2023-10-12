using AutoMapper;

namespace MoviesApiCore.Helper
{
    public class MappingProfile:Profile
    {
        protected MappingProfile()
        {
            CreateMap<Movie, MovieDetailsDTO>();
            CreateMap<MovieDTO, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
        }

    
    }
}
