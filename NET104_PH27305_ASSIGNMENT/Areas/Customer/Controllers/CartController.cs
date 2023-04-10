using Microsoft.AspNetCore.Mvc;

namespace NET104_PH27305_ASSIGNMENT.Areas.Customer.Controllers;

[Area("Customer")]
public class CartController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
