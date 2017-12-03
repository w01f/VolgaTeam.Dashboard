using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class DashboardSlideControl : UserControl
	public partial class DashboardSlideControl : XtraTabPage
	{
		protected BaseDashboardContainer SlideContainer { get; }

		public virtual SlideType SlideType { get; }
		public virtual string ControlName { get; }
		public virtual bool ReadyForOutput { get; }
		public Theme SelectedTheme => SlideContainer.GetSelectedTheme(SlideType);

		public DashboardSlideControl()
		{
			InitializeComponent();
			Resize += OnResizeControl;
			OnResizeControl(this,EventArgs.Empty);
		}

		private void OnResizeControl(Object sender, EventArgs e)
		{
			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			pictureEditSplash.Visible = Width - (Int32)(800 * scaleFactor.Width) >= (Int32)(200 * scaleFactor.Width);
			if (pictureEditSplash.Image != null)
				pictureEditSplash.Properties.SizeMode = pictureEditSplash.Image.Width > pictureEditSplash.Width
					? PictureSizeMode.Zoom
					: PictureSizeMode.Clip;
		}

		protected DashboardSlideControl(BaseDashboardContainer slideContainer) : this()
		{
			SlideContainer = slideContainer;
		}

		public virtual void LoadData()
		{
			throw new NotImplementedException();
		}

		public virtual void ApplyChanges()
		{
			throw new NotImplementedException();
		}
	}
}
