using Asa.Browser.Controls.BusinessClasses.Enums;
using Asa.Browser.Controls.BusinessClasses.Interfaces;

namespace Asa.Browser.Controls.BusinessClasses.Objects.LinkViewContent
{
	class ExcelContent : ViewContent, IPrintableContent
	{
		public override LinkContentType ContentType=>LinkContentType.Excel;
		public string PrintableFileUrl => OriginalFileUrl;
		public int? CurrentPage => null;
	}
}
