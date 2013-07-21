using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Dashboard
{
	public class ListManager
	{
		private static readonly ListManager _instance = new ListManager();
		private ListManager()
		{
			Init();
		}

		public string ListsFolder { get; set; }

		public Users UsersList { get; set; }

		public CoverLists CoverLists { get; set; }

		public static ListManager Instance
		{
			get { return _instance; }
		}

		#region Home
		public ClientGoalsLists ClientGoalsLists { get; set; }
		public LeadoffStatementLists LeadoffStatementLists { get; set; }
		public TargetCustomersLists TargetCustomersLists { get; set; }
		public SimpleSummaryLists SimpleSummaryLists { get; set; }
		#endregion

		private void Init()
		{
			ListsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			UsersList = new Users();

			CoverLists = new CoverLists();

			#region Home
			ClientGoalsLists = new ClientGoalsLists();
			LeadoffStatementLists = new LeadoffStatementLists();
			TargetCustomersLists = new TargetCustomersLists();
			SimpleSummaryLists = new SimpleSummaryLists();
			#endregion
		}
	}

	#region Users
	public class Users
	{
		private readonly string _listsFileName;
		private readonly List<User> _users = new List<User>();

		public Users()
		{
			_listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Users XML", "Users.xml");
			Load();
		}

		private void Load()
		{
			bool tempBool;

			XmlNode node;
			if (File.Exists(_listsFileName))
			{
				var document = new XmlDocument();
				document.Load(_listsFileName);

				node = document.SelectSingleNode(@"/Users");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						if (childNode.Name.Equals("User"))
						{
							var user = new User();
							foreach (XmlAttribute attribute in childNode.Attributes)
							{
								switch (attribute.Name)
								{
									case "Station":
										user.Station = attribute.Value;
										break;
									case "FirstName":
										user.FirstName = attribute.Value;
										break;
									case "LastName":
										user.LastName = attribute.Value;
										break;
									case "Phone":
										user.Phone = attribute.Value;
										break;
									case "Email":
										user.Email = attribute.Value;
										break;
									case "IsAdmin":
										tempBool = false;
										bool.TryParse(attribute.Value, out tempBool);
										user.IsAdmin = tempBool;
										break;
								}
							}
							_users.Add(user);
						}
					}
				}
			}
		}

		public User[] GetUsersByStation(string stationName)
		{
			return _users.ToArray();
		}
	}

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
	#endregion

	#region Cover
	public class CoverLists
	{
		private readonly string _listsFileName;

		public CoverLists()
		{
			_listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Basic Slides XML", "Add Cover.xml");
			Headers = new List<string>();
			Quotes = new List<Quote>();
			Load();
		}

		public List<string> Headers { get; set; }
		public List<Quote> Quotes { get; set; }

		private void Load()
		{
			XmlNode node;
			if (File.Exists(_listsFileName))
			{
				var document = new XmlDocument();
				document.Load(_listsFileName);

				node = document.SelectSingleNode(@"/CoverSlide");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						switch (childNode.Name)
						{
							case "SlideHeader":
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value))
												Headers.Add(attribute.Value);
											break;
									}
								}
								break;
							case "Quote":
								var quote = new Quote();
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											quote.Text = attribute.Value;
											break;
										case "Author":
											quote.Author = attribute.Value;
											break;
									}
								}
								Quotes.Add(quote);
								break;
						}
					}
				}
			}
		}
	}

	public class Quote
	{
		public Quote()
		{
			Text = string.Empty;
			Author = string.Empty;
		}

		public string Text { get; set; }
		public string Author { get; set; }

		public bool IsSet
		{
			get { return !string.IsNullOrEmpty(Text + Author); }
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<Text>" + Text.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Text>");
			result.AppendLine(@"<Author>" + Author.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Author>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Text":
						Text = childNode.InnerText;
						break;
					case "Author":
						Author = childNode.InnerText;
						break;
				}
			}
		}
	}
	#endregion

	#region Home

	#region Client Goals
	public class ClientGoalsLists
	{
		private readonly string _listsFileName;

		public ClientGoalsLists()
		{
			_listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Basic Slides XML", "Needs Analysis.xml");
			Headers = new List<string>();
			Goals = new List<string>();
			Load();
		}

		public List<string> Headers { get; set; }
		public List<string> Goals { get; set; }

		private void Load()
		{
			XmlNode node;
			if (File.Exists(_listsFileName))
			{
				var document = new XmlDocument();
				document.Load(_listsFileName);

				node = document.SelectSingleNode(@"/ClientGoals");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						switch (childNode.Name)
						{
							case "SlideHeader":
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value))
												Headers.Add(attribute.Value);
											break;
									}
								}
								break;
							case "Goal":
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value))
												Goals.Add(attribute.Value);
											break;
									}
								}
								break;
						}
					}
				}
			}
		}
	}
	#endregion

	#region Leadoff Statement
	public class LeadoffStatementLists
	{
		private readonly string _listsFileName;

		public LeadoffStatementLists()
		{
			_listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Basic Slides XML", "Intro Slide.xml");
			Headers = new List<string>();
			Statements = new List<string>();
			Load();
		}

		public List<string> Headers { get; set; }
		public List<string> Statements { get; set; }

		private void Load()
		{
			XmlNode node;
			if (File.Exists(_listsFileName))
			{
				var document = new XmlDocument();
				document.Load(_listsFileName);

				node = document.SelectSingleNode(@"/LeadOff");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						switch (childNode.Name)
						{
							case "SlideHeader":
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value))
												Headers.Add(attribute.Value);
											break;
									}
								}
								break;
							case "Statement":
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value))
												Statements.Add(attribute.Value);
											break;
									}
								}
								break;
						}
					}
				}
			}
		}
	}
	#endregion

	#region Target Customers
	public class TargetCustomersLists
	{
		private readonly string _listsFileName;

		public TargetCustomersLists()
		{
			_listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Basic Slides XML", "Target Customer.xml");
			Headers = new List<string>();
			Demos = new List<string>();
			HHIs = new List<string>();
			Geographies = new List<string>();
			Load();
		}

		public List<string> Headers { get; set; }
		public List<string> Demos { get; set; }
		public List<string> HHIs { get; set; }
		public List<string> Geographies { get; set; }

		private void Load()
		{
			XmlNode node;
			if (File.Exists(_listsFileName))
			{
				var document = new XmlDocument();
				document.Load(_listsFileName);

				node = document.SelectSingleNode(@"/TargetCustomers");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						switch (childNode.Name)
						{
							case "SlideHeader":
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value))
												Headers.Add(attribute.Value);
											break;
									}
								}
								break;
							case "Demo":
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value))
												Demos.Add(attribute.Value);
											break;
									}
								}
								break;
							case "HHI":
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value))
												HHIs.Add(attribute.Value);
											break;
									}
								}
								break;
							case "Geography":
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value))
												Geographies.Add(attribute.Value);
											break;
									}
								}
								break;
						}
					}
				}
			}
		}
	}
	#endregion

	#region Simple Summary
	public class SimpleSummaryLists
	{
		private readonly string _listsFileName;

		public SimpleSummaryLists()
		{
			_listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Basic Slides XML", "Closing Summary.xml");
			Headers = new List<string>();
			Details = new List<string>();
			Load();
		}

		public List<string> Headers { get; set; }
		public List<string> Details { get; set; }

		private void Load()
		{
			XmlNode node;
			if (File.Exists(_listsFileName))
			{
				var document = new XmlDocument();
				document.Load(_listsFileName);

				node = document.SelectSingleNode(@"/SimpleSummary");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						switch (childNode.Name)
						{
							case "SlideHeader":
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value))
												Headers.Add(attribute.Value);
											break;
									}
								}
								break;
							case "Detail":
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value))
												Details.Add(attribute.Value);
											break;
									}
								}
								break;
						}
					}
				}
			}
		}
	}
	#endregion

	#endregion
}