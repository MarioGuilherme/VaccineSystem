using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VaccineSystem.Models;
using System.Diagnostics;

namespace VaccineSystem.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        // Método responsável por retorar a View da página inicial do projeto
        public IActionResult Index() {
            return View();
        }

        // Método responsável por retorar a View de erro
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}