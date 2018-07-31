using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Helpers;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.FormStyle;
using Asa.Schedules.Common.Controls.ContentEditors.Controls;
using Asa.Schedules.Common.Controls.ContentEditors.Interfaces;
using Asa.Solutions.Common.Common;
using DevComponents.DotNetBar;
using DevExpress.Skins;

namespace Asa.Solutions.Common.PresentationClasses
{
	//public abstract partial class BaseSolutionContainerControl<TChangeInfo> : UserControl where TChangeInfo : BaseScheduleChangeInfo
	public abstract partial class BaseSolutionContainerControl<TChangeInfo> : BaseContentOutputControl<TChangeInfo>, IMultipleSlidesOutputControl where TChangeInfo : BaseScheduleChangeInfo
	{
		private bool _allowToHandleEvents;
		protected abstract SolutionsManager SolutionManager { get; }
		protected List<ISolutionToggle> SolutionToggles { get; }
		protected ISolutionToggle SelectedSolutionToggle => SolutionToggles.FirstOrDefault(st => st.Checked);
		protected List<ISolutionEditor> SolutionEditors { get; }
		protected ISolutionEditor ActiveSolutionEditor => SolutionEditors.FirstOrDefault(e => e.SolutionId == SelectedSolutionToggle?.SolutionInfo.Id);

		public abstract RibbonPanel PanelSolutions { get; }
		public abstract ButtonItem ButtonPowerPoint { get; }
		public abstract ButtonItem ButtonPdf { get; }
		public abstract ButtonItem ButtonEmail { get; }

		public abstract MainFormStyleConfiguration StyleConfiguration { get; }

		protected BaseSolutionContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			SolutionToggles = new List<ISolutionToggle>();
			SolutionEditors = new List<ISolutionEditor>();

