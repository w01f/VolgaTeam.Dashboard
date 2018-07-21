using System;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabPageContainerControl<TShareTabControl> : XtraTabPage, IShareTabPageContainer where TShareTabControl : ShareTabBaseControl
	{
		public ShareControl ParentControl { get; }
		public ShareTabBaseControl ContentControl { get; private set; }
		
		public ShareTabPageContainerControl(ShareControl shareControl)
		{
			ParentControl = shareControl;
			InitializeComponent();

			if (typeof(ShareTabAControl) == typeof(TShareTabControl))
				Text = ParentControl.SlideContainer.StarInfo.Titles.Tab5SubATitle;
			else if (typeof(ShareTabBControl) == typeof(TShareTabControl))
				Text = ParentControl.SlideContainer.StarInfo.Titles.Tab5SubBTitle;
			else if (typeof(ShareTabCControl) == typeof(TShareTabControl))
				Text = ParentControl.SlideContainer.StarInfo.Titles.Tab5SubCTitle;
			else if (typeof(ShareTabDControl) == typeof(TShareTabControl))
				Text = ParentControl.SlideContainer.StarInfo.Titles.Tab5SubDTitle;
			else if (typeof(ShareTabEControl) == typeof(TShareTabControl))
				Text = ParentControl.SlideContainer.StarInfo.Titles.Tab5SubETitle;
			else if (typeof(ShareTabUControl) == typeof(TShareTabControl))
				Text = ParentControl.SlideContainer.StarInfo.Titles.Tab5SubUTitle;
			else if (typeof(ShareTabVControl) == typeof(TShareTabControl))
				Text = ParentControl.SlideContainer.StarInfo.Titles.Tab5SubVTitle;
			else if (typeof(ShareTabWControl) == typeof(TShareTabControl))
				Text = ParentControl.SlideContainer.StarInfo.Titles.Tab5SubWTitle;
		}

		public void LoadContent()
		{
			if (ContentControl != null) return;

			Application.DoEvents();
			ContentControl = (TShareTabControl)Activator.CreateInstance(typeof(TShareTabControl), this);
			ContentControl.Dock = DockStyle.Fill;
			Controls.Add(ContentControl);
			ParentControl.SlideContainer.AssignCloseActiveEditorsOnOutsideClick(ContentControl);
			Application.DoEvents();
		}
	}
}
