using Microsoft.AspNetCore.Mvc;
using SellerProduct.IServices;
using SellerProduct.Models;
using SellerProduct.Services;

namespace NET104_PH27305_ASSIGNMENT.Controllers;

public class CartDetailController : Controller
{
    private readonly ILogger<CartDetailController> _logger;
    private readonly ICartDetailServices _cartDetailServices;

    public CartDetailController(ILogger<CartDetailController> logger)
    {
        _logger = logger;
        _cartDetailServices = new CartDetailServices();
    }

    public ActionResult Show()
    {
        List<CartDetail> lst = _cartDetailServices.GetAll();
        return View(lst);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(CartDetail p)
    {
        if (_cartDetailServices.Create(p))
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
        var obj = _cartDetailServices.GetById(id);
        return View(obj);
    }

    public IActionResult Delete(Guid id)
    {
        if (_cartDetailServices.Delete(id))
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
        var obj = _cartDetailServices.GetById(id);
        return View(obj);
    }

    public IActionResult Edit(CartDetail p)
    {
        if (_cartDetailServices.Update(p))
        {
            return RedirectToAction("Show");
        }
        else
        {
            return BadRequest();
        }
    }
}
