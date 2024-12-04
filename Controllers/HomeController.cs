using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Test.Models;

namespace Test.Controllers
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
        public IActionResult Islem()
        {
            return View(new IslemModel());
            // ilk i�lemi bo� g�nderiyoruz
        }
        [HttpPost]
        public IActionResult Islem(IslemModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Oprt)
                {
                    case "+": model.Result = model.S1 + model.S2; break;
                    case "-": model.Result = model.S1 - model.S2; break;
                    case "*": model.Result = model.S1 * model.S2; break;
                    case "/":
                        if (model.S2 == 0)
                        {
                            ModelState.AddModelError("", "S�f�ra b�lme hatas�."); // Error model ine burada exec at�yoruz
                            return View("Index", model);
                        }
                        model.Result = model.S1 / model.S2;
                        break;
                    case "%": model.Result = (model.S1 * model.S2) / 100;break;
                    default:
                    ModelState.AddModelError("", "Hatal� i�lem");

                    return View("Index" , model);
                }
                // T�m i�lemleri .cshtml de ki value den gelen de�ere g�re yapmas�n� sa�l�yoruz
            }
            return View("Index", model);
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
