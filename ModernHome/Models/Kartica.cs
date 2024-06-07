﻿using ModernHome.Utility;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernHome.Models
{
    public class Kartica
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Broj Kartice")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Broj kartice mora sadržavati 16 cifara.")]
        public int brojKartice { get; set; }
        [DisplayName("CVV")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Broj CVV mora sadržavati 3 cifare.")]
        public int CVV { get; set; }
        [DisplayName("Datum isteka (mm/yyyy)")]
        [ValidateDate]
        public string datumIsteka { get; set; }
        [ForeignKey("Korisnik")]
        public String Idkorisnik { get; set; }
        //public Korisnik korisnik { get; set; }
        public Kartica() { }
    }
}
