using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatterApi.Domain.Entities
{
    public partial class ChatterApiContext : DbContext
    {
        public ChatterApiContext() : base("name=ChatterApiContext")
        {

        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Message>().HasRequired(cm => cm.User);
        }
    }
}
