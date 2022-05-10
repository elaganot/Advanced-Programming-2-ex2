using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models
{
    public class StarRating
    {

        [Key]
        public string Name { get; set; }

        public int Rate { get; set; }

        public string IpAddress { get; set; }


    }
}
