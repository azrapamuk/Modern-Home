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
        [EnumDataType(typeof(TipNamjestaja))]
        public TipNamjestaja tip {  get; set; }
        [EnumDataType(typeof(Boje))]
        public Boje boja { get; set; }
        public int kolicina { get; set; }
        public double cijena { get; set; }
        [ForeignKey("Dimenzije")]
        public int Iddimenzije { get; set; }
        [EnumDataType(typeof(Dimenzije))]
        public Dimenzije dimenzije { get; set; }
        public Artikal() { }
        public String slika { get; set; }   

    }
}
