using Microsoft.AspNetCore.Mvc;
using SellerProduct.IServices;
using SellerProduct.Models;
using SellerProduct.Services;

namespace NET104_PH27305_ASSIGNMENT.Controllers;

public class BillDetailController : Controller
{
    private readonly ILogger<BillDetailController> _logger;
    private readonly IBillDetailServices _billDetailServices;

    public BillDetailController(ILogger<BillDetailController> logger)
    {
        _logger = logger;
        _billDetailServices = new BillDetailServices();
    }

    public ActionResult Show()
    {
        List<BillDetails> lst = _billDetailServices.GetAll();
        return View(lst);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(BillDetails p)
    {
        if (_billDetailServices.Create(p))
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
        var obj = _billDetailServices.GetById(id);
        return View(obj);
    }

    public IActionResult Delete(Guid id)
    {
        if (_billDetailServices.Delete(id))
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
        var obj = _billDetailServices.GetById(id);
        return View(obj);
    }

    public IActionResult Edit(BillDetails p)
    {
        if (_billDetailServices.Update(p))
        {
            return RedirectToAction("Show");
        }
        else
        {
            return BadRequest();
        }
    }
}
