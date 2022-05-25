using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Chat.Data;
using E_Chat.Models;

namespace E_Chat.Controllers
{
    public class RatingsController : Controller
    {
        private readonly RatingsService _service;

        public RatingsController(RatingsService service)
        {
            _service = service;
        }

        // GET: Ratings
        public async Task<IActionResult> Index(string searchString)
        {

            var ratings = from rating in _service.GetAllRatings()
                          select rating;

            if (!String.IsNullOrEmpty(searchString))
            {
                ratings = ratings.Where(s => s.Name!.Contains(searchString));
            }

            return View(ratings.ToList());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = _service.Details(id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Feedback,Date,Created,Score")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                rating.Time = DateTime.Now.ToString("HH:mm");
                rating.Date = DateTime.Now.ToString("dd/MM/yyyy");
                _service.SaveNewRating(rating);
                return RedirectToAction(nameof(Index));
            }
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = _service.Edit(id);
            //if (rating == null)
            //{
            //    return NotFound();
            //}
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Feedback,Date,Created,Score")] Rating rating)
        {

            if (id != rating.Name)
            {
                return NotFound();

            }

            if (ModelState.IsValid)
            {
                try
                {
                    rating.Time = DateTime.Now.ToString("HH:mm");
                    rating.Date = DateTime.Now.ToString("dd / MM / yyyy");
                    _service.Update(rating);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.Name))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = _service.Details(id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(string id)
        {
            return _service.GetAllRatings().Any(e => e.Name == id);
        }
    }
}
