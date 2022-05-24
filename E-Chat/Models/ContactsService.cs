﻿using System;
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
        public int UpdateContact(string id, string UserName, Contact updatedContact);
        public int DeleteContact(string id, string UserName);
        public int CreateMessage(string UserName, string id, Content Content);
        public int UpdateMessage(string id, int id2, string UserName, string Content);
        public int DeleteMessage(string id, int id2, string UserName);




    }
    public class ContactsService : IContactsService
    {

        //private static List<User> Users { get; set; }

        private static List<User> Users = new List<User>();

        public List<User> GetAllUsers()
        {
            return Users;
        }

        public int SaveNewMessage(TransferParam newMessage)
        {
            var user = Users.Find(x => x.UserName == newMessage.To);

            if (user == null)
            {
                return -1;
            }

            var contact = user.MyContacts.Find(x => x.Id == newMessage.From);
            if (contact == null)
            {
                return -1;
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

            return 0;
        }

        public int CreateNewConversation(InvitationsParam newConversation)
        {
            var user = Users.Find(x => x.UserName == newConversation.To);

            if (user == null)
            {
                return -1;
            }

            var newContact = new Contact() { Id = newConversation.From, Name = newConversation.From, Server = newConversation.Server, Messages = new List<Message>() };

            var contact = user.MyContacts.Find(x => x.Id == newConversation.From);

            if (contact == null)
            {
                user.MyContacts.Add(newContact);
            }

            return 0;
        }


        public void SaveNewUser(User user)
        {
            Users.Add(user);
        }

        public int CreateNewContact(string UserName, Contact newContact)
        {
            var user = Users.Find(x => x.UserName == UserName);
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

        public int UpdateContact(string id, string UserName, Contact updatedContact)
        {
            var user = Users.Find(x => x.UserName == UserName);

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
            var user = Users.Find(x => x.UserName == UserName);

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

        public int CreateMessage(string UserName, string id, Content Content)
        {
            var user = Users.Find(x => x.UserName == UserName);

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
            var message = new Message() { Id = MessageId, Content = Content.Text, Created = DateTime.Now.ToString(), Sent = true };
            user.MyContacts.Find(x => x.Id == id).Messages.Add(message);
            user.MyContacts.Find(x => x.Id == id).Last = Content.Text;
            user.MyContacts.Find(x => x.Id == id).Lastdate = message.Created;

            return 0;
        }

        public int UpdateMessage(string id, int id2, string UserName, string Content)
        {
            var user = Users.Find(x => x.UserName == UserName);

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

            user.MyContacts.Find(x => x.Id == id).Messages.Find(x => x.Id == id2).Content = Content;
            user.MyContacts.Find(x => x.Id == id).Last = Content;

            return 0;
        }

        public int DeleteMessage(string id, int id2, string UserName)
        {
            var user = Users.Find(x => x.UserName == UserName);

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