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

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var movies = JsonConvert.DeserializeObject<List<MovieView>>(jsonData);
                return movies;
            }
            return null;
        }
        public List<MovieView> getMovieByQuantity(int quantity)
        {
            var response = client.GetAsync("/api/Movie/GetMovieByQuantity?quantity=" + quantity).Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var movies = JsonConvert.DeserializeObject<List<MovieView>>(jsonData);
                return movies;
            }
            return null;
        }
       
        public bool createMovie(MovieView viewModel)
        {
            var result = client.PostAsJsonAsync("/api/Movie/AddMovie", viewModel).Result;
            if(result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

    }
}
