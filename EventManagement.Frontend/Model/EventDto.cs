using EventManagement.Frontend.Helpers;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Frontend.Models
{
    public class EventDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(100, ErrorMessage = "Event name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time.")]
        [FutureDate(ErrorMessage = "Date must be in the future.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(500, ErrorMessage = "Location cannot exceed 500 characters.")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "MaxAttendees is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "MaxAttendees must be at least 1.")]
        public int MaxAttendees { get; set; }

        [StringLength(200, ErrorMessage = "Category cannot exceed 200 characters.")]
        public string? Category { get; set; }
        public List<string> Attendees { get; set; } = new List<string>();
        public bool HasRespond { get; set; }
        public string? CreatedByUsername { get; set; }
    }
}
