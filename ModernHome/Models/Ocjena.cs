﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernHome.Models
{
    public class Ocjena
    {
        [Key]
        public int Id { get; set; }
        public Ocjene ocjena {  get; set; }
        public string komentar {  get; set; }
        [ForeignKey("Korisnik")]
        public int korisnikID { get; set; }
        public Ocjena() { }

    }
}
