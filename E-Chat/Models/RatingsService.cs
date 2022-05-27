using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Chat.Data;
using E_Chat.Models;
using System.Text;
using E_Chat.Hubs;

namespace E_Chat.Models
{
    public interface IRatingsService
    {

        public IEnumerable<Rating> GetAllRatings();
        public void SaveNewRating(string name, int score, string feedback);
        public Rating Details(int id);
        public Rating Edit(int id);
        public void Update(Rating rating);
        public void Delete(int id);

    }
    public class RatingsService : IRatingsService
    {
        private readonly E_ChatContext _context;

        public RatingsService(E_ChatContext context)
        {
            _context = context;
        }

        public IEnumerable<Rating> GetAllRatings()
        {
            return _context.Rating.ToList();
        }
        public void SaveNewRating(string name, int score, string feedback)
        {
            int nextId;
            if (_context.Rating.Count<Rating>() == 0)
            {
                nextId = 1;
            }
            else {
                nextId = _context.Rating.Max<Rating>(r => r.RatingId) + 1;
            }
                _context.Rating.Add(new Rating() {RatingId=nextId,Score=score,Feedback=feedback,Name=name, Time = DateTime.Now.ToString("HH:mm"), Date = DateTime.Now.ToString("dd/MM/yyyy") });
                _context.SaveChanges();
            }
        
        public Rating Details(int id)
        {
            var rating = _context.Rating
                    .FirstOrDefault(m => m.RatingId == id);
            if (rating == null)
            {
                return null;
            }

            return rating;
        }

        public Rating Edit(int id)
        {
            var rating = _context.Rating.Find(id);
            if (rating == null)
            {
                return null;
            }
            return rating;
        }

        public void Update(Rating rating)
        {
            _context.Update(rating);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var rating = _context.Rating.FindAsync(id);
            _context.Rating.Remove(rating.Result);
            _context.SaveChanges();
        }
    }

}
