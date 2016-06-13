using System;
using System.Drawing;
using System.Linq;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Images;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Digital
{
	public class SectionDigitalProduct : IJsonCloneable<SectionDigitalProduct>
	{
		private SectionDigitalInfo Parent { get; set; }

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
		public SectionDigitalProduct() { }

		public SectionDigitalProduct(SectionDigitalInfo parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = Parent.Products.Count + 1;
			Logo = MediaMetaData.Instance.ListManager.Images.Where(g => g.IsDefault).Select(g => g.Images.FirstOrDefault(i => i.IsDefault)).FirstOrDefault()?.Clone<ImageSource, ImageSource>();
		}

		public void Dispose()
		{
			Logo.Dispose();
			Logo = null;

			Parent = null;
		}

		public void AfterClone(SectionDigitalProduct source, bool fullClone = true)
		{
			Parent = source.Parent;
			UniqueID = Guid.NewGuid();
		}
	}
}
