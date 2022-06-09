using System;
using System.ComponentModel.DataAnnotations;

namespace E_Chat.Models

{
    public class User
    {
        [Key]
        public int Id { get; set; }
        //[Required(ErrorMessage = "Enter Your Name:")]
        //[Display(Name = "name")]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        //[RegularExpression("^[a - zA - Z0 - 9] *$")]
        public string Password { get; set; }
        public string Picture { get; set; }

    }
}
