using System;
using System.Data;
using ACTApp.Models;

namespace ACTApp.Interfaces
{
    public interface IHotel
    {
        public void AddHotelList(DataTable hotel);

        public List<AccommodationModel> GetHotelList(int eventId);
    }
}

