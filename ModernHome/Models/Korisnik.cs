using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernHome.Models
{
    public class Korisnik:IdentityUser
    {
        public string ime { get; set; }
        public string prezime { get; set; }
        public string adresa { get; set; }
        public string brojTelefona { get; set; }
        public Korisnik() { }
    }
}
