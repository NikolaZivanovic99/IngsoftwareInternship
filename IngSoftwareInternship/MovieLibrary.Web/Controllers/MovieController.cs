using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business;
using MovieLibrary.Business.Service;
using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;

namespace MovieLibrary.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _service;
        private readonly IDirectorService _serviceDirector;
        private readonly IGenresService _genresService;
        public MovieController(IMovieService service, IDirectorService serviceDirector,IGenresService genresService)
        {
            _service = service;
            _serviceDirector = serviceDirector;
            _genresService = genresService;
        }
        public async Task<IActionResult> Index()
        {
            return View( await _service.GetMovies());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Directors = await _serviceDirector.GetDirectors();
            ViewBag.Genres = await _genresService.GetGenres();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel movie, int[] directors, int[] genres)
        {
            if (ModelState.IsValid)
            {
                await _service.AddMovie(movie,directors,genres);
                TempData["AlertMessage"] = "Added Successfully..";
                return RedirectToAction("Index");
            }
            else
            {
                return View(movie);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                MovieViewModel movie = await _service.GetMovie(id);
                ViewBag.Directors = await _serviceDirector.GetDirectors();
                ViewBag.Genres = await _genresService.GetGenres();  
                return View(movie);
            }
            catch (ValidationException ex) 
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }           
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieViewModel movie, int[] directors, int[]genres)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdateMovie(movie,directors,genres);
                    TempData["AlertMessage"] = "Updated successfully.";
                    return RedirectToAction("Index");
                }
                else 
                {
                    return View(movie);
                }                
            }
            catch (ValidationException ex ) 
            {
                ViewBag.Message = string.Format(ex.Message);
                return View(movie);
            }           
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                return View(await _service.GetMovie(id));
            }
            catch (ValidationException ex) 
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return View( await _service.GetMovie(id));
            }
            catch (ValidationException ex) 
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id) 
        {
            try
            {
                await _service.DeleteMovie(id);
                TempData["AlertMessage"] = "Movie Deleted Successfully..!";
                return RedirectToAction("Index");
            }
            catch (ValidationException ex) 
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }           
        }      
    }
}
