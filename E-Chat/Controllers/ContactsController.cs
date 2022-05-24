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
using E_Chat.Hubs;

namespace E_Chat.Controllers
{
    [ApiController]
    [Route("api/")]
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

            contacts.Add(new Contact() { Name = "ela", Id = "ela", Server = "localhost:7213", Last = "WOW", Lastdate = "19:21", Messages = messages });

            var messages1 = new List<Message>();
            messages1.Add(new Message() { Id = 1, Content = "Eden you are my hero", Created = "19:20", Sent = false });
            messages1.Add(new Message() { Id = 2, Content = "How did you get this phone number???", Created = "19:21", Sent = true });

            contacts.Add(new Contact() { Name = "kim", Id = "kim", Server = "localhost:7213", Last = "How did you get this phone number???", Lastdate = "19:21", Messages = messages1 });

            Users.Add(new User() { UserName = "eden", Name = "Eden Hamami", Password = "a123456", MyContacts = contacts });

            var contacts2 = new List<Contact>();

            var messages2 = new List<Message>();
            messages2.Add(new Message() { Id = 1, Content = "Ela, look what I made us", Created = "19:20", Sent = true });
            messages2.Add(new Message() { Id = 2, Content = "WOW", Created = "19:21", Sent = false });

            contacts2.Add(new Contact() { Name = "Eden Hamami", Id = "eden", Server = "localhost:7213", Last = "WOW", Lastdate = "19:21", Messages = messages2 });

            var messages12 = new List<Message>();
            messages12.Add(new Message() { Id = 1, Content = "Eden you are my hero", Created = "19:20", Sent = false });
            messages12.Add(new Message() { Id = 2, Content = "How did you get this phone number???", Created = "19:21", Sent = true });

            contacts2.Add(new Contact() { Name = "kim", Id = "kim", Server = "localhost:7213", Last = "How did you get this phone number???", Lastdate = "19:21", Messages = messages12 });

            Users.Add(new User() { UserName = "ela", Name = "ela", Password = "a123456", MyContacts = contacts2 });

        }

        [HttpPost]
        [Route("transfer")]
        public async Task<IActionResult> NewMessage([FromBody] TransferParam newMessage)
        {
            // TODO: implement

            var user = Users.Find(x => x.UserName == newMessage.To);

            if (user == null)
            {
                return NotFound();
            }

            var contact = user.MyContacts.Find(x => x.Id == newMessage.From);
            if (contact == null)
            {
                return NotFound();
            }

            int MessageId = 0;
            if (user.MyContacts.Find(x => x.Id == newMessage.From).Messages == null)
            {
                user.MyContacts.Find(x => x.Id == newMessage.From).Messages = new List<Message>();
                MessageId = 1;
            }
            else
            {
                MessageId = user.MyContacts.Find(x => x.Id == newMessage.From).Messages.Count() + 1;
            }
            var message = new Message() { Id = MessageId, Content = newMessage.Content, Created = DateTime.Now.ToString(), Sent = false };
            user.MyContacts.Find(x => x.Id == newMessage.From).Messages.Add(message);
            user.MyContacts.Find(x => x.Id == newMessage.From).Last = newMessage.Content;
            user.MyContacts.Find(x => x.Id == newMessage.From).Lastdate = message.Created;

            return StatusCode(201);
        }

        [HttpPost]
        [Route("invitations")]
        public async Task<IActionResult> NewConversation([FromBody] InvitationsParam newConversation)
        {
            // TODO: implement

            var user = Users.Find(x => x.UserName == newConversation.To);

            if (user == null)
            {
                return NotFound();
            }

            var newContact = new Contact() { Id = newConversation.From, Name = newConversation.From, Server = newConversation.Server, Messages = new List<Message>()};


            user.MyContacts.Add(newContact);

            return StatusCode(201);
        }

        [HttpGet]
        [Route("[controller]/users")]
        public async Task<IActionResult> Index()
        {
            // TODO: implement

            return Json(Users.ToList());
        }

        [HttpPost]
        [Route("[controller]/users")]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            // TODO: implement
            Users.Add(newUser);
            return StatusCode(201);
        }

        [HttpGet]
        [Route("[controller]/{UserName}")]
        public async Task<IActionResult> Index(string UserName)
        {
            // TODO: implement

            var user = Users.Find(x => x.UserName == UserName);

            if (user == null)
            {
                return NotFound();
            }
            List<selectedFields> selectedFields=new List<selectedFields>();
            foreach (Contact contact in user.MyContacts)
            {
                selectedFields newContact = new selectedFields();
                newContact.Id = contact.Id;
                newContact.Name=contact.Name;
                newContact.Server=contact.Server;
                newContact.Last=contact.Last;
                newContact.Lastdate=contact.Lastdate;
                selectedFields.Add(newContact);
            }

             return Json(selectedFields.ToList());
        }

        [HttpPost]
        [Route("[controller]/{UserName}")]
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
        [Route("[controller]/{UserName}/{id}")]
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
        [Route("[controller]/{UserName}/{id}")]
        public async Task<IActionResult> UpdateContact(string id, string UserName, [FromBody] Contact updatedContact)
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

            user.MyContacts.Find(x => x.Id == id).Name = updatedContact.Name;
            user.MyContacts.Find(x => x.Id == id).Server = updatedContact.Server;
            return NoContent();
        }

        [HttpDelete]
        [Route("[controller]/{UserName}/{id}")]
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
        [Route("[controller]/{UserName}/{id}/messages")]
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
        [Route("[controller]/{UserName}/{id}/messages")]
        public async Task<IActionResult> CreateMessage( string UserName, string id, [FromBody] Content Content)
        {
            // TODO: implement
            //string Content = "ela";
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
            var message = new Message() { Id = MessageId, Content = Content.Text, Created = DateTime.Now.ToString(), Sent = true };
            user.MyContacts.Find(x => x.Id == id).Messages.Add(message);
            user.MyContacts.Find(x => x.Id == id).Last = Content.Text;
            user.MyContacts.Find(x => x.Id == id).Lastdate = message.Created;

            return StatusCode(201);

        }

        [HttpPut]
        [Route("[controller]/{UserName}/{id}/messages/{id2}")]
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
        [Route("[controller]/{UserName}/{id}/messages/{id2}")]
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
        [Route("[controller]/{UserName}/{id}/messages/{id2}")]
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