			xtraScrollableControlPageTemplates.Padding = new System.Windows.Forms.Padding(0, 0, 0, SolutionToggleHelper.ButtonPadding);

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemSolutionToggles.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSolutionToggles.MaxSize, scaleFactor);
			layoutControlItemSolutionToggles.MinSize = RectangleHelper.ScaleSize(layoutControlItemSolutionToggles.MinSize, scaleFactor);
		}

		#region BaseContentEditControl Override

		public override void InitControl()
		{
			base.InitControl();

			_allowToHandleEvents = false;
			LoadControlPanel();
			_allowToHandleEvents = true;

			Resize += OnResize;
		}

		protected override void UpdateEditedContet()
		{
			if (SolutionEditors.Any() && !ContentUpdateInfo.ChangeInfo.WholeScheduleChanged)
				return;
			SolutionEditors.ForEach(se => se.LoadData());
			ShowActiveEditor(false);
		}

		protected override void ApplyChanges()
		{
			ActiveSolutionEditor?.ApplyChanges();
		}

		protected override void SaveData()
		{
			SolutionEditors.ForEach(se => se.SaveData());
			base.SaveData();
		}
		#endregion

		#region Solution Content
		private void ShowActiveEditor(bool showSplash)
		{
			if (SelectedSolutionToggle == null)
				return;
			if (ActiveSolutionEditor == null)
			{
				var newEditor = CreateSolutionEditor(SelectedSolutionToggle.SolutionInfo);
				ConfigureSolutionEditor(newEditor, showSplash);
				SolutionEditors.Add(newEditor);
				newEditor.LoadData();
			}
			var activeEditorControl = (Control)ActiveSolutionEditor;
			if (!pnContent.Controls.Contains(activeEditorControl))
				pnContent.Controls.Add(activeEditorControl);
			ActiveSolutionEditor.ShowEditor();
		}

		protected abstract ISolutionEditor CreateSolutionEditor(BaseSolutionInfo solutionInfo);

		private void ConfigureSolutionEditor(ISolutionEditor editor, bool showSplash)
		{
			editor.InitControl(showSplash);
			editor.DataChanged += OnEditorDataChanged;
			editor.SlideTypeChanged += OnSelectedSlideChanged;
			editor.OutputStatusChanged += OnEditorOutputStatusChanged;
		}

		private void OnEditorDataChanged(object sender, EventArgs e)
		{
			SettingsNotSaved = true;
		}
		#endregion

		#region Solution Toggles
		private void LoadControlPanel()
		{
			SolutionToggles.Clear();
			xtraScrollableControlPageTemplates.Controls.Clear();
			foreach (var solutionInfo in SolutionManager.Solutions)
			{
				var solutionToggle = SolutionToggleHelper.Create(solutionInfo, xtraScrollableControlPageTemplates.Width - (Int32)(SolutionToggleHelper.ButtonPadding * Utilities.GetScaleFactor(CreateGraphics().DpiX).Width) * 2);
				solutionToggle.Click += OnSolutionToggeleClick;
				solutionToggle.CheckedChanged += OnSolutionToggeleCheck;
				solutionToggle.HoverColor = StyleConfiguration.ToggleHoverColor;
				solutionToggle.SelectedColor = StyleConfiguration.ToggleSelectedColor;
				SolutionToggles.Add(solutionToggle);
				xtraScrollableControlPageTemplates.Controls.Add((Control)solutionToggle);
			}
			if (SolutionToggles.Any())
				SolutionToggles.First().Checked = true;
			ResizeControlPanel();
		}

		private void ResizeControlPanel()
		{
			var paddings = (Int32)(SolutionToggleHelper.ButtonPadding * Utilities.GetScaleFactor(CreateGraphics().DpiX).Width);
			var top = paddings;
			var left = paddings;
			var buttonWidth = xtraScrollableControlPageTemplates.Width - (paddings * 2);
			foreach (var solutionToggle in SolutionToggles.OfType<Control>().ToList())
			{
				solutionToggle.Top = top;
				solutionToggle.Left = left;
				solutionToggle.Width = buttonWidth;
				top += solutionToggle.Height + (Int32)(SolutionToggleHelper.ButtonPadding * Utilities.GetScaleFactor(CreateGraphics().DpiX).Width);
			}
		}

		private void OnSolutionToggeleClick(object sender, EventArgs e)
		{
			var solutionToggle = (SolutionImageToggle)sender;
			if (solutionToggle.Checked) return;

			ActiveSolutionEditor?.ApplyChanges();

			SolutionToggles.ForEach(st => st.Checked = false);
			solutionToggle.Checked = true;
		}

		private void OnSolutionToggeleCheck(object sender, EventArgs e)
		{
			if (!_allowToHandleEvents) return;
			var solutionToggle = (ISolutionToggle)sender;
			if (!solutionToggle.Checked) return;
			ShowActiveEditor(true);
		}

		private void OnResize(object sender, EventArgs e)
		{
			ResizeControlPanel();
		}
		#endregion

		#region Output
		protected abstract void LoadThemes(SlideType slideType);

		private void OnSelectedSlideChanged(object sender, SelectedSlideTypeChanged e)
		{
			LoadThemes(e.SlideType);
		}

		private void OnEditorOutputStatusChanged(object sender, OutputStatusChangedEventArgs e)
		{
			ButtonPowerPoint.ShowSubItems = e.MultipleSlidesAllowed;
			ButtonPowerPoint.Enabled =
				ButtonPdf.Enabled =
						ButtonEmail.Enabled = e.IsOutputEnabled;
			((RibbonBar)ButtonPowerPoint.ContainerControl).RecalcLayout();
			PanelSolutions.PerformLayout();
		}

		public override void OutputPowerPoint()
		{
			ActiveSolutionEditor?.OutputPowerPointCurrent();
		}

		public override void OutputPowerPointBeforePopup(PopupOpenEventArgs e)
		{
			e.Cancel = ActiveSolutionEditor == null || !ActiveSolutionEditor.MultipleSlidesAllowed;
		}

		public override void OutputPowerPointAll()
		{
			ActiveSolutionEditor?.OutputPowerPointAll();
		}

		public override void OutputPdf()
		{
			ActiveSolutionEditor?.OutputPdf();
		}

		public override void Email()
		{
			ActiveSolutionEditor?.Email();
		}
		#endregion
	}
}
