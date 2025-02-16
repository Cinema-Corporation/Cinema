using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly TmdbRepository _tmdbRepository;

        public MoviesController(TmdbRepository tmdbRepository)
        {
            _tmdbRepository = tmdbRepository;
        }

        public async Task<IActionResult> Index()
        {
            //var movies = await _tmdbRepository.GetLatestMoviesAsync();
            return View(); 
        }
    }
}
