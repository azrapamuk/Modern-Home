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
        public int Idkorisnik { get; set; }
        public Korisnik korisnik { get; set; }
        public Ocjena() { }

    }
}
