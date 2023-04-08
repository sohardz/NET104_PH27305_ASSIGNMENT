using Microsoft.AspNetCore.Mvc;
using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;
using NET104_PH27305_ASSIGNMENT.Services;

namespace NET104_PH27305_ASSIGNMENT.Controllers;

public class BillController : Controller
{
    private readonly ILogger<BillController> _logger;
    private readonly IBillServices _billServices;

    public BillController(ILogger<BillController> logger)
    {
        _logger = logger;
        _billServices = new BillServices();
    }

    public ActionResult Show()
    {
        List<Bill> lst = _billServices.GetAll();
        return View(lst);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Bill p)
    {
        if (_billServices.Create(p))
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
        var obj = _billServices.GetById(id);
        return View(obj);
    }

    public IActionResult Delete(Guid id)
    {
        if (_billServices.Delete(id))
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
        var obj = _billServices.GetById(id);
        return View(obj);
    }

    public IActionResult Edit(Bill p)
    {
        if (_billServices.Update(p))
        {
            return RedirectToAction("Show");
        }
        else
        {
            return BadRequest();
        }
    }
}
