using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business.Services.ServiceInterfaces;
using MovieLibrary.Business.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Web.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenresService _service;
        public GenreController(IGenresService service)
        {
            _service = service;
        }
    
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetGenres());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreViewModel genre)
        {
            if (ModelState.IsValid)
            {
                await _service.AddGenre(genre);
                TempData["AlertMessage"] = "Added Successfully..";
                return RedirectToAction("Index");
            }
            else
            {
                return View(genre);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                GenreViewModel genre = await _service.GetGenre(id);
                return View(genre);
            }
            catch (ValidationException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GenreViewModel genre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdateGenre(genre);
                    TempData["AlertMessage"] = "Updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(genre);
                }
            }
            catch (ValidationException ex)
            {
                ViewBag.Message = string.Format(ex.Message);
                return View(genre);
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                return View(await _service.GetGenre(id));
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
                return View(await _service.GetGenre(id));
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
                await _service.DeleteGenre(id);
                TempData["AlertMessage"] = "Genre Deleted Successfully..!";
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
