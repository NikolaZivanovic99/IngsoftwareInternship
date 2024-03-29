﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business;
using MovieLibrary.Business.Services.ServiceInterfaces;
using MovieLibrary.Business.ViewModels;
using MovieLibrary.Data.Models;

namespace MovieLibrary.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMovieService _movieService;

        public UserController(IUserService service, IMovieService movieService)
        {
            _service = service;
            _movieService = movieService;
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View( await _service.GetUsers());
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
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
        [Authorize(Roles = "Administrator")]
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

        public async  Task<IActionResult> Details(string id) 
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
        public async Task<IActionResult> WatchList(string id)
        {
            try
            {
                UserViewModel user = await _service.GetUser(id);
                return View(user.Movies);
            }
            catch (ValidationException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Deletee(string? id)
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
        public async Task<IActionResult> DeleteMovie(int id)
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
        [HttpPost]
        public async Task<IActionResult> DeleteMovie(int id,string userId)
        {
            try
            {
                await _service.DeleteMovie(id,userId);
                TempData["AlertMessage"] = "User Deleted Successfully..!";
                return RedirectToAction("Index","Movie");
            }
            catch (ValidationException ex)
            {
                TempData["AlertMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
