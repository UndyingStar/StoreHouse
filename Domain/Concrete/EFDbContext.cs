using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Materials> Materials { get; set; }
        public DbSet<Orders> Orders { get; set; }


        public DbSet<Users> Users { get; set; }
    }
}
