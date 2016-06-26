using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Digital;

namespace Asa.Media.Controls.BusinessClasses.Output.DigitalInfo
{
	public class BaseDigitalInfoOutputModel
	{
		public const int MaxDigitalProducts = 6;

		private readonly MediaDigitalInfo _parent;
		public string SummaryInfo { get; set; }
		public string[] Logos { get; set; }

		public List<DigitalInfoRecordOutputModel> Records { get; }

		public Dictionary<string, string> ReplacementsList { get; }

		public BaseDigitalInfoOutputModel(MediaDigitalInfo parent)
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
			var digitalProductsCount = Records.Count;
			logosOnSlide.Clear();
			for (int i = 0; i < MaxDigitalProducts; i++)
			{
				var fileName = String.Empty;
				if (i < digitalProductsCount)
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
			var temp = new List<string>();

			ReplacementsList.Clear();

			var digitalProductsCount = Records.Count;
			for (var i = 0; i < MaxDigitalProducts; i++)
			{
				key = String.Format("Digital{0}", i + 1);
				if (i < digitalProductsCount)
				{
					var digitalProduct = Records[i];
					temp.Clear();
					if (!String.IsNullOrEmpty(digitalProduct.Category))
						temp.Add(digitalProduct.Category);
					if (!String.IsNullOrEmpty(digitalProduct.SubCategory))
						temp.Add(digitalProduct.SubCategory);
					if (!String.IsNullOrEmpty(digitalProduct.Product))
						temp.Add(digitalProduct.Product);
					if (!String.IsNullOrEmpty(digitalProduct.Info))
						temp.Add(digitalProduct.Info);
					value = String.Join("    ", temp);
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
