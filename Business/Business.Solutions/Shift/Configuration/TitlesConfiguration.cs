using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class TitlesConfiguration
	{
		public string Tab0Title { get; private set; }

		public string Tab1Title { get; private set; }
		public string Tab1SubATitle { get; private set; }
		public string Tab1SubBTitle { get; private set; }
		public string Tab1SubCTitle { get; private set; }
		public string Tab1SubDTitle { get; private set; }
		public string Tab1SubETitle { get; private set; }
		public string Tab1SubFTitle { get; private set; }
		public string Tab1SubGTitle { get; private set; }
		public string Tab1SubHTitle { get; private set; }
		public string Tab1SubITitle { get; private set; }
		public string Tab1SubJTitle { get; private set; }

		public string Tab2Title { get; private set; }
		public string Tab2SubATitle { get; private set; }
		public string Tab2SubBTitle { get; private set; }
		public string Tab2SubCTitle { get; private set; }
		public string Tab2SubDTitle { get; private set; }
		public string Tab2SubETitle { get; private set; }
		public string Tab2SubFTitle { get; private set; }
		public string Tab2SubGTitle { get; private set; }
		public string Tab2SubHTitle { get; private set; }
		public string Tab2SubITitle { get; private set; }
		public string Tab2SubJTitle { get; private set; }

		public string Tab3Title { get; private set; }
		public string Tab3SubATitle { get; private set; }
		public string Tab3SubBTitle { get; private set; }
		public string Tab3SubCTitle { get; private set; }
		public string Tab3SubDTitle { get; private set; }
		public string Tab3SubETitle { get; private set; }
		public string Tab3SubFTitle { get; private set; }
		public string Tab3SubGTitle { get; private set; }
		public string Tab3SubHTitle { get; private set; }
		public string Tab3SubITitle { get; private set; }
		public string Tab3SubJTitle { get; private set; }

		public string Tab4Title { get; private set; }
		public string Tab4SubATitle { get; private set; }
		public string Tab4SubBTitle { get; private set; }
		public string Tab4SubCTitle { get; private set; }
		public string Tab4SubDTitle { get; private set; }
		public string Tab4SubETitle { get; private set; }
		public string Tab4SubFTitle { get; private set; }
		public string Tab4SubGTitle { get; private set; }
		public string Tab4SubHTitle { get; private set; }
		public string Tab4SubITitle { get; private set; }
		public string Tab4SubJTitle { get; private set; }

		public string Tab5Title { get; private set; }
		public string Tab5SubATitle { get; private set; }
		public string Tab5SubBTitle { get; private set; }
		public string Tab5SubCTitle { get; private set; }
		public string Tab5SubDTitle { get; private set; }
		public string Tab5SubETitle { get; private set; }
		public string Tab5SubFTitle { get; private set; }
		public string Tab5SubGTitle { get; private set; }
		public string Tab5SubHTitle { get; private set; }
		public string Tab5SubITitle { get; private set; }
		public string Tab5SubJTitle { get; private set; }

		public string Tab6Title { get; private set; }
		public string Tab6SubATitle { get; private set; }
		public string Tab6SubBTitle { get; private set; }
		public string Tab6SubCTitle { get; private set; }
		public string Tab6SubDTitle { get; private set; }
		public string Tab6SubETitle { get; private set; }
		public string Tab6SubFTitle { get; private set; }
		public string Tab6SubGTitle { get; private set; }
		public string Tab6SubHTitle { get; private set; }
		public string Tab6SubITitle { get; private set; }
		public string Tab6SubJTitle { get; private set; }

		public string Tab7Title { get; private set; }
		public string Tab7SubATitle { get; private set; }
		public string Tab7SubBTitle { get; private set; }
		public string Tab7SubCTitle { get; private set; }
		public string Tab7SubDTitle { get; private set; }
		public string Tab7SubETitle { get; private set; }
		public string Tab7SubFTitle { get; private set; }
		public string Tab7SubGTitle { get; private set; }
		public string Tab7SubHTitle { get; private set; }
		public string Tab7SubITitle { get; private set; }
		public string Tab7SubJTitle { get; private set; }

		public string Tab8Title { get; private set; }
		public string Tab8SubATitle { get; private set; }
		public string Tab8SubBTitle { get; private set; }
		public string Tab8SubCTitle { get; private set; }
		public string Tab8SubDTitle { get; private set; }
		public string Tab8SubETitle { get; private set; }
		public string Tab8SubFTitle { get; private set; }
		public string Tab8SubGTitle { get; private set; }
		public string Tab8SubHTitle { get; private set; }
		public string Tab8SubITitle { get; private set; }
		public string Tab8SubJTitle { get; private set; }

		public string Tab9Title { get; private set; }
		public string Tab9SubATitle { get; private set; }
		public string Tab9SubBTitle { get; private set; }
		public string Tab9SubCTitle { get; private set; }
		public string Tab9SubDTitle { get; private set; }
		public string Tab9SubETitle { get; private set; }
		public string Tab9SubFTitle { get; private set; }
		public string Tab9SubGTitle { get; private set; }
		public string Tab9SubHTitle { get; private set; }
		public string Tab9SubITitle { get; private set; }
		public string Tab9SubJTitle { get; private set; }

		public void Load(StorageFile dataFile)
		{
			var document = new XmlDocument();
			document.Load(dataFile.LocalPath);

			Tab0Title = document.SelectSingleNode(@"//Settings/Tab_0")?.InnerText;

			Tab1Title = document.SelectSingleNode(@"//Settings/Tab_1/Name")?.InnerText;
			Tab1SubATitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_A")?.InnerText;
			Tab1SubBTitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_B")?.InnerText;
			Tab1SubCTitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_C")?.InnerText;
			Tab1SubDTitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_D")?.InnerText;
			Tab1SubETitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_E")?.InnerText;
			Tab1SubFTitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_F")?.InnerText;
			Tab1SubGTitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_G")?.InnerText;
			Tab1SubHTitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_H")?.InnerText;
			Tab1SubITitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_I")?.InnerText;
			Tab1SubJTitle = document.SelectSingleNode(@"//Settings/Tab_1/SubTab_J")?.InnerText;

			Tab2Title = document.SelectSingleNode(@"//Settings/Tab_2/Name")?.InnerText;
			Tab2SubATitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_A")?.InnerText;
			Tab2SubBTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_B")?.InnerText;
			Tab2SubCTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_C")?.InnerText;
			Tab2SubDTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_D")?.InnerText;
			Tab2SubETitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_E")?.InnerText;
			Tab2SubFTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_F")?.InnerText;
			Tab2SubGTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_G")?.InnerText;
			Tab2SubHTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_H")?.InnerText;
			Tab2SubITitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_I")?.InnerText;
			Tab2SubJTitle = document.SelectSingleNode(@"//Settings/Tab_2/SubTab_J")?.InnerText;

			Tab3Title = document.SelectSingleNode(@"//Settings/Tab_3/Name")?.InnerText;
			Tab3SubATitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_A")?.InnerText;
			Tab3SubBTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_B")?.InnerText;
			Tab3SubCTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_C")?.InnerText;
			Tab3SubDTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_D")?.InnerText;
			Tab3SubETitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_E")?.InnerText;
			Tab3SubFTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_F")?.InnerText;
			Tab3SubGTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_G")?.InnerText;
			Tab3SubHTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_H")?.InnerText;
			Tab3SubITitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_I")?.InnerText;
			Tab3SubJTitle = document.SelectSingleNode(@"//Settings/Tab_3/SubTab_J")?.InnerText;

			Tab4Title = document.SelectSingleNode(@"//Settings/Tab_4/Name")?.InnerText;
			Tab4SubATitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_A")?.InnerText;
			Tab4SubBTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_B")?.InnerText;
			Tab4SubCTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_C")?.InnerText;
			Tab4SubDTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_D")?.InnerText;
			Tab4SubETitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_E")?.InnerText;
			Tab4SubFTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_F")?.InnerText;
			Tab4SubGTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_G")?.InnerText;
			Tab4SubHTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_H")?.InnerText;
			Tab4SubITitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_I")?.InnerText;
			Tab4SubJTitle = document.SelectSingleNode(@"//Settings/Tab_4/SubTab_J")?.InnerText;

			Tab5Title = document.SelectSingleNode(@"//Settings/Tab_5/Name")?.InnerText;
			Tab5SubATitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_A")?.InnerText;
			Tab5SubBTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_B")?.InnerText;
			Tab5SubCTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_C")?.InnerText;
			Tab5SubDTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_D")?.InnerText;
			Tab5SubETitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_E")?.InnerText;
			Tab5SubFTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_F")?.InnerText;
			Tab5SubGTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_G")?.InnerText;
			Tab5SubHTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_H")?.InnerText;
			Tab5SubITitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_I")?.InnerText;
			Tab5SubJTitle = document.SelectSingleNode(@"//Settings/Tab_5/SubTab_J")?.InnerText;

			Tab6Title = document.SelectSingleNode(@"//Settings/Tab_6/Name")?.InnerText;
			Tab6SubATitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_A")?.InnerText;
			Tab6SubBTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_B")?.InnerText;
			Tab6SubCTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_C")?.InnerText;
			Tab6SubDTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_D")?.InnerText;
			Tab6SubETitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_E")?.InnerText;
			Tab6SubFTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_F")?.InnerText;
			Tab6SubGTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_G")?.InnerText;
			Tab6SubHTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_H")?.InnerText;
			Tab6SubITitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_I")?.InnerText;
			Tab6SubJTitle = document.SelectSingleNode(@"//Settings/Tab_6/SubTab_J")?.InnerText;

			Tab7Title = document.SelectSingleNode(@"//Settings/Tab_7/Name")?.InnerText;
			Tab7SubATitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_A")?.InnerText;
			Tab7SubBTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_B")?.InnerText;
			Tab7SubCTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_C")?.InnerText;
			Tab7SubDTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_D")?.InnerText;
			Tab7SubETitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_E")?.InnerText;
			Tab7SubFTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_F")?.InnerText;
			Tab7SubGTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_G")?.InnerText;
			Tab7SubHTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_H")?.InnerText;
			Tab7SubITitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_I")?.InnerText;
			Tab7SubJTitle = document.SelectSingleNode(@"//Settings/Tab_7/SubTab_J")?.InnerText;

			Tab8Title = document.SelectSingleNode(@"//Settings/Tab_8/Name")?.InnerText;
			Tab8SubATitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_A")?.InnerText;
			Tab8SubBTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_B")?.InnerText;
			Tab8SubCTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_C")?.InnerText;
			Tab8SubDTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_D")?.InnerText;
			Tab8SubETitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_E")?.InnerText;
			Tab8SubFTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_F")?.InnerText;
			Tab8SubGTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_G")?.InnerText;
			Tab8SubHTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_H")?.InnerText;
			Tab8SubITitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_I")?.InnerText;
			Tab8SubJTitle = document.SelectSingleNode(@"//Settings/Tab_8/SubTab_J")?.InnerText;

			Tab9Title = document.SelectSingleNode(@"//Settings/Tab_9/Name")?.InnerText;
			Tab9SubATitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_A")?.InnerText;
			Tab9SubBTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_B")?.InnerText;
			Tab9SubCTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_C")?.InnerText;
			Tab9SubDTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_D")?.InnerText;
			Tab9SubETitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_E")?.InnerText;
			Tab9SubFTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_F")?.InnerText;
			Tab9SubGTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_G")?.InnerText;
			Tab9SubHTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_H")?.InnerText;
			Tab9SubITitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_I")?.InnerText;
			Tab9SubJTitle = document.SelectSingleNode(@"//Settings/Tab_9/SubTab_J")?.InnerText;
		}
	}
}
