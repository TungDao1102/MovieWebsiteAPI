using ModelAccess.ViewModel;

namespace BTLonWebMovie.Models.ViewModels
{
    public class MovieViewModel
    {
        public MovieView? movie { get; set; }
        public List<ActorView>? listActor { get; set; }
        public List<DirectorView>? listDirector { get; set; }
        public List<GenresView>? listGenres { get; set; }
    }
}
