﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Dashboard.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Themes;
using DevComponents.DotNetBar;
using Asa.Dashboard.ToolForms;

namespace Asa.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class SlideBaseControl : UserControl
	{
		public bool SettingsNotSaved { get; set; }

		public virtual string SlideName
		{
			get { return null; }
		}

		public virtual SuperTooltipInfo TooltipLoad
		{
			get { return null; }
		}

		public virtual SuperTooltipInfo TooltipHelp
		{
			get { return null; }
		}

		public virtual ButtonItem ThemeButton
		{
			get { return null; }
		}

		public ButtonItem LoadButton
		{
			get { return FormMain.Instance.buttonItemHomeLoad; }
		}

		public event EventHandler<SlideEventArgs> SlideChanged;

		protected SlideBaseControl()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96) { }
			comboBoxEditSlideHeader.MouseUp += TextEditorsHelper.Editor_MouseUp;
			comboBoxEditSlideHeader.MouseDown += TextEditorsHelper.Editor_MouseDown;
			comboBoxEditSlideHeader.Enter += TextEditorsHelper.Editor_Enter;
		}

		protected void SetLoadState(bool enable)
		{
			LoadButton.Enabled = enable;
		}

		protected virtual void UpdateSavedFilesState() { }

		protected void SetOutputState(bool enable)
		{
			FormMain.Instance.buttonItemPowerPoint.Enabled = enable;
			FormMain.Instance.buttonItemPreview.Enabled = enable;
		}

		protected void LoadThemes(SlideType slideType)
		{
			var themes = SettingsManager.Instance.ThemeManager.GetThemes(slideType);
			FormMain.Instance.HideThemeButtons();
			ThemeButton.Visible = true;
			FormThemeSelector.Link(ThemeButton, themes, (SettingsManager.Instance.GetSelectedTheme(slideType) ?? new Theme(null)).Name, (t =>
			{
				if (SettingsManager.Instance.ThemeManager.GetThemes(SlideType.Cleanslate).Any(slideTheme => slideTheme.Name == t.Name))
					SettingsManager.Instance.SetSelectedTheme(SlideType.Cleanslate, t.Name);
				if (SettingsManager.Instance.ThemeManager.GetThemes(SlideType.Cover).Any(slideTheme => slideTheme.Name == t.Name))
					SettingsManager.Instance.SetSelectedTheme(SlideType.Cover, t.Name);
				if (SettingsManager.Instance.ThemeManager.GetThemes(SlideType.LeadoffStatement).Any(slideTheme => slideTheme.Name == t.Name))
					SettingsManager.Instance.SetSelectedTheme(SlideType.LeadoffStatement, t.Name);
				if (SettingsManager.Instance.ThemeManager.GetThemes(SlideType.ClientGoals).Any(slideTheme => slideTheme.Name == t.Name))
					SettingsManager.Instance.SetSelectedTheme(SlideType.ClientGoals, t.Name);
				if (SettingsManager.Instance.ThemeManager.GetThemes(SlideType.TargetCustomers).Any(slideTheme => slideTheme.Name == t.Name))
					SettingsManager.Instance.SetSelectedTheme(SlideType.TargetCustomers, t.Name);
				if (SettingsManager.Instance.ThemeManager.GetThemes(SlideType.SimpleSummary).Any(slideTheme => slideTheme.Name == t.Name))
					SettingsManager.Instance.SetSelectedTheme(SlideType.SimpleSummary, t.Name);
				SettingsManager.Instance.SaveDashboardSettings();
			}));
			if (!themes.Any())
			{
				var selectorToolTip = new SuperTooltipInfo("Important Info", "", "Click to get more info why output is disabled", null, null, eTooltipColor.Gray);
				FormMain.Instance.buttonItemPowerPoint.Visible = false;
				FormMain.Instance.ribbonBarPowerPoint.Text = "Important Info";
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeThemeCleanslate, selectorToolTip);
			}
			else
			{
				FormMain.Instance.buttonItemPowerPoint.Visible = true;
				var selectorToolTip = new SuperTooltipInfo("Slide Theme", "", "Select the PowerPoint Slide theme you want to use for this schedule", null, null, eTooltipColor.Gray);
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeThemeCleanslate, selectorToolTip);
			}
			FormMain.Instance.ribbonBarPowerPoint.RecalcLayout();
			FormMain.Instance.ribbonPanelHome.PerformLayout();
		}

		protected virtual void SaveChanges(string fileName = "") { }

		public virtual void LoadClick()
		{
			UpdateSavedFilesState();
		}

		public virtual void SelectSlideType(SlideType slideType)
		{
			foreach (var button in pnSlideSelector.Controls.OfType<ButtonX>())
				button.Checked = false;
			FormMain.Instance.buttonItemHomeOverview.Checked = false;
			switch (slideType)
			{
				case SlideType.Cleanslate:
					FormMain.Instance.buttonItemHomeOverview.Checked = true;
					break;
				case SlideType.Cover:
					buttonXCover.Checked = true;
					break;
				case SlideType.LeadoffStatement:
					buttonXLeadoff.Checked = true;
					break;
				case SlideType.ClientGoals:
					buttonXClientGoals.Checked = true;
					break;
				case SlideType.TargetCustomers:
					buttonXTargetCustomers.Checked = true;
					break;
				case SlideType.SimpleSummary:
					buttonXSummary.Checked = true;
					break;
			}
			LoadThemes(slideType);
			UpdateSavedFilesState();
		}

		private void SlideType_Click(object sender, EventArgs e)
		{
			if (SlideChanged == null) return;
			var slideType = SlideType.None;
			if (sender == buttonXCover)
				slideType = SlideType.Cover;
			else if (sender == buttonXLeadoff)
				slideType = SlideType.LeadoffStatement;
			else if (sender == buttonXClientGoals)
				slideType = SlideType.ClientGoals;
			else if (sender == buttonXTargetCustomers)
				slideType = SlideType.TargetCustomers;
			else if (sender == buttonXSummary)
				slideType = SlideType.SimpleSummary;
			SlideChanged(this, new SlideEventArgs { SlideType = slideType });
		}

		private void SaveTemplate_Click(object sender, EventArgs e)
		{
			using (var form = new FormSaveTemplate())
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!String.IsNullOrEmpty(form.TemplateName))
				{
					SettingsNotSaved = true;
					SaveChanges(form.TemplateName);
					UpdateSavedFilesState();
					PopupMessageHelper.Instance.ShowInformation("Template saved");
				}
				else
					PopupMessageHelper.Instance.ShowWarning("Template Name can't be empty");
			}
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (Control)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (Control)(sender);
			pic.Top -= 1;
		}
		#endregion
	}

	public class SlideEventArgs : EventArgs
	{
		public SlideType SlideType { get; set; }
	}
}