using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Helpers;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Solutions.Common.Common;
using DevComponents.DotNetBar;

namespace Asa.Solutions.Common.PresentationClasses
{
	//public partial class BaseSolutionContainerControl : UserControl
	public abstract partial class BaseSolutionContainerControl<TChangeInfo> : BaseContentOutputControl<TChangeInfo> where TChangeInfo : BaseScheduleChangeInfo
	{
		private bool _allowToHandleEvents;
		protected abstract SolutionsManager SolutionManager { get; }
		protected List<SolutionToggle> SolutionToggles { get; }
		protected SolutionToggle SelectedSolutionToggle => SolutionToggles.FirstOrDefault(st => st.Checked);
		protected List<ISolutionEditor> SolutionEditors { get; }
		protected ISolutionEditor ActiveSolutionEditor => SolutionEditors.FirstOrDefault(e => e.SolutionType == SelectedSolutionToggle?.SolutionInfo.Type);

		public abstract RibbonPanel PanelSolutions { get; }
		public abstract RibbonBar BarHome { get; }
		public abstract LabelItem LabelHome { get; }
		public abstract ButtonItem ButtonPowerPoint { get; }
		public abstract ButtonItem ButtonPdf { get; }
		public abstract ButtonItem ButtonPreview { get; }
		public abstract ButtonItem ButtonEmail { get; }

		protected BaseSolutionContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			SolutionToggles = new List<SolutionToggle>();
			SolutionEditors = new List<ISolutionEditor>();
			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
			}
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
			ShowActiveEditor();
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
		private void ShowActiveEditor()
		{
			if (SelectedSolutionToggle == null)
				return;
			if (ActiveSolutionEditor == null)
			{
				var newEditor = CreateSolutionEditor(SelectedSolutionToggle.SolutionInfo);
				ConfigureSolutionEditor(newEditor);
				SolutionEditors.Add(newEditor);
				newEditor.LoadData();
			}
			var activeEditorControl = (Control)ActiveSolutionEditor;
			if (!splitContainerControl.Panel1.Controls.Contains(activeEditorControl))
				splitContainerControl.Panel1.Controls.Add(activeEditorControl);
			ActiveSolutionEditor.ShowEditor();
			UpdateHomeButton();
		}

		protected abstract ISolutionEditor CreateSolutionEditor(BaseSolutionInfo solutionInfo);

		private void ConfigureSolutionEditor(ISolutionEditor editor)
		{
			editor.InitControl();
			editor.DataChanged += OnEditorDataChanged;
			editor.SlideTypeChanged += OnSelectedSlideChanged;
			editor.OutputStatusChanged += OnEditorOutputStatusChanged;
		}

		private void UpdateHomeButton()
		{
			BarHome.Text = ActiveSolutionEditor?.HomeText;
			LabelHome.Image = ActiveSolutionEditor?.HomeLogo;
			BarHome.RecalcLayout();
			PanelSolutions.PerformLayout();
		}

		private void OnEditorDataChanged(Object sender, EventArgs e)
		{
			SettingsNotSaved = true;
		}
		#endregion

		#region Solution Toggles
		private void LoadControlPanel()
		{
			SolutionToggles.Clear();
			xtraTabPageTemplates.Controls.Clear();
			foreach (var solutionInfo in SolutionManager.Solutions)
			{
				var solutionToggle = SolutionToggle.Create(solutionInfo);
				solutionToggle.Click += OnSolutionToggeleClick;
				solutionToggle.CheckedChanged += OnSolutionToggeleCheck;
				SolutionToggles.Add(solutionToggle);
				xtraTabPageTemplates.Controls.Add(solutionToggle);
			}
			if (SolutionToggles.Any())
				SolutionToggles.First().Checked = true;
			ResizeControlPanel();
		}

		private void ResizeControlPanel()
		{
			var paddings = SolutionToggle.ButtonPadding;
			var top = paddings;
			var left = paddings;
			var buttonWidth = xtraTabPageTemplates.Width - (paddings * 2);
			foreach (var solutionToggle in SolutionToggles)
			{
				solutionToggle.Top = top;
				solutionToggle.Left = left;
				solutionToggle.Width = buttonWidth;
				top += (SolutionToggle.ButtonHeight + SolutionToggle.ButtonPadding);
			}
		}

		private void OnSolutionToggeleClick(object sender, EventArgs e)
		{
			var solutionToggle = (SolutionToggle)sender;
			if (solutionToggle.Checked) return;

			ActiveSolutionEditor?.ApplyChanges();

			SolutionToggles.ForEach(st => st.Checked = false);
			solutionToggle.Checked = true;
		}

		private void OnSolutionToggeleCheck(object sender, EventArgs e)
		{
			if (!_allowToHandleEvents) return;
			var solutionToggle = (SolutionToggle)sender;
			if (!solutionToggle.Checked) return;
			ShowActiveEditor();
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

		private void OnEditorOutputStatusChanged(Object sender, OutputStatusChangedEventArgs e)
		{
			ButtonPowerPoint.Enabled =
				ButtonPdf.Enabled =
					ButtonPreview.Enabled =
						ButtonEmail.Enabled = e.IsOutputEnabled;
		}

		public override void OutputPowerPoint()
		{
			//ActiveSolutionEditor?.OutputPowerPoint();
		}

		public override void OutputPdf()
		{
			//ActiveSolutionEditor?.OutputPdf();
		}

		public override void Preview()
		{
			//ActiveSolutionEditor?.Preview();
		}

		public override void Email()
		{
			//ActiveSolutionEditor?.Email();
		}
		#endregion
	}
}
