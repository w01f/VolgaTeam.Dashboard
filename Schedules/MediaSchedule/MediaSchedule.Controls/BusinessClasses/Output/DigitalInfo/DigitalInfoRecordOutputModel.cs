using Asa.Common.Core.Objects.Images;

namespace Asa.Media.Controls.BusinessClasses.Output.DigitalInfo
{
	public class DigitalInfoRecordOutputModel
	{
		public string LineID { get; set; }
		public ImageSource Logo { get; set; }
		public string Category { get; set; }
		public string SubCategory { get; set; }
		public string Product { get; set; }
		public string Info { get; set; }
	}
}
