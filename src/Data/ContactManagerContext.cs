using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data
{
    public class ContactManagerContext : DbContext
    {
        public ContactManagerContext(DbContextOptions<ContactManagerContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
