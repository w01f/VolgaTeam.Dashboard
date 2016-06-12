using System;
using Asa.Business.Media.Configuration;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Summary
{
	public class MediaInfoSummaryItem : ProductInfoSummaryItem
	{
		public override string Title => "Media Summary";

		[JsonConstructor]
		private MediaInfoSummaryItem() { }

		public MediaInfoSummaryItem(CustomSummaryContent summaryContent) : base(summaryContent) { }

		protected override void LoadProductInfo()
		{
			ShowValue = true;
			Value = String.Format("Local {0} Campaign", MediaMetaData.Instance.DataTypeString);
		}
	}
}
