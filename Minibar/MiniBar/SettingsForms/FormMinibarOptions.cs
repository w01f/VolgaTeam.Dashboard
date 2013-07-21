using System;
using System.Windows.Forms;
using NewBizWiz.MiniBar.BusinessClasses;

namespace NewBizWiz.MiniBar.SettingsForms
{
	public partial class FormMinibarOptions : Form
	{
		private bool _tempOwnControl;

		public FormMinibarOptions()
		{
			InitializeComponent();
		}

		private void LoadApplicationsSettings()
		{
			rbAutorunNormal.Checked = SettingsManager.Instance.AutoRunNormal;
			rbAutorunHidden.Checked = SettingsManager.Instance.AutoRunHidden;
			rbAutorunFloat.Checked = SettingsManager.Instance.AutoRunFloat;
			rbAutorunNone.Checked = !SettingsManager.Instance.AutoRunFloat & !SettingsManager.Instance.AutoRunNormal & !SettingsManager.Instance.AutoRunHidden;
			rbHideAll.Checked = SettingsManager.Instance.HideAll;
			rbHideSpecificProgram.Checked = SettingsManager.Instance.HideSpecificProgram;
			rbHideSpecificProgramMaximized.Checked = SettingsManager.Instance.HideSpecificProgramMaximized;
			rbVisiblePowerPoint.Checked = SettingsManager.Instance.VisiblePowerPoint;
			rbVisiblePowerPointMaximized.Checked = SettingsManager.Instance.VisiblePowerPointMaximaized;
			rbCloseShutdown.Checked = SettingsManager.Instance.CloseShutdown;
			rbCloseHide.Checked = SettingsManager.Instance.CloseHide;
			rbCloseFloat.Checked = SettingsManager.Instance.CloseFloat;
			memoEditPrograms.Lines = SettingsManager.Instance.PrimaryApplications.ToArray();
		}

		private void FormMinibarOptions_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				lock (AppManager.Locker)
				{
					SettingsManager.Instance.OwnControl = rbControlOwn.Checked;
					SettingsManager.Instance.AutoRunNormal = rbAutorunNormal.Checked;
					SettingsManager.Instance.AutoRunHidden = rbAutorunHidden.Checked;
					SettingsManager.Instance.AutoRunFloat = rbAutorunFloat.Checked;
					SettingsManager.Instance.HideAll = rbHideAll.Checked;
					SettingsManager.Instance.HideSpecificProgram = rbHideSpecificProgram.Checked;
					SettingsManager.Instance.HideSpecificProgramMaximized = rbHideSpecificProgramMaximized.Checked;
					SettingsManager.Instance.VisiblePowerPoint = rbVisiblePowerPoint.Checked;
					SettingsManager.Instance.VisiblePowerPointMaximaized = rbVisiblePowerPointMaximized.Checked;
					SettingsManager.Instance.CloseShutdown = rbCloseShutdown.Checked;
					SettingsManager.Instance.CloseHide = rbCloseHide.Checked;
					SettingsManager.Instance.CloseFloat = rbCloseFloat.Checked;
					SettingsManager.Instance.QuickRetraction = checkEditQuickRetraction.Checked;
					SettingsManager.Instance.PrimaryApplications.Clear();
					foreach (string application in memoEditPrograms.Lines)
						if (!string.IsNullOrEmpty(application.Trim()))
							SettingsManager.Instance.PrimaryApplications.Add(application.Trim());
					SettingsManager.Instance.SaveMinibarSettings();
				}
			}
			else
			{
				SettingsManager.Instance.OwnControl = _tempOwnControl;
				SettingsManager.Instance.LoadMinibarApplicationSettings();
			}
		}

		private void FormMinibarOptions_Load(object sender, EventArgs e)
		{
			checkEditQuickRetraction.Checked = SettingsManager.Instance.QuickRetraction;
			_tempOwnControl = SettingsManager.Instance.OwnControl;
			rbControlImport.Checked = !SettingsManager.Instance.OwnControl;
			rbControlOwn.Checked = SettingsManager.Instance.OwnControl;
		}

		private void rbControl_CheckedChanged(object sender, EventArgs e)
		{
			xtraTabPageAutorun.PageEnabled = rbControlOwn.Checked;
			xtraTabPageVisibility.PageEnabled = rbControlOwn.Checked;
			xtraTabPagePrograms.PageEnabled = rbControlOwn.Checked & (rbHideSpecificProgram.Checked | rbHideSpecificProgramMaximized.Checked);
			xtraTabPageClose.PageEnabled = rbControlOwn.Checked;
			SettingsManager.Instance.OwnControl = rbControlOwn.Checked;
			SettingsManager.Instance.LoadMinibarApplicationSettings();
			LoadApplicationsSettings();
		}

		private void rbHide_CheckedChanged(object sender, EventArgs e)
		{
			xtraTabPagePrograms.PageEnabled = rbControlOwn.Checked & (rbHideSpecificProgram.Checked | rbHideSpecificProgramMaximized.Checked);
		}
	}
}