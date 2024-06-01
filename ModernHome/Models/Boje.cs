using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModernHome.Models
{
    public enum Boje
    {
        [Display(Name="Crna boja")]
        crna,
        [Display(Name = "Bijela boja")]
        bijela,
        [Display(Name = "Bež boja")]
        bež,
        [Display(Name = "Smeđa boja")]
        smeđa,
        [Display(Name = "Plava boja")]
        plava,
        [Display(Name ="Crvena boja")]
        crvena,
        [Display(Name = "Žuta boja")]
        žuta,
        [Display(Name = "Zelena boja")]
        zelena,
        [Display(Name = "Narandžasta boja")]
        narandžasta

    }
}
