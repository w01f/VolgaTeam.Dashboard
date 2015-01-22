namespace CommandCentral.Entities.Dashboard
{
	internal class User
	{
		public User()
		{
			Station = string.Empty;
			FirstName = string.Empty;
			LastName = string.Empty;
			Phone = string.Empty;
			Email = string.Empty;
			SavedPresentationsFolder = string.Empty;
			SyncStartup = string.Empty;
			SyncTime = string.Empty;
		}

		public string Station { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string SavedPresentationsFolder { get; set; }
		public string SyncTime { get; set; }
		public string SyncStartup { get; set; }
		public bool IsAdmin { get; set; }
	}
}