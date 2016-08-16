using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Preview;
using Asa.Solutions.Dashboard.InteropClasses;
using Asa.Solutions.Dashboard.PresentationClasses.Output;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public sealed partial class ClientGoalsControl : DashboardSlideControl, IClientGoalsOutputData, IDashboardSlide
	{
		private bool _allowToSave;
		public override SlideType SlideType => SlideType.ClientGoals;
		public string SlideName => "C. Needs Analysis";

		public ClientGoalsControl(BaseDashboardContainer slideContainer) : base(slideContainer)
		{
			InitializeComponent();
			Text = SlideName;
			if ((CreateGraphics()).DpiX > 96)
			{

				laGoal1.Font = new Font(laGoal1.Font.FontFamily, laGoal1.Font.Size - 3, laGoal1.Font.Style);
				laGoal2.Font = new Font(laGoal2.Font.FontFamily, laGoal2.Font.Size - 3, laGoal2.Font.Style);
				laGoal3.Font = new Font(laGoal3.Font.FontFamily, laGoal3.Font.Size - 3, laGoal3.Font.Style);
				laGoal4.Font = new Font(laGoal4.Font.FontFamily, laGoal4.Font.Size - 3, laGoal4.Font.Style);
				laGoal5.Font = new Font(laGoal5.Font.FontFamily, laGoal5.Font.Size - 3, laGoal5.Font.Style);
			}
			comboBoxEditGoal1.EnableSelectAll();
			comboBoxEditGoal2.EnableSelectAll();
			comboBoxEditGoal3.EnableSelectAll();
			comboBoxEditGoal4.EnableSelectAll();
			comboBoxEditGoal5.EnableSelectAll();

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Headers);

			comboBoxEditGoal1.Properties.Items.Clear();
			comboBoxEditGoal1.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Goals);

			comboBoxEditGoal2.Properties.Items.Clear();
			comboBoxEditGoal2.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Goals);

			comboBoxEditGoal3.Properties.Items.Clear();
			comboBoxEditGoal3.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Goals);

			comboBoxEditGoal4.Properties.Items.Clear();
			comboBoxEditGoal4.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Goals);

			comboBoxEditGoal5.Properties.Items.Clear();
			comboBoxEditGoal5.Properties.Items.AddRange(SlideContainer.DashboardInfo.ClientGoalsLists.Goals);

			pbSplash.Image = SlideContainer.DashboardInfo.ClientGoalsSplashLogo;
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

			comboBoxEditGoal1.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.ClientGoalsState.Goal1) ? SlideContainer.EditedContent.ClientGoalsState.Goal1 : null;
			comboBoxEditGoal2.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.ClientGoalsState.Goal2) ? SlideContainer.EditedContent.ClientGoalsState.Goal2 : null;
			comboBoxEditGoal3.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.ClientGoalsState.Goal3) ? SlideContainer.EditedContent.ClientGoalsState.Goal3 : null;
			comboBoxEditGoal4.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.ClientGoalsState.Goal4) ? SlideContainer.EditedContent.ClientGoalsState.Goal4 : null;
			comboBoxEditGoal5.EditValue = !String.IsNullOrEmpty(SlideContainer.EditedContent.ClientGoalsState.Goal5) ? SlideContainer.EditedContent.ClientGoalsState.Goal5 : null;

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

		private void EditValueChanged(object sender, EventArgs e)
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

		public void GenerateOutput()
		{
			SolutionDashboardPowerPointHelper.Instance.AppendClientGoals(this);
		}

		public PreviewGroup GeneratePreview()
		{
			var tempFileName = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			SolutionDashboardPowerPointHelper.Instance.PrepareClientGoals(this, tempFileName);
			return new PreviewGroup { Name = SlideName, PresentationSourcePath = tempFileName };
		}
		#endregion
	}
}