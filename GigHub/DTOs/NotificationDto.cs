﻿using GigHub.Models;
using System;

namespace GigHub.DTOs
{
    public class NotificationDto
    {
        

        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        public string OriginalVenue { get; set; }
       
        public GigDto Gig { get; set; }
    }
}