using Microsoft.AspNetCore.Mvc;
using SellerProduct.IServices;
using SellerProduct.Models;
using SellerProduct.Services;

namespace NET104_PH27305_ASSIGNMENT.Controllers;

public class CartController : Controller
{
    private readonly ILogger<CartController> _logger;
    private readonly ICartServices _cartServices;

    public CartController(ILogger<CartController> logger)
    {
        _logger = logger;
        _cartServices = new CartServices();
    }

    public ActionResult Show()
    {
        List<Cart> lst = _cartServices.GetAll();
        return View(lst);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Cart p)
    {
        if (_cartServices.Create(p))
        {
            return RedirectToAction("Show");
        }
        else
        {
            return BadRequest();
        }
    }

    public IActionResult Details(Guid id)
    {
        var obj = _cartServices.GetById(id);
        return View(obj);
    }

    public IActionResult Delete(Guid id)
    {
        if (_cartServices.Delete(id))
        {
            return RedirectToAction("Show");
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var obj = _cartServices.GetById(id);
        return View(obj);
    }

    public IActionResult Edit(Cart p)
    {
        if (_cartServices.Update(p))
        {
            return RedirectToAction("Show");
        }
        else
        {
            return BadRequest();
        }
    }
}
