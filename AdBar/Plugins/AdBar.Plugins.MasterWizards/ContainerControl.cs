using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AdBar.Plugins.Common.BusinessClasses;
using AdBar.Plugins.Core;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.Core.Common;
using NewBizWiz.MiniBar.InteropClasses;

namespace AdBar.Plugins.MasterWizards
{
	public partial class ContainerControl : UserControl, IAdBarControl
	{
		private readonly PluginController _controller = new PluginController();
		private bool _allowToSave;

		public ContainerControl()
		{
			InitializeComponent();
			UpdateControlInternal();
		}

		private void UpdateControlInternal()
		{
			_allowToSave = false;
			SetPresentationSettings();
			comboBoxEditStyle.Properties.Items.Clear();
			foreach (var masterWizard in MasterWizardManager.Instance.MasterWizards.Keys)
				comboBoxEditStyle.Properties.Items.Add(masterWizard);

			int selectedIndex = comboBoxEditStyle.Properties.Items.IndexOf(SettingsManager.Instance.SelectedWizard);
			if (selectedIndex < 0)
				selectedIndex = 0;

			if (comboBoxEditStyle.Properties.Items.Count > 0)
				comboBoxEditStyle.SelectedIndex = selectedIndex;

			_allowToSave = true;
		}

		private void SetPresentationSettings()
		{
			if (SettingsManager.Instance.Orientation.Equals("Landscape"))
			{
				if (SettingsManager.Instance.SizeWidth == 10 && SettingsManager.Instance.SizeHeght == 7.5)
				{
					buttonItemSize1.Checked = true;
					buttonItemSize2.Checked = false;
					buttonItemSize3.Checked = false;
					buttonItemSize4.Checked = false;
					buttonItemSize5.Checked = false;
				}
				else if (SettingsManager.Instance.SizeWidth == 10.75 && SettingsManager.Instance.SizeHeght == 8.25)
				{
					buttonItemSize1.Checked = false;
					buttonItemSize2.Checked = true;
					buttonItemSize3.Checked = false;
					buttonItemSize4.Checked = false;
					buttonItemSize5.Checked = false;
				}
				else if (SettingsManager.Instance.SizeWidth == 10 && SettingsManager.Instance.SizeHeght == 5.63)
				{
					buttonItemSize1.Checked = false;
					buttonItemSize2.Checked = false;
					buttonItemSize3.Checked = true;
					buttonItemSize4.Checked = false;
					buttonItemSize5.Checked = false;
				}
			}
			else
			{
				if (SettingsManager.Instance.SizeWidth == 10 && SettingsManager.Instance.SizeHeght == 7.5)
				{
					buttonItemSize1.Checked = false;
					buttonItemSize2.Checked = false;
					buttonItemSize3.Checked = false;
					buttonItemSize4.Checked = true;
					buttonItemSize5.Checked = false;
				}
				else if (SettingsManager.Instance.SizeWidth == 10.75 && SettingsManager.Instance.SizeHeght == 8.25)
				{
					buttonItemSize1.Checked = false;
					buttonItemSize2.Checked = false;
					buttonItemSize3.Checked = false;
					buttonItemSize4.Checked = false;
					buttonItemSize5.Checked = true;
				}
			}
		}

		private void SelectMasterWizard(string name)
		{
			MasterWizard masterWizard;
			MasterWizardManager.Instance.MasterWizards.TryGetValue(name, out masterWizard);
			MasterWizardManager.Instance.SelectedWizard = masterWizard;
			if (MasterWizardManager.Instance.SelectedWizard == null) return;
			SettingsManager.Instance.SelectedWizard = masterWizard.Name;
			UpdateSlideSize();
			SettingsManager.Instance.SaveSharedSettings();
		}

		private void UpdateSlideSize()
		{
			buttonItemSize1.Enabled = MasterWizardManager.Instance.SelectedWizard.Has43;
			if (buttonItemSize1.Checked && !buttonItemSize1.Enabled)
				buttonItemSize1.Checked = false;
			buttonItemSize2.Enabled = MasterWizardManager.Instance.SelectedWizard.Has54;
			if (buttonItemSize2.Checked && !buttonItemSize2.Enabled)
				buttonItemSize2.Checked = false;
			buttonItemSize3.Enabled = MasterWizardManager.Instance.SelectedWizard.Has169;
			if (buttonItemSize3.Checked && !buttonItemSize3.Enabled)
				buttonItemSize3.Checked = false;
			buttonItemSize4.Enabled = MasterWizardManager.Instance.SelectedWizard.Has34;
			if (buttonItemSize4.Checked && !buttonItemSize4.Enabled)
				buttonItemSize4.Checked = false;
			buttonItemSize5.Enabled = MasterWizardManager.Instance.SelectedWizard.Has45;
			if (buttonItemSize5.Checked && !buttonItemSize5.Enabled)
				buttonItemSize5.Checked = false;

			var oldAllowToSave = _allowToSave;
			_allowToSave = true;
			if (buttonItemSize1.Checked || buttonItemSize2.Checked || buttonItemSize3.Checked || buttonItemSize4.Checked || buttonItemSize5.Checked) return;
			if (buttonItemSize1.Enabled)
				buttonItemSize1.Checked = true;
			else if (buttonItemSize2.Enabled)
				buttonItemSize2.Checked = true;
			else if (buttonItemSize3.Enabled)
				buttonItemSize3.Checked = true;
			else if (buttonItemSize4.Enabled)
				buttonItemSize4.Checked = true;
			else if (buttonItemSize5.Enabled)
				buttonItemSize5.Checked = true;
			_allowToSave = oldAllowToSave;
		}

