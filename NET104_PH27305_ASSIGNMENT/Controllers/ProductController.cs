using Microsoft.AspNetCore.Mvc;
using NET104_PH27305_ASSIGNMENT.Services;
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
    public ActionResult Create(Product p, IFormFile imageFile)
    {
        //if (imageFile != null && imageFile.Length > 0)
        //{
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", imageFile.FileName);
        //    var stream = new FileStream(path, FileMode.Create);
        //    imageFile.CopyTo(stream);
        //    p.Description = imageFile.FileName;
        //}

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

        var products = SessionServices.GetObjFromSession(HttpContext.Session, "History");
        var existingProduct = products.FirstOrDefault(p => p.Id == id);
        if (products.Count == 0)
        {
            products.Add(obj);
            SessionServices.SetObjToSession(HttpContext.Session, "History", products);
        }
        else
        {
            if (SessionServices.CheckObjInList(id, products))
            {
                if (existingProduct != null)
                {
                    return View(obj);
                }
                else
                {
                    products.Remove(existingProduct);
                }
            }
            else
            {
                products.Add(obj);
                SessionServices.SetObjToSession(HttpContext.Session, "History", products);
            }
        }

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

    [HttpPost]
    public IActionResult CallBack(Guid id, string action)
    {
        if (action == "CallBack")
        {
            var obj = SessionServices.GetObjFromSession(HttpContext.Session, "History").FirstOrDefault(p => p.Id == id);
            if (_productServices.Update(obj))
            {
                return RedirectToAction("Show");
            }
            else return BadRequest();
        }
        else
        {
            var obj = _productServices.GetById(id);
            return RedirectToAction("Update", obj);
        }
    }

    public IActionResult HistoryEdit()
    {
        var products = SessionServices.GetObjFromSession(HttpContext.Session, "History");
        return View(products);
    }

}
