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
        public async Task<IActionResult> Index()
        {
            return View( await _service.GetMovies());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                await _service.AddMovie(movie);
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
                    await _service.UpdateMovie(movie);
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
