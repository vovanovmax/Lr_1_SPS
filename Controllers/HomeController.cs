using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;
using Lr_2.Services; // Підключаємо бібліотеку сервісів

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender; 

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync(model.ToEmail, model.Subject, model.Message);
                ViewBag.Status = "Лист успішно надіслано!";
                return View();
            }
            return View(model);
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

        public IActionResult Tables()
        {
            return View();
        }

        public IActionResult Charts()
        {
            return View();
        }

        public IActionResult layoutstatic()
        {
            return View();
        }

        public IActionResult layoutsidenavlight()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Password()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Error500()
        {
            return View();
        }

        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult Error401()
        {
            return View();
        }
    }
}