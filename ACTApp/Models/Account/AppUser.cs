using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ACTApp.Models.Account
{
	public class AppUser
	{
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string EmailId { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Status { get; set; }
    }
}

