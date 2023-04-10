using Microsoft.AspNetCore.Mvc;
using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;
using NET104_PH27305_ASSIGNMENT.Services;
using System.Diagnostics;

namespace NET104_PH27305_ASSIGNMENT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServices _productServices;
        private readonly IUserServices _userServices;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productServices = new ProductServices();
            _userServices = new UserServices();
        }

        public IActionResult Index()
        {
            var lst = _productServices.GetAll().Where(c => c.AvailableQuantity > 5);
            return View(lst);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (_userServices.Create(user))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = _userServices.Login(email,password);
                if (user != null)
                {
                    HttpContext.Session.SetString("email", email);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}