using System;
using ACTApp.Interfaces;
using ACTApp.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ACTApp.Models.DataContext;
using ACTApp.Models.Account;

namespace ACTApp.Repository.UserIdentity
{
	public class UserIdentityRepo : IUserIdentity
	{
		private readonly DataContext context;

		public UserIdentityRepo(DataContext context, IDataProtectionProvider provider)
		{
			this.context = context;
		}
		public AppUser AddUser(AppUser applicationUser)
		{
			var result = context.tb_Register.Add(applicationUser);
			context.SaveChanges();
			return result.Entity;
		}
		public AppUser FindByEmail(string EmailId)
		{
			var Users = context.tb_Register.Where(x => x.EmailId == EmailId).FirstOrDefault();

			return Users;
		}
		public AppUser FindByMobile(string Mobile)
		{
			var Users = context.tb_Register.Where(x => x.Mobile == Mobile).FirstOrDefault();

			return Users;
		}
		public AppUser CheckUserLogin(string UserName, string Password)
		{
			var CheckUsers = context.tb_Register.Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();

			return CheckUsers;
		}
    }
}

