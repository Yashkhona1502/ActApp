using System;
using System.ComponentModel.DataAnnotations;
using ACTApp.Models.Account;
using ACTApp.Models.Event;
using Microsoft.EntityFrameworkCore;

namespace ACTApp.Models.DataContext
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options): base(options)
		{

		}
		public DbSet<AppUser> tb_Register { get; set; }

		public DbSet<AddEvent> tb_Event { get; set; }
	}
}

