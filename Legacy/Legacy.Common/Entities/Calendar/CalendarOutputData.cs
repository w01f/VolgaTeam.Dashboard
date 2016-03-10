using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;
using Asa.Legacy.Common.Entities.Common;

namespace Asa.Legacy.Common.Entities.Calendar
{
	public abstract class CalendarOutputData
	{
		protected readonly List<ImageSource> _dayLogosPaths = new List<ImageSource>();
		protected string _encodedLogo;

		protected CalendarOutputData()
		{
			Notes = new List<CalendarNote>();

			ShowCustomComment = false;
			ApplyForAllCustomComment = true;

			#region Basic
			ShowHeader = true;
			ShowBusinessName = true;
			ShowDecisionMaker = true;
			Header = String.Empty;
			ApplyForAllBasic = true;
			#endregion

			#region Style

			SlideColor = "gray";
			ApplyForAllThemeColor = true;

			ShowLogo = true;
			ApplyForAllLogo = true;

			ShowBigDate = false;

			#endregion
		}

		public List<CalendarNote> Notes { get; private set; }

		#region Basic
		public bool ShowHeader { get; set; }
		public bool ShowBusinessName { get; set; }
		public bool ShowDecisionMaker { get; set; }

		public string Header { get; set; }
		public bool ApplyForAllBasic { get; set; }
		#endregion

		#region Notes
		public bool ShowCustomComment { get; set; }
		public string CustomComment { get; set; }
		public bool ApplyForAllCustomComment { get; set; }
		#endregion

		#region Style
		public string SlideColor { get; set; }
		public bool ApplyForAllThemeColor { get; set; }
		public bool ShowLogo { get; set; }
		public Image Logo { get; set; }
		public bool ApplyForAllLogo { get; set; }
		public bool ShowBigDate { get; set; }
		#endregion

		public virtual void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
					#region Basic
					case "ShowHeader":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowHeader = tempBool;
						break;
					case "ShowBusinessName":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowBusinessName = tempBool;
						break;
					case "ShowDecisionMaker":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDecisionMaker = tempBool;
						break;
					case "Header":
						Header = childNode.InnerText;
						break;
					case "ApplyForAllBasic":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllBasic = tempBool;
						break;
					#endregion

					#region Notes
					case "ShowCustomComment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCustomComment = tempBool;
						break;
					case "CustomComment":
						CustomComment = childNode.InnerText;
						break;
					case "ApplyForAllCustomComment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllCustomComment = tempBool;
						break;
					#endregion

					#region Style
					case "SlideColor":
						SlideColor = childNode.InnerText;
						break;
					case "ApplyForAllThemeColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllThemeColor = tempBool;
						break;

					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "Logo":
						if (string.IsNullOrEmpty(childNode.InnerText))
							Logo = null;
						else
						{
							Logo = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
							_encodedLogo = childNode.InnerText;
						}
						break;
					case "ApplyForAllLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllLogo = tempBool;
						break;

					case "ShowBigDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowBigDate = tempBool;
						break;
						#endregion
				}
			}
		}
	}
}
