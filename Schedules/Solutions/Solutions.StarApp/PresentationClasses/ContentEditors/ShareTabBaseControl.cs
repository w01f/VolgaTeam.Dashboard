using System;
using System.Windows.Forms;
using Asa.Solutions.StarApp.PresentationClasses.ImageEdit;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabBaseControl : UserControl
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		protected ShareControl ShareContentContainer { get; }
		protected ShareControlImageEditorHelper ImageEditorHelper { get; }

		public ShareTabBaseControl()
		{
			InitializeComponent();
			ImageEditorHelper = new ShareControlImageEditorHelper(this);
		}

		public ShareTabBaseControl(ShareControl shareContentContainer) : this()
		{
			ShareContentContainer = shareContentContainer;
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
