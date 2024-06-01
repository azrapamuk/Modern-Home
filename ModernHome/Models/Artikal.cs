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
        [EnumDataType(typeof(TipNamjestaja))] public TipNamjestaja tip {  get; set; }
        [EnumDataType(typeof(Boje))] 
        public Boje boja { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Količina mora biti cijeli pozitivan broj!")]
        public int kolicina { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "Cijena ne smije biti negativna!")]
        public double cijena { get; set; }
        [ForeignKey("Dimenzije")]
        public int Iddimenzije { get; set; }
        //public Dimenzije dimenzije { get; set; }
        public string slika { get; set; }
        public Artikal() { }
    }
}
