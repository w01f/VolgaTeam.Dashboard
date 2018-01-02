using System;

namespace Asa.Business.Solutions.Common.Dictionaries
{
	public class User
	{
		public string Station { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public bool IsAdmin { get; set; }

		public string FullName => String.Format("{0} {1}", FirstName, LastName);

		public string NameWithAdress => String.Join(" | ", FullName, Phone, Email);

		public User()
		{
			Station = string.Empty;
			FirstName = string.Empty;
			LastName = string.Empty;
			Phone = string.Empty;
			Email = string.Empty;
		}

		public override String ToString()
		{
			return NameWithAdress;
		}
	}
}
