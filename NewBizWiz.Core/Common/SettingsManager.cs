using System;
using System.IO;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public class SettingsManager
	{
		private static SettingsManager _instance;

		private SettingsManager()
		{
			Orientation = "Landscape";
			SizeWidth = 10;
			SizeHeght = 7.5;
			SelectedWizard = String.Empty;
			DashboardName = "6 Minute Seller";
			DashboardCode = String.Empty;
		}

		public static SettingsManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new SettingsManager();
				return _instance;
			}
		}

		public string SharedListFolder { get; set; }
		public string ThemeCollectionPath { get; set; }
		public string SlideMastersPath { get; set; }
		public string HelpBrowserSettingsPath { get; set; }

		public string SelectedWizard { get; set; }
		public double SizeHeght { get; set; }
		public double SizeWidth { get; set; }
		public string Orientation { get; set; }

		public string DashboardName { get; set; }
		public string DashboardCode { get; set; }
		
		public string Size
		{
			get
			{
				switch (Orientation)
				{
					case "Landscape":
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "4 x 3";
						if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "5 x 4";
						if (SizeWidth == 13 && SizeHeght == 7.32)
							return "16 x 9";
						return "4 x 3";
					case "Portrait":
						if (SizeWidth == 7.5 && SizeHeght == 10)
							return "3 x 4";
						if (SizeWidth == 8.25 && SizeHeght == 10.75)
							return "4 x 5";
						if (SizeWidth == 7.32 && SizeHeght == 13)
							return "9 x 16";
						return "4 x 3";
					default:
						return "4 x 3";
				}
			}
		}

		public string SlideSize
		{
			get
			{
				switch (Orientation)
				{
					case "Landscape":
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "Landscape 4 x 3";
						if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "Landscape 5 x 4";
						if (SizeWidth == 13 && SizeHeght == 7.32)
							return "Landscape 16 x 9";
						return "Landscape 4 x 3";
					case "Portrait":
						if (SizeWidth == 7.5 && SizeHeght == 10)
							return "Portrait 3 x 4";
						if (SizeWidth == 8.25 && SizeHeght == 10.75)
							return "Portrait 4 x 5";
						if (SizeWidth == 7.32 && SizeHeght == 13)
							return "Portrait 9 x 16";
						return "Landscape 4 x 3";
					default:
						return "Landscape 4 x 3";
				}
			}
		}

		public string SlideFolder
		{
			get
			{
				switch (Orientation)
				{
					case "Landscape":
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "Slides43";
						if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "Slides54";
						if (SizeWidth == 13 && SizeHeght == 7.32)
							return "Slides169";
						return "Slides43";
					case "Portrait":
						if (SizeWidth == 7.5 && SizeHeght == 10)
							return "Slides34";
						if (SizeWidth == 8.25 && SizeHeght == 10.75)
							return "Slides45";
						if (SizeWidth == 7.32 && SizeHeght == 13)
							return "Slides916";
						return "Slides43";
					default:
						return "Slides43";
				}
			}
		}

		public string SlideMasterFolder
		{
			get { return Size.Replace(" ", ""); }
		}

		public string SettingsPath { get; set; }
		public string OutgoingFolderPath { get; set; }

		public void LoadSharedSettings()
		{
			LoadDashdoardCode();

			if (!ResourceManager.Instance.SharedSettingsFile.ExistsLocal()) return;
			
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.SharedSettingsFile.LocalPath);

			var node = document.SelectSingleNode(@"/SharedSettings/SelectedWizard");
			if (node != null)
				SelectedWizard = node.InnerText;
			node = document.SelectSingleNode(@"/SharedSettings/SizeWidth");
			double tempDouble;
			if (node != null)
			{
				if (double.TryParse(node.InnerText, out tempDouble))
					SizeWidth = tempDouble;
			}
			node = document.SelectSingleNode(@"/SharedSettings/SizeHeght");
			if (node != null)
			{
				if (double.TryParse(node.InnerText, out tempDouble))
					SizeHeght = tempDouble;
			}
			node = document.SelectSingleNode(@"/SharedSettings/Orientation");
			if (node != null)
				Orientation = node.InnerText;
		}

		public void SaveSharedSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<SharedSettings>");
			xml.AppendLine(@"<SelectedWizard>" + SelectedWizard.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedWizard>");
			xml.AppendLine(@"<SizeHeght>" + SizeHeght + @"</SizeHeght>");
			xml.AppendLine(@"<SizeWidth>" + SizeWidth + @"</SizeWidth>");
			xml.AppendLine(@"<Orientation>" + Orientation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Orientation>");
			xml.AppendLine(@"</SharedSettings>");

			using (var sw = new StreamWriter(ResourceManager.Instance.SharedSettingsFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		private void LoadDashdoardCode()
		{
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.DashboardCodeFile.LocalPath);
			var node = document.SelectSingleNode(@"/Settings/dashboard/DashboardCode");
			if (node != null)
			{
				DashboardCode = node.InnerText.Trim().ToLower();
			}
		}
	}
}