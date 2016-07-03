using System;
using System.Collections.Generic;
using System.IO;
using Asa.Business.Media.Entities.NonPersistent.Digital;
using Asa.Media.Controls.BusinessClasses.Managers;

namespace Asa.Media.Controls.BusinessClasses.Output.DigitalInfo
{
	public class DigitalInfoStrategyOutputModel
	{
		public bool ShowLogos { get; set; }
		public string Total1 { get; set; }
		public string Total2 { get; set; }
		public string[] Logos { get; set; }

		public List<DigitalInfoStrategyRecordOutputModel> Records { get; }

		public string TemplatePath => BusinessObjects.Instance.OutputManager.GetDigitalStrategyFile(Records.Count);

		public DigitalInfoStrategyOutputModel()
		{
			Records = new List<DigitalInfoStrategyRecordOutputModel>();
		}

		public void GetLogos()
		{
			Logos = new string[] { };
			if (!ShowLogos) return;
			var logosOnSlide = new List<string>();
			var recordsCount = Records.Count;
			logosOnSlide.Clear();
			for (var i = 0; i < BaseDigitalInfoOneSheetOutputModel.MaxRecords; i++)
			{
				var fileName = String.Empty;
				if (i < recordsCount)
				{
					var digitalProduct = Records[i];
					if (digitalProduct.Logo != null && digitalProduct.Logo.ContainsData)
					{
						fileName = Path.GetTempFileName();
						digitalProduct.Logo.BigImage.Save(fileName);
					}
				}
				logosOnSlide.Add(fileName);
			}
			Logos = logosOnSlide.ToArray();
		}
	}
}
