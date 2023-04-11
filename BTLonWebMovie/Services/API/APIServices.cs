using Azure;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;

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
        public List<UserView> getAllUserView()
        {
            var response = client.GetAsync("/api/User/GetAllUser").Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var users = JsonConvert.DeserializeObject<List<UserView>>(jsonData);
            return users;
        }

        public bool createUser(UserView userView)
        {
            var result = client.PostAsJsonAsync("/api/User/AddUser", userView).Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public UserView getUserById(int userId)
        {
            var response = client.GetAsync("/api/User/GetUserById?UserId=" + userId).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<UserView>(jsonData);
            return user;
        }
        public bool editUser(UserView userView)
        {
            var result = client.PutAsJsonAsync("/api/User/EditUser", userView).Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public bool deleteUser(int userId)
        {
            var result = client.DeleteAsync("/api/User/DeleteUser?id=" + userId).Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public string UpdateAvatar(string PathFile)
        {
            var response = client.GetAsync("/api/User/UpdateAvatar?pathFile=" + PathFile).Result;
            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                return jsonData;
            }
            return null;
        }

        public List<MovieView> searchMovieByGenres(int genresId)
        {
            var response = client.GetAsync("/api/Movie/SearchMovieByGenre?genreId=" + genresId).Result;
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

        public List<GenresView> getAllGenres()
        {
            var response = client.GetAsync("/api/Genres/GetAllGenres").Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var genres = JsonConvert.DeserializeObject<List<GenresView>>(jsonData);
            return genres;
        }

        public bool RegisterUser(UserView userView)
        {
            var result = client.PostAsJsonAsync("/api/User/Register", userView).Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool SendEmail(SendEmail sendEmail)
        {
            var result = client.PostAsJsonAsync("/api/User/SendOtp", sendEmail).Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public bool VerifyUser(string email)
        {
            var response = client.PutAsync($"/api/User/VerifyUser?Email={email}", null).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public List<ActorView> getActorByMovie(int movieId)
        {
            var response = client.GetAsync("/api/Actor/GetActorByMovieId?movieId=" + movieId).Result;
            if(response.IsSuccessStatusCode)
           {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var actorView = JsonConvert.DeserializeObject<List<ActorView>>(jsonData);
                return actorView;
           }
           return null;
        }

        public List<DirectorView> getDirectorByMovie(int movieId)
        {
            var response = client.GetAsync("/api/Director/GetDirectorByMovieId?movieId=" + movieId).Result;
            if(response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var directors = JsonConvert.DeserializeObject<List<DirectorView>>(jsonData);
                return directors;
            }
            return null;
        }
        public List<GenresView> getGenresByMovie(int movieId)
        {
            var response = client.GetAsync("/api/Genres/GetGenresByMovieId?movieId=" + movieId).Result;
            if(response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var genres = JsonConvert.DeserializeObject<List<GenresView>>(jsonData);
                return genres;
            }
            return null;
        }
        public List<MovieView> getTopMovie(int quantity)
        {
            var response = client.GetAsync("/api/Movie/GetTopMovie?quantity=" + quantity).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var topview = JsonConvert.DeserializeObject<List<MovieView>>(jsonData);
            return topview;
        }
        public void AddMovieFavorite(MovieFavoriteView favoriteView)
        {
            client.PostAsJsonAsync("/api/MovieFavorite/AddMovieFavorite", favoriteView);
        }
    }
}
