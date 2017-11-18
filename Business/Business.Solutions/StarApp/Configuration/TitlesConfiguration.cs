using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class TitlesConfiguration
	{
		public string Tab0Title { get; private set; }

		public string Tab1Title { get; private set; }
		public string Tab1SubATitle { get; private set; }

		public string Tab2Title { get; private set; }
		public string Tab2SubATitle { get; private set; }
		public string Tab2SubBTitle { get; private set; }

		public string Tab3Title { get; private set; }
		public string Tab3SubATitle { get; private set; }
		public string Tab3SubBTitle { get; private set; }
		public string Tab3SubCTitle { get; private set; }

		public string Tab4Title { get; private set; }
		public string Tab4SubATitle { get; private set; }
		public string Tab4SubBTitle { get; private set; }
		public string Tab4SubCTitle { get; private set; }

		public string Tab5Title { get; private set; }
		public string Tab5SubATitle { get; private set; }
		public string Tab5SubBTitle { get; private set; }
		public string Tab5SubCTitle { get; private set; }
		public string Tab5SubDTitle { get; private set; }
		public string Tab5SubETitle { get; private set; }

		public string Tab6Title { get; private set; }
		public string Tab6SubATitle { get; private set; }
		public string Tab6SubBTitle { get; private set; }
		public string Tab6SubCTitle { get; private set; }
		public string Tab6SubDTitle { get; private set; }

		public string Tab7Title { get; private set; }
		public string Tab7SubATitle { get; private set; }
		public string Tab7SubBTitle { get; private set; }
		public string Tab7SubCTitle { get; private set; }

		public string Tab8Title { get; private set; }
		public string Tab8SubATitle { get; private set; }
		public string Tab8SubBTitle { get; private set; }
		public string Tab8SubCTitle { get; private set; }
		public string Tab8SubDTitle { get; private set; }

		public string Tab9Title { get; private set; }
		public string Tab9SubATitle { get; private set; }
		public string Tab9SubBTitle { get; private set; }
		public string Tab9SubCTitle { get; private set; }

		public string Tab10Title { get; private set; }
		public string Tab10SubATitle { get; private set; }
		public string Tab10SubBTitle { get; private set; }
		public string Tab10SubCTitle { get; private set; }
		public string Tab10SubDTitle { get; private set; }

		public string Tab11Title { get; private set; }
		public string Tab11SubATitle { get; private set; }
		public string Tab11SubBTitle { get; private set; }
		public string Tab11SubCTitle { get; private set; }

		public void Load(StorageFile dataFile)
		{
			var document = new XmlDocument();
			document.Load(dataFile.LocalPath);

			Tab0Title = document.SelectSingleNode(@"//Settings/Tab_0")?.InnerText;

			Tab1Title = document.SelectSingleNode(@"//Settings/Tab_1/Name")?.InnerText;
			Tab1SubATitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_A")?.InnerText;
			
			Tab2Title = document.SelectSingleNode(@"//Settings/Tab_2/Name")?.InnerText;
			Tab2SubATitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_A")?.InnerText;
			Tab2SubBTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_B")?.InnerText;

			Tab3Title = document.SelectSingleNode(@"//Settings/Tab_3/Name")?.InnerText;
			Tab3SubATitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_A")?.InnerText;
			Tab3SubBTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_B")?.InnerText;
			Tab3SubCTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_C")?.InnerText;

			Tab4Title = document.SelectSingleNode(@"//Settings/Tab_4/Name")?.InnerText;
			Tab4SubATitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_A")?.InnerText;
			Tab4SubBTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_B")?.InnerText;
			Tab4SubCTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_C")?.InnerText;

			Tab5Title = document.SelectSingleNode(@"//Settings/Tab_5/Name")?.InnerText;
			Tab5SubATitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_A")?.InnerText;
			Tab5SubBTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_B")?.InnerText;
			Tab5SubCTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_C")?.InnerText;
			Tab5SubDTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_D")?.InnerText;
			Tab5SubETitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_E")?.InnerText;

			Tab6Title = document.SelectSingleNode(@"//Settings/Tab_6/Name")?.InnerText;
			Tab6SubATitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_A")?.InnerText;
			Tab6SubBTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_B")?.InnerText;
			Tab6SubCTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_C")?.InnerText;
			Tab6SubDTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_D")?.InnerText;

			Tab7Title = document.SelectSingleNode(@"//Settings/Tab_7/Name")?.InnerText;
			Tab7SubATitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_A")?.InnerText;
			Tab7SubBTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_B")?.InnerText;
			Tab7SubCTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_C")?.InnerText;

			Tab8Title = document.SelectSingleNode(@"//Settings/Tab_8/Name")?.InnerText;
			Tab8SubATitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_A")?.InnerText;
			Tab8SubBTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_B")?.InnerText;
			Tab8SubCTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_C")?.InnerText;
			Tab8SubDTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_D")?.InnerText;

			Tab9Title = document.SelectSingleNode(@"//Settings/Tab_9/Name")?.InnerText;
			Tab9SubATitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_A")?.InnerText;
			Tab9SubBTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_B")?.InnerText;
			Tab9SubCTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_C")?.InnerText;

			Tab10Title = document.SelectSingleNode(@"//Settings/Tab_10/Name")?.InnerText;
			Tab10SubATitle = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_A")?.InnerText;
			Tab10SubBTitle = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_B")?.InnerText;
			Tab10SubCTitle = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_C")?.InnerText;
			Tab10SubDTitle = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_D")?.InnerText;

			Tab11Title = document.SelectSingleNode(@"//Settings/Tab_11/Name")?.InnerText;
			Tab11SubATitle = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_A")?.InnerText;
			Tab11SubBTitle = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_B")?.InnerText;
			Tab11SubCTitle = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_C")?.InnerText;
		}
	}
}
