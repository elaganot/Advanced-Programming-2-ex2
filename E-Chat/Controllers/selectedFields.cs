using System.ComponentModel.DataAnnotations;
namespace E_Chat.Controllers
{
    internal class selectedFields
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Server { get; set; }

        public string Last { get; set; }
        public string Lastdate { get; set; }
    }
}