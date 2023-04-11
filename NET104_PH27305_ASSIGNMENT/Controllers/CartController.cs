using Microsoft.AspNetCore.Mvc;
using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;
using NET104_PH27305_ASSIGNMENT.Services;

namespace NET104_PH27305_ASSIGNMENT.Controllers;

public class CartController : Controller
{
    private readonly ILogger<CartController> _logger;
    private readonly ICartServices _cartServices;
    private readonly ICartDetailServices _cartDetailServices;
    private readonly IProductServices _productServices;

    public CartController(ILogger<CartController> logger)
    {
        _logger = logger;
        _cartServices = new CartServices();
        _productServices = new ProductServices();
        _cartDetailServices = new CartDetailServices();
    }

    public ActionResult Show()
    {
        var userId = HttpContext.Session.GetString("userId");
        ViewData["userId"] = userId;
        if (!string.IsNullOrEmpty(userId))
        {
            ViewBag.listCartDetails = _cartDetailServices.GetAll().Where(c => c.UserId == Guid.Parse(userId)).ToList();
            ViewBag.listProduct = _productServices.GetAll();
            return View();
        }
        return RedirectToAction("Login", "Home");
    }

    public ActionResult Create(Guid productId)
    {
        var userId = HttpContext.Session.GetString("userId");
        Guid id = Guid.Parse(userId);
        if (!string.IsNullOrEmpty(userId))
        {
            List<CartDetail> cartDetails = _cartDetailServices.GetAll().Where(c => c.UserId == id).ToList();
            CartDetail obj = new()
            {
                UserId = id,
                ProductId = productId,
                Quantity = 1
            };
            if (cartDetails.Any(c => c.UserId == id && c.ProductId == productId))
            {
                obj.Quantity = cartDetails.FirstOrDefault(c => c.UserId == id && c.ProductId == productId).Quantity + 1;
                return _cartDetailServices.Update(obj.ProductId, obj.UserId, obj) ? RedirectToAction("Show") : BadRequest();
            }
            else
            {
                return _cartDetailServices.Create(obj) ? RedirectToAction("Show") : BadRequest();
            }
        }
        return BadRequest();
    }


    public IActionResult Details(Guid productId, Guid userId)
    {
        ViewBag.cartDetails = _cartDetailServices.GetById(productId, userId);

        return View();
    }

    public IActionResult Delete(Guid productId)
    {
        Guid id = Guid.Parse(HttpContext.Session.GetString("userId"));
        return _cartDetailServices.Delete(productId, id) ? RedirectToAction("Show") : BadRequest();
    }

    public IActionResult Edit(CartDetail obj)
    {
        obj.UserId = Guid.Parse("00000000-0000-0000-0000-000000000000");

        var result = _cartDetailServices.Update(obj.ProductId, obj.UserId, obj);

        if (result)
        {
            return RedirectToAction("Index");
        }

        return View();
    }
}
