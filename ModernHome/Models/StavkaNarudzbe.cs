using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernHome.Models
{
    public class StavkaNarudzbe
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Artikal")]
        public int artikalID { get; set; }
        public int kolicina { get; set; }
        public double cijena { get; set; }
        [ForeignKey("Korpa")]
        public int korpaID { get; set; }
        public StavkaNarudzbe() { }

    }
}