		private void buttonItemSize_Click(object sender, EventArgs e)
		{
			if (_controller.AplicationDetected())
			{
				if (Utilities.Instance.ShowWarningQuestion("All active applications will be closed before you change PowerPoint settings.\nDo you want to continue?") == DialogResult.Yes)
					_controller.CloseActiveApplications();
				else
					return;
			}
			if (AdBarPowerPointHelper.Instance.PowerPointDetected())
			{
				using (var form = new FormFormatChangeNotification())
				{
					var buttonPressed = (sender as ButtonItem);
					string currentFormatText = SettingsManager.Instance.SlideSize;
					string futureFormatText = string.Empty;
					if (buttonPressed == buttonItemSize1)
						futureFormatText = "Landscape 4 x 3";
					else if (buttonPressed == buttonItemSize2)
						futureFormatText = "Landscape 5 x 4";
					else if (buttonPressed == buttonItemSize3)
						futureFormatText = "Landscape 16 x 9";
					else if (buttonPressed == buttonItemSize4)
						futureFormatText = "Portrait 3 x 4";
					else if (buttonPressed == buttonItemSize5)
						futureFormatText = "Portrait 4 x 5";
					form.labelControlCurrentState.Text = "Your curent presentation is: " + currentFormatText;
					form.labelControlFutureState.Text = "You want to change your presentation to: " + futureFormatText;
					if (form.ShowDialog() != DialogResult.Yes)
						return;
				}
			}
			buttonItemSize1.Checked = false;
			buttonItemSize2.Checked = false;
			buttonItemSize3.Checked = false;
			buttonItemSize4.Checked = false;
			buttonItemSize5.Checked = false;
			(sender as ButtonItem).Checked = true;
			SettingsManager.Instance.SaveSharedSettings();
			AdBarPowerPointHelper.Instance.Disconnect();
			AdBarPowerPointHelper.Instance.Connect(false);
			AdBarPowerPointHelper.Instance.SetPresentationSettings();
		}

		private void buttonItemSize_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave || !(sender as ButtonItem).Checked) return;
			if (buttonItemSize1.Checked)
			{
				SettingsManager.Instance.SizeWidth = 10;
				SettingsManager.Instance.SizeHeght = 7.5;
				SettingsManager.Instance.Orientation = "Landscape";
			}
			else if (buttonItemSize2.Checked)
			{
				SettingsManager.Instance.SizeWidth = 10.75;
				SettingsManager.Instance.SizeHeght = 8.25;
				SettingsManager.Instance.Orientation = "Landscape";
			}
			else if (buttonItemSize3.Checked)
			{
				SettingsManager.Instance.SizeWidth = 13;
				SettingsManager.Instance.SizeHeght = 7.32;
				SettingsManager.Instance.Orientation = "Landscape";
			}
			else if (buttonItemSize4.Checked)
			{
				SettingsManager.Instance.SizeWidth = 7.5;
				SettingsManager.Instance.SizeHeght = 10;
				SettingsManager.Instance.Orientation = "Portrait";
			}
			else if (buttonItemSize5.Checked)
			{
				SettingsManager.Instance.SizeWidth = 8.25;
				SettingsManager.Instance.SizeHeght = 10.75;
				SettingsManager.Instance.Orientation = "Portrait";
			}
			if (StateChanged != null)
				StateChanged(this, new AdBarControlStateEventArgs());
		}

		private void comboBoxEditStyle_EditValueChanging(object sender, ChangingEventArgs e)
		{
			if (!_allowToSave) return;
			e.Cancel = false;
			if (_controller.AplicationDetected())
			{
				if (Utilities.Instance.ShowWarningQuestion("All active applications will be closed before you change PowerPoint settings.\nDo you want to continue?") == DialogResult.Yes)
				{
					_controller.CloseActiveApplications();
					e.Cancel = false;
				}
				else
				{
					e.Cancel = true;
					return;
				}
			}
			if (!AdBarPowerPointHelper.Instance.PowerPointDetected()) return;
			using (var form = new FormFormatChangeNotification())
			{
				var currentFormatText = SettingsManager.Instance.SelectedWizard;
				var futureFormatText = e.NewValue != null ? e.NewValue.ToString() : string.Empty;
				form.labelControlCurrentState.Text = "Your curent wizard is: " + currentFormatText;
				form.labelControlFutureState.Text = "You want to change your wizard to: " + futureFormatText;
				if (form.ShowDialog() == DialogResult.Yes)
					e.Cancel = false;
				else
					e.Cancel = true;
			}
		}

		private void comboBoxEditStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxEditStyle.SelectedIndex < 0) return;
			SelectMasterWizard(comboBoxEditStyle.EditValue.ToString());
			if (_allowToSave && StateChanged != null)
				StateChanged(this, new AdBarControlStateEventArgs());
		}

		#region IAdBarControl Memebers

		public string ControlName
		{
			get { return "slidepack"; }
		}

		public IEnumerable<RibbonBar> RibbonBars
		{
			get { return new[] { ribbonBar }; }
		}

		public event EventHandler<AdBarControlStateEventArgs> StateChanged;

		public void UpdateControl(IAdBarControl raisedBy, object[] stateParameters)
		{
		}

		#endregion
	}
}