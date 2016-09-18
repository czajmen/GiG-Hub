using System;

namespace GigHub.Models.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get;  set; }

        public DateTime? OrigialDateTime { get; set; }
        public string OriginalVenue { get; set; }

        public GigDto Gig { get; set; }

    }
}