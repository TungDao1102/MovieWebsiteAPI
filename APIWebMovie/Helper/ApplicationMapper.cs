using APIWebMovie.Models;
using AutoMapper;
using ModelAccess.ViewModel;

namespace APIWebMovie.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() { 
            CreateMap<User, UserView>().ReverseMap();
            CreateMap<Movie, MovieView>().ReverseMap();
            CreateMap<Director, DirectorView>().ReverseMap();
            CreateMap<Genre, GenresView>().ReverseMap(); 
            CreateMap<TypeMovie, TypeMovieView>().ReverseMap(); 
            CreateMap<ReView, ReviewView>().ReverseMap();
            CreateMap<Bill, BillView>().ReverseMap();
            CreateMap<DetailUserMovieFavorite, MovieFavoriteView>().ReverseMap();
            CreateMap<Actor, ActorView>().ReverseMap();
            CreateMap<DetailGenresMovie, DetailGenresView>().ReverseMap();
            CreateMap<DetailActorMovie, DetailActorView>().ReverseMap();
            CreateMap<DetailDirectorMovie, DetailDirectorView>().ReverseMap();
        }
    }
}
