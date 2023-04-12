using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;
using NET104_PH27305_ASSIGNMENT.Services;
using System.ComponentModel.DataAnnotations;

namespace NET104_PH27305_ASSIGNMENT.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly IUserServices _userServices;
    private readonly IRoleServices _roleServices;
    private readonly IProductServices _productServices;
    private readonly IBillServices _billServices;
    private readonly IBillDetailServices _billDetailServices;
    private readonly ICartServices _cartServices;
    private readonly ICartDetailServices _cartDetailServices;

    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
        _userServices = new UserServices();
        _roleServices = new RoleServices();
        _productServices = new ProductServices();
        _billServices = new BillServices();
        _cartServices = new CartServices();
        _billDetailServices = new BillDetailServices();
        _cartDetailServices = new CartDetailServices();
    }

    public IActionResult Index()
    {
        return View();
    }

    #region Đăng nhập
    public bool CheckLogin(string email, string password)
    {
        var user = _userServices.GetByEmail(email.Trim());
        if (user != null && user.Password == password && user.Status == 1)
        {
            var role = _roleServices.GetById(user.RoleId);
            if (role.Name == "Staff") return true;
        }
        return false;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var isValid = CheckLogin(email, password);
        if (isValid)
        {
            var user = _userServices.GetByEmail(email.Trim());
            var userId = user.Id.ToString();
            HttpContext.Session.SetString("userId", userId);
            if (user != null)
                return RedirectToAction("Index");
            ViewData["ErrorMessage"] = "User not found";
        }
        else
        {
            ViewBag.ErrorMessage = "The user name or password provided is incorrect.";
        }
        return BadRequest();
    }
    #endregion

    #region Sản phẩm
    public IActionResult ShowProduct()
    {
        List<Product> lst = _productServices.GetAll();
        return View(lst);
    }

    public ActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    public ActionResult CreateProduct(Product p, IFormFile imageFile)
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
            return RedirectToAction("ShowProduct");
        }
        else
        {
            return BadRequest();
        }
    }

    public IActionResult DetailsProduct(Guid id)
    {
        var obj = _productServices.GetById(id);
        return View(obj);
    }

    public IActionResult DeleteProduct(Guid id)
    {
        if (_productServices.Delete(id))
        {
            return RedirectToAction("ShowProduct");
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult EditProduct(Guid id)
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

    public IActionResult EditProduct(Product p)
    {
        if (p.AvailableQuantity < 1)
        {
            ModelState.AddModelError("", "số lượng phải lớn hơn 0");
            return View();
        }
        else if (p.Price <= 1)
        {
            ModelState.AddModelError("", "giá phải lớn hơn 1");
            return View();
        }
        else if (p.Status < 0)
        {
            ModelState.AddModelError("", "tình trạng phải lớn hơn hoặc bằng 0");
            return View();
        }
        else if (_productServices.Update(p))
        {
            return RedirectToAction("ShowProduct");
        }
        else return BadRequest();
    }

    [HttpPost]
    public IActionResult CallBackProduct(Guid id, string action)
    {
        if (action == "CallBack")
        {
            var obj = SessionServices.GetObjFromSession(HttpContext.Session, "History").FirstOrDefault(p => p.Id == id);
            if (_productServices.Update(obj))
            {
                return RedirectToAction("ShowProduct");
            }
            else return BadRequest();
        }
        else
        {
            var obj = _productServices.GetById(id);
            return RedirectToAction("Update", obj);
        }
    }

    public IActionResult HistoryEditProduct()
    {
        var products = SessionServices.GetObjFromSession(HttpContext.Session, "History");
        return View(products);
    }
    #endregion
}

