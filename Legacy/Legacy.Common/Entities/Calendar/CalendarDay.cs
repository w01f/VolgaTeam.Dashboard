using System;
using System.Xml;
using Asa.Legacy.Common.Entities.Common;

namespace Asa.Legacy.Common.Entities.Calendar
{
	public abstract class CalendarDay
	{
		protected string _userData;

		protected CalendarDay()
		{
			Logo = new ImageSource();
		}

		public DateTime Date { get; set; }
		public bool BelongsToSchedules { get; set; }
		public bool HasNotes { get; set; }
		public bool EditedByUser { get; private set; }
		public ImageSource Logo { get; set; }
		public virtual string ImportedData => String.Empty;

		public string Comment
		{
			get { return !String.IsNullOrEmpty(_userData) ? _userData : ImportedData; }
			set
			{
				if (ImportedData != value && !String.IsNullOrEmpty(value))
				{
					_userData = value;
					EditedByUser = true;
				}
				else
				{
					_userData = null;
					EditedByUser = false;
				}
			}
		}

		public abstract int WeekDayIndex { get; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Date":
						DateTime tempDate;
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							Date = tempDate;
						break;
					case "UserData":
					case "Comment1":
						_userData = childNode.InnerText;
						EditedByUser = true;
						break;
					case "Logo":
						Logo.Deserialize(childNode);
						break;
				}
			}
		}
	}
}
