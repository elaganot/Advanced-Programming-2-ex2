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
      
        //image
        public List<Message> Messages { get; set; }
    }
}
