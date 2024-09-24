using System;
using System.Data;
using ACTApp.Models.DataContext;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ACTApp.Interfaces;
using ACTApp.Models;

namespace ACTApp.Repository.Hotel
{
    public class HotelRepo : IHotel
    {
        public readonly DataContext context;

        public HotelRepo(DataContext context, IDataProtectionProvider provider)
        {
            this.context = context;
        }

        public void AddHotelList(DataTable hotel)
        {
            context.Database.ExecuteSqlRaw("Exec sp_InsertHotel @DataTable", new SqlParameter("@DataTable", SqlDbType.Structured) { TypeName = "dbo.Hotel", Value = hotel});
        }
        public List<AccommodationModel> GetHotelList(int eventId)
        {
            var result = context.tb_Hotel.Where(x => x.EventId == eventId).ToList();

            return result;
        }
    }
}

