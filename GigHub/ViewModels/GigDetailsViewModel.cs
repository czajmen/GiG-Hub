using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;
using GigHub.Models.Dtos;

namespace GigHub.ViewModels
{
    public class GigDetailsViewModel
    {

        public GigDto Gig { get; set; }
        public ApplicationUserDto User{ get; set; }
        public bool isGoing { get; set; }

    }
}