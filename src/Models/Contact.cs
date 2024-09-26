using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be over 100 characters")]
        public string Name { get; set; }

        [Display(Name = "Date of birth")]
        public DateOnly DateOfBirth { get; set; }

        public bool Married { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Precision(18, 2)]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Salary cannot be lower than 0")]
        public decimal Salary { get; set; }
    }
}
