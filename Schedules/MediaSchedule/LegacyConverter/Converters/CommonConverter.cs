namespace Asa.Media.LegacyConverter.Converters
{
	static class CommonConverter
	{
		public static void ImportData(
			this Common.Core.Objects.Images.ImageSource target,
			Legacy.Common.Entities.Common.ImageSource source)
		{
			if (source == null || !source.ContainsData) return;
			target.IsDefault = source.IsDefault;
			target.BigImage = source.BigImage;
			target.SmallImage = source.SmallImage;
			target.TinyImage = source.TinyImage;
			target.XtraTinyImage = source.XtraTinyImage;
			target.FileName = source.FileName;
		}

		public static void ImportData(
			this Common.Core.Objects.Output.ContractSettings target,
			Legacy.Common.Entities.Common.ContractSettings source)
		{
			target.ShowDisclaimer = source.ShowDisclaimer;
			target.ShowSignatureLine = source.ShowSignatureLine;
			target.RateExpirationDate = source.RateExpirationDate;
		}

		public static void ImportData(
			this Common.Core.Objects.Output.TextGroup target,
			Legacy.Common.Entities.Common.TextGroup source)
		{
			foreach (var oldItem in source.Items)
			{
				if (oldItem is Legacy.Common.Entities.Common.TextGroup)
				{
					var oldTextGroup = (Legacy.Common.Entities.Common.TextGroup)oldItem;
					var textGroup = new Common.Core.Objects.Output.TextGroup(
						oldTextGroup.Separator,
						oldTextGroup.BorderLeft,
						oldTextGroup.BorderRight);
					textGroup.ImportData(oldTextGroup);
					target.Items.Add(textGroup);
				}
				else if (oldItem is Legacy.Common.Entities.Common.TextItem)
				{
					var oldTextItem = (Legacy.Common.Entities.Common.TextItem)oldItem;
					var textItem = new Common.Core.Objects.Output.TextItem(oldTextItem.Text, oldTextItem.IsBold);
					target.Items.Add(textItem);
				}
			}
		}

		public static void ImportData(
			this Business.Common.Entities.NonPersistent.Common.DateRange target,
			Legacy.Common.Entities.Common.DateRange source)
		{
			target.StartDate = source.StartDate;
			target.FinishDate = source.FinishDate;
		}
	}
}
