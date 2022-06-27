using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business.Service;
using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;

namespace MovieLibrary.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _service;
        public MovieController(IMovieService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View(_service.GetMovies());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MovieViewModel movie)
        {
            _service.AddMovie(movie);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            MovieViewModel movie = _service.GetMovie(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(MovieViewModel movie)
        {
            _service.UpdateMovie(movie);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            MovieViewModel movie = _service.GetMovie(id);
            return View(movie);
        }
    
        public IActionResult Delete(int id)
        {
            MovieViewModel movie = _service.GetMovie(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Delete(int? id) 
        {
            _service.DeleteMovie(id);
            return RedirectToAction("Index");
        }
       
    }
}
