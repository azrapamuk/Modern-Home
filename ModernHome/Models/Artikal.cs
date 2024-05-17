using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernHome.Models
{
    public class Artikal
    {
        [Key]
        public int Id { get; set; }
        public string naziv {  get; set; }
        public TipNamjestaja tip {  get; set; }
        public Boje boja { get; set; }
        public int kolicina { get; set; }
        public double cijena { get; set; }
        [ForeignKey("Dimenzije")]
        public int dimenzijeID { get; set; }
        public Artikal() { }

    }
}
