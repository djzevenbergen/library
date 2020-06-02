using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
  public class AuthorsController : Controller
  {
    private readonly LibraryContext _db;

    public AuthorsController(LibraryContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Author> model = _db.Authors.ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }
  }
}
