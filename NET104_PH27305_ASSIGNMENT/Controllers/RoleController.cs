using Microsoft.AspNetCore.Mvc;
using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;
using NET104_PH27305_ASSIGNMENT.Services;

namespace NET104_PH27305_ASSIGNMENT.Controllers;

public class RoleController : Controller
{
    private readonly ILogger<RoleController> _logger;
    private readonly IRoleServices _roleServices;

    public RoleController(ILogger<RoleController> logger)
    {
        _logger = logger;
        _roleServices = new RoleServices();
    }

    public ActionResult Show()
    {
        List<Role> lst = _roleServices.GetAll();
        return View(lst);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Role p)
    {
        if (_roleServices.Create(p))
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
        var obj = _roleServices.GetById(id);
        return View(obj);
    }

    public IActionResult Delete(Guid id)
    {
        if (_roleServices.Delete(id))
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
        var obj = _roleServices.GetById(id);
        return View(obj);
    }

    public IActionResult Edit(Role p)
    {
        if (_roleServices.Update(p))
        {
            return RedirectToAction("Show");
        }
        else
        {
            return BadRequest();
        }
    }
}
