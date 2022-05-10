using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models

{
    public class RatingPage
    {

        public virtual ICollection<Rating> ratings { get; set; }




    }

}

