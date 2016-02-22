using System;
using System.Drawing;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Enums;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.GUI.Summary;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	class SummaryMediaProductItemControl : SummaryProductItemControl
	{
		protected override Image ItemLogo
		{
			get
			{
				if (Product is DigitalProduct)
					return Common.GUI.Properties.Resources.SummaryDigital;
				if (Product is Program)
				{
					switch (MediaMetaData.Instance.DataType)
					{
						case MediaDataType.TVSchedule:
							return Common.GUI.Properties.Resources.SummaryTV;
						case MediaDataType.RadioSchedule:
							return Common.GUI.Properties.Resources.SummaryRadio;
					}
				}
				throw new ArgumentOutOfRangeException("Undefined Summary Product set");
			}
		}
		public override string ItemIcon
		{
			get
			{
				if (!String.IsNullOrEmpty(ItemTitle) && ShowValueOutput)
				{
					if (Product is DigitalProduct)
						return "digital.png";
					if (Product is Program)
					{
						switch (MediaMetaData.Instance.DataType)
						{
							case MediaDataType.TVSchedule:
								return "tv.png";
							case MediaDataType.RadioSchedule:
								return "radio.png";
						}
					}
				}
				return String.Empty;
			}
		}
	}
}
