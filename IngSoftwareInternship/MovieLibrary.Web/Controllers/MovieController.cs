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
            try
            {
                if (ModelState.IsValid)
                {
                    _service.AddMovie(movie);
                    TempData["AlertMessage"] = "Added Successfully..";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(movie);
                }
            }
            catch (ValidationException) 
            {
                ViewBag.Message = string.Format("The movie with the entered data already exists. Please try again");
                return View(movie);
            }
            
        }

        public IActionResult Edit(int id)
        {
            try
            {
                MovieViewModel movie = _service.GetMovie(id);
                return View(movie);
            }
            catch (ValidationException) 
            {
                TempData["AlertMessage"] = "A movie with this identifier does not exist. Please try again.";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public IActionResult Edit(MovieViewModel movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.UpdateMovie(movie);
                    TempData["AlertMessage"] = "Updated successfully.";
                    return RedirectToAction("Index");
                }
                else 
                {
                    return View(movie);
                }
                
            }
            catch (ValidationException) 
            {
                ViewBag.Message = string.Format("There is no movie with the specified data. Please try again");
                return View(movie);
            }
            
        }

        public IActionResult Details(int id)
        {
            try
            {
                return View(_service.GetMovie(id));
            }
            catch (ValidationException) 
            {
                TempData["AlertMessage"] = "A movie with this identifier does not exist. Please try again.";
                return RedirectToAction("Index");
            }
        }
    
        public IActionResult Delete(int id)
        {
            try
            {
                return View(_service.GetMovie(id));
            }
            catch (ValidationException) 
            {
                TempData["AlertMessage"] = "A movie with this identifier does not exist. Please try again.";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public IActionResult Delete(int? id) 
        {
            try
            {
                _service.DeleteMovie(id);
                TempData["AlertMessage"] = "Movie Deleted Successfully..!";
                return RedirectToAction("Index");
            }
            catch (ValidationException) 
            {
                TempData["AlertMessage"] = "A movie with this identifier does not exist. Please try again.";
                return RedirectToAction("Index");
            }
            
        }
       
    }
}
