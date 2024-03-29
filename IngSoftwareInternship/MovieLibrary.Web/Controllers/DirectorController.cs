﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Business.Services.ServiceInterfaces;
using MovieLibrary.Business.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Web.Controllers
{
    [Authorize]
    public class DirectorController : Controller
    {
        private readonly IDirectorService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DirectorController(IDirectorService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetDirectors());
        }
        [Authorize(Roles ="Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(DirectorViewModel director)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                await _service.AddDirector(director,wwwRootPath);
                TempData["AlertMessage"] = "Added Successfully..";
                return RedirectToAction("Index");
            }
            else
            {
                return View(director);
            }
        }
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(DirectorViewModel director)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    await _service.UpdateDirector(director,wwwRootPath);
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                await _service.DeleteDirector(id, wwwRootPath);
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
