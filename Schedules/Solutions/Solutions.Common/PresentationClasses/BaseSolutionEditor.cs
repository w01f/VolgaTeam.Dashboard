using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Interfaces;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.FormStyle;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Slides;
using Asa.Solutions.Common.Common;

namespace Asa.Solutions.Common.PresentationClasses
{
	public abstract class BaseSolutionEditor : UserControl, ISolutionEditor
	{
		public BaseSolutionInfo SolutionInfo { get; }

		public string SolutionId => SolutionInfo.Id;
		public DateTime LastToggled { get; set; }
		public abstract SlideType SelectedSlideType { get; }
		public abstract string HelpKey { get; }

		public abstract PowerPointProcessor PowerPointProcessor { get; }

		public abstract Form MainForm { get; }
		public abstract Color? AccentColor { get; }

		public abstract MainFormStyleConfiguration StyleConfiguration { get; }
		public abstract ISolutionsResourceManager ResourceManager { get; }

		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<SelectedSlideTypeChanged> SlideTypeChanged;
		public event EventHandler<OutputStatusChangedEventArgs> OutputStatusChanged;

		protected BaseSolutionEditor(BaseSolutionInfo solutionInfo)
		{
			Dock = DockStyle.Fill;
			SolutionInfo = solutionInfo;
			LastToggled = DateTime.Now;
		}

		public virtual void Release()
		{
		}

		#region GUI Methods
		public abstract void InitControl(bool showSplash);

		public virtual void ShowEditor(bool showSplash)
		{
			LastToggled = DateTime.Now;
			BringToFront();
			RaiseSlideTypeChanged();
			RaiseOutputStatuesChanged();
		}
		public abstract void ShowHomeSlide(bool showSplash);
		#endregion

		#region Data Management
		public abstract void LoadData();
		public abstract void SaveData();
		public abstract void ApplyChanges();
		public void RaiseDataChanged()
		{
			DataChanged?.Invoke(this, EventArgs.Empty);
			RaiseOutputStatuesChanged();
		}
		#endregion

		#region Output
		public abstract bool ReadyForOutput { get; }
		public abstract bool MultipleSlidesAllowed { get; }

		public abstract bool CheckPowerPointRunning();
		public abstract void OutputPowerPointCurrent();
		public abstract void OutputPowerPointAll();
		public abstract void OutputPowerPointCustom(IList<OutputItem> outputItems);
		public abstract void OutputPdf();
		public abstract void Email();

		public void OnCustomSlideOutput(object sender, SlideMasterEventArgs e)
		{
			if (!CheckPowerPointRunning()) return;
			if (e.SlideMaster == null) return;

			OutputPowerPointCustom(new[] {
				new OutputItem
			{
				Name = e.SlideMaster.Name,
				IsCurrent = true,
				SlidesCount = 1,
				PresentationSourcePath = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
					Path.GetFileName(Path.GetTempFileName())),
				SlideGeneratingAction = (processor, destinationPresentation) =>
				{
					processor.AppendSlideMaster(e.SlideMaster.GetMasterPath(), destinationPresentation);
				},
				PreviewGeneratingAction = (processor, presentationSourcePath) =>
				{
					processor.PreparePresentation(presentationSourcePath,
						presentation => processor.AppendSlideMaster(e.SlideMaster.GetMasterPath(), presentation));
				}
			}});
		}

		public void OnCustomSlidePreview(object sender, SlideMasterEventArgs e)
		{
			if (!CheckPowerPointRunning()) return;
			if (e.SlideMaster == null) return;
			var previewGroup = new OutputGroup
			{
				Name = e.SlideMaster.Name,
				IsCurrent = true,
				Items = new List<OutputItem>(new[]
				{
					new OutputItem
					{
						Name = e.SlideMaster.Name,
						IsCurrent = true,
						SlidesCount = 1,
						PresentationSourcePath = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath,
							Path.GetFileName(Path.GetTempFileName())),
						SlideGeneratingAction = (processor, destinationPresentation) =>
						{
							processor.AppendSlideMaster(e.SlideMaster.GetMasterPath(),destinationPresentation);
						},
						PreviewGeneratingAction = (processor, presentationSourcePath) =>
						{
							processor.PreparePresentation(presentationSourcePath, presentation => processor.AppendSlideMaster(e.SlideMaster.GetMasterPath(), presentation));
						}
					}
				})
			};

			var selectedOutputItems = new List<OutputItem>();
			using (var form = new FormPreview(
				MainForm,
				PowerPointProcessor))
			{
				form.LoadGroups(new[] { previewGroup });
				if (form.ShowDialog() == DialogResult.OK)
					selectedOutputItems.AddRange(form.GetSelectedItems());
			}

			OutputPowerPointCustom(selectedOutputItems);
		}

		public void RaiseSlideTypeChanged()
		{
			SlideTypeChanged?.Invoke(this, new SelectedSlideTypeChanged { SlideType = SelectedSlideType });
		}

		public void RaiseOutputStatuesChanged()
		{
			OutputStatusChanged?.Invoke(this, new OutputStatusChangedEventArgs
			{
				MultipleSlidesAllowed = MultipleSlidesAllowed,
				IsOutputEnabled = ReadyForOutput
			});
		}
		#endregion
	}
}
