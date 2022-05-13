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

        public ContactsController(E_ChatContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // TODO: implement

            var value = HttpContext.Session.Get("Name");
            return Json(new
            {
                Name = value
            });
        }

        [HttpPost]
        public async Task<IActionResult> Index(Contact newContact)
        {
            // TODO: implement
            HttpContext.Session.Set("Name", Encoding.ASCII.GetBytes(newContact.Name));
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            // TODO: implement
            return Json(new
            {
                Id = id
            });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateContact(int id)
        {
            // TODO: implement
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            // TODO: implement
            return Ok();
        }

        [HttpGet]
        [Route("{id}/messages")]
        public async Task<IActionResult> GetMessages(int id)
        {
            // TODO: implement
            return Json(new
            {
                Messages = new int[] { id, id, id }
            });
        }

        [HttpPost]
        [Route("{id}/messages")]
        public async Task<IActionResult> CreateMessage(int id)
        {
            // TODO: implement
            return Json(new
            {
                Messages = new int[] { id, id, id }
            });
        }

        [HttpPut]
        [Route("{id}/messages/{id2}")]
        public async Task<IActionResult> UpdateMessage([Bind("id")] int id, [Bind("id2")] int id2, [FromBody] Message message)
        {
            // TODO: implement
            return Ok();
        }

        [HttpDelete]
        [Route("{id}/messages/{id2}")]
        public async Task<IActionResult> DeleteMessage([Bind("id")] int id, [Bind("id2")] int id2)
        {
            // TODO: implement
            return Ok();
        }

        [HttpGet]
        [Route("{id}/messages/{id2}")]
        public async Task<IActionResult> GetMessage([Bind("id")] int id, [Bind("id2")] int id2)
        {
            // TODO: implement
            return Json(new
            {
                CoolMessages = new int[] { id, id2, id }
            });
        }
    }
}
