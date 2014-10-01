using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.Core.QuickShare;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls;
using NewBizWiz.QuickShare.Controls.Properties;
using Padding = System.Windows.Forms.Padding;

namespace NewBizWiz.QuickShare.Controls.PresentationClasses.ScheduleControls
{
	public class SchedulePage : IDisposable
	{
		public bool NeedFullUpdate { get; set; }
		public PackageSchedule Schedule { get; private set; }
		public RibbonTabItem Tab { get; private set; }
		public RibbonPanel Panel { get; private set; }
		public ButtonItem ThemeButton { get; private set; }
		public ButtonItem PowerPointButton { get; private set; }
		public RibbonBar PowerPointBar { get; private set; }
		public ButtonItem EmailButton { get; private set; }
		public ButtonItem PreviewButton { get; private set; }
		public RibbonBar QuarterBar { get; private set; }
		public ButtonItem QuarterButton { get; private set; }
		public ButtonItem SaveButton { get; private set; }
		public ButtonItem SaveAsButton { get; private set; }
		public ButtonItem HelpButton { get; private set; }
		public ButtonItem ProgramAddButton { get; private set; }
		public ButtonItem ProgramDeleteButton { get; private set; }
		public ScheduleSectionControl Section { get; private set; }

		public SchedulePage(PackageSchedule schedule)
		{
			NeedFullUpdate = true;
			Schedule = schedule;
			BuildTab();
		}

		public void BuidSection()
		{
			Section = new PackageScheduleSectionControl(this);
		}

