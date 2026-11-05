using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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