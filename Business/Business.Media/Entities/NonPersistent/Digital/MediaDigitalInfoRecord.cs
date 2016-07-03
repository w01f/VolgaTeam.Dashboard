using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Asa.Business.Online.Dictionaries;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Images;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Digital
{
	public class MediaDigitalInfoRecord : IJsonCloneable<MediaDigitalInfoRecord>
	{
		public MediaDigitalInfo Parent { get; set; }

		public Guid UniqueID { get; set; }
		public decimal Index { get; set; }
		public ImageSource Logo { get; set; }
		public string Category { get; set; }
		public string SubCategory { get; set; }
		public string Name { get; set; }
		public string Info { get; set; }

		#region Calculated Properties
		public Image SmallLogo => Logo?.TinyImage;
		#endregion

		[JsonConstructor]
		public MediaDigitalInfoRecord() { }

		public MediaDigitalInfoRecord(MediaDigitalInfo parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = Parent.Records.Count + 1;
			Logo = ListManager.Instance.Images.Where(g => g.IsDefault).Select(g => g.Images.FirstOrDefault(i => i.IsDefault)).FirstOrDefault()?.Clone<ImageSource, ImageSource>();
		}

		public void Dispose()
		{
			Logo.Dispose();
			Logo = null;

			Parent = null;
		}

		public void AfterClone(MediaDigitalInfoRecord source, bool fullClone = true)
		{
			Parent = source.Parent;
			UniqueID = Guid.NewGuid();
		}

		#region Output

		public string OneSheetDetails
		{
			get
			{
				var temp = new List<string>();
				if(Parent.ShowCategory && !String.IsNullOrEmpty(Category))
					temp.Add(Category);
				if (Parent.ShowSubCategory && !String.IsNullOrEmpty(SubCategory))
					temp.Add(SubCategory);
				if (Parent.ShowProduct && !String.IsNullOrEmpty(Name))
					temp.Add(Name);
				if (Parent.ShowInfo && !String.IsNullOrEmpty(Info))
					temp.Add(Info);
				return String.Join("    ", temp);
			}
		}

		#endregion
	}
}
