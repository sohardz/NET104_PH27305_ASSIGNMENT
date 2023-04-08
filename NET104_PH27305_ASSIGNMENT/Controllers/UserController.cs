using Microsoft.AspNetCore.Mvc;
using NET104_PH27305_ASSIGNMENT.IServices;
using NET104_PH27305_ASSIGNMENT.Models;
using NET104_PH27305_ASSIGNMENT.Services;

namespace NET104_PH27305_ASSIGNMENT.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserServices _userServices;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
        _userServices = new UserServices();
    }

    public ActionResult Show()
    {
        List<User> lst = _userServices.GetAll();
        return View(lst);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(User p)
    {
        if (_userServices.Create(p))
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
        var obj = _userServices.GetById(id);
        return View(obj);
    }

    public IActionResult Delete(Guid id)
    {
        if (_userServices.Delete(id))
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
        var obj = _userServices.GetById(id);
        return View(obj);
    }

    public IActionResult Edit(User p)
    {
        if (_userServices.Update(p))
        {
            return RedirectToAction("Show");
        }
        else
        {
            return BadRequest();
        }
    }
}
