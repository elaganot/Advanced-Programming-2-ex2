using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models
{
    public class Contact
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }

        public string Picture { get; set; }

        public List<Message> Messages { get; set; }
    }
}