		private void BuildTab()
		{
			#region Exit
			var buttonExit = new ButtonItem();
			buttonExit.Image = Resources.Exit;
			buttonExit.Click += (o, e) => CloseAction();

			var ribbonBarExit = new RibbonBar();
			ribbonBarExit.AutoOverflowEnabled = true;
			ribbonBarExit.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
			ribbonBarExit.BackgroundStyle.CornerType = eCornerType.Square;
			ribbonBarExit.ContainerControlProcessDialogKey = true;
			ribbonBarExit.Dock = DockStyle.Left;
			ribbonBarExit.DragDropSupport = true;
			ribbonBarExit.Items.Add(buttonExit);
			ribbonBarExit.Style = eDotNetBarStyle.StyleManagerControlled;
			ribbonBarExit.Text = "EXIT";
			ribbonBarExit.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			#endregion

			#region Quarter
			QuarterButton = new ButtonItem();
			QuarterButton.AutoCheckOnClick = true;
			QuarterButton.Image = Resources.Quarter;

			QuarterBar = new RibbonBar();
			QuarterBar.AutoOverflowEnabled = true;
			QuarterBar.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
			QuarterBar.BackgroundStyle.CornerType = eCornerType.Square;
			QuarterBar.ContainerControlProcessDialogKey = true;
			QuarterBar.Dock = DockStyle.Left;
			QuarterBar.DragDropSupport = true;
			QuarterBar.Items.Add(QuarterButton);
			QuarterBar.Style = eDotNetBarStyle.StyleManagerControlled;
			QuarterBar.Text = "Qtr View";
			QuarterBar.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			#endregion

			#region Options
			SaveButton = new ButtonItem();
			SaveButton.Image = Resources.SaveSmall;

			HelpButton = new ButtonItem();
			HelpButton.Image = Resources.HelpMiddle;

			var itemContainerOptions1 = new ItemContainer();
			itemContainerOptions1.BackgroundStyle.CornerType = eCornerType.Square;
			itemContainerOptions1.LayoutOrientation = eOrientation.Vertical;
			itemContainerOptions1.SubItems.AddRange(new[] { SaveButton, HelpButton });

			SaveAsButton = new ButtonItem();
			SaveAsButton.Image = Resources.SaveAs;

			var buttonFloater = new ButtonItem();
			buttonFloater.Image = Resources.FloaterSmall;
			buttonFloater.Click += (o, e) => FloaterAction();

			var itemContainerOptions2 = new ItemContainer();
			itemContainerOptions2.BackgroundStyle.CornerType = eCornerType.Square;
			itemContainerOptions2.LayoutOrientation = eOrientation.Vertical;
			itemContainerOptions2.SubItems.AddRange(new[] { SaveAsButton, buttonFloater });

			var ribbonBarOptions = new RibbonBar();
			ribbonBarOptions.AutoOverflowEnabled = true;
			ribbonBarOptions.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
			ribbonBarOptions.BackgroundStyle.CornerType = eCornerType.Square;
			ribbonBarOptions.ContainerControlProcessDialogKey = true;
			ribbonBarOptions.Dock = DockStyle.Left;
			ribbonBarOptions.DragDropSupport = true;
			ribbonBarOptions.Items.AddRange(new[] { itemContainerOptions1, itemContainerOptions2 });
			ribbonBarOptions.Style = eDotNetBarStyle.StyleManagerControlled;
			ribbonBarOptions.Text = "Options";
			ribbonBarOptions.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			#endregion

			#region PowerPoint
			PowerPointButton = new ButtonItem();
			PowerPointButton.Image = Resources.PowerPoint;

			ThemeButton = new ButtonItem();
			ThemeButton.AutoExpandOnClick = true;
			ThemeButton.SubItemsExpandWidth = 14;
			ThemeButton.Text = String.Format("Theme:{0}Default", Environment.NewLine);

			PowerPointBar = new RibbonBar();
			PowerPointBar.AutoOverflowEnabled = true;
			PowerPointBar.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
			PowerPointBar.BackgroundStyle.CornerType = eCornerType.Square;
			PowerPointBar.ContainerControlProcessDialogKey = true;
			PowerPointBar.Dock = DockStyle.Left;
			PowerPointBar.DragDropSupport = true;
			PowerPointBar.Items.AddRange(new[] { PowerPointButton, ThemeButton });
			PowerPointBar.Style = eDotNetBarStyle.StyleManagerControlled;
			PowerPointBar.Text = "PowerPoint";
			PowerPointBar.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			#endregion

			#region Email
			EmailButton = new ButtonItem();
			EmailButton.Image = Resources.EmailBig;

			var ribbonBarEmail = new RibbonBar();
			ribbonBarEmail.AutoOverflowEnabled = true;
			ribbonBarEmail.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
			ribbonBarEmail.BackgroundStyle.CornerType = eCornerType.Square;
			ribbonBarEmail.ContainerControlProcessDialogKey = true;
			ribbonBarEmail.Dock = DockStyle.Left;
			ribbonBarEmail.DragDropSupport = true;
			ribbonBarEmail.Items.Add(EmailButton);
			ribbonBarEmail.Style = eDotNetBarStyle.StyleManagerControlled;
			ribbonBarEmail.Text = "Email";
			ribbonBarEmail.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			#endregion

			#region Preview
			PreviewButton = new ButtonItem();
			PreviewButton.Image = Resources.Preview;

			var ribbonBarPreview = new RibbonBar();
			ribbonBarPreview.AutoOverflowEnabled = true;
			ribbonBarPreview.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
			ribbonBarPreview.BackgroundStyle.CornerType = eCornerType.Square;
			ribbonBarPreview.ContainerControlProcessDialogKey = true;
			ribbonBarPreview.Dock = DockStyle.Left;
			ribbonBarPreview.DragDropSupport = true;
			ribbonBarPreview.Items.Add(PreviewButton);
			ribbonBarPreview.Style = eDotNetBarStyle.StyleManagerControlled;
			ribbonBarPreview.Text = "Preview";
			ribbonBarPreview.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			#endregion

			#region Program
			ProgramAddButton = new ButtonItem();
			ProgramAddButton.Image = Resources.AddProgram;

			ProgramDeleteButton = new ButtonItem();
			ProgramDeleteButton.Image = Resources.DeleteProgram;

			var itemContainerProgram = new ItemContainer();
			itemContainerProgram.BackgroundStyle.CornerType = eCornerType.Square;
			itemContainerProgram.LayoutOrientation = eOrientation.Vertical;
			itemContainerProgram.SubItems.AddRange(new[] { ProgramAddButton, ProgramDeleteButton });

			var ribbonBarProgram = new RibbonBar();
			ribbonBarProgram.AutoOverflowEnabled = true;
			ribbonBarProgram.BackgroundMouseOverStyle.CornerType = eCornerType.Square;
			ribbonBarProgram.BackgroundStyle.CornerType = eCornerType.Square;
			ribbonBarProgram.ContainerControlProcessDialogKey = true;
			ribbonBarProgram.Dock = DockStyle.Left;
			ribbonBarProgram.DragDropSupport = true;
			ribbonBarProgram.Items.Add(itemContainerProgram);
			ribbonBarProgram.Style = eDotNetBarStyle.StyleManagerControlled;
			ribbonBarProgram.Text = "Program";
			ribbonBarProgram.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			#endregion

			Panel = new RibbonPanel();
			Panel.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
			Panel.Controls.Add(ribbonBarOptions);
			Panel.Controls.Add(QuarterBar);
			Panel.Controls.Add(ribbonBarExit);
			Panel.Controls.Add(PowerPointBar);
			Panel.Controls.Add(ribbonBarProgram);
			Panel.Controls.Add(ribbonBarPreview);
			Panel.Controls.Add(ribbonBarEmail);
			Panel.Dock = DockStyle.Fill;
			Panel.Padding = new Padding(3, 0, 3, 2);

			Tab = new RibbonTabItem();
			Tab.Panel = Panel;
			Tab.Tag = this;
		}

		public void Update(bool quick = false)
		{
			Tab.Enabled = Schedule.IsConfigured;
			Tab.Text = String.Format("Pkg {0}", Schedule.DisplayedIndex);
			if (!quick && Section != null)
			{
				Section.LoadSchedule(!NeedFullUpdate);
				NeedFullUpdate = false;
			}
		}

		public void Dispose()
		{
			ThemeButton = null;
			PowerPointButton = null;
			PowerPointBar = null;
			EmailButton = null;
			PreviewButton = null;
			QuarterBar = null;
			QuarterButton = null;
			SaveButton = null;
			SaveAsButton = null;
			HelpButton = null;
			ProgramAddButton = null;
			ProgramDeleteButton = null;
			Panel.Dispose();
			Panel = null;
			Tab.Panel = null;
			Tab.Dispose();
			Tab = null;
			if (Section != null)
				Section.Dispose();
			Section = null;
		}

		private void FloaterAction()
		{
			Controller.Instance.ShowFloater(null);
		}
		private void CloseAction()
		{
			Controller.Instance.CloseApp();
		}
	}
}
