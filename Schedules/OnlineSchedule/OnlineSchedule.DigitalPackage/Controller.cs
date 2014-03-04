using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.DigitalPackage.BusinessClasses;
using NewBizWiz.OnlineSchedule.DigitalPackage.PresentationClasses;

namespace NewBizWiz.OnlineSchedule.DigitalPackage
{
	public class Controller
	{
		private static readonly Controller _instance = new Controller();
		private Controller() { }
		public static Controller Instance
		{
			get { return _instance; }
		}

		public event EventHandler<ScheduleEventArgs> ScheduleChanging;
		public event EventHandler<EventArgs> ScheduleCreated;
		public event EventHandler<EventArgs> ScheduleChanged;
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;

		public Form FormMain { get; set; }
		public SuperTooltip Supertip { get; set; }
		public RibbonControl Ribbon { get; set; }
		public RibbonTabItem TabDigitalPackage { get; set; }

		public void Init()
		{
			#region Web Package
			DigitalPackage = new DigitalPackageControl(FormMain);
			DigitalPackageAdd.Click += DigitalPackage.Add_Click;
			DigitalPackageDelete.Click += DigitalPackage.Delete_Click;
			DigitalPackageSave.Click += DigitalPackage.Save_Click;
			DigitalPackageSaveAs.Click += DigitalPackage.SaveAs_Click;
			DigitalPackagePowerPoint.Click += DigitalPackage.PowerPoint_Click;
			DigitalPackagePreview.Click += DigitalPackage.Preview_Click;
			DigitalPackageEmail.Click += DigitalPackage.Email_Click;
			DigitalPackageHelp.Click += DigitalPackage.Help_Click;
			DigitalPackageOptions.CheckedChanged += DigitalPackage.TogledButton_CheckedChanged;
			#endregion

			UpdateOutputButtonsAccordingThemeStatus();

			ConfigureSpecialButtons();
		}

		public void RemoveInstance()
		{
			DigitalPackage.Dispose();
			FloaterRequested = null;
		}

		public void LoadData()
		{
			DigitalPackage.LoadSchedule(false);
		}

		public void CreateSchedule()
		{
			if (ScheduleCreated != null)
				ScheduleCreated(this, EventArgs.Empty);
		}

		public void OpenSchedule(string scheduleFilePath)
		{
			if (ScheduleChanging != null)
				ScheduleChanging(this, new ScheduleEventArgs() { ScheduleFilePath = scheduleFilePath });
		}

		public void SaveSchedule(Schedule localSchedule, bool quickSave, Control sender)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nSaving settings...";
				form.TopMost = true;
				var thread = new Thread(delegate() { BusinessWrapper.Instance.ScheduleManager.SaveSchedule(localSchedule, quickSave, sender); });
				form.Show();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			if (ScheduleChanged != null)
				ScheduleChanged(this, EventArgs.Empty);
		}

		public void UpdateSimpleOutputTabPageState(bool enable)
		{
		}

		public void UpdateOutputButtonsAccordingThemeStatus()
		{
			if (!BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.None).Any())
			{
				var selectorToolTip = new SuperTooltipInfo("Important Info", "", "Click to get more info why output is disabled", null, null, eTooltipColor.Gray);
				var themesDisabledHandler = new Action(() => BusinessWrapper.Instance.HelpManager.OpenHelpLink("NoTheme"));

				DigitalPackagePowerPoint.Visible = false;
				(DigitalPackagePowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(DigitalPackageEmail.ContainerControl as RibbonBar).Visible = false;
				(DigitalPackagePreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
				DigitalPackageTheme.Click += (o, e) => themesDisabledHandler();
			}
			else
			{
				var selectorToolTip = new SuperTooltipInfo("Slide Theme", "", "Select the PowerPoint Slide theme you want to use for this schedule", null, null, eTooltipColor.Gray);
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
			}

			Ribbon.SelectedRibbonTabChanged += (o, e) =>
			{
				(DigitalPackagePowerPoint.ContainerControl as RibbonBar).Text = (DigitalPackageTheme.Tag as Theme).Name;
			};
		}

		private void ConfigureSpecialButtons()
		{
			DigitalPackageSpecialButtons.Text = Core.OnlineSchedule.ListManager.Instance.SpecialLinksGroupName;
			foreach (var specialLinkButton in Core.OnlineSchedule.ListManager.Instance.SpecialLinkButtons)
			{
				var toolTip = new SuperTooltipInfo(specialLinkButton.Name, "", specialLinkButton.Tooltip, null, null, eTooltipColor.Gray);
				var clickAction = new Action(() =>
				{
					try
					{
						Process.Start(specialLinkButton.Paths.FirstOrDefault(p => File.Exists(p) || specialLinkButton.Type == "URL"));
					}
					catch { }
				});

				{
					var button = new ButtonItem();
					button.Image = specialLinkButton.Logo;
					button.Text = specialLinkButton.Name;
					button.Tag = specialLinkButton;
					Supertip.SetSuperTooltip(button, toolTip);
					button.Click += (o, e) => clickAction();
					DigitalPackageSpecialButtons.Items.Add(button);
				}
			}
		}

		public void ShowFloater(Action afterShow)
		{
			var args = new FloaterRequestedEventArgs { AfterShow = afterShow };
			if (FloaterRequested != null)
				FloaterRequested(null, args);
		}

		#region Command Controls

		#region Web Package
		public RibbonBar DigitalPackageSpecialButtons { get; set; }
		public ButtonItem DigitalPackageAdd { get; set; }
		public ButtonItem DigitalPackageDelete { get; set; }
		public ButtonItem DigitalPackageHelp { get; set; }
		public ButtonItem DigitalPackageSave { get; set; }
		public ButtonItem DigitalPackageSaveAs { get; set; }
		public ButtonItem DigitalPackagePreview { get; set; }
		public ButtonItem DigitalPackageEmail { get; set; }
		public ButtonItem DigitalPackagePowerPoint { get; set; }
		public ButtonItem DigitalPackageTheme { get; set; }
		public ButtonItem DigitalPackageOptions { get; set; }
		#endregion

		#endregion

		#region Forms
		public DigitalPackageControl DigitalPackage { get; private set; }
		#endregion
	}

	public class ScheduleEventArgs : EventArgs
	{
		public string ScheduleFilePath { get; set; }
	}
}