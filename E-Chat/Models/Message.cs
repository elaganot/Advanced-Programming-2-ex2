using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models

{
    public class Message
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Created { get; set; }
        [Required]
        public bool Sent { get; set; }

    }
}
