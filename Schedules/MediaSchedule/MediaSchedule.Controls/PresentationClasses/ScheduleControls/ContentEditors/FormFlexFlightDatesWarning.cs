using Asa.Common.Core.Helpers;
using DevExpress.Skins;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public partial class FormFlexFlightDatesWarning : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormFlexFlightDatesWarning()
		{
			InitializeComponent();

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}