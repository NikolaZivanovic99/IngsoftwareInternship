using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business;
using MovieLibrary.Business.ServiceInterface;
using MovieLibrary.Business.ViewModel;
using MovieLibrary.Data.Models;

namespace MovieLibrary.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _service.GetUsers());
        }

        public async Task<IActionResult> Create()
        {
            UserViewModel user =new UserViewModel();
            user.Occupations= await _service.GetOccupations();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.AddUser(user);
                    TempData["AlertMessage"] = "Added Successfully..";
                    return RedirectToAction("Index");
                }
                else
                {
                    user.Occupations= await _service.GetOccupations();
                    return View(user);
                }
            }
            catch (ValidationException ex) 
            {
                ViewBag.Message = string.Format(ex.Message);
                 user.Occupations = await _service.GetOccupations();
                return View(user);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                UserViewModel user = await _service.GetUser(id);
                user.Occupations= await _service.GetOccupations();
                return View(user);
            }
            catch (ValidationException ex) 
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }    
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.UpdateUser(user);
                    TempData["AlertMessage"] = "Updated successfully.";
                    return RedirectToAction("Index");
                }
                else 
                {
                    user.Occupations= await _service.GetOccupations();
                    return View(user);
                }
            }
            catch (ValidationException ex) 
            {
                user.Occupations = await _service.GetOccupations();
                ViewBag.Message = string.Format(ex.Message);
                return View(user);
            }
        }

        public async  Task<IActionResult> Details(int id) 
        {
            try
            {
                return View( await _service.GetUser(id));
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
                return View(await _service.GetUser(id));
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
                await _service.DeleteUser(id);
                TempData["AlertMessage"] = "User Deleted Successfully..!";
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
