using System.ComponentModel.DataAnnotations;

namespace ModernHome.Models
{
    public enum TipNamjestaja
    {
        [Display(Name = "Dvosjed")]
        dvosjed,
        [Display(Name = "Trosjed")]
        trosjed,
        [Display(Name = "Krevet")]
        krevet,
        [Display(Name = "Stolica")]
        stolica,
        [Display(Name = "Fotelja")]
        fotelja,
        [Display(Name = "Ugaona garnitura")]
        ugaona_garnitura,
        [Display(Name = "Sto")]
        sto
    }
}
