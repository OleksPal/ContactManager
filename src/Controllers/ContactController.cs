using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(IFormFile file)
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
                        // Save the file to the server or perform any other necessary operations
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);

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
