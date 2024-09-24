using ContactManager.Extensions;
using ContactManager.Models;
using ContactManager.Repositories;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                // Handle file upload logic here
                if (file != null && file.Length > 0)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    string[] allowedExtensions = { ".csv" };

                    if (allowedExtensions.Contains(fileExtension))
                    {
                        var records = new List<ContactCsvRecord>();
                        using (var reader = new StringReader(await file.ReadAsStringAsync()))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            records = csv.GetRecords<ContactCsvRecord>().ToList();
                        }

                        _contactRepository.InsertRangeAsync(contacts);

                        // Display success message
                        ViewBag.Message = "File uploaded successfully!";
                    }
                    else
                    {
                        ViewBag.Message = "Only csv files are allowed.";
                    }
                }
                else
                {
                    // Display error message
                    ViewBag.Message = "Please select a file to upload.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }
    }
}
