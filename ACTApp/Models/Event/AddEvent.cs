using System;
using System.ComponentModel.DataAnnotations;

namespace ACTApp.Models.Event
{
    public class AddEvent
    {
        
        [Key]
        public int EventId { get; set; }

        public string EventName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public string Status { get; set; }
    }
}

