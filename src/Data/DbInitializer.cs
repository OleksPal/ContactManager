using ContactManager.Models;

namespace ContactManager.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ContactManagerContext context)
        {
            if (context.Contacts.Any())
                return;

            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = "TestContact",
                DateOfBirth = DateOnly.MinValue,
                Married = false,
                Phone = "+1234567890",
                Salary = 100
            };

            context.Contacts.Add(contact);

            context.SaveChanges();
        }
    }
}
