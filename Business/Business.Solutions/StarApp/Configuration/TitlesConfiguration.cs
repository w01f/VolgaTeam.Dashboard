using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class TitlesConfiguration
	{
		public string Tab0Title { get; private set; }

		public string Tab1Title { get; private set; }
		public string Tab1SubATitle { get; private set; }
		public string Tab1SubUTitle { get; private set; }
		public string Tab1SubVTitle { get; private set; }
		public string Tab1SubWTitle { get; private set; }

		public string Tab2Title { get; private set; }
		public string Tab2SubATitle { get; private set; }
		public string Tab2SubBTitle { get; private set; }
		public string Tab2SubUTitle { get; private set; }
		public string Tab2SubVTitle { get; private set; }
		public string Tab2SubWTitle { get; private set; }

		public string Tab3Title { get; private set; }
		public string Tab3SubATitle { get; private set; }
		public string Tab3SubBTitle { get; private set; }
		public string Tab3SubCTitle { get; private set; }
		public string Tab3SubUTitle { get; private set; }
		public string Tab3SubVTitle { get; private set; }
		public string Tab3SubWTitle { get; private set; }

		public string Tab4Title { get; private set; }
		public string Tab4SubATitle { get; private set; }
		public string Tab4SubBTitle { get; private set; }
		public string Tab4SubCTitle { get; private set; }
		public string Tab4SubUTitle { get; private set; }
		public string Tab4SubVTitle { get; private set; }
		public string Tab4SubWTitle { get; private set; }

		public string Tab5Title { get; private set; }
		public string Tab5SubATitle { get; private set; }
		public string Tab5SubBTitle { get; private set; }
		public string Tab5SubCTitle { get; private set; }
		public string Tab5SubDTitle { get; private set; }
		public string Tab5SubETitle { get; private set; }
		public string Tab5SubUTitle { get; private set; }
		public string Tab5SubVTitle { get; private set; }
		public string Tab5SubWTitle { get; private set; }

		public string Tab6Title { get; private set; }
		public string Tab6SubATitle { get; private set; }
		public string Tab6SubBTitle { get; private set; }
		public string Tab6SubCTitle { get; private set; }
		public string Tab6SubDTitle { get; private set; }
		public string Tab6SubUTitle { get; private set; }
		public string Tab6SubVTitle { get; private set; }
		public string Tab6SubWTitle { get; private set; }

		public string Tab7Title { get; private set; }
		public string Tab7SubATitle { get; private set; }
		public string Tab7SubBTitle { get; private set; }
		public string Tab7SubCTitle { get; private set; }
		public string Tab7SubUTitle { get; private set; }
		public string Tab7SubVTitle { get; private set; }
		public string Tab7SubWTitle { get; private set; }

		public string Tab8Title { get; private set; }
		public string Tab8SubATitle { get; private set; }
		public string Tab8SubBTitle { get; private set; }
		public string Tab8SubCTitle { get; private set; }
		public string Tab8SubDTitle { get; private set; }
		public string Tab8SubUTitle { get; private set; }
		public string Tab8SubVTitle { get; private set; }
		public string Tab8SubWTitle { get; private set; }

		public string Tab9Title { get; private set; }
		public string Tab9SubATitle { get; private set; }
		public string Tab9SubBTitle { get; private set; }
		public string Tab9SubCTitle { get; private set; }
		public string Tab9SubUTitle { get; private set; }
		public string Tab9SubVTitle { get; private set; }
		public string Tab9SubWTitle { get; private set; }

		public string Tab10Title { get; private set; }
		public string Tab10SubATitle { get; private set; }
		public string Tab10SubBTitle { get; private set; }
		public string Tab10SubCTitle { get; private set; }
		public string Tab10SubDTitle { get; private set; }
		public string Tab10SubUTitle { get; private set; }
		public string Tab10SubVTitle { get; private set; }
		public string Tab10SubWTitle { get; private set; }

		public string Tab11Title { get; private set; }
		public string Tab11SubATitle { get; private set; }
		public string Tab11SubBTitle { get; private set; }
		public string Tab11SubCTitle { get; private set; }
		public string Tab11SubUTitle { get; private set; }
		public string Tab11SubVTitle { get; private set; }
		public string Tab11SubWTitle { get; private set; }

		public void Load(StorageFile dataFile)
		{
			var document = new XmlDocument();
			document.Load(dataFile.LocalPath);

			Tab0Title = document.SelectSingleNode(@"//Settings/Tab_0")?.InnerText;

			Tab1Title = document.SelectSingleNode(@"//Settings/Tab_1/Name")?.InnerText;
			Tab1SubATitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_A")?.InnerText;
			Tab1SubUTitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_U")?.InnerText;
			Tab1SubVTitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_V")?.InnerText;
			Tab1SubWTitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_W")?.InnerText;

			Tab2Title = document.SelectSingleNode(@"//Settings/Tab_2/Name")?.InnerText;
			Tab2SubATitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_A")?.InnerText;
			Tab2SubBTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_B")?.InnerText;
			Tab2SubUTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_U")?.InnerText;
			Tab2SubVTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_V")?.InnerText;
			Tab2SubWTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_W")?.InnerText;

			Tab3Title = document.SelectSingleNode(@"//Settings/Tab_3/Name")?.InnerText;
			Tab3SubATitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_A")?.InnerText;
			Tab3SubBTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_B")?.InnerText;
			Tab3SubCTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_C")?.InnerText;
			Tab3SubUTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_U")?.InnerText;
			Tab3SubVTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_V")?.InnerText;
			Tab3SubWTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_W")?.InnerText;

			Tab4Title = document.SelectSingleNode(@"//Settings/Tab_4/Name")?.InnerText;
			Tab4SubATitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_A")?.InnerText;
			Tab4SubBTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_B")?.InnerText;
			Tab4SubCTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_C")?.InnerText;
			Tab4SubUTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_U")?.InnerText;
			Tab4SubVTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_V")?.InnerText;
			Tab4SubWTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_W")?.InnerText;

			Tab5Title = document.SelectSingleNode(@"//Settings/Tab_5/Name")?.InnerText;
			Tab5SubATitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_A")?.InnerText;
			Tab5SubBTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_B")?.InnerText;
			Tab5SubCTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_C")?.InnerText;
			Tab5SubDTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_D")?.InnerText;
			Tab5SubETitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_E")?.InnerText;
			Tab5SubUTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_U")?.InnerText;
			Tab5SubVTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_V")?.InnerText;
			Tab5SubWTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_W")?.InnerText;

			Tab6Title = document.SelectSingleNode(@"//Settings/Tab_6/Name")?.InnerText;
			Tab6SubATitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_A")?.InnerText;
			Tab6SubBTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_B")?.InnerText;
			Tab6SubCTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_C")?.InnerText;
			Tab6SubDTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_D")?.InnerText;
			Tab6SubUTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_U")?.InnerText;
			Tab6SubVTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_V")?.InnerText;
			Tab6SubWTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_W")?.InnerText;

			Tab7Title = document.SelectSingleNode(@"//Settings/Tab_7/Name")?.InnerText;
			Tab7SubATitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_A")?.InnerText;
			Tab7SubBTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_B")?.InnerText;
			Tab7SubCTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_C")?.InnerText;
			Tab7SubUTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_U")?.InnerText;
			Tab7SubVTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_V")?.InnerText;
			Tab7SubWTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_W")?.InnerText;

			Tab8Title = document.SelectSingleNode(@"//Settings/Tab_8/Name")?.InnerText;
			Tab8SubATitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_A")?.InnerText;
			Tab8SubBTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_B")?.InnerText;
			Tab8SubCTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_C")?.InnerText;
			Tab8SubDTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_D")?.InnerText;
			Tab8SubUTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_U")?.InnerText;
			Tab8SubVTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_V")?.InnerText;
			Tab8SubWTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_W")?.InnerText;

			Tab9Title = document.SelectSingleNode(@"//Settings/Tab_9/Name")?.InnerText;
			Tab9SubATitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_A")?.InnerText;
			Tab9SubBTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_B")?.InnerText;
			Tab9SubCTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_C")?.InnerText;
			Tab9SubUTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_U")?.InnerText;
			Tab9SubVTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_V")?.InnerText;
			Tab9SubWTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_W")?.InnerText;

			Tab10Title = document.SelectSingleNode(@"//Settings/Tab_10/Name")?.InnerText;
			Tab10SubATitle = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_A")?.InnerText;
			Tab10SubBTitle = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_B")?.InnerText;
			Tab10SubCTitle = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_C")?.InnerText;
			Tab10SubDTitle = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_D")?.InnerText;
			Tab10SubUTitle = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_U")?.InnerText;
			Tab10SubVTitle = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_V")?.InnerText;
			Tab10SubWTitle = document.SelectSingleNode(@"//Settings/Tab_10/SubTab_W")?.InnerText;

			Tab11Title = document.SelectSingleNode(@"//Settings/Tab_11/Name")?.InnerText;
			Tab11SubATitle = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_A")?.InnerText;
			Tab11SubBTitle = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_B")?.InnerText;
			Tab11SubCTitle = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_C")?.InnerText;
			Tab11SubUTitle = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_U")?.InnerText;
			Tab11SubVTitle = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_V")?.InnerText;
			Tab11SubWTitle = document.SelectSingleNode(@"//Settings/Tab_11/SubTab_W")?.InnerText;
		}
	}
}
