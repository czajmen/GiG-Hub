using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {

        [Required]
        [Display(Name = "Miejsce Spotkania")]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        [Display(Name = "Data")]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        [Display(Name = "Godzina")]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Główny gaturnek muzyczny")]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Imie i Nazwisko")]
        public string Name { get; set; }

        public DateTime GetDateTime(string Date, string Time)
        {

            return DateTime.Parse(string.Format("{0} {1}", Date, Time));

        }
    }
}
