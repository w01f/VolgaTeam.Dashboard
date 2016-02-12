namespace Asa.Business.Dashboard.Dictionaries
{
	public class User
	{
		public User()
		{
			Station = string.Empty;
			FirstName = string.Empty;
			LastName = string.Empty;
			Phone = string.Empty;
			Email = string.Empty;
		}

		public string Station { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public bool IsAdmin { get; set; }

		public string FullName
		{
			get { return FirstName + " " + LastName; }
		}
	}
}
