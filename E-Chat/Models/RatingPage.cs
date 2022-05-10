using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models

{
    public class RatingPage
    {

        public virtual ICollection<StarRating> ratings { get; set; }

        public int RateCount
        {
            get { return ratings.Count; }
        }
        public int RateTotal
        {
            get
            {

                return (ratings.Sum(m => m.Rate));
            }
        }




    }

}

