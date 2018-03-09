using System;
using System.Windows.Forms;
using Asa.Solutions.StarApp.PresentationClasses.ImageEdit;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabBaseControl : UserControl
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		protected ROIControl ROIContentContainer { get; }
		protected ROIControlImageEditorHelper ImageEditorHelper { get; }

		public ROITabBaseControl()
		{
			InitializeComponent();
			ImageEditorHelper = new ROIControlImageEditorHelper(this);
		}

		public ROITabBaseControl(ROIControl shareContentContainer) : this()
		{
			ROIContentContainer = shareContentContainer;
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
