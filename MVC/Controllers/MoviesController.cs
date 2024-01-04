﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Services;
using Business.Models;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class MoviesController : Controller
    {
        // TODO: Add service injections here
        private readonly IMovieService _movieService;
		private readonly IDirectorService _directorService;
		private readonly IGenreService _genreService;

		public MoviesController(IMovieService movieService, IDirectorService directorService, IGenreService genreService)
        {
            _movieService = movieService;
			_directorService = directorService;
		    _genreService = genreService;

	}

        // GET: Movies
        public IActionResult Index()
        {
            List<MovieModel> movieList = _movieService.Query().ToList(); // TODO: Add get list service logic here
			return View(movieList);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int id)
        {
            MovieModel movie = _movieService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
			if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "DirectorOutput");
			return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieModel movie)
        {
			if (ModelState.IsValid)
			{
				// TODO: Add insert service logic here
				bool result = _movieService.Add(movie);
				if (result)
				{
					TempData["Message"] = "Movie added successfully.";
					return RedirectToAction(nameof(Index));
				}
				ModelState.AddModelError("", "Movie couldn't be added!");
			}
			// TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "DirectorOutput");
			return View(movie);
		}

        // GET: Movies/Edit/5
        public IActionResult Edit(int id)
        {
            MovieModel movie = _movieService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
			if (movie == null)
            {
                return NotFound();
            }
			// TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "DirectorOutput");
			return View(movie);
        }

        // POST: Movies/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MovieModel movie)
        {
			if (ModelState.IsValid)
			{
				// TODO: Add update service logic here
				var result = _movieService.Update(movie);
				if (result)
				{
					TempData["Message"] = "Director updated successfully.";
					return RedirectToAction(nameof(Index));
				}
				ModelState.AddModelError("", "Director couldn't be updated!");
			}
			// TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "DirectorOutput");
			return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(int id)
        {
			MovieModel movie = _movieService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
			if (movie == null)
			{
				return NotFound();
			}
			return View(movie);
		}

        // POST: Movies/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
			// TODO: Add delete service logic here
			_movieService.Delete(id);
			TempData["Message"] = "Movie deleted successfully.";
			return RedirectToAction(nameof(Index));
		}
	}
}
