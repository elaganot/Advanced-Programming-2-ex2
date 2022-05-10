using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Chat.Models;

namespace E_Chat.Data
{
    public class E_ChatContext : DbContext
    {
        public E_ChatContext (DbContextOptions<E_ChatContext> options)
            : base(options)
        {
        }

        public DbSet<E_Chat.Models.Rating> Rating { get; set; }
    }
}
