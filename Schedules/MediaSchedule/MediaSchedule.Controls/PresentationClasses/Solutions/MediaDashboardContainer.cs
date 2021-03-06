﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Solutions;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Dashboard.Configuration;
using Asa.Business.Solutions.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.FormStyle;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.ToolForms;
using Asa.Common.Resources.Solutions;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Solutions.Dashboard.PresentationClasses.ContentEditors;
using DevExpress.XtraPrinting.Native;

namespace Asa.Media.Controls.PresentationClasses.Solutions
{
	class MediaDashboardContainer : BaseDashboardContainer
	{
		public override PowerPointProcessor PowerPointProcessor => BusinessObjects.Instance.PowerPointManager.Processor;

		public MediaDashboardContainer(BaseSolutionInfo solutionInfo) : base(solutionInfo) { }

		public override IDashboardSettingsContainer SettingsContainer => MediaMetaData.Instance.SettingsManager;
		public override Form MainForm => Controller.Instance.FormMain;
		public override Color? AccentColor => BusinessObjects.Instance.FormStyleManager.Style.AccentColor;
		public override MainFormStyleConfiguration StyleConfiguration => BusinessObjects.Instance.FormStyleManager.Style;
		public override ISolutionsResourceManager ResourceManager => BusinessObjects.Instance.ImageResourcesManager as ISolutionsResourceManager;

		public override void LoadData()
		{
			EditedContent?.Dispose();
			EditedContent = BusinessObjects.Instance.ScheduleManager.ActiveSchedule
				.GetScheduleSolutionContent<MediaDashboardContent>(SolutionInfo)
				.Clone<MediaDashboardContent, DashboardContent>();
			base.LoadData();
		}

		public override void SaveData()
		{
			BusinessObjects.Instance.ScheduleManager.ActiveSchedule
				.ApplyScheduleSolutionContent(SolutionInfo, EditedContent.Clone<MediaDashboardContent, DashboardContent>());
		}

		public override Theme GetSelectedTheme(SlideType slideType)
		{
			return MediaMetaData.Instance.SettingsManager.GetSelectedTheme(slideType);
		}

		public override Boolean CheckPowerPointRunning()
		{
			return Controller.Instance.CheckPowerPointRunning();
		}

		public override void OutputPowerPointCurrent()
		{
			var outputItems = GetOutputItems(true);
			OutputPowerPointCustom(outputItems);
		}

		public override void OutputPowerPointAll()
		{
			var outputItems = GetOutputItems(false);
			OutputPowerPointCustom(outputItems);
		}

		public override void OutputPowerPointCustom(IList<OutputItem> outputItems)
		{
			if (!outputItems.Any()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				outputItems.ForEach(item => item.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, null));
				FormProgress.CloseProgress();
			});
		}

		public override void OutputPdf()
		{
			var outputItems = GetOutputItems(false);
			if (!outputItems.Any()) return;

			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			FormProgress.ShowOutputProgress();
			Controller.Instance.ShowFloater(() =>
			{
				var pdfFileName = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("{0}-{1:MM-dd-yy-hmmss}.pdf", "6ms", DateTime.Now));
				BusinessObjects.Instance.PowerPointManager.Processor.BuildPdf(pdfFileName, presentation =>
				{
					foreach (var outputItem in outputItems)
						outputItem.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, presentation);
				});
				FormProgress.CloseProgress();
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
			});
		}

		public override void Email()
		{
			var outputItems = GetOutputItems(false);
			if (!outputItems.Any()) return;

			using (var form = new FormEmailFileName())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Email...");
					FormProgress.ShowProgress();
					Controller.Instance.ShowFloater(() =>
					{
						var emailFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1:MM-dd-yy-hmmss}.pdf", SolutionInfo.ToggleTitle, DateTime.Now));
						var defaultItem = outputItems.First();
						BusinessObjects.Instance.PowerPointManager.Processor.PreparePresentation(emailFileName, presentation =>
						{
							foreach (var outputItem in outputItems)
								outputItem.SlideGeneratingAction?.Invoke(BusinessObjects.Instance.PowerPointManager.Processor, presentation);
						});

						var emailFile = Path.Combine(
							Path.GetFullPath(defaultItem.PresentationSourcePath)
								.Replace(Path.GetFileName(defaultItem.PresentationSourcePath), string.Empty),
							form.FileName + ".pptx");
						File.Copy(emailFileName, emailFile, true);

						FormProgress.CloseProgress();

						try
						{
							if (OutlookHelper.Instance.Open())
							{
								OutlookHelper.Instance.CreateMessage("Advertising Schedule", emailFile);
								OutlookHelper.Instance.Close();
							}
							else
								PopupMessageHelper.Instance.ShowWarning("Cannot open Outlook");
							File.Delete(emailFile);
						}
						catch { }
					});
				}
			}
		}
	}
}
