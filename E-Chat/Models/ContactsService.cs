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

namespace E_Chat.Models
{
    public interface IContactsService
    {
        List<User> GetAllUsers();
        public int SaveNewMessage(TransferParam newMessage);
        public int CreateNewConversation(InvitationsParam newConversation);
        void SaveNewUser(User user);
        public int CreateNewContact(string UserName, Contact newContact);
        public int UpdateContact(string id, string UserName, UpdateContactParams updatedContact);
        public int DeleteContact(string id, string UserName);
        public int CreateMessage(string UserName, string id, ContentParam Content);
        public int UpdateMessage(string id, int id2, string UserName, ContentParam Content);
        public int DeleteMessage(string id, int id2, string UserName);




    }
    public class ContactsService : IContactsService
    {

        private readonly E_ChatContext _context;

        public List<User> GetAllUsers()
        {
            return _context.User.ToList();
        }

        public int SaveNewMessage(TransferParam newMessage)
        {
            var user = _context.User.Where(x => x.UserName == newMessage.To).FirstOrDefault();
            if (user == null)
            {
                return -1;
            }

            var contact = _context.Contact
                .Where(x => x.UserId == user.Id && x.ContactName == newMessage.From)
                .FirstOrDefault();
            if (contact == null)
            {
                return -1;
            }

            
            var message = new Message() { 
                From = newMessage.From,
                To = newMessage.To,
                Content = newMessage.Content, 
                Created = DateTime.Now.ToString()
            };
            _context.Message.Add(message);
            _context.SaveChanges();

            return 0;
        }

        public int CreateNewConversation(InvitationsParam newConversation)
        {
            var user = _context.User.Where(x => x.UserName == newConversation.To).FirstOrDefault();
            if (user == null)
            {
                return -1;
            }

            var newContact = new Contact() { 
                UserId = user.Id, 
                ContactUserName = newConversation.From, 
                ContactName = newConversation.From, 
                Server = newConversation.Server
            };

            var contact = _context.Contact
                .Where(x => x.UserId == user.Id && x.ContactUserName == newContact.ContactUserName)
                .FirstOrDefault();
            if (contact == null)
            {
                _context.Contact.Add(newContact);
                _context.SaveChanges();
            }

            return 0;
        }


        public void SaveNewUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public int CreateNewContact(string UserName, Contact newContact)
        {
            var user = _context.User.Where(x => x.UserName == UserName).FirstOrDefault();
            if (user == null)
            {
                return -1;
            }

            var contact = user.MyContacts.Find(x => x.Id == newContact.Id);

            if (contact == null)
            {
                user.MyContacts.Add(newContact);
            }

            return 0;
        }

        public int UpdateContact(string id, string UserName, UpdateContactParams updatedContact)
        {
            var user = _context.User.Where(x => x.UserName == UserName).FirstOrDefault();

            if (user == null)
            {
                return -1;
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return -1;
            }

            user.MyContacts.Find(x => x.Id == id).Name = updatedContact.Name;
            user.MyContacts.Find(x => x.Id == id).Server = updatedContact.Server;
            return 0;
        }

        public int DeleteContact(string id, string UserName)
        {
            var user = _context.User.Where(x => x.UserName == UserName).FirstOrDefault();

            if (user == null)
            {
                return -1;
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return -1;
            }

            user.MyContacts.Remove(contact);
            return 0;
        }

        public int CreateMessage(string UserName, string id, ContentParam Content)
        {
            var user = _context.User.Where(x => x.UserName == UserName).FirstOrDefault();

            if (user == null)
            {
                return -1;
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return -1;
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
            var message = new Message() { Id = MessageId, Content = Content.Content, Created = DateTime.Now.ToString(), Sent = true };
            user.MyContacts.Find(x => x.Id == id).Messages.Add(message);
            user.MyContacts.Find(x => x.Id == id).Last = Content.Content;
            user.MyContacts.Find(x => x.Id == id).Lastdate = message.Created;

            return 0;
        }

        public int UpdateMessage(string id, int id2, string UserName, ContentParam Content)
        {
            var user = _context.User.Where(x => x.UserName == UserName).FirstOrDefault();

            if (user == null)
            {
                return -1;
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return -1;
            }

            if (contact.Messages == null)
            {
                return -1;
            }

            if (contact.Messages.Find(x => x.Id == id2) == null)
            {
                return -1;
            }

            user.MyContacts.Find(x => x.Id == id).Messages.Find(x => x.Id == id2).Content = Content.Content;
            user.MyContacts.Find(x => x.Id == id).Last = Content.Content;

            return 0;
        }

        public int DeleteMessage(string id, int id2, string UserName)
        {
            var user = _context.User.Where(x => x.UserName == UserName).FirstOrDefault();

            if (user == null)
            {
                return -1;
            }

            var contact = user.MyContacts.Find(x => x.Id == id);
            if (contact == null)
            {
                return -1;
            }

            if (contact.Messages == null)
            {
                return -1;
            }

            var message = contact.Messages.Find(x => x.Id == id2);
            if (message == null)
            {
                return -1;
            }

            contact.Messages.Remove(message);

            return 0;
        }

    }


}