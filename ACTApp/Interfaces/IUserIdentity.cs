using System;
using ACTApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACTApp.Models.Account;

namespace ACTApp.Interfaces
{
	public interface IUserIdentity
	{
		public AppUser AddUser(AppUser applicationUser);

		public AppUser FindByEmail(string EmailId);

		public AppUser FindByMobile(string Mobile);

		public AppUser CheckUserLogin(string UserName, string Password);

    }
}

