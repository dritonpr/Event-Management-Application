﻿using EventManagement.API.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.API.Dto
{
    public class EventDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(100, ErrorMessage = "Event name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time.")]
        [FutureDate(ErrorMessage = "Date must be in the future.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "MaxAttendees is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "MaxAttendees must be at least 1.")]
        public int MaxAttendees { get; set; }

        public List<string> Attendees { get; set; } = new List<string>();
    }
}
