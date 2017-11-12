using Asa.Schedules.Common.Controls.ContentEditors.Events;
using DevComponents.DotNetBar;

namespace Asa.Schedules.Common.Controls.ContentEditors.Interfaces
{
	public interface IContentControl
	{
		string Identifier { get; }
		bool IsActive { get; set; }
		RibbonTabItem TabPage { get; }
		void InitMetaData();
		void InitControl();
		void ShowControl(ContentOpenEventArgs args = null);
		void GetHelp();
	}
}