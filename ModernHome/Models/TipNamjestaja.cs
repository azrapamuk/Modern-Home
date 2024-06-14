using System.ComponentModel.DataAnnotations;

namespace ModernHome.Models
{
    public enum TipNamjestaja
    {
        [Display(Name = "Dnevna soba")]
        dnevnasoba,
        [Display(Name = "Kuhinja")]
        kuhinja,
        [Display(Name = "Spavaća soba")]
        spavacasoba,
        [Display(Name = "Ured")]
        ured,
        [Display(Name = "Dekoracije")]
        dekoracije,
        [Display(Name = "Razno")]
        razno
    }
}
