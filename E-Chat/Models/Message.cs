using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models

{
    public class Message
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string SenderName { get; set; }

    }
}
