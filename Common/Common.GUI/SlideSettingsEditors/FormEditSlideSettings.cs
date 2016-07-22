﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;

namespace Asa.Common.GUI.SlideSettingsEditors
{
	public partial class FormEditSlideSettings : MetroForm
	{
		public FormEditSlideSettings()
		{
			InitializeComponent();
		}

		private void FormEditSlideSettings_Load(object sender, System.EventArgs e)
		{
			buttonXSize43.Tag = SlideSettings.ReadFromString("4x3");
			buttonXSize169.Tag = SlideSettings.ReadFromString("16x9");
			buttonXSize34.Tag = SlideSettings.ReadFromString("3x4");

			var availableWizards = MasterWizardManager.Instance.MasterWizards.Values.Where(w => SlideSettings.GetAvailableConfigurations().Any(w.HasSlideConfiguration)).ToList();
			comboBoxEditSlideFormat.Properties.Items.AddRange(availableWizards);
			comboBoxEditSlideFormat.EditValue = availableWizards.FirstOrDefault(w => w.Name == SettingsManager.Instance.SelectedWizard);

			var buttons = GetSizeButtons().ToList();
			buttons.ForEach(b => b.Checked = false);
			buttons.First(b => ((SlideSettings)b.Tag).IsEqual(PowerPointManager.Instance.SlideSettings)).Checked = true;
		}

		private IEnumerable<ButtonX> GetSizeButtons()
		{
			return new[]
			{
				buttonXSize43,
				buttonXSize169,
				buttonXSize34
			};
		}

		private void comboBoxEditSlideFormat_EditValueChanged(object sender, System.EventArgs e)
		{
			var selectedWizard = (MasterWizard)comboBoxEditSlideFormat.EditValue;
			var buttons = GetSizeButtons().ToList();
			buttons.ForEach(b => b.Checked = false);
			foreach (var button in buttons)
			{
				button.Checked = false;
				button.Enabled = selectedWizard.HasSlideConfiguration((SlideSettings)button.Tag);
			}
			buttons.First(b => b.Enabled).Checked = true;
		}

		private void buttonXSize_Click(object sender, System.EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			var buttons = GetSizeButtons().ToList();
			buttons.ForEach(b => b.Checked = false);
			button.Checked = true;
		}

		private void FormEditSlideSettings_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;

			SettingsManager.Instance.SelectedWizard = ((MasterWizard)comboBoxEditSlideFormat.EditValue).Name;
			SettingsManager.Instance.SaveSharedSettings();
			MasterWizardManager.Instance.SetMasterWizard();

			var buttons = GetSizeButtons().ToList();
			PowerPointManager.Instance.ApplySettings((SlideSettings)buttons.First(b => b.Checked).Tag);
		}

		private void FormEditSlideSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Do you want to change current PowerPoint slide settings?") != DialogResult.Yes)
				e.Cancel = true;
		}
	}
}
