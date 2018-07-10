using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class ROITabBaseControl : UserControl
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		protected ROIControl ROIContentContainer { get; }

		public virtual StarAppOutputType OutputType { get; }
		public virtual string OutputName { get; }
		public int SlidesCount => 1;
		public bool ReadyForOutput => GetOutputDataTextItems().Any();

		protected ROITabBaseControl()
		{
			InitializeComponent();
		}

		protected ROITabBaseControl(ROIControl shareContentContainer) : this()
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

		protected virtual OutputDataPackage GetOutputData()
		{
			throw new NotImplementedException();
		}

		protected virtual Dictionary<string, string> GetOutputDataTextItems()
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
