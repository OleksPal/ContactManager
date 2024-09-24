using ContactManager.Models;

namespace ContactManager.Repositories
{
    public interface IContactRepository
    {
        Task<ICollection<Contact>> GetAllAsync();
        Task<ICollection<Contact>> InsertRangeAsync(IEnumerable<Contact> contacts);
    }
}
