using System;

namespace CommandCentral.BusinessClasses.Entities.Dashboard
{
	class User
	{
		public string Station { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
		public bool IsAdmin { get; set; }

		public User()
		{
			Station = String.Empty;
			FirstName = String.Empty;
			LastName = String.Empty;
			Phone = String.Empty;
			Email = String.Empty;
			Login = String.Empty;
		}
	}
}