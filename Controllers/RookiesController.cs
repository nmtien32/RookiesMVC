using Microsoft.AspNetCore.Mvc;
using RookiesMVC.Models;

namespace Extention.Controllers;
[Route("NashTech")]
public class RookiesController : Controller
{
    private static List<Person> _people = new List<Person>{
               new Person( "Nguyen","Minh", "Male",new DateTime(1999,02,11),"0962373853","ThanhHoa",true ),
                new Person( "Le","Nam", "Male",new DateTime(2000,12,12),"0912145112","HaNoi",false ),
                new Person( "Tran","Quynh", "Male",new DateTime(2001,09,01),"086686868","SaiGon",false ),
                new Person( "Nam","Em", "Female",new DateTime(2003,07,10),"0972158138","NamDinh",true ),
                new Person( "Tran","Quang", "Female",new DateTime(2002,11,11),"0973561582","QuangNinh",false )
        };

    [Route("Index")]
    public IActionResult Index()
    {
        return Json(_people);
    }

    [Route("GetMaleMembers")]
    public IActionResult GetMaleMember()
    {
        var malemembers = _people.Where(a => a.Gender == "Male");
        return Json(malemembers);
    }

    [Route("GetOldestMember")]
    public IActionResult GetOldestMember()
    {
        var maxAge = _people.Max(a => a.Age);
        var oldestMember = _people.FirstOrDefault(a => a.Age == maxAge);
        return Json(oldestMember);
    }

    [Route("GetFullNameMembers")]
    public IActionResult GetFullNameMember()
    {
        var fullName = _people.Select(a => a.FullName);
        return Json(fullName);
    }

    [Route("GetMembersByYear")]
    public IActionResult GetMemberByYear(int year, string compareType)
    {
        switch (compareType)
        {
            case "equals":
                return Json(_people.Where(a => a.DoB.Year == year));
            case "greaterThan":
                return Json(_people.Where(a => a.DoB.Year > year));
            case "lessThan":
                return Json(_people.Where(a => a.DoB.Year < year));
            default: return Json(null);
        }
    }

    [Route("GetMember2000")]
    public IActionResult GetMemberBornIn2000()
    {
        return RedirectToAction("GetmembersByYear", new { year = 2000, compareType = "equals" });
    }

    [Route("GetMemberAfter2000")]
    public IActionResult GetMemberBornAfter2000()
    {
        return RedirectToAction("GetmembersByYear", new { year = 2000, compareType = "greaterThan" });
    }

    [Route("GetMemberBefore2000")]
    public IActionResult GetMemberBornBefore2000()
    {
        return RedirectToAction("GetmembersByYear", new { year = 2000, compareType = "lessThan" });
    }
    private readonly ILogger<RookiesController> _logger;

    public RookiesController(ILogger<RookiesController> logger)
    {
        _logger = logger;
    }
}