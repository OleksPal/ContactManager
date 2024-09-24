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

        public async Task<Contact> UpdateAsync(Guid id, ContactCsvRecord record)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (contact is null)
                return null;

            contact.Name = record.Name;
            contact.DateOfBirth = record.DateOfBirth;
            contact.Married = record.Married;
            contact.Phone = record.Phone;
            contact.Salary = record.Salary;

            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact> DeleteAsync(Guid id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (contact is null)
                return null;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }           
    }
}
