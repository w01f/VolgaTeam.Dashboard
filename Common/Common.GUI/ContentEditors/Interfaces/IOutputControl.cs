namespace Asa.Common.GUI.ContentEditors.Interfaces
{
	public interface IOutputControl
	{
		void OnOuterThemeChanged();
		void OutputPowerPoint();
		void OutputPdf();
		void Preview();
		void Email();
	}
}
