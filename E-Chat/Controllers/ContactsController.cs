using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Chat.Data;
using E_Chat.Models;
using System.Text;

namespace E_Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {

        private readonly E_ChatContext _context;

        public static List<User> Users { get; set; }

        public ContactsController(E_ChatContext context)
        {
            _context = context;
            Users = new List<User>();
            var contacts = new List<Contact>();

            var messages = new List<Message>();
            messages.Add(new Message() { Id = 1, Content = "Ela, look what I made us", Created = "19:20", Sent = true });
            messages.Add(new Message() { Id = 2, Content = "WOW", Created = "19:21", Sent= false });

            contacts.Add(new Contact() { Name = "ela", Id = "ela", Server = "localhost:7265", Last = "WOW", Lastdate = "19:21", Messages = messages });

            var messages1 = new List<Message>();
            messages1.Add(new Message() { Id = 1, Content = "Eden you are my hero", Created = "19:20", Sent = false });
            messages1.Add(new Message() { Id = 2, Content = "How did you get this phone number???", Created = "19:21", Sent = true });

            contacts.Add(new Contact() { Name = "kim", Id = "kim", Server = "localhost:7265", Last = "How did you get this phone number???", Lastdate = "19:21", Messages = messages1 });

            Users.Add(new User() { UserName = "eden", Name = "Eden Hamami", Password = "a123456", MyContacts = contacts });
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> Index()
        {
            // TODO: implement


            return Json(Users.ToList());
        }

        [HttpGet]
        [Route("{UserName}")]
        public async Task<IActionResult> Index(string UserName)
        {
            // TODO: implement

            var user = Users.Find(x => x.UserName == UserName);

            if (user == null)
            {
                return NotFound();
            }

            return Json(user.MyContacts.ToList());
        }

        [HttpPost]
        [Route("{UserName}")]
        public async Task<IActionResult> CreateContact(string UserName, [FromBody] Contact newContact)
        {
            // TODO: implement

            var user = Users.Find(x => x.UserName == UserName);
            if (user == null)
            {
                return NotFound();
            }

            user.MyContacts.Add(newContact);

            return StatusCode(201);



            //HttpContext.Session.Set("Name", Encoding.ASCII.GetBytes(newContact.Name));
        }

        [HttpGet]
        [Route("{UserName}/{id}")]
        public async Task<IActionResult> GetContactById(string id, string UserName)
        {
            // TODO: implement

            var user = Users.Find(x => x.UserName == UserName);

            if (user == null)
            {
                return NotFound();
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }



            return Json(new
            {
                contact.Id,
                contact.Name,
                contact.Server,
                contact.Last,
                contact.Lastdate
            });

        }

        [HttpPut]
        [Route("{UserName}/{id}")]
        public async Task<IActionResult> UpdateContact(string id, string UserName, [FromBody] string Name, string Server)
        {
            // TODO: implement


            var user = Users.Find(x => x.UserName == UserName);

            if (user == null)
            {
                return NotFound();
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            user.MyContacts.Find(x => x.Id == id).Name = Name;
            user.MyContacts.Find(x => x.Id == id).Server = Server;
            return NoContent();
        }

        [HttpDelete]
        [Route("{UserName}/{id}")]
        public async Task<IActionResult> DeleteContact(string id, string UserName)
        {
            // TODO: implement

            var user = Users.Find(x => x.UserName == UserName);

            if (user == null)
            {
                return NotFound();
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            user.MyContacts.Remove(contact);
            return NoContent();
        }

        [HttpGet]
        [Route("{UserName}/{id}/messages")]
        public async Task<IActionResult> GetMessages(string id, string UserName)
        {
            // TODO: implement

            var user = Users.Find(x => x.UserName == UserName);

            if (user == null)
            {
                return NotFound();
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return Json(contact.Messages.ToList());
        }

        [HttpPost]
        [Route("{UserName}/{id}/messages")]
        public async Task<IActionResult> CreateMessage(string id, string UserName, [FromBody] string Content)
        {
            // TODO: implement

            var user = Users.Find(x => x.UserName == UserName);

            if (user == null)
            {
                return NotFound();
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            int MessageId = 0;
            if (user.MyContacts.Find(x => x.Id == id).Messages == null)
            {
                user.MyContacts.Find(x => x.Id == id).Messages = new List<Message>();
                MessageId = 1;
            }
            else
            {
                MessageId = user.MyContacts.Find(x => x.Id == id).Messages.Count() + 1;
            }
            var message = new Message() { Id = MessageId, Content = Content, Created = DateTime.Now.ToString(), Sent = false };
            user.MyContacts.Find(x => x.Id == id).Messages.Add(message);
            user.MyContacts.Find(x => x.Id == id).Last = Content;
            user.MyContacts.Find(x => x.Id == id).Lastdate = message.Created;

            return StatusCode(201);

        }

        [HttpPut]
        [Route("{UserName}/{id}/messages/{id2}")]
        public async Task<IActionResult> UpdateMessage([Bind("id")] string id, [Bind("id2")] int id2, [Bind("UserName")] string UserName, [FromBody] string Content)
        {
            // TODO: implement

            var user = Users.Find(x => x.UserName == UserName);

            if (user == null)
            {
                return NotFound();
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            if (contact.Messages == null)
            {
                return NotFound();
            }

            if (contact.Messages.Find(x => x.Id == id2) == null)
            {
                return NotFound();
            }

            user.MyContacts.Find(x => x.Id == id).Messages.Find(x => x.Id == id2).Content = Content;
            user.MyContacts.Find(x => x.Id == id).Last = Content;

            return NoContent();
        }

        [HttpDelete]
        [Route("{UserName}/{id}/messages/{id2}")]
        public async Task<IActionResult> DeleteMessage([Bind("id")] string id, [Bind("id2")] int id2, [Bind("UserName")] string UserName)
        {
            // TODO: implement
            var user = Users.Find(x => x.UserName == UserName);

            if (user == null)
            {
                return NotFound();
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            if (contact.Messages == null)
            {
                return NotFound();
            }

            var message = contact.Messages.Find(x => x.Id == id2);
            if (message == null)
            {
                return NotFound();
            }

            contact.Messages.Remove(message);

            return NoContent();
        }

        [HttpGet]
        [Route("{UserName}/{id}/messages/{id2}")]
        public async Task<IActionResult> GetMessage([Bind("id")] string id, [Bind("id2")] int id2, [Bind("UserName")] string UserName)
        {
            // TODO: implement

            var user = Users.Find(x => x.UserName == UserName);

            if (user == null)
            {
                return NotFound();
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            if (contact.Messages == null)
            {
                return NotFound();
            }

            var message = contact.Messages.Find(x => x.Id == id2);
            if (message == null)
            {
                return NotFound();
            }

            return Json(new
            {
                message.Id,
                message.Content,
                message.Created,
                message.Sent
            });
        }
    }
}
