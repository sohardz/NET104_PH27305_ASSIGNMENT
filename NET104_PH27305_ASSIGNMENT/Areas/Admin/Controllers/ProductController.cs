using Microsoft.AspNetCore.Mvc;

namespace NET104_PH27305_ASSIGNMENT.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
