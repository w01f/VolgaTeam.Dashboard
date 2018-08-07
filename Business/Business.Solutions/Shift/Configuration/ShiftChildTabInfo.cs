using System.Drawing;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public abstract class ShiftChildTabInfo : ShiftTabInfo
	{
		public ShiftChildTabType TabType { get; }
		public Image RightLogo { get; protected set; }
		public Image FooterLogo { get; protected set; }
		public Image BackgroundLogo { get; protected set; }
		public virtual bool IsRegularChildTab => false;
		public bool EnableOutput { get; protected set; }
		public TextEditorConfiguration CommonEditorConfiguration { get; set; }

		protected ShiftChildTabInfo(ShiftChildTabType tabType)
		{
			TabType = tabType;
			EnableOutput = true;
			CommonEditorConfiguration = TextEditorConfiguration.Empty();
		}
	}
}
