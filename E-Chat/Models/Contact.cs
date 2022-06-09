using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models
{
    public class Contact
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string ContactUserName { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string Server { get; set; }

    }
}
