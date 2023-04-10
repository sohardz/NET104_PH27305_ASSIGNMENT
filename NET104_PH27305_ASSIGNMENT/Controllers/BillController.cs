using Microsoft.AspNetCore.Mvc;
using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;
using NET104_PH27305_ASSIGNMENT.Services;

namespace NET104_PH27305_ASSIGNMENT.Controllers;

public class BillController : Controller
{
    private readonly ILogger<BillController> _logger;
    private readonly IBillServices _billServices;
    private readonly IProductServices _productServices;
    private readonly IBillDetailServices _billDetailServices;
    private readonly ICartDetailServices _cartDetailServices;

    public BillController(ILogger<BillController> logger)
    {
        _logger = logger;
        _billServices = new BillServices();
        _productServices = new ProductServices();
        _billDetailServices = new BillDetailServices();
        _cartDetailServices = new CartDetailServices();
    }

    public ActionResult Show(Guid billId)
    {
        var listBilldetails = _billDetailServices.GetAll();
        ViewBag.listBillDetails = listBilldetails.Where(c => c.BillId == billId).ToList();
        ViewBag.listProduct = _productServices.GetAll();
        return View();
    }

    // card details có user id với product id
    public ActionResult Create(Guid userId)
    {
        List<CartDetail> lstCartdetails = _cartDetailServices.GetAll().Where(c=>c.UserId == userId).ToList();

        var bill = new Bill()
        {
            Id = Guid.NewGuid(),
            DateofCreation = DateTime.Now,
            UserId = userId,
            Status = 1,
        };
                
        if (_billServices.Create(bill))
        {
            foreach (var x in lstCartdetails)
            {
                var billDetail = new BillDetails()
                {
                    ProductId = x.ProductId,
                    BillId = bill.Id,
                    Quantity = x.Quantity,
                    Price = _productServices.GetAll().FirstOrDefault(c => c.Id == x.ProductId).Price,
                };
                _billDetailServices.Create(billDetail);
                _cartDetailServices.Delete(x.ProductId,x.UserId);
                var product = _productServices.GetAll().FirstOrDefault(c=>c.Id == x.ProductId);
                product.AvailableQuantity = product.AvailableQuantity - x.Quantity;
                _productServices.Update(product);
            }

            return RedirectToAction("Show", new { billId = bill.Id });
        }
        else
        {
            return BadRequest();
        }
    }

    public IActionResult Details(Guid id)
    {
        var obj = _billServices.GetById(id);
        return View(obj);
    }
}
