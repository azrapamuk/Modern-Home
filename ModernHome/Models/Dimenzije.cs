using System.ComponentModel.DataAnnotations;

namespace ModernHome.Models
{
    public class Dimenzije
    {
        [Key]
        public int Id { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "Visina ne smije biti negativna")]
        public double visina { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "Sirina ne smije biti negativna")]
        public double sirina { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "Duzina ne smije biti negativna")]
        public double duzina { get; set; }
        public Dimenzije() { }
    }
}
