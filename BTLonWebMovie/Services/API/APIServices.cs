using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;
using Newtonsoft.Json;

namespace BTLonWebMovie.Services.API
{
    public class APIServices
    {
        IHttpClientFactory _factory;
        HttpClient client;

        public APIServices(IHttpClientFactory factory)
        {
            _factory = factory;
            client = _factory.CreateClient("myclient");
        }

        public List<MovieView> getAllMovieView()
        {
            var response = client.GetAsync("/api/Movie/GetAllMovie").Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var movies = JsonConvert.DeserializeObject<List<MovieView>>(jsonData);
            return movies;
        }
        public List<MovieView> getDeletedMovieView()
        {
            var response = client.GetAsync("/api/Movie/GetDeletedMovie").Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var movies = JsonConvert.DeserializeObject<List<MovieView>>(jsonData);
            return movies;
        }

        public List<MovieView> getMovieByQuantity(int quantity)
        {
            var response = client.GetAsync("/api/Movie/GetMovieByQuantity?quantity=" + quantity).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var movies = JsonConvert.DeserializeObject<List<MovieView>>(jsonData);
            return movies;
        }

        public bool createMovie(MovieView viewModel)
        {
            var result = client.PostAsJsonAsync("/api/Movie/AddMovie", viewModel).Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public MovieView getMovieById(int movieId)
        {
            var response = client.GetAsync("/api/Movie/id?id=" + movieId).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var movie = JsonConvert.DeserializeObject<MovieView>(jsonData);
            return movie;
        }
        public bool editMovie(MovieView viewModel)
        {
            var result = client.PutAsJsonAsync("/api/Movie/EditMovie", viewModel).Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool deleteMovie(int movieId)
        {
            var result = client.DeleteAsync("/api/Movie/DeleteMovie?MovieId=" + movieId).Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool restoreMovie(int movieId)
        {
            var result = client.PutAsync($"/api/Movie/RestoreMovieDeleted?movieId={movieId}", null).Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public List<BillView> getAllBill()
        {
            var response = client.GetAsync("/api/Bill/GetAllBill").Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var bills = JsonConvert.DeserializeObject<List<BillView>>(jsonData);
            return bills;
        }

        public List<GenresView> getAllGenres()
        {
            var response = client.GetAsync("/api/Genres/GetAllGenres").Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var genres = JsonConvert.DeserializeObject<List<GenresView>>(jsonData);
            return genres;
        }

        public List<MovieView> searchMovieByGenres(int genreId) {
            var response = client.GetAsync("/api/Movie/SearchMovieByGenre?genreId=" + genreId).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var movies = JsonConvert.DeserializeObject<List<MovieView>>(jsonData);
            return movies;
        }

        public List<MovieView> searchMovieByNameOrActor(string name)
        {
            var response = client.GetAsync("/api/Movie/SearchMovieByNameOrActor?name=" + name).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var movies = JsonConvert.DeserializeObject<List<MovieView>>(jsonData);
            return movies;
        }
    }
}
