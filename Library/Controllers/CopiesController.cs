using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

//new using directives
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
  [Authorize]
  //new line
  public class CopiesController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager; //new line

    //updated constructor
    public CopiesController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    //updated Index method
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userBooks = _db.Books.Where(entry => entry.User.Id == currentUser.Id).ToList();
      var copies = _db.Copies.ToList();
      return View(copies);
    }

    [HttpPost("/checkout")]
    public async Task<ActionResult> Checkout(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);

      //var userCopy = _db.Copies.FirstOrDefault(entry => entry.BookId == id);// && entry.Available == true;


      int uId = int.Parse(userId);
      if (uId != 0)
      {
        DateTime checkoutDate = DateTime.Now;
        DateTime dueDate = checkoutDate.AddDays(8);

        _db.CopyPatron.Add(new CopyPatron() { PatronId = uId, CopyId = userCopy.CopyId, CheckoutDate = checkoutDate, DueDate = dueDate });
      }
      _db.SaveChanges();
      //go through the copies table and find the copyid of the first available copy with the desired book id
      //change that copy's available colummn to false
      //
      //when someone checks out a book, the checkoutdate column is set equal to getdatetime and the duedate is set for getdatetime plus 8 days
      //make a copypatron with copyid, userid, and duedate
      //
      //when 
      return RedirectToAction("Details", "Patrons", new { id = userId });
    }

    public ActionResult Create()
    {
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "Title");
      return View();
    }

    //updated Create post method
    [HttpPost]
    public async Task<ActionResult> Create(Copy copy, int BookId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      copy.User = currentUser;
      _db.Copies.Add(copy);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisBook = _db.Copies
          // .Include(copy => copy.Available)
          // .ThenInclude(join => join.Author)
          .FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }
  }
}
//   public ActionResult Edit(int id)
//   {
//     var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
//     ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
//     return View(thisBook);
//   }

//   [HttpPost]
//   public ActionResult Edit(Book book, int AuthorId)
//   {
//     if (AuthorId != 0)
//     {
//       _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
//     }
//     _db.Entry(book).State = EntityState.Modified;
//     _db.SaveChanges();
//     return RedirectToAction("Index");
//   }

//   public ActionResult AddAuthor(int id)
//   {
//     var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
//     ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
//     return View(thisBook);
//   }

//   [HttpPost]
//   public ActionResult AddAuthor(Book book, int AuthorId)
//   {
//     if (AuthorId != 0)
//     {
//       _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
//     }
//     _db.SaveChanges();
//     return RedirectToAction("Index");
//   }

//   public ActionResult Delete(int id)
//   {
//     var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
//     return View(thisBook);
//   }

//   [HttpPost, ActionName("Delete")]
//   public ActionResult DeleteConfirmed(int id)
//   {
//     var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
//     _db.Books.Remove(thisBook);
//     _db.SaveChanges();
//     return RedirectToAction("Index");
//   }

//   [HttpPost]
//   public ActionResult DeleteAuthor(int joinId)
//   {
//     var joinEntry = _db.AuthorBook.FirstOrDefault(entry => entry.AuthorBookId == joinId);
//     _db.AuthorBook.Remove(joinEntry);
//     _db.SaveChanges();
//     return RedirectToAction("Index");
//   }
// }
