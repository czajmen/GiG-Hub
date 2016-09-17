using GigHub.Controllers;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {

        public int Id { get; set; }

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

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<GigsController, Task<ActionResult>>> update = (c => c.Update(this));

                Expression<Func<GigsController, Task<ActionResult>>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;

                return (action.Body as MethodCallExpression).Method.Name;


            }
        }

        //[Required]
        //[StringLength(100)]
        //[Display(Name = "Imie i Nazwisko")]
        //public string Name { get; set; }

        public DateTime GetDateTime(string Date, string Time)
        {

            return DateTime.Parse(string.Format("{0} {1}", Date, Time));

        }
    }
}
