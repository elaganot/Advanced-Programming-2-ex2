using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models
{
    public class Rating
    {
        private int score;

        [Key]
        public int RatingId { get; set; }


        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }
        public string Feedback { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        [Range(1, 5)]
        [Required(ErrorMessage = "The score must be between 1 and 5")]
        public int Score { get => score; set => score = value; }



    }
}
