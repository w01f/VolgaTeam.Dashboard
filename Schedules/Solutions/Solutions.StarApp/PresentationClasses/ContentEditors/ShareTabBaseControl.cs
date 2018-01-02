using System;
using System.Windows.Forms;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ShareTabBaseControl : UserControl
	{
		protected bool _allowToSave;

		protected ShareControl ShareContentContainer { get; }

		public ShareTabBaseControl()
		{
			InitializeComponent();
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
