﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.FormStyle;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.SlideSettingsEditors;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Schedules.Common.Controls;

namespace Asa.Media.Single
{
    public class AppManager
    {
        private readonly FloaterManager _floater = new FloaterManager();

        private AppManager() { }

        public static AppManager Instance { get; } = new AppManager();

        public void RunApplication(MediaDataType mediaType)
        {
            bool stopRun = false;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;

            LicenseHelper.Register();

            var textSettings = new StartFormTextConfiguration();
            textSettings.Load(Path.Combine(Common.Core.Configuration.ResourceManager.Instance.AppRootFolderPath, "sync_text.xml"));

            MediaMetaData.Instance.Init(mediaType);
            var appTitle = String.Format("SellerPoint for {0}", MediaMetaData.Instance.DataTypeString);

            if (BusinessObjects.Instance.PowerPointManager.IsPowerPointMultipleInstances())
            {
                using (var form = new FormPowerPointSeveralInstancesWarning())
                {
                    form.Text = appTitle;
                    if (form.ShowDialog() != DialogResult.OK)
                        return;
                }
            }

            PopupMessageHelper.Instance.Title = appTitle;
            AppProfileManager.Instance.InitApplication(MediaMetaData.Instance.AppType);

            FileStorageManager.Instance.UsingLocalMode += (o, e) =>
            {
                if (FileStorageManager.Instance.UseLocalMode) return;
                FormStart.CloseProgress();
                if (FileStorageManager.Instance.DataState != DataActualityState.Updated)
                {
                    PopupMessageHelper.Instance.ShowWarning("Server is not available. Application will be closed");
                    stopRun = true;
                    Application.Exit();
                    return;
                }
                if (PopupMessageHelper.Instance.ShowWarningQuestion("Server is not available. Do you want to continue to work in local mode?", "adSALESapps.com ") != DialogResult.Yes)
                {
                    stopRun = true;
                    Application.Exit();
                }
            };

            FileStorageManager.Instance.Authorizing += (o, e) =>
            {
                var authManager = new AuthManager();
                FormStart.SetTitle(textSettings.VersionText ?? "Checking credentials...");
                e.LightCheck = true;
                authManager.Auth(e);
            };

            FormStart.ShowProgress();
            FormStart.SetTitle(textSettings.ConnectText ?? "Connecting to adSALEScloud…");
            var thread = new Thread(() =>
            {
                AsyncHelper.RunSync(() => FileStorageManager.Instance.InitStorage());
                if (FileStorageManager.Instance.Activated)
                    AsyncHelper.RunSync(() => AppProfileManager.Instance.LoadProfile());
            });
            thread.Start();
            while (thread.IsAlive)
                Application.DoEvents();

            if (stopRun) return;

            if (FileStorageManager.Instance.Activated)
            {
                if (FileStorageManager.Instance.SubStorages.Any() &&
                    String.IsNullOrEmpty(AppProfileManager.Instance.SubStorageName))
                {
                    if (FileStorageManager.Instance.SubStorages.Count > 1)
                    {
                        FormStart.CloseProgress();

                        using (var form = new FormMediaSettings())
                        {
                            form.comboBoxEditMedia.Properties.Items.AddRange(FileStorageManager.Instance.SubStorages);
                            form.comboBoxEditMedia.EditValue = FileStorageManager.Instance.SubStorages.FirstOrDefault();
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                AppProfileManager.Instance.SubStorageName = form.comboBoxEditMedia.EditValue as string;
                                AppProfileManager.Instance.SaveProfile();
                                FormStart.ShowProgress();
                            }
                            else
                            {
                                FormStart.Destroy();
                                return;
                            }
                        }
                    }
                    else
                    {
                        AppProfileManager.Instance.SubStorageName = FileStorageManager.Instance.SubStorages.FirstOrDefault();
                        AppProfileManager.Instance.SaveProfile();
                    }
                }

                thread = new Thread(() =>
                    {
                        AsyncHelper.RunSync(() => FileStorageManager.Instance.CheckDataSate());
                    });
                thread.Start();
                while (thread.IsAlive)
                    Application.DoEvents();

                FileStorageManager.Instance.Downloading += OnFileDownloading;
                FileStorageManager.Instance.Extracting += OnFileExtracting;

                if (FileStorageManager.Instance.DataState == DataActualityState.NotExisted)
                    FormStart.SetTitle(textSettings.InitialLoadingText ?? "Syncing adSALEScloud for the 1st time…");
                else if (FileStorageManager.Instance.DataState == DataActualityState.Outdated)
                    FormStart.SetTitle(textSettings.RefreshText ?? "Refreshing data from adSALEScloud…");
                else
                    FormStart.SetTitle("Loading application data...");

                thread = new Thread(() =>
                {
                    AsyncHelper.RunSync(() => Business.Media.Configuration.ResourceManager.Instance.LoadSubStorageIndependentResources());

                    Controller.Instance.InitBusinessObjects();
                    AsyncHelper.RunSync(FileStorageManager.Instance.FixCommonDataState);
                });
                thread.Start();
                while (thread.IsAlive)
                    Application.DoEvents();

                FormStart.SetTitle(textSettings.LaunchText ?? "Launching SellerPoint...");
                Application.DoEvents();

                FormMain.Instance.Init();

                Controller.Instance.InitForm();

                FileStorageManager.Instance.Downloading -= OnFileDownloading;
                FileStorageManager.Instance.Extracting -= OnFileExtracting;
            }

