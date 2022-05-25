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
using E_Chat;


namespace E_Chat.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ContactsController : Controller
    {

        private readonly ContactsService _service;


        public ContactsController(ContactsService service)
        {
            _service = service;

        }

        [HttpPost]
        [Route("transfer")]
        public async Task<IActionResult> NewMessage([FromBody] TransferParam newMessage)
        {
            // TODO: implement
            int RetVal = _service.SaveNewMessage(newMessage);


            if (RetVal == -1)
            {
                return NotFound();
            }

            return StatusCode(201);
        }

        [HttpPost]
        [Route("invitations")]
        public async Task<IActionResult> NewConversation([FromBody] InvitationsParam newConversation)
        {
            // TODO: implement

            int RetVal = _service.CreateNewConversation(newConversation);


            if (RetVal == -1)
            {
                return NotFound();
            }

            return StatusCode(201);

        }

        [HttpGet]
        [Route("[controller]/users")]
        public async Task<IActionResult> Index()
        {
            // TODO: implement

            return Json(_service.GetAllUsers().ToList());
        }

        [HttpPost]
        [Route("[controller]/users")]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            // TODO: implement
            _service.SaveNewUser(newUser);
            return StatusCode(201);
        }

        [HttpGet]
        [Route("[controller]/{UserName}")]
        public async Task<IActionResult> Index(string UserName)
        {
            // TODO: implement

            var Users = _service.GetAllUsers();

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



            int RetVal = _service.CreateNewContact(UserName, newContact);


            if (RetVal == -1)
            {
                return NotFound();
            }

            return StatusCode(201);



            //HttpContext.Session.Set("Name", Encoding.ASCII.GetBytes(newContact.Name));
        }

        [HttpGet]
        [Route("[controller]/{UserName}/{id}")]
        public async Task<IActionResult> GetContactById(string id, string UserName)
        {
            // TODO: implement
            var Users = _service.GetAllUsers();

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
        public async Task<IActionResult> UpdateContact(string UserName, string id, [FromBody] UpdateContactParams updatedContact)
        {
            // TODO: implement


            int RetVal = _service.UpdateContact(id, UserName, updatedContact);


            if (RetVal == -1)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("[controller]/{UserName}/{id}")]
        public async Task<IActionResult> DeleteContact(string id, string UserName)
        {
            // TODO: implement


            int RetVal = _service.DeleteContact(id, UserName);


            if (RetVal == -1)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        [Route("[controller]/{UserName}/{id}/messages")]
        public async Task<IActionResult> GetMessages(string id, string UserName)
        {
            // TODO: implement
            var Users = _service.GetAllUsers();

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
        public async Task<IActionResult> CreateMessage(string UserName, string id, [FromBody] ContentParam Content)
        {
            // TODO: implement
            int RetVal = _service.CreateMessage(UserName, id, Content);


            if (RetVal == -1)
            {
                return NotFound();
            }


            return StatusCode(201);

        }

        [HttpPut]
        [Route("[controller]/{UserName}/{id}/messages/{id2}")]
        public async Task<IActionResult> UpdateMessage([Bind("id")] string id, [Bind("id2")] int id2, [Bind("UserName")] string UserName, [FromBody] ContentParam Content)
        {
            // TODO: implement

            int RetVal = _service.UpdateMessage(id, id2, UserName, Content);


            if (RetVal == -1)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("[controller]/{UserName}/{id}/messages/{id2}")]
        public async Task<IActionResult> DeleteMessage([Bind("id")] string id, [Bind("id2")] int id2, [Bind("UserName")] string UserName)
        {
            // TODO: implement

            int RetVal = _service.DeleteMessage(id, id2, UserName);


            if (RetVal == -1)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        [Route("[controller]/{UserName}/{id}/messages/{id2}")]
        public async Task<IActionResult> GetMessage([Bind("id")] string id, [Bind("id2")] int id2, [Bind("UserName")] string UserName)
        {
            // TODO: implement
            var Users = _service.GetAllUsers();

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
