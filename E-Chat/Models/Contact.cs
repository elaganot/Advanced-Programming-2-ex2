using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models
{
    public class Contact
    {
        [Key]
        //public int Id { get; set; }

        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Server { get; set; }

        public string Last { get; set; }
        public string Lastdate { get; set; }

        //public string Picture { get; set; }

        public List<Message> Messages { get; set; }
    }
}
