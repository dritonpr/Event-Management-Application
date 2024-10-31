using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EventManagement.Core
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(200, ErrorMessage = "Event name cannot exceed 200 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Location is required.")]
        [StringLength(500, ErrorMessage = "Location cannot exceed 500 characters.")]
        public string Location { get; set; }
        public int MaxAttendees { get; set; }

        [StringLength(200, ErrorMessage = "Category cannot exceed 200 characters.")]
        public string Category { get; set; }
        public List<string> Attendees { get; set; } = new List<string>();
    }
}
