using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MVC_Email_Azure.Models;
using System.Diagnostics;

namespace MVC_Email_Azure.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailService;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailService, IConfiguration config)
        {
            _logger = logger;
            _emailService = emailService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                return View();
            }

            await _emailService.SendEmailAsync(email, "testSubject", "testbody");

            return View("Index");
        }

        public IActionResult MySecret()
        {
            string mySecret = _config["MySecrets:MySecret"]!;

            if (string.IsNullOrEmpty(mySecret))
            {
                mySecret = "Not Today!";
            }

            ViewData["MySecret"] = mySecret;

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
    }
}