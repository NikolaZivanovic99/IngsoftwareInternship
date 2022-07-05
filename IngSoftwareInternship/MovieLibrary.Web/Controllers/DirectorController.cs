using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Business.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Web.Controllers
{
    public class DirectorController : Controller
    {
        private readonly IDirectorService _service;
        public DirectorController(IDirectorService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetDirectors());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DirectorViewModel director)
        {
            if (ModelState.IsValid)
            {
                await _service.AddDirector(director);
                TempData["AlertMessage"] = "Added Successfully..";
                return RedirectToAction("Index");
            }
            else
            {
                return View(director);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                DirectorViewModel director = await _service.GetDirector(id);
                return View(director);
            }
            catch (ValidationException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DirectorViewModel director)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdateDirector(director);
                    TempData["AlertMessage"] = "Updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(director);
                }
            }
            catch (ValidationException ex)
            {
                ViewBag.Message = string.Format(ex.Message);
                return View(director);
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                return View(await _service.GetDirector(id));
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
                return View(await _service.GetDirector(id));
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
                await _service.DeleteDirector(id);
                TempData["AlertMessage"] = "Director Deleted Successfully..!";
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
