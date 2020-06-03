using System.Collections.Generic;

namespace Library.Models
{
  public class Patron
  {
    public int PatronId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<CopyPatron> Copies { get; set; }
    public virtual ApplicationUser User { get; set; }
    public Patron()
    {
      this.Copies = new HashSet<CopyPatron>();
      //this.PatronId = User.Id;
    }
  }
}