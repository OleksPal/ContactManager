using ContactManager.Data;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactManagerContext _context;

        public ContactRepository(ContactManagerContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<ICollection<Contact>> InsertRangeAsync(IEnumerable<Contact> contacts)
        {
            await _context.Contacts.AddRangeAsync(contacts);
            await _context.SaveChangesAsync();

            return contacts.ToList();
        }
    }
}
