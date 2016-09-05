using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Digital;

namespace Asa.Media.Controls.BusinessClasses.Output.DigitalInfo
{
	public class BaseDigitalInfoOneSheetOutputModel
	{
		public const int MaxRecords = 6;
		public const int SlideCount = 1;

		private readonly MediaDigitalInfo _parent;
		public string SummaryInfo { get; set; }
		public string[] Logos { get; set; }

		public List<DigitalInfoRecordOutputModel> Records { get; }

		public Dictionary<string, string> ReplacementsList { get; }

		public BaseDigitalInfoOneSheetOutputModel(MediaDigitalInfo parent)
		{
			_parent = parent;
			Records = new List<DigitalInfoRecordOutputModel>();
			ReplacementsList = new Dictionary<string, string>();
		}

		public void GetLogos()
		{
			Logos = new string[] { };
			if (!_parent.ShowLogo) return;
			var logosOnSlide = new List<string>();
			var recordsCount = Records.Count;
			logosOnSlide.Clear();
			for (int i = 0; i < MaxRecords; i++)
			{
				var fileName = String.Empty;
				if (i < recordsCount)
				{
					var digitalProduct = Records[i];
					if (digitalProduct.Logo != null && digitalProduct.Logo.ContainsData)
					{
						fileName = Path.GetTempFileName();
						digitalProduct.Logo.SmallImage.Save(fileName);
					}
				}
				logosOnSlide.Add(fileName);
			}
			Logos = logosOnSlide.ToArray();
		}

		public virtual void PopulateReplacementsList()
		{
			var key = string.Empty;
			var value = string.Empty;

			ReplacementsList.Clear();

			var digitalProductsCount = Records.Count;
			for (var i = 0; i < MaxRecords; i++)
			{
				key = String.Format("Digital{0}", i + 1);
				if (i < digitalProductsCount)
				{
					var digitalProduct = Records[i];
					value = Records[i].Details;
					if (!ReplacementsList.ContainsKey(key))
						ReplacementsList.Add(key, value);

					key = String.Format("d{0}", (i + 1).ToString("00"));
					value = digitalProduct.LineID;
					if (!ReplacementsList.ContainsKey(key))
						ReplacementsList.Add(key, value);

					Application.DoEvents();
				}
				else
				{
					value = "Delete Row";
					if (!ReplacementsList.ContainsKey(key))
						ReplacementsList.Add(key, value);
				}
			}
		}
	}
}
