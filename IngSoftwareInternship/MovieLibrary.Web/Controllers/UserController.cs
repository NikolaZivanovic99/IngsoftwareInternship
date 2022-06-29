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

        public IActionResult Index()
        {
            return View(_service.GetUsers());
        }

        public IActionResult Create()
        {
            List<OccupationViewModel> occupations = _service.GetOccupations();
            ViewBag.Occupations = occupations;
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.AddUser(user);
                    TempData["AlertMessage"] = "Added Successfully..";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Occupations = _service.GetOccupations();
                    return View(user);
                }
            }
            catch (ValidationException) 
            {
                ViewBag.Message = string.Format("The user with the entered data already exists. Please try again");
                ViewBag.Occupations = _service.GetOccupations();
                return View(user);
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                UserViewModel user = _service.GetUser(id);
                ViewBag.Occupations = _service.GetOccupations();
                return View(user);
            }
            catch (ValidationException) 
            {
                TempData["AlertMessage"] = "A user with this identifier does not exist. Please try again.";
                return RedirectToAction("Index");
            }    
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.UpdateUser(user);
                    TempData["AlertMessage"] = "Updated successfully.";
                    return RedirectToAction("Index");
                }
                else 
                {
                    ViewBag.Occupations = _service.GetOccupations();
                    return View(user);
                }
            }
            catch (ValidationException) 
            {
                ViewBag.Occupations = _service.GetOccupations();
                ViewBag.Message = string.Format("There is no user with the specified data. Please try again");
                return View(user);
            }
        }

        public IActionResult Details(int id) 
        {
            try
            {
                return View(_service.GetUser(id));
            }
            catch (ValidationException) 
            {
                TempData["AlertMessage"] = "A user with this identifier does not exist. Please try again.";
                return RedirectToAction("Index");
            } 
        }

        public IActionResult Delete(int id)
        {
            try
            {
                return View(_service.GetUser(id));
            }
            catch (ValidationException)
            {
                TempData["AlertMessage"] = "A user with this identifier does not exist. Please try again.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            try
            {
                _service.DeleteUser(id);
                TempData["AlertMessage"] = "User Deleted Successfully..!";
                return RedirectToAction("Index");
            }
            catch (ValidationException) 
            {
                TempData["AlertMessage"] = "A user with this identifier does not exist. Please try again.";
                return RedirectToAction("Index");
            }
        }
    }
}
