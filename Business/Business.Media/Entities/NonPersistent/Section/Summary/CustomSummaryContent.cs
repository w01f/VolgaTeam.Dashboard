using System;
using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class CustomSummaryContent : CustomSummarySettings, ISectionSummaryContent
	{
		public SectionSummary Parent { get; private set; }
		public SectionSummaryTypeEnum SummaryType => SectionSummaryTypeEnum.Custom;

		[JsonConstructor]
		private CustomSummaryContent() { }

		public CustomSummaryContent(SectionSummary parent)
		{
			Parent = parent;
			Init();
		}

		public void AfterCreate()
		{
			if (!Items.OfType<ProductInfoSummaryItem>().Any())
			{
				Items.Clear();
				Init();
			}
		}

		public void SynchronizeSectionContent()
		{
			Items.OfType<ProductInfoSummaryItem>().ToList().ForEach(item => item.Synchronize());
		}

		public override void Dispose()
		{
			base.Dispose();
			Parent = null;
		}

		private void Init()
		{
			var defaultMediaItem = AddItem<ProductInfoSummaryItem>(this);
			defaultMediaItem.Value = String.Format("Local {0} Campaign", MediaMetaData.Instance.DataTypeString);
			defaultMediaItem.DataSourceType = SummaryItemDataSourceType.Media;
			defaultMediaItem.Synchronize();

			var defaultDigitalItem = AddItem<ProductInfoSummaryItem>(this);
			defaultDigitalItem.Value = "Digital Campaign";
			defaultDigitalItem.DataSourceType = SummaryItemDataSourceType.Digital;
			defaultDigitalItem.Synchronize();
		}
	}
}
