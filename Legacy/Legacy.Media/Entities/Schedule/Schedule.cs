using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Legacy.Media.Entities.Settings;

namespace Asa.Legacy.Media.Entities.Schedule
{
	public abstract class Schedule
	{
		private DateTime? _userFlightDateStart;
		private DateTime? _userFlightDateEnd;

		public abstract string Name { get; set; }
		public bool IsNew { get; set; }
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public string ClientType { get; set; }
		public string AccountNumber { get; set; }
		public string Status { get; set; }
		public DateTime? PresentationDate { get; set; }
		public DateTime? FlightDateStart { get; set; }
		public DateTime? FlightDateEnd { get; set; }
		public bool UseDemo { get; set; }
		public bool ImportDemo { get; set; }
		public string Demo { get; set; }
		public string Source { get; set; }
		public DemoType DemoType { get; set; }
		public bool MondayBased { get; private set; }

		public SpotType SelectedSpotType { get; set; }

		public List<Daypart> Dayparts { get; private set; }
		public List<Station> Stations { get; private set; }

		public ScheduleBuilderViewSettings ViewSettings { get; set; }

		public DateTime? UserFlightDateStart
		{
			get
			{
				return _userFlightDateStart;
			}
		}

		public DateTime? UserFlightDateEnd
		{
			get
			{
				return _userFlightDateEnd;
			}
		}

		public string DisplayedSpotType
		{
			get { return String.Format("{0}ly", SelectedSpotType); }
			set
			{
				SpotType temp;
				if (!String.IsNullOrEmpty(value) && Enum.TryParse(value.Replace("ly", ""), true, out temp))
					SelectedSpotType = temp;
				else
					SelectedSpotType = SpotType.Week;
			}
		}

		protected Schedule()
		{
			BusinessName = string.Empty;
			DecisionMaker = string.Empty;
			AccountNumber = string.Empty;
			PresentationDate = DateTime.Now;
			UseDemo = false;
			ImportDemo = false;
			DemoType = DemoType.Imp;
			MondayBased = true;

			Dayparts = new List<Daypart>();
			Stations = new List<Station>();

			ViewSettings = new ScheduleBuilderViewSettings();
		}

		public virtual void Deserialize(XmlNode rootNode)
		{
			var node = rootNode.SelectSingleNode(@"BusinessName");
			if (node != null)
				BusinessName = node.InnerText;

			node = rootNode.SelectSingleNode(@"DecisionMaker");
			if (node != null)
				DecisionMaker = node.InnerText;

			node = rootNode.SelectSingleNode(@"ClientType");
			if (node != null)
				ClientType = node.InnerText;

			node = rootNode.SelectSingleNode(@"AccountNumber");
			if (node != null)
				AccountNumber = node.InnerText;

			node = rootNode.SelectSingleNode(@"Status");
			if (node != null)
				Status = node.InnerText;

			node = rootNode.SelectSingleNode(@"PresentationDate");
			DateTime tempDateTime;
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					PresentationDate = tempDateTime;

			node = rootNode.SelectSingleNode(@"FlightDateStart");
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					FlightDateStart = tempDateTime;

			node = rootNode.SelectSingleNode(@"FlightDateEnd");
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					FlightDateEnd = tempDateTime;

			node = rootNode.SelectSingleNode(@"UserFlightDateStart");
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					_userFlightDateStart = tempDateTime;
			if (!_userFlightDateStart.HasValue)
				_userFlightDateEnd = FlightDateStart;

			node = rootNode.SelectSingleNode(@"UserFlightDateEnd");
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					_userFlightDateEnd = tempDateTime;
			if (!_userFlightDateEnd.HasValue)
				_userFlightDateEnd = FlightDateEnd;

			node = rootNode.SelectSingleNode(@"UseDemo");
			bool tempBool;
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					UseDemo = tempBool;

			node = rootNode.SelectSingleNode(@"ImportDemo");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					ImportDemo = tempBool;

			node = rootNode.SelectSingleNode(@"DemoType");
			int tempInt;
			if (node != null)
				if (int.TryParse(node.InnerText, out tempInt))
					DemoType = (DemoType)tempInt;

			node = rootNode.SelectSingleNode(@"MondayBased");
			if (node != null)
			{
				bool temp;
				if (Boolean.TryParse(node.InnerText, out temp))
					MondayBased = temp;
			}

			node = rootNode.SelectSingleNode(@"Demo");
			if (node != null)
				Demo = node.InnerText;

			node = rootNode.SelectSingleNode(@"Source");
			if (node != null)
				Source = node.InnerText;

			node = rootNode.SelectSingleNode(@"SelectedSpotType");
			if (node != null)
				if (int.TryParse(node.InnerText, out tempInt))
					SelectedSpotType = (SpotType)tempInt;

			node = rootNode.SelectSingleNode(@"Dayparts");
			if (node != null)
			{
				Dayparts.Clear();
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var daypart = new Daypart();
					daypart.Deserialize(childNode);
					Dayparts.Add(daypart);
				}
			}

			node = rootNode.SelectSingleNode(@"Stations");
			if (node != null)
			{
				Stations.Clear();
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var station = new Station();
					station.Deserialize(childNode);
					Stations.Add(station);
				}
			}

			node = rootNode.SelectSingleNode(@"ViewSettings");
			if (node != null)
			{
				ViewSettings.Deserialize(node);
			}
		}
	}
}
