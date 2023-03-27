using Microsoft.AspNetCore.Mvc;
using NET104_PH27305_ASSIGNMENT.Models;
using SellerProduct.IServices;
using SellerProduct.Services;
using System.Diagnostics;

namespace NET104_PH27305_ASSIGNMENT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServices _productServices;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productServices = new ProductServices();
        }

        public IActionResult Index()
        {
            var lst = _productServices.GetAll().Where(c => c.AvailableQuantity > 5);
            return View(lst);
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