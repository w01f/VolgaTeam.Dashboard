﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Dashboard.InteropClasses;
using Asa.Solutions.Dashboard.PresentationClasses.Output;
using DevExpress.Skins;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class ClientGoalsControl : DashboardSlideControl, IClientGoalsOutputData, IDashboardSlide
	{
		private bool _allowToSave;
		public override SlideType SlideType => SlideType.ClientGoals;
		public override string ControlName => SlideContainer.DashboardInfo.ClientGoalsTitle;

		public ClientGoalsControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = ControlName;

			comboBoxEditSlideHeader.EnableSelectAll();
			comboBoxEditGoal1.EnableSelectAll();
			comboBoxEditGoal2.EnableSelectAll();
			comboBoxEditGoal3.EnableSelectAll();
			comboBoxEditGoal4.EnableSelectAll();
			comboBoxEditGoal5.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Headers.Select(item => item.Value).ToArray());

			comboBoxEditGoal1.Properties.Items.Clear();
			comboBoxEditGoal1.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());

			comboBoxEditGoal2.Properties.Items.Clear();
			comboBoxEditGoal2.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());

			comboBoxEditGoal3.Properties.Items.Clear();
			comboBoxEditGoal3.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());

			comboBoxEditGoal4.Properties.Items.Clear();
			comboBoxEditGoal4.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());

			comboBoxEditGoal5.Properties.Items.Clear();
			comboBoxEditGoal5.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Goals.Where(item => !item.IsPlaceholder).ToArray());

			pictureEditSplash.Image = SlideContainer.DashboardInfo.GraphicResources?.ClientGoalsSplashLogo;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControl.MaximumSize = RectangleHelper.ScaleSize(layoutControl.MaximumSize, scaleFactor);
			layoutControl.MinimumSize = RectangleHelper.ScaleSize(layoutControl.MinimumSize, scaleFactor);
			layoutControlItemSlideHeader.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MaxSize, scaleFactor);
			layoutControlItemSlideHeader.MinSize = RectangleHelper.ScaleSize(layoutControlItemSlideHeader.MinSize, scaleFactor);
		}

		public override void LoadData()
		{
			_allowToSave = false;
			if (String.IsNullOrEmpty(SlideContainer.EditedContent.ClientGoalsState.SlideHeader))
			{
				if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
					comboBoxEditSlideHeader.SelectedIndex = 0;
			}
			else
				comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ClientGoalsState.SlideHeader;

			comboBoxEditGoal1.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.ClientGoalsState.Goal1) ? 
				SlideContainer.EditedContent.ClientGoalsState.Goal1 : 
				SlideContainer.DashboardInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).Select(listDataItem => listDataItem.Value).ElementAtOrDefault(0);
			comboBoxEditGoal2.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.ClientGoalsState.Goal2) ? 
				SlideContainer.EditedContent.ClientGoalsState.Goal2 :
				SlideContainer.DashboardInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).Select(listDataItem => listDataItem.Value).ElementAtOrDefault(1);
			comboBoxEditGoal3.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.ClientGoalsState.Goal3) ? 
				SlideContainer.EditedContent.ClientGoalsState.Goal3 :
				SlideContainer.DashboardInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).Select(listDataItem => listDataItem.Value).ElementAtOrDefault(2);
			comboBoxEditGoal4.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.ClientGoalsState.Goal4) ? 
				SlideContainer.EditedContent.ClientGoalsState.Goal4 :
				SlideContainer.DashboardInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).Select(listDataItem => listDataItem.Value).ElementAtOrDefault(3);
			comboBoxEditGoal5.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.ClientGoalsState.Goal5) ? 
				SlideContainer.EditedContent.ClientGoalsState.Goal5 :
				SlideContainer.DashboardInfo.ClientGoalsLists.Goals.Where(listDataItem => listDataItem.IsDefault).Select(listDataItem => listDataItem.Value).ElementAtOrDefault(4);

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			SlideContainer.EditedContent.ClientGoalsState.SlideHeader = comboBoxEditSlideHeader.EditValue?.ToString() ?? String.Empty;
			SlideContainer.EditedContent.ClientGoalsState.Goal1 = comboBoxEditGoal1.EditValue?.ToString() ?? String.Empty;
			SlideContainer.EditedContent.ClientGoalsState.Goal2 = comboBoxEditGoal2.EditValue?.ToString() ?? String.Empty;
			SlideContainer.EditedContent.ClientGoalsState.Goal3 = comboBoxEditGoal3.EditValue?.ToString() ?? String.Empty;
			SlideContainer.EditedContent.ClientGoalsState.Goal4 = comboBoxEditGoal4.EditValue?.ToString() ?? String.Empty;
			SlideContainer.EditedContent.ClientGoalsState.Goal5 = comboBoxEditGoal5.EditValue?.ToString() ?? String.Empty;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SlideContainer.RaiseDataChanged();
		}

		#region Output Staff
		public override bool ReadyForOutput => !String.IsNullOrEmpty(comboBoxEditGoal1.EditValue?.ToString().Trim()) ||
			!String.IsNullOrEmpty(comboBoxEditGoal2.EditValue?.ToString().Trim()) ||
			!String.IsNullOrEmpty(comboBoxEditGoal3.EditValue?.ToString().Trim()) ||
			!String.IsNullOrEmpty(comboBoxEditGoal4.EditValue?.ToString().Trim()) ||
			!String.IsNullOrEmpty(comboBoxEditGoal5.EditValue?.ToString().Trim());

		public int GoalsCount => SelectedGoals.Length;

		public string Title => comboBoxEditSlideHeader.EditValue?.ToString() ?? String.Empty;

		public string[] SelectedGoals
		{
			get
			{
				var result = new List<string>();
				if (!String.IsNullOrEmpty(comboBoxEditGoal1.EditValue?.ToString().Trim()))
					result.Add(comboBoxEditGoal1.EditValue.ToString());
				if (!String.IsNullOrEmpty(comboBoxEditGoal2.EditValue?.ToString().Trim()))
					result.Add(comboBoxEditGoal2.EditValue.ToString());
				if (!String.IsNullOrEmpty(comboBoxEditGoal3.EditValue?.ToString().Trim()))
					result.Add(comboBoxEditGoal3.EditValue.ToString());
				if (!String.IsNullOrEmpty(comboBoxEditGoal4.EditValue?.ToString().Trim()))
					result.Add(comboBoxEditGoal4.EditValue.ToString());
				if (!String.IsNullOrEmpty(comboBoxEditGoal5.EditValue?.ToString().Trim()))
					result.Add(comboBoxEditGoal5.EditValue.ToString());
				return result.ToArray();
			}
		}

		public OutputGroup GetOutputData()
		{
			return new OutputGroup
			{
				Name = ControlName,
				IsCurrent = SlideContainer.SelectedSlideType == SlideType,
				Items = new List<OutputItem>(new[]
				{
					new OutputItem
					{
						Name = ControlName,
						PresentationSourcePath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath,
							Path.GetFileName(Path.GetTempFileName())),
						SlidesCount = 1,
						IsCurrent = true,
						SlideGeneratingAction = (processor, destinationPresentation) =>
						{
							processor.AppendDashboardClientGoals(this,destinationPresentation);
						},
						PreviewGeneratingAction = (processor, presentationSourcePath) =>
						{
							processor.PrepareDashboardClientGoals(this, presentationSourcePath);
						}
					}
				})
			};
		}
		#endregion
	}
}