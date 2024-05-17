using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernHome.Models
{
    public class Kartica
    {
        [Key]
        public int Id { get; set; }
        public int brojKartice { get; set; }
        public int CVV { get; set; }
        public DateTime datumIsteka { get; set; }
        [ForeignKey("Korisnik")]
        public int korisnikID { get; set; }
        public Kartica() { }
    }
}
