using Microsoft.EntityFrameworkCore;
using RedPoc.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedPoc.Repository.Context
{
    public class RedContext : DbContext
    {
        public RedContext(DbContextOptions<RedContext> options)
                     : base(options)
        {
        }

        public RedContext()
         : base()
        {
        }

        public DbSet<Order> Order { get; set; }

    }
}
