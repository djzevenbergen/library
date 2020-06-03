using System.Collections.Generic;

namespace Library.Models
{
  public class Copy
  {
    public Copy()
    {
      this.Patrons = new HashSet<CopyPatron>();
    }

    public int CopyId { get; set; }
    public int BookId { get; set; }
    //public int NumberOfCopies { get; set; }
    //public int AvailableCopies {get; set;}
    public bool Available { get; set; }
    public virtual ApplicationUser User { get; set; }
    // public virtual Copy Copy { get; set; }
    public ICollection<CopyPatron> Patrons { get; }
  }
}