            FormStart.CloseProgress();
            FormStart.Destroy();

            if (FileStorageManager.Instance.Activated)
            {
                if (BusinessObjects.Instance.PowerPointManager.SettingsSource == SettingsSourceEnum.PowerPoint &&
                MasterWizardManager.Instance.SelectedWizard != null &&
                !MasterWizardManager.Instance.SelectedWizard.HasSlideConfiguration(SlideSettingsManager.Instance.SlideSettings))
                {
                    var availableMasterWizards = MasterWizardManager.Instance.MasterWizards.Values.Where(w => w.HasSlideConfiguration(SlideSettingsManager.Instance.SlideSettings)).ToList();
                    if (availableMasterWizards.Any())
                    {
                        using (var form = new FormSelectMasterWizard())
                        {
                            form.comboBoxEditSlideFormat.Properties.Items.AddRange(availableMasterWizards);
                            form.comboBoxEditSlideFormat.EditValue = availableMasterWizards.FirstOrDefault();
                            if (form.ShowDialog() != DialogResult.OK)
                                return;
                            SettingsManager.Instance.SelectedWizard = ((MasterWizard)form.comboBoxEditSlideFormat.EditValue).Name;
                            MasterWizardManager.Instance.SetMasterWizard();
                        }
                    }
                    else
                    {
                        PopupMessageHelper.Instance.ShowWarning("You already have a PowerPoint file opened that is not compatible with Sales Ninja.\nPlease close that presentation, and open Sales Ninja again.");
                        return;
                    }
                }
                Application.Run(FormMain.Instance);
            }
            else
                PopupMessageHelper.Instance.ShowWarning("This app is not activated. Contact adSALESapps Support (help@adSALESapps.com)");
        }

        private void OnFileDownloading(Object sender, FileProcessingProgressEventArgs e)
        {
            FormStart.SetDetails(e.ProgressPercent < 100 ?
                String.Format("Loading {0} - {1}%", e.FileName, e.ProgressPercent) :
                String.Empty);
        }

        private void OnFileExtracting(Object sender, FileProcessingProgressEventArgs e)
        {
            FormStart.SetDetails(e.ProgressPercent < 100 ?
                String.Format("Extracting {0} - {1}%", e.FileName, e.ProgressPercent) :
                String.Empty);
        }

        public void ActivateMainForm(bool maximize = true)
        {
            var processList = Process.GetProcesses();
            foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains(String.Format("{0}seller", MediaMetaData.Instance.DataTypeString.ToLower()))))
            {
                if (process.MainWindowHandle.ToInt32() != 0)
                {
                    Utilities.ActivateForm(process.MainWindowHandle, maximize, false);
                    break;
                }
            }
        }

        public void ShowFloater(IFloaterSupportedForm sender, FloaterRequestedEventArgs e)
        {
            var afterBack = new Action<bool>(ActivateMainForm);
            _floater.ShowFloater(sender ?? FormMain.Instance, null, e.Logo, e.AfterShow, null, afterBack);
        }

        public void RestartApp()
        {
            Process.Start(Application.ExecutablePath, "force");
            Application.Exit();
        }
    }
}