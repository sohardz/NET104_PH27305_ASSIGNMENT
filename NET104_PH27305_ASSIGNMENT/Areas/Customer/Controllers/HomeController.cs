using Microsoft.AspNetCore.Mvc;
using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Services;

namespace NET104_PH27305_ASSIGNMENT.Areas.Customer.Controllers;

[Area("Customer")]
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
}
