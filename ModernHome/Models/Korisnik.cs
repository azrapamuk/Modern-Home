using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernHome.Models
{
    public class Korisnik:IdentityUser
    {
        [Required]
        public string ime { get; set; }
        [Required]
        public string prezime { get; set; }
        public string adresa { get; set; }
        [Required]
        [RegularExpression(@"^\+[1-9]\d{6,12}$", ErrorMessage = "Neispravan format broja telefona!")]
        public string brojTelefona { get; set; }
        public Korisnik() { }
    }
}
