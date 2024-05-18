using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernHome.Models
{
    public class StavkaNarudzbe
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Artikal")]
        public int Idartikal { get; set; }
        public Artikal artikal { get; set; }
        public int kolicina { get; set; }
        public double cijena { get; set; }
        [ForeignKey("Korpa")]
        public int Idkorpa { get; set; }
        public Korpa korpa { get; set; }
        public StavkaNarudzbe() { }

    }
}
