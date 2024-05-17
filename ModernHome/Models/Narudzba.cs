using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernHome.Models
{
    public class Narudzba
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Korisnik")]
        public int korisnikID { get; set; }
        public DateTime vrijemeNarudzbe {  get; set; }
        public bool stanjeIsporuke { get; set; }
        [ForeignKey("Korpa")]
        public int korpaID { get; set; }
        public Narudzba() { }

    }
}
