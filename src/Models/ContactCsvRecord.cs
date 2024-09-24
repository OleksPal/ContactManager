using CsvHelper.Configuration.Attributes;

namespace ContactManager.Models
{
    public class ContactCsvRecord
    {
        [Name("Name")]
        public string Name { get; set; }

        [Name("DateOfBirth")]
        public DateOnly DateOfBirth { get; set; }

        [Name("Married")]
        public bool Married { get; set; }

        [Name("Phone")]
        public string Phone { get; set; }

        [Name("Salary")]
        public decimal Salary { get; set; }
    }
}
