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
        private readonly IMovieService _movieService;
        private readonly IDirectorService _directorService;
        private readonly IGenresService _genresService;
        public MovieController(IMovieService movieService, IDirectorService directorService, IGenresService genresService)
        {
            _movieService = movieService;
            _directorService = directorService;
            _genresService = genresService;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewBag.Genres= await _genresService.GetGenres();
            return View(await _movieService.GetMovies());
        }
        [HttpPost]
        public async Task<IActionResult> SearchMovie(string movieSearch,int genresSearch)
        {
            ViewBag.Genres = await _genresService.GetGenres();
            List<MovieViewModel> movies = await _movieService.SearchMovie(movieSearch,genresSearch);
            return View("Index",movies);
        }
        public async Task<IActionResult> Create()
        {
            MovieViewModel movie = new MovieViewModel();
            movie.DirectorViewModels = await _directorService.GetDirectors();
            movie.GenreViewModels= await _genresService.GetGenres();
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.AddMovie(movie);
                TempData["AlertMessage"] = "Added Successfully..";
                return RedirectToAction("Index");
            }
            else
            {
                movie.DirectorViewModels = await _directorService.GetDirectors();
                movie.GenreViewModels = await _genresService.GetGenres();
                return View(movie);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                MovieViewModel movie = await _movieService.GetMovie(id);
                movie.DirectorViewModels = await _directorService.GetDirectors();
                movie.GenreViewModels = await _genresService.GetGenres();  
                return View(movie);
            }
            catch (ValidationException ex) 
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }           
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieViewModel movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _movieService.UpdateMovie(movie);
                    TempData["AlertMessage"] = "Updated successfully.";
                    return RedirectToAction("Index");
                }
                else 
                {
                    movie.DirectorViewModels = await _directorService.GetDirectors();
                    movie.GenreViewModels= await _genresService.GetGenres();
                    return View(movie);
                }                
            }
            catch (ValidationException ex ) 
            {
                ViewBag.Message = string.Format(ex.Message);
                movie.DirectorViewModels = await _directorService.GetDirectors();
                movie.GenreViewModels = await _genresService.GetGenres();
                return View(movie);
            }           
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                return View(await _movieService.GetMovie(id));
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
                return View( await _movieService.GetMovie(id));
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
                await _movieService.DeleteMovie(id);
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
