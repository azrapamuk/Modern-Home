using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModernHome.Models
{
    public enum Boje
    {

        [Display(Name = "Crvena boja")]  //0
        crvena,
        [Display(Name = "Narandžasta boja")]  //1
        narandzasta,
        [Display(Name = "Žuta boja")]  //2
        zuta,
        [Display(Name = "Zelena boja")]  //3
        zelena,
        [Display(Name = "Plava boja")]  //4
        plava,
        [Display(Name = "Ljubičasta boja")]  //5
        ljubicasta,
        [Display(Name = "Roza boja")]  //6
        roza,
        [Display(Name = "Bijela boja")]  //7
        bijela,
        [Display(Name = "Siva boja")]  //8
        siva,
        [Display(Name = "Crna boja")]  //9
        crna,
        [Display(Name = "Smeđa boja")]  //10
        smedja,
        [Display(Name = "Bež boja")]  //11
        bez,
    }
}
