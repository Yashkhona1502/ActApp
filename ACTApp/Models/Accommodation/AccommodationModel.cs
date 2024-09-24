using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ACTApp.Models
{
    public class AccommodationModel
    {
        [Key]
        public int ID { get; set; }
        public string HotelName { get; set; }

        public int TotalRoom {  get; set; }

        public string Location { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; } 

        public string PostalCode { get; set; }

        public string StreetAddress { get; set; }

        public string ClusterName { get; set; }

        public int EventId { get; set; }
    }
}
