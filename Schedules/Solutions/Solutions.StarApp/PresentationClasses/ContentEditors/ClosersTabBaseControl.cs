using System;
using System.Windows.Forms;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Common.PresentationClasses.Output;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ClosersTabBaseControl : UserControl
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		protected ClosersControl ClosersContentContainer { get; }

		public virtual StarAppOutputType OutputType { get; }
		public virtual string OutputName { get; }
		public int SlidesCount => 1;

		public ClosersTabBaseControl()
		{
			InitializeComponent();
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

		protected virtual OutputDataPackage GetOutputData()
		{
			throw new NotImplementedException();
		}

		public virtual void GenerateOutput()
		{
			throw new NotImplementedException();
		}

		public virtual PreviewGroup GeneratePreview()
		{
			throw new NotImplementedException();
		}
	}
}
