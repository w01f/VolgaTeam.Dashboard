using System;
using System.Collections.Generic;

namespace Asa.Business.Solutions.Common.Dictionaries
{
	public class User
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Title { get; set; }
		public bool IsAdmin { get; set; }

		public List<string> Groups { get; }

		public string FullName
		{
			get
			{
				var values = new List<string>();
				if (!String.IsNullOrEmpty(FirstName))
					values.Add(FirstName);
				if (!String.IsNullOrEmpty(LastName))
					values.Add(LastName);
				return String.Join(" ", values);
			}
		}

		public string NameWithAdress
		{
			get
			{
				var values = new List<string>();
				if (!String.IsNullOrEmpty(FullName))
					values.Add(FullName);
				if (!String.IsNullOrEmpty(Phone))
					values.Add(Phone);
				if (!String.IsNullOrEmpty(Email))
					values.Add(Email);
				return String.Join(" | ", values);
			}
		}

		public User()
		{
			Groups = new List<String>();
		}

		public override String ToString()
		{
			var values = new List<string>();
			if (!String.IsNullOrEmpty(FullName))
				values.Add(FullName);
			if (!String.IsNullOrEmpty(Title))
				values.Add(Title);
			return String.Join("  |  ", values);
		}
	}
}
