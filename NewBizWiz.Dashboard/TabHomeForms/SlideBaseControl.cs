using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.Dashboard.SettingsManager;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class SlideBaseControl : UserControl
	{
		private readonly Color _selectedButtonColor = Color.FromArgb(255, 218, 150);
		private readonly Color _regularButtonColor = Color.FromArgb(175, 210, 255);

		public virtual string SlideName
		{
			get { return null; }
		}

		public virtual SuperTooltipInfo Tooltip
		{
			get { return null; }
		}

		public virtual ButtonItem ThemeButton
		{
			get { return null; }
		}

		public event EventHandler<SlideEventArgs> SlideChanged;

		protected SlideBaseControl()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				laSlideHeader.Font = new Font(laSlideHeader.Font.FontFamily, laSlideHeader.Font.Size - 2, laSlideHeader.Font.Style);
				buttonXSavedFiles.Font = new Font(buttonXSavedFiles.Font.FontFamily, buttonXSavedFiles.Font.Size - 2, buttonXSavedFiles.Font.Style);
			}
			comboBoxEditSlideHeader.MouseUp += FormMain.Instance.Editor_MouseUp;
			comboBoxEditSlideHeader.MouseDown += FormMain.Instance.Editor_MouseDown;
			comboBoxEditSlideHeader.Enter += FormMain.Instance.Editor_Enter;
		}

		protected void SetLoadState(bool enable)
		{
			buttonXSavedFiles.Enabled = enable;
		}

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
			FormThemeSelector.Link(ThemeButton, themes, SettingsManager.Instance.GetSelectedTheme(slideType).Name, (t =>
			{
				SettingsManager.Instance.SetSelectedTheme(slideType, t.Name);
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
				var selectorToolTip = new SuperTooltipInfo("Slide Theme", "", "Select the PowerPoint Slide theme you want to use for this schedule", null, null, eTooltipColor.Gray);
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeThemeCleanslate, selectorToolTip);
			}
			FormMain.Instance.ribbonBarPowerPoint.RecalcLayout();
		}

		protected virtual void SavedFiles_Click(object sender, EventArgs e)
		{

		}

		public void SelectSlideType(SlideType slideType)
		{
			foreach (var button in pnSlideSelector.Controls.OfType<SimpleButton>())
				button.Appearance.BackColor = _regularButtonColor;
			FormMain.Instance.buttonItemHomeOverview.Checked = false;
			switch (slideType)
			{
				case SlideType.Cleanslate:
					FormMain.Instance.buttonItemHomeOverview.Checked = true;
					break;
				case SlideType.Cover:
					simpleButtonCover.Appearance.BackColor = _selectedButtonColor;
					break;
				case SlideType.LeadoffStatement:
					simpleButtonLeadoff.Appearance.BackColor = _selectedButtonColor;
					break;
				case SlideType.ClientGoals:
					simpleButtonClientGoals.Appearance.BackColor = _selectedButtonColor;
					break;
				case SlideType.TargetCustomers:
					simpleButtonTargetCustomers.Appearance.BackColor = _selectedButtonColor;
					break;
				case SlideType.SimpleSummary:
					simpleButtonSummary.Appearance.BackColor = _selectedButtonColor;
					break;
			}
			LoadThemes(slideType);
		}

		private void SlideType_Click(object sender, EventArgs e)
		{
			if (SlideChanged == null) return;
			var slideType = SlideType.None;
			if (sender == simpleButtonCover)
				slideType = SlideType.Cover;
			else if (sender == simpleButtonLeadoff)
				slideType = SlideType.LeadoffStatement;
			else if (sender == simpleButtonClientGoals)
				slideType = SlideType.ClientGoals;
			else if (sender == simpleButtonTargetCustomers)
				slideType = SlideType.TargetCustomers;
			else if (sender == simpleButtonSummary)
				slideType = SlideType.SimpleSummary;
			SlideChanged(this, new SlideEventArgs { SlideType = slideType });
		}
	}

	public class SlideEventArgs : EventArgs
	{
		public SlideType SlideType { get; set; }
	}
}
