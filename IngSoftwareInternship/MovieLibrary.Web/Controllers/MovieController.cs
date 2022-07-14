using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business;
using MovieLibrary.Business.Services;
using MovieLibrary.Business.Services.ServiceInterfaces;
using MovieLibrary.Business.ViewModels;
using MovieLibrary.Data.Models;

namespace MovieLibrary.Web.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IDirectorService _directorService;
        private readonly IGenresService _genresService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MovieController(IMovieService movieService, IDirectorService directorService, IGenresService genresService,IWebHostEnvironment webHostEnvironment)
        {
            _movieService = movieService;
            _directorService = directorService;
            _genresService = genresService;
            _webHostEnvironment = webHostEnvironment;
        }      
        public async Task<IActionResult> Index()
        {
            ViewBag.Genres= await _genresService.GetGenres();
            return View(await _movieService.GetMovies());
        }
        [HttpPost]
        public async Task<IActionResult> SearchMovie(string movieSearch,int genreId)
        {
            ViewBag.Genres = await _genresService.GetGenres();
            List<MovieViewModel> movies = await _movieService.SearchMovie(movieSearch, genreId);
            return View("Index",movies);
        }
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> Create()
        {
            MovieViewModel movie = new MovieViewModel();
            movie.DirectorViewModels = await _directorService.GetDirectors();
            movie.GenreViewModels= await _genresService.GetGenres();
            return View(movie);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                await _movieService.AddMovie(movie, wwwRootPath);
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(MovieViewModel movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    await _movieService.UpdateMovie(movie,wwwRootPath);
                    TempData["AlertMessage"] = "Updated successfully.";
                    return RedirectToAction("Index");
                }
                else 
                {
                    movie = await _movieService.GetMovie(movie.MovieId);
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id) 
        {
            try
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                await _movieService.DeleteMovie(id,wwwRootPath);
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
