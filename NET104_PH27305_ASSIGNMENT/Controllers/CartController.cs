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

    public ActionResult Show(Guid userId)
    {
        var listCartDetails = _cartDetailServices.GetAll();
        ViewBag.listCartDetails = listCartDetails.Where(c => c.UserId == userId).ToList();
        ViewBag.listProduct = _productServices.GetAll();
        return View();
    }

    public ActionResult Create(Guid productId, Guid userId)
    {
        List<CartDetail> cartDetails = _cartDetailServices.GetAll();

        CartDetail obj = new()
        {
            UserId = userId,
            ProductId = productId,
            Quantity = 1
        };

        if (cartDetails.Any(c => c.UserId == userId && c.ProductId == productId))
        {
            obj.Quantity = cartDetails.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId).Quantity + 1;
            return _cartDetailServices.Update(obj.ProductId, obj.UserId, obj) ? RedirectToAction("Show", new { userId }) : BadRequest();
        }
        else
        {
            return _cartDetailServices.Create(obj) ? RedirectToAction("Show", new { userId }) : BadRequest();
        }
    }


    public IActionResult Details(Guid productId, Guid userId)
    {
        ViewBag.cartDetails = _cartDetailServices.GetById(productId, userId);

        return View();
    }

    public IActionResult Delete(Guid productId, Guid userId)
    {
        return _cartDetailServices.Delete(productId, userId) ? RedirectToAction("Show", new { userId }) : BadRequest();
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
