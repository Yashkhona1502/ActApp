using System;
using System.Data;
using ACTApp.Models.DataContext;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ACTApp.Interfaces;

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
    }
}

