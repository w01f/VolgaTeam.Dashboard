using Asa.Common.GUI.ContentEditors.Events;
using DevComponents.DotNetBar;

namespace Asa.Common.GUI.ContentEditors.Interfaces
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