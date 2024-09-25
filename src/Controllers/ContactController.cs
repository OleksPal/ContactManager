using ContactManager.Extensions;
using ContactManager.Mappers;
using ContactManager.Models;
using ContactManager.Repositories;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactRepository.GetAllAsync();
            return View(contacts);
        }

        public async Task<JsonResult> GetAllContacts()
        {
            var contacts = await _contactRepository.GetAllAsync();
            return Json(contacts);
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

                        var contacts = records.Select(r => r.ToContact());
                        await _contactRepository.InsertRangeAsync(contacts);

                        // Display success message
                        TempData["Message"] = "File uploaded successfully!";
                    }
                    else
                    {
                        TempData["Message"] = "Only csv files are allowed.";
                    }
                }
                else
                {
                    // Display error message
                    TempData["Message"] = "Please select a file to upload.";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<ContentResult> DeleteContact(Guid id)
        {
            var deletedContact = await _contactRepository.DeleteAsync(id);

            if (deletedContact is null)
                return Content("Contact was not found!");
            else
                return Content("Contact was deleted successfully!");
        }
    }
}
