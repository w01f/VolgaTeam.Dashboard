using System;
using System.Collections.Generic;

namespace CommandCentral.BusinessClasses.Entities.Dashboard
{
	class User
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
		public string Title { get; set; }
		public bool IsAdmin { get; set; }

		public List<string> Groups { get; }

		public User()
		{
			FirstName = String.Empty;
			LastName = String.Empty;
			Phone = String.Empty;
			Email = String.Empty;
			Login = String.Empty;
			Title = String.Empty;

			Groups = new List<String>();
		}
	}
}