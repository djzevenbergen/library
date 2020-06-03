using System;

namespace Library.Models
{
  public class CopyPatron
  {
    public int CopyPatronId { get; set; }
    public int CopyId { get; set; }
    public int PatronId { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public Copy Copy { get; set; }
    public Patron Patron { get; set; }


  }
}