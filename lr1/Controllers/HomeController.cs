using lr1.Models;
using lr1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lr1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private readonly IEmailSender _emailSender;

        public HomeController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Send()
        {
            await _emailSender.SendEmailAsync(
                "recipient@example.com",
                "Test",
                "Hello World!"
            );

            return Content("Email sent!");
        }
        
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/Files",
                file.FileName
            );

            Directory.CreateDirectory("wwwroot/Files");

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Content("Файл завантажено успішно");
        }
    }

}