using System;
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

		public virtual string SlideName => null;

		public virtual SuperTooltipInfo TooltipLoad => null;

		public virtual SuperTooltipInfo TooltipHelp => null;

		public virtual ButtonItem ThemeButton => null;

		public ButtonItem LoadButton => FormMain.Instance.buttonItemHomeLoad;

		public event EventHandler<SlideEventArgs> SlideChanged;

		protected SlideBaseControl()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96) { }
			comboBoxEditSlideHeader.EnableSelectAll();
		}

		protected void SetLoadState(bool enable)
		{
			LoadButton.Enabled = enable;
		}

		protected virtual void UpdateSavedFilesState() { }

		protected void SetOutputState(bool enable)
		{
			FormMain.Instance.buttonItemPowerPoint.Enabled = enable;
		}

		protected void LoadThemes(SlideType slideType)
		{
			var themes = SettingsManager.Instance.ThemeManager.GetThemes(slideType);
			FormMain.Instance.HideThemeButtons();
			ThemeButton.Visible = true;
			FormThemeSelector.Link(
				ThemeButton,
				FormMain.Instance,
				themes,
				SettingsManager.Instance.GetSelectedTheme(slideType)?.Name,
				SettingsManager.Instance,
				(theme, applyForAllSlideTypes) =>
				{
					SettingsManager.Instance.SetSelectedTheme(slideType, theme.Name, applyForAllSlideTypes);
					SettingsManager.Instance.SaveSettings();
				}
			);
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
	}

	public class SlideEventArgs : EventArgs
	{
		public SlideType SlideType { get; set; }
	}
}
