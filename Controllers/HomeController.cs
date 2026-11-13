using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;
using Lr_2.Services;
using Microsoft.AspNetCore.Localization;


namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _appEnvironment;

        public HomeController(
            ILogger<HomeController> logger,
            IEmailSender emailSender,
            IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _emailSender = emailSender;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl ?? "~/");
        }
        
        [HttpGet]
        public IActionResult Files()
        {
            string filesFolder = Path.Combine(_appEnvironment.WebRootPath, "Files");
            if (!Directory.Exists(filesFolder))
            {
                Directory.CreateDirectory(filesFolder);
            }

            var directoryInfo = new DirectoryInfo(filesFolder);
            var filesList = directoryInfo.GetFiles()
                .OrderByDescending(f => f.LastWriteTime)
                .Select(f => new FileViewModel
                {
                    Name = f.Name,
                    Extension = f.Extension.ToLower(),
                    Size = $"{Math.Round(f.Length / 1024.0, 2)} KB",
                    LastModified = f.LastWriteTime
                })
                .ToList();

            return View(filesList);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                string filesFolder = Path.Combine(_appEnvironment.WebRootPath, "Files");
                if (!Directory.Exists(filesFolder))
                {
                    Directory.CreateDirectory(filesFolder);
                }

                string safeFileName = Path.GetFileName(uploadedFile.FileName);
                string filePath = Path.Combine(filesFolder, safeFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                TempData["FileMessage"] = $"Файл «{safeFileName}» успішно завантажено на сервер!";
            }
            else
            {
                TempData["FileError"] = "Оберіть файл перед натисканням кнопки завантаження.";
            }

            return RedirectToAction("Files");
        }


        [HttpGet]
        public async Task<IActionResult> ViewFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return RedirectToAction("Files");

            string safeFileName = Path.GetFileName(fileName);
            string filePath = Path.Combine(_appEnvironment.WebRootPath, "Files", safeFileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Файл не знайдено на сервері.");

            var fileInfo = new FileInfo(filePath);
            string ext = fileInfo.Extension.ToLower();

            var model = new FileViewModel
            {
                Name = fileInfo.Name,
                Extension = ext,
                Size = $"{Math.Round(fileInfo.Length / 1024.0, 2)} KB",
                LastModified = fileInfo.LastWriteTime
            };

            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg" };
            string[] textExtensions = { ".txt", ".log", ".json", ".xml", ".cs", ".html", ".css", ".js", ".md", ".csv" };

            if (imageExtensions.Contains(ext))
            {
                model.FileCategory = "image";
            }
            else if (ext == ".pdf")
            {
                model.FileCategory = "pdf";
            }
            else if (textExtensions.Contains(ext))
            {
                model.FileCategory = "text";
                model.TextContent = await System.IO.File.ReadAllTextAsync(filePath);
            }
            else
            {
                model.FileCategory = "other";
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult SendEmail() => View();

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

        public IActionResult Privacy() => View();

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