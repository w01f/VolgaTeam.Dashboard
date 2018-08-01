using System.Drawing;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public abstract class StarChildTabInfo : StarTabInfo
	{
		public abstract StarChildTabType TabType { get; }
		public Image RightLogo { get; protected set; }
		public Image FooterLogo { get; protected set; }
		public Image BackgroundLogo { get; protected set; }
		public virtual bool IsRegularChildTab => false;
		public bool EnableOutput { get; protected set; }

		protected StarChildTabInfo()
		{
			EnableOutput = true;
		}
	}
}
