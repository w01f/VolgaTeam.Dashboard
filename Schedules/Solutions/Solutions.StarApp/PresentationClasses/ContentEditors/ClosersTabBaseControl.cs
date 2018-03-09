using System;
using System.Windows.Forms;
using Asa.Solutions.StarApp.PresentationClasses.ImageEdit;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabBaseControl : UserControl
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		protected ClosersControl ClosersContentContainer { get; }
		protected ClosersControlImageEditorHelper ImageEditorHelper { get; }

		public ClosersTabBaseControl()
		{
			InitializeComponent();
			ImageEditorHelper = new ClosersControlImageEditorHelper(this);
		}

		public ClosersTabBaseControl(ClosersControl closersContentContainer) : this()
		{
			ClosersContentContainer = closersContentContainer;
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
