using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.PresentationClasses.ImageEdit;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public partial class StarAppControl : UserControl, IStarAppSlide
	{
		protected StarAppCommonControlImageEditorHelper ImageEditorHelper { get; }

		public BaseStarAppContainer SlideContainer { get; }

		public virtual SlideType SlideType { get; }
		public virtual bool ReadyForOutput { get; }
		public Theme SelectedTheme => SlideContainer.GetSelectedTheme(SlideType);

		public StarAppControl()
		{
			InitializeComponent();
			ImageEditorHelper = new StarAppCommonControlImageEditorHelper(this);
		}

		protected StarAppControl(BaseStarAppContainer slideContainer) : this()
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

		public virtual string SlideName => null;

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
