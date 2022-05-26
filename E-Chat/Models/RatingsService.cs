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
        public Rating Details(string id);
        public Rating Edit(string id);
        public void Update(Rating rating);
        public void Delete(string id);

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
                _context.Rating.Add(new Rating() {RatingId=nextId,Score=score,Feedback=feedback,Name=name});
                _context.SaveChanges();
            }
        
        public Rating Details(string id)
        {
            var rating = _context.Rating
                    .FirstOrDefault(m => m.Name == id);
            if (rating == null)
            {
                return null;
            }

            return rating;
        }

        public Rating Edit(string id)
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

        public void Delete(string id)
        {
            var rating = _context.Rating.FindAsync(id);
            _context.Rating.Remove(rating.Result);
            _context.SaveChanges();
        }
    }

}
