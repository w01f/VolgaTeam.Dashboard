﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	public class OnlineWebPackageControl : WebPackageControl
	{
		public Schedule LocalSchedule { get; set; }
		public override ISchedule Schedule { get { return LocalSchedule; } }

		public override DigitalPackageSettings Settings
		{
			get { return LocalSchedule.ViewSettings.DigitalPackageSettings; }
		}
		public override IEnumerable<ProductPackageRecord> PackageRecords
		{
			get { return LocalSchedule.Products.Select(p => p.PackageRecord).ToList(); }
		}
		public override ButtonItem OptionsButtons
		{
			get { return Controller.Instance.DigitalPackageOptions; }
		}

		public override ButtonItem Preview
		{
			get { return Controller.Instance.DigitalPackagePreview; }
		}

		public override ButtonItem PowerPoint
		{
			get { return Controller.Instance.DigitalPackagePowerPoint; }
		}

		public override ButtonItem Email
		{
			get { return Controller.Instance.DigitalPackageEmail; }
		}

		public OnlineWebPackageControl(Form form)
			: base(form)
		{
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
		}

		public override void LoadSchedule(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			base.LoadSchedule(quickLoad);
		}

		protected override bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
				LocalSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(LocalSchedule, false, this);
			return base.SaveSchedule(scheduleName);
		}

		public override void Help_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("dgpkg");
		}

		public override void OutputSlides()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					OnlineSchedulePowerPointHelper.Instance.AppendWebPackage(this);
					formProgress.Close();
				});
			}
		}

		public override void ShowPreview(string tempFileName)
		{
			using (var formPreview = new FormPreview())
			{
				formPreview.Text = "Preview Digital Package";
				formPreview.PresentationFile = tempFileName;
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = _formContainer.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = _formContainer.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(_formContainer.Handle, true, false);
				else
					Utilities.Instance.ActivateMiniBar();
			}
		}
	}
}
