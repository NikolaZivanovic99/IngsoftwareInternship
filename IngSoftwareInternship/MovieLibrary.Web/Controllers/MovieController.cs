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

        public IActionResult Edit(int id)
        {
            try
            {
                MovieViewModel movie = _service.GetMovie(id);
                return View(movie);
            }
            catch (ValidationException ex) 
            {
                TempData["AlertMessage"] = ex.Message;
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
            catch (ValidationException ex ) 
            {
                ViewBag.Message = string.Format(ex.Message);
                return View(movie);
            }           
        }

        public IActionResult Details(int id)
        {
            try
            {
                return View(_service.GetMovie(id));
            }
            catch (ValidationException ex) 
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    
        public IActionResult Delete(int id)
        {
            try
            {
                return View(_service.GetMovie(id));
            }
            catch (ValidationException ex) 
            {
                TempData["AlertMessage"] = ex.Message;
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
            catch (ValidationException ex) 
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }           
        }      
    }
}
