using System.ComponentModel.DataAnnotations;

namespace ModernHome.Models
{
    public enum Ocjene
    {
        [Display(Name = "Jedan")]
        jedan,
        [Display(Name = "Dva")]
        dva,
        [Display(Name = "Tri")]
        tri,
        [Display(Name = "Četiri")]
        cetiri,
        [Display(Name = "Pet")]
        pet
    }
}
