using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business.ServiceInterface;
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
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            User userBack = _service.AddUser(user);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            User user = _service.GetUser(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            User userBack = _service.UpdateUser(user);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id) 
        {
            User user = _service.GetUser(id);
            return View(user);
        }
        public IActionResult Delete(int id)
        {
            User user = _service.GetUser(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            _service.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
