using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
  public class PatronController : Controller
  {
    private readonly LibraryContext _db;

    public PatronController(LibraryContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      return View(_db.Patrons.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patron patron)
    {
      _db.Patrons.Add(patron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      var thisPatron = _db.Patrons
          .Include(patron => patron.Copies)
          .ThenInclude(join => join.Copy)
          .FirstOrDefault(patron => patron.PatronId == id);
      return View(thisPatron);
    }


  }

}