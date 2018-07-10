using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public partial class StarAppControl : UserControl, IStarAppSlideContainer, IScheduleResourceHolder
	{
		protected bool _allowToSave;
		protected bool _dataChanged;

		public BaseStarAppContainer SlideContainer { get; }

		public virtual SlideType SlideType { get; }
		public Theme SelectedTheme => SlideContainer.GetSelectedTheme(SlideType);
		public BaseScheduleResourceContainer ResourceContainer => SlideContainer.EditedContent.ScheduleResources;

		protected StarAppControl()
		{
			InitializeComponent();
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

		public virtual bool ReadyForOutput { get; }
		public virtual string OutputName { get; }

		public virtual OutputGroup GetOutputGroup()
		{
			throw new NotImplementedException();
		}

		public virtual void GenerateOutput(IList<OutputConfiguration> configurations)
		{
			throw new NotImplementedException();
		}

		public virtual IList<PreviewGroup> GeneratePreview(IList<OutputConfiguration> configurations)
		{
			throw new NotImplementedException();
		}
	}
}
