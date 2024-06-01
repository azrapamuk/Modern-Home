using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernHome.Models
{
    public class Korpa
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Ukupan iznos")]
        [Range(0, Double.MaxValue, ErrorMessage = "Ukupan iznos ne smije biti negativan!")]
        public double ukupanIznos { get; set; }
        [ForeignKey("Korisnik")]
        public int Idkorisnik { get; set; }
        //public Korisnik korisnik { get; set; }
        public Korpa() { }

    }
}
