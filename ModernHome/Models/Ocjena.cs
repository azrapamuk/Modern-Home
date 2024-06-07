using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernHome.Models
{
    public class Ocjena
    {
        [Key]
        public int Id { get; set; }
        [EnumDataType(typeof(Ocjene))]
        public Ocjene ocjena {  get; set; }
        public string komentar {  get; set; }
        [ForeignKey("Korisnik")]
        public string Idkorisnik { get; set; }
        //public Korisnik korisnik { get; set; }
        [ForeignKey("Artikal")]
        public int Idartikal {  get; set; }
        public DateTime datum {  get; set; }
        public Ocjena() { }

    }
}
