using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class TitlesConfiguration
	{
		public string RibbonTitle { get; private set; }

		public string Tab0Title { get; private set; }

		public string Tab1Title { get; private set; }
		public string Tab1SubATitle { get; private set; }
		public string Tab1SubBTitle { get; private set; }

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

			var node = document.SelectSingleNode(@"//Settings/RibbonGroupName");
			RibbonTitle = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_0");
			Tab0Title = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_1/Name");
			Tab1Title = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_A");
			Tab1SubATitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_B");
			Tab1SubBTitle = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_2/Name");
			Tab2Title = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_A");
			Tab2SubATitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_B");
			Tab2SubBTitle = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_3/Name");
			Tab3Title = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_A");
			Tab3SubATitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_B");
			Tab3SubBTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_C");
			Tab3SubCTitle = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_4/Name");
			Tab4Title = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_A");
			Tab4SubATitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_B");
			Tab4SubBTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_C");
			Tab4SubCTitle = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_5/Name");
			Tab5Title = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_A");
			Tab5SubATitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_B");
			Tab5SubBTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_C");
			Tab5SubCTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_D");
			Tab5SubDTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_E");
			Tab5SubETitle = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_6/Name");
			Tab6Title = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_A");
			Tab6SubATitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_B");
			Tab6SubBTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_C");
			Tab6SubCTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_D");
			Tab6SubDTitle = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_7/Name");
			Tab7Title = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_A");
			Tab7SubATitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_B");
			Tab7SubBTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_C");
			Tab7SubCTitle = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_8/Name");
			Tab8Title = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_A");
			Tab8SubATitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_B");
			Tab8SubBTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_C");
			Tab8SubCTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_D");
			Tab8SubDTitle = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_9/Name");
			Tab9Title = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_A");
			Tab9SubATitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_B");
			Tab9SubBTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_C");
			Tab9SubCTitle = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_10/Name");
			Tab10Title = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_A");
			Tab10SubATitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_B");
			Tab10SubBTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_C");
			Tab10SubCTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_D");
			Tab10SubDTitle = node?.InnerText;

			node = document.SelectSingleNode(@"//Settings/Tab_11/Name");
			Tab11Title = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_A");
			Tab11SubATitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_B");
			Tab11SubBTitle = node?.InnerText;
			node = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_C");
			Tab11SubCTitle = node?.InnerText;
		}
	}
}
