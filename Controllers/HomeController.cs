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
            // ilk iþlemi boþ gönderiyoruz
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
                            ModelState.AddModelError("", "Sýfýra bölme hatasý."); // Error model ine burada exec atýyoruz
                            return View("Index", model);
                        }
                        model.Result = model.S1 / model.S2;
                        break;
                    case "%": model.Result = (model.S1 * model.S2) / 100;break;
                    default:
                    ModelState.AddModelError("", "Hatalý iþlem");

                    return View("Index" , model);
                }
                // Tüm iþlemleri .cshtml de ki value den gelen deðere göre yapmasýný saðlýyoruz
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
