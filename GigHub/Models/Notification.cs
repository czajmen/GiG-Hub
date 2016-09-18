using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }
        public NotificationType Type { get; private set; }

        public DateTime? OrigialDateTime { get; set; }
        public string OriginalVenue { get; set; }


        [Required]
        public Gig Gig { get; private set; }

        public Notification()
        {

        }

        public Notification(Gig gig, NotificationType type)
        {
            if (gig == null)
            {
                throw new ArgumentNullException();
            }


            Gig = gig;
            DateTime = DateTime.Now;
            Type = type;
        }


    }
}