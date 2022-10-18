using Microsoft.AspNetCore.Mvc;
using RookiesMVC.Models;
using RookiesMVC.Service;
namespace RookiesMVC.Controllers;
public class RookiesController : Controller
{
    private readonly ILogger<RookiesController> _logger;
    private readonly IPersonService _personService;

    public RookiesController(ILogger<RookiesController> logger, IPersonService personService)
    {
        _logger = logger;
        _personService = personService;
    }
    public IActionResult Index()
    {
        var model = _personService.GetAll();
        return View(model);

    }
    public IActionResult Detail(int index)
    {
        var person = _personService.GetOne(index);
        if (person != null)
        {
            var model = new PersonDetailModel
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                DateOfBirth = person.DateOfBirth,
                Phone = person.Phone,
                BirthPlace = person.BirthPlace
            };
            ViewData["Index"] = index;
            return View(model);
        };
        return View();
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(PersonCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var person = new PersonModel(model.FirstName, model.LastName, model.Gender, model.DateOfBirth, model.Phone, model.BirthPlace, false);
            _personService.Create(person);
            return RedirectToAction("Index");
        }
        return View(model);
    }
    [HttpGet]
    public IActionResult Edit(int index)
    {
        var person = _personService.GetOne(index);
        if (person != null)
        {
            var model = new PersonUpdateModel
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Phone = person.Phone,
                BirthPlace = person.BirthPlace
            };
            ViewData["Index"] = index;
            return View(model);
        };
        return View();
    }
    [HttpPost]
    public IActionResult Update(int index, PersonUpdateModel model)
    {
        if (ModelState.IsValid)
        {
            var person = _personService.GetOne(index);
            if (person != null)
            {
                person.FirstName = model.FirstName;
                person.LastName = model.LastName;
                person.Phone = model.Phone;
                person.BirthPlace = model.BirthPlace;

                _personService.Update(index, person);
            }
            return RedirectToAction("Index");
        }
        return View(model);
    }
    [HttpPost]
    public IActionResult Delete(int index)
    {
        var result = _personService.Delete(index);
        if (result == null)
        {
            return NotFound();
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult DeleteAndRedirectToResultView(int index)
    {
        var result = _personService.Delete(index);
        if (result == null)
        {
            return NotFound();
        }
        HttpContext.Session.SetString("DeletedPersonName", result.FullName);
        return RedirectToAction("DeleteResult");
    }
    public IActionResult DeleteResult()
    {
        ViewBag.DeletedName = HttpContext.Session.GetString("DeletedPersonName");
        return View();
    }
}