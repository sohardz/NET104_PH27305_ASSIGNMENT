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
        private readonly IRoleServices _roleServices;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productServices = new ProductServices();
            _userServices = new UserServices();
            _roleServices = new RoleServices();
        }

        public IActionResult Index()
        {
            var lst = _productServices.GetAll().Where(c => c.AvailableQuantity > 1);
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

        public bool CheckLogin(string email, string password)
        {
            var user = _userServices.GetByEmail(email.Trim());
            if (user != null && user.Password == password && user.Status == 1)
            {
                var role = _roleServices.GetById(user.RoleId);
                if (role.Name != "Staff") return true;
            }
            return false;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var isValid = CheckLogin(email, password);
            if (isValid)
            {
                var user = _userServices.GetByEmail(email.Trim());
                var userId = user.Id.ToString();
                HttpContext.Session.SetString("userId", userId);
                if (user != null)
                    return RedirectToAction("Index");
                ViewData["ErrorMessage"] = "User not found";
            }
            else
            {
                ViewBag.ErrorMessage = "The user name or password provided is incorrect.";
            }
            return BadRequest();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}