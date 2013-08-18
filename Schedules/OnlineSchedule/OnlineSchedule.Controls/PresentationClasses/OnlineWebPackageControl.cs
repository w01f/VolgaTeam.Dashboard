﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;

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
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("pkg");
		}
	}
}