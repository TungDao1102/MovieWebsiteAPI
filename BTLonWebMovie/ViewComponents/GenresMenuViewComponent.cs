using BTLonWebMovie.Services.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BTLonWebMovie.ViewComponents
{
    public class GenresMenuViewComponent : ViewComponent
    {
        APIServices _services;
        public GenresMenuViewComponent(IHttpClientFactory factory)
        {
            _services = new APIServices(factory);
        }

        public IViewComponentResult Invoke()
        {
            var genres = _services.getAllGenres();
            return View(genres);
        }
    }
}
