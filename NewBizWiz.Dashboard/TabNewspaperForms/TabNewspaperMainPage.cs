﻿using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Dashboard.ToolForms;

namespace NewBizWiz.Dashboard.TabNewspaperForms
{
	[ToolboxItem(false)]
	public partial class TabNewspaperMainPage : UserControl
	{
		private static TabNewspaperMainPage _instance;

		private TabNewspaperMainPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
		}

		public static TabNewspaperMainPage Instance
		{
			get
			{
				if (_instance == null)
					_instance = new TabNewspaperMainPage();
				return _instance;
			}
		}

		public void UpdatePageAccordingToggledButton()
		{
			Control parent = Parent;
			Parent = null;
			Controls.Clear();
			if (FormMain.Instance.buttonItemNewspaperScheduleBuilder != null && FormMain.Instance.buttonItemNewspaperScheduleBuilder.Checked)
			{
				PrintScheduleBuilderControl.Instance.LoadSchedules();
				FormMain.Instance.OutsideClick = PrintScheduleBuilderControl.Instance.OutsideClick;
				Controls.Add(PrintScheduleBuilderControl.Instance);
			}
			else
			{
				var borderedControl = new WhiteBorderControl();
				Controls.Add(borderedControl);
				Control parentSecond = borderedControl.panelExTop.Parent;
				borderedControl.panelExTop.Parent = null;
				borderedControl.panelExTop.Controls.Clear();
				borderedControl.OutputClick = null;

				//if (FormMain.Instance.buttonItemPrintScheduleBuilder != null && FormMain.Instance.buttonItemPrintScheduleBuilder.Checked)
				//    borderedControl.panelExTop.Controls.Add(PrintScheduleBuilderControl.Instance);

				borderedControl.panelExTop.Parent = parentSecond;
			}
			Parent = parent;
		}
	}
}