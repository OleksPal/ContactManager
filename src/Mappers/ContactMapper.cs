using ContactManager.Models;

namespace ContactManager.Mappers
{
    public static class ContactMapper
    {
        public static Contact ToContact(this ContactCsvRecord record)
        {
            return new Contact
            {
                Id = Guid.NewGuid(),
                Name = record.Name,
                DateOfBirth = record.DateOfBirth,
                Married = record.Married,
                Phone = record.Phone,
                Salary = record.Salary
            };
        }
    }
}
