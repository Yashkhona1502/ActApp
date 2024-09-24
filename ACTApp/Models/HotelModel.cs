using ACTApp.Models;

namespace ACTApp.Models
{
    public class HotelModel
    {
        public FileUploadModel FileUploadModel { get; set; }
        public List<AccommodationModel> accommodations { get; set; }
    }
}
