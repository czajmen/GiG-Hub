using GigHub.Models;
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
        [Display(Name = "Data")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Godzina")]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Główny gaturnek muzyczny")]
        public int Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

    }
}
