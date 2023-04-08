using Microsoft.AspNetCore.Mvc;
using SellerProduct.IServices;
using SellerProduct.Models;
using SellerProduct.Services;

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

    //[Route("Show/{id}")]
    public ActionResult Show(Guid userId)
    {
        //userId = Guid.Parse("00000000-0000-0000-0000-000000000000");
        //if (idUser == Guid.Empty)
        //{
        //    return RedirectToAction("Index", "Home");
        //}

        //userId = Guid.Parse("0D77B2C0-DFD1-4DA2-DF97-08DB35CDB8BE");

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

        // Check sản phẩm đã có trong giỏ hàng hay chưa
        // Nếu có => Update +1 cho Amount
        // Nếu không => Create
        if (cartDetails.Any(c => c.UserId == userId && c.ProductId == productId))
        {
            // Update
            obj.Quantity = cartDetails.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId).Quantity + 1;
            var resultUpdate = _cartDetailServices.Update(obj.ProductId, obj.UserId, obj);

            if (resultUpdate)
            {
                return RedirectToAction("Show", new { userId = userId });
            }
        }
        else
        {
            var result = _cartDetailServices.Create(obj);

            if (result)
            {
                return RedirectToAction("Show", new { userId = userId });
            }
        }

        return RedirectToAction("Index", "Home");
    }


    public IActionResult Details(Guid productId, Guid userId)
    {
        ViewBag.cartDetails = _cartDetailServices.GetById(productId, userId);

        return View();
    }

    public IActionResult Delete(Guid productId, Guid userId)
    {
        var result = _cartDetailServices.Delete(productId, userId);

        return RedirectToAction("Index", new { idUser = Guid.Empty });
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
