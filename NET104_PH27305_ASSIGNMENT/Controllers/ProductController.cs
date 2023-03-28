using Microsoft.AspNetCore.Mvc;
using SellerProduct.IServices;
using SellerProduct.Models;
using SellerProduct.Services;

namespace NET104_PH27305_ASSIGNMENT.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductServices _productServices;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
        _productServices = new ProductServices();
    }

    public ActionResult Show()
    {
        List<Product> lst = _productServices.GetAll();
        return View(lst);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Product p)
    {
        if (_productServices.Create(p))
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
        var obj = _productServices.GetById(id);
        return View(obj);
    }

    public IActionResult Delete(Guid id)
    {
        if (_productServices.Delete(id))
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
        var obj = _productServices.GetById(id);
        return View(obj);
    }

    public IActionResult Edit(Product p)
    {
        if (p.AvailableQuantity <= 1)
        {
            ModelState.AddModelError("", "số lượng phải lớn hơn 1");
            return View();
        }
        else if (p.Price <= 1)
        {
            ModelState.AddModelError("", "giá phải lớn hơn 1");
            return View();
        }
        else if (p.Status <= 1)
        {
            ModelState.AddModelError("", "tình trạng phải lớn hơn 1");
            return View();
        }
        else if (_productServices.Update(p))
        {
            return RedirectToAction("Show");
        }
        else return BadRequest();
    }
}
