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
        [Range(0, int.MaxValue, ErrorMessage = "Količina mora biti cijeli pozitivan broj!")]
        public int kolicina { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "Cijena ne smije biti negativna!")]
        public double cijena { get; set; }
        [ForeignKey("Korpa")]
        public int Idkorpa { get; set; }
        public Korpa korpa { get; set; }
        public StavkaNarudzbe() { }

    }
}
