using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models
{
    public class Rating
    {
        [Key]
        public string Name { get; set; }
        public string Feedback { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Score { get; set; }



    }
}
