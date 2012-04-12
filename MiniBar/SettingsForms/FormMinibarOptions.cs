using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniBar.SettingsForms
{
    public partial class FormMinibarOptions : Form
    {
        private bool _tempOwnControl = false;

        public FormMinibarOptions()
        {
            InitializeComponent();
        }

        private void LoadApplicationsSettings()
        {
            rbAutorunNormal.Checked = ConfigurationClasses.SettingsManager.Instance.AutoRunNormal;
            rbAutorunHidden.Checked = ConfigurationClasses.SettingsManager.Instance.AutoRunHidden;
            rbAutorunFloat.Checked = ConfigurationClasses.SettingsManager.Instance.AutoRunFloat;
            rbAutorunNone.Checked = !ConfigurationClasses.SettingsManager.Instance.AutoRunFloat & !ConfigurationClasses.SettingsManager.Instance.AutoRunNormal & !ConfigurationClasses.SettingsManager.Instance.AutoRunHidden;
            rbHideAll.Checked = ConfigurationClasses.SettingsManager.Instance.HideAll;
            rbHideSpecificProgram.Checked = ConfigurationClasses.SettingsManager.Instance.HideSpecificProgram;
            rbHideSpecificProgramMaximized.Checked = ConfigurationClasses.SettingsManager.Instance.HideSpecificProgramMaximized;
            rbVisiblePowerPoint.Checked = ConfigurationClasses.SettingsManager.Instance.VisiblePowerPoint;
            rbVisiblePowerPointMaximized.Checked = ConfigurationClasses.SettingsManager.Instance.VisiblePowerPointMaximaized;
            rbCloseShutdown.Checked = ConfigurationClasses.SettingsManager.Instance.CloseShutdown;
            rbCloseHide.Checked = ConfigurationClasses.SettingsManager.Instance.CloseHide;
            rbCloseFloat.Checked = ConfigurationClasses.SettingsManager.Instance.CloseFloat;
            memoEditPrograms.Lines = ConfigurationClasses.SettingsManager.Instance.PrimaryApplications.ToArray();
        }

        private void FormMinibarOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                lock (AppManager.Locker)
                {
                    ConfigurationClasses.SettingsManager.Instance.OwnControl = rbControlOwn.Checked;
                    ConfigurationClasses.SettingsManager.Instance.AutoRunNormal = rbAutorunNormal.Checked;
                    ConfigurationClasses.SettingsManager.Instance.AutoRunHidden = rbAutorunHidden.Checked;
                    ConfigurationClasses.SettingsManager.Instance.AutoRunFloat = rbAutorunFloat.Checked;
                    ConfigurationClasses.SettingsManager.Instance.HideAll = rbHideAll.Checked;
                    ConfigurationClasses.SettingsManager.Instance.HideSpecificProgram = rbHideSpecificProgram.Checked;
                    ConfigurationClasses.SettingsManager.Instance.HideSpecificProgramMaximized = rbHideSpecificProgramMaximized.Checked;
                    ConfigurationClasses.SettingsManager.Instance.VisiblePowerPoint = rbVisiblePowerPoint.Checked;
                    ConfigurationClasses.SettingsManager.Instance.VisiblePowerPointMaximaized = rbVisiblePowerPointMaximized.Checked;
                    ConfigurationClasses.SettingsManager.Instance.CloseShutdown = rbCloseShutdown.Checked;
                    ConfigurationClasses.SettingsManager.Instance.CloseHide = rbCloseHide.Checked;
                    ConfigurationClasses.SettingsManager.Instance.CloseFloat = rbCloseFloat.Checked;
                    ConfigurationClasses.SettingsManager.Instance.QuickRetraction = checkEditQuickRetraction.Checked;
                    ConfigurationClasses.SettingsManager.Instance.PrimaryApplications.Clear();
                    foreach (string application in memoEditPrograms.Lines)
                        if (!string.IsNullOrEmpty(application.Trim()))
                            ConfigurationClasses.SettingsManager.Instance.PrimaryApplications.Add(application.Trim());
                    ConfigurationClasses.SettingsManager.Instance.SaveSharedSettings();
                    ConfigurationClasses.SettingsManager.Instance.SaveMinibarSettings();
                }
            }
            else
            {
                ConfigurationClasses.SettingsManager.Instance.OwnControl = _tempOwnControl;
                ConfigurationClasses.SettingsManager.Instance.LoadMinibarApplicationSettings();
            }
        }

        private void FormMinibarOptions_Load(object sender, EventArgs e)
        {
            checkEditQuickRetraction.Checked = ConfigurationClasses.SettingsManager.Instance.QuickRetraction;
            _tempOwnControl = ConfigurationClasses.SettingsManager.Instance.OwnControl;
            rbControlImport.Checked = !ConfigurationClasses.SettingsManager.Instance.OwnControl;
            rbControlOwn.Checked = ConfigurationClasses.SettingsManager.Instance.OwnControl;
        }

        private void rbControl_CheckedChanged(object sender, EventArgs e)
        {
            xtraTabPageAutorun.PageEnabled = rbControlOwn.Checked;
            xtraTabPageVisibility.PageEnabled = rbControlOwn.Checked;
            xtraTabPagePrograms.PageEnabled = rbControlOwn.Checked & (rbHideSpecificProgram.Checked | rbHideSpecificProgramMaximized.Checked);
            xtraTabPageClose.PageEnabled = rbControlOwn.Checked;
            ConfigurationClasses.SettingsManager.Instance.OwnControl = rbControlOwn.Checked;
            ConfigurationClasses.SettingsManager.Instance.LoadMinibarApplicationSettings();
            LoadApplicationsSettings();
        }

        private void rbHide_CheckedChanged(object sender, EventArgs e)
        {
            xtraTabPagePrograms.PageEnabled = rbControlOwn.Checked & (rbHideSpecificProgram.Checked | rbHideSpecificProgramMaximized.Checked);
        }
    }
}
