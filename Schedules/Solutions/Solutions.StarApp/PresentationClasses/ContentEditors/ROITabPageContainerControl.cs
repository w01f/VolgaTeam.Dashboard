using System;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabPageContainerControl<TROITabControl> : XtraTabPage, IROITabPageContainer where TROITabControl : ROITabBaseControl
	{
		private readonly ROIControl _roiControl;
		public ROITabBaseControl ContentControl { get; private set; }
		

		public ROITabPageContainerControl(ROIControl roiControl)
		{
			_roiControl = roiControl;
			InitializeComponent();

			if (typeof(ROITabAControl) == typeof(TROITabControl))
				Text = _roiControl.SlideContainer.StarInfo.Titles.Tab6SubATitle;
			else if (typeof(ROITabBControl) == typeof(TROITabControl))
				Text = _roiControl.SlideContainer.StarInfo.Titles.Tab6SubBTitle;
			else if (typeof(ROITabCControl) == typeof(TROITabControl))
				Text = _roiControl.SlideContainer.StarInfo.Titles.Tab6SubCTitle;
			else if (typeof(ROITabDControl) == typeof(TROITabControl))
				Text = _roiControl.SlideContainer.StarInfo.Titles.Tab6SubDTitle;
		}

		public void LoadContent()
		{
			if (ContentControl != null) return;
			Application.DoEvents();
			ContentControl = (TROITabControl)Activator.CreateInstance(typeof(TROITabControl), _roiControl);
			ContentControl.Dock = DockStyle.Fill;
			Controls.Add(ContentControl);
			Application.DoEvents();
		}
	}
}
