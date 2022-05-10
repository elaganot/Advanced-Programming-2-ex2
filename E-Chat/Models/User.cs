using System;
using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models

{
    public class User
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        //image
        public List<Contact> MyContacts { get; set; }

    }
}
