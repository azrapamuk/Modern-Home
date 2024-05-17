using System.ComponentModel.DataAnnotations;

namespace ModernHome.Models
{
    public class Dimenzije
    {
        [Key]
        public int Id { get; set; }
        public double visina { get; set; }
        public double sirina { get; set; }
        public double dužina { get; set; }
        public Dimenzije() { }
    }
}
