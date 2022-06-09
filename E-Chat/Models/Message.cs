using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models

{
    public class Message
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Created { get; set; }

    }
}
