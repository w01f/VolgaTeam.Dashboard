using System;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabPageContainerControl<TShareTabControl> : XtraTabPage, IShareTabPageContainer where TShareTabControl : ShareTabBaseControl
	{
		private readonly ShareControl _shareControl;
		public ShareTabBaseControl ContentControl { get; private set; }
		

		public ShareTabPageContainerControl(ShareControl shareControl)
		{
			_shareControl = shareControl;
			InitializeComponent();

			if (typeof(ShareTabAControl) == typeof(TShareTabControl))
				Text = _shareControl.SlideContainer.StarInfo.Titles.Tab5SubATitle;
			else if (typeof(ShareTabBControl) == typeof(TShareTabControl))
				Text = _shareControl.SlideContainer.StarInfo.Titles.Tab5SubBTitle;
			else if (typeof(ShareTabCControl) == typeof(TShareTabControl))
				Text = _shareControl.SlideContainer.StarInfo.Titles.Tab5SubCTitle;
			else if (typeof(ShareTabDControl) == typeof(TShareTabControl))
				Text = _shareControl.SlideContainer.StarInfo.Titles.Tab5SubDTitle;
			else if (typeof(ShareTabEControl) == typeof(TShareTabControl))
				Text = _shareControl.SlideContainer.StarInfo.Titles.Tab5SubETitle;
		}

		public void LoadContent()
		{
			if (ContentControl != null) return;
			Application.DoEvents();
			ContentControl = (TShareTabControl)Activator.CreateInstance(typeof(TShareTabControl), _shareControl);
			ContentControl.Dock = DockStyle.Fill;
			Controls.Add(ContentControl);
			_shareControl.SlideContainer.AssignCloseActiveEditorsOnOutsideClick(ContentControl);
			Application.DoEvents();
		}
	}
}
