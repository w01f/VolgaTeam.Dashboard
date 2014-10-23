using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Calendar.Controls.PresentationClasses;
using NewBizWiz.Calendar.Controls.Properties;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.Gallery;
using NewBizWiz.CommonGUI.RateCard;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Calendar.Controls
{
	public class Controller
	{
		private static readonly Controller _instance = new Controller();
		private Controller() { }
		public static Controller Instance
		{
			get { return _instance; }
		}

		public event EventHandler<EventArgs> ScheduleChanged;
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;

		public Form FormMain { get; set; }
		public SuperTooltip Supertip { get; set; }
		public RibbonControl Ribbon { get; set; }
		public RibbonTabItem TabHome { get; set; }
		public RibbonTabItem TabCalendar { get; set; }
		public RibbonTabItem TabGrid { get; set; }
		public RibbonTabItem TabRateCard { get; set; }
		public RibbonTabItem TabGallery1 { get; set; }
		public RibbonTabItem TabGallery2 { get; set; }

		public void Init()
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Application Started"));

			HomeControl = new HomeControl();
			HomeHelp.Click += HomeControl.HomeHelp_Click;
			HomeSave.Click += HomeControl.HomeSave_Click;
			HomeSaveAs.Click += HomeControl.HomeSaveAs_Click;
			HomeBusinessName.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeDecisionMaker.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeClientType.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeAccountNumberText.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeAccountNumberCheck.CheckedChanged += HomeControl.checkBoxItemAccountNumber_CheckedChanged;
			HomePresentationDate.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += HomeControl.FlightDateStartEditValueChanged;
			HomeFlightDatesEnd.EditValueChanged += HomeControl.FlightDateEndEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += HomeControl.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesEnd.EditValueChanged += HomeControl.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesStart.CloseUp += HomeControl.dateEditFlightDatesStart_CloseUp;
			HomeFlightDatesEnd.CloseUp += HomeControl.dateEditFlightDatesEnd_CloseUp;

			HomeBusinessName.TabIndex = 0;
			HomeBusinessName.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeDecisionMaker.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeClientType.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomePresentationDate.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeFlightDatesStart.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeFlightDatesEnd.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;

			CalendarVisualizer = new CalendarVisualizer();
			CalendarMonthsList.SelectedIndexChanged += CalendarVisualizer.imageListBoxEditCalendar_SelectedIndexChanged;
			CalendarCopy.Click += CalendarVisualizer.buttonItemCalendarCopy_Click;
			CalendarPaste.Click += CalendarVisualizer.buttonItemCalendarPaste_Click;
			CalendarClone.Click += CalendarVisualizer.buttonItemCalendarClone_Click;
			CalendarSave.Click += CalendarVisualizer.buttonItemCalendarSave_Click;
			CalendarSaveAs.Click += CalendarVisualizer.buttonItemCalendarSaveAs_Click;
			CalendarPreview.Click += CalendarVisualizer.buttonItemCalendarPreview_Click;
			CalendarPowerPoint.Click += CalendarVisualizer.buttonItemCalendarPowerPoint_Click;
			CalendarEmail.Click += CalendarVisualizer.buttonItemCalendarEmail_Click;
			CalendarHelp.Click += CalendarVisualizer.buttonItemCalendarHelp_Click;

			GridMonthsList.SelectedIndexChanged += CalendarVisualizer.imageListBoxEditCalendar_SelectedIndexChanged;
			GridCopy.Click += CalendarVisualizer.buttonItemCalendarCopy_Click;
			GridPaste.Click += CalendarVisualizer.buttonItemCalendarPaste_Click;
			GridClone.Click += CalendarVisualizer.buttonItemCalendarClone_Click;
			GridSave.Click += CalendarVisualizer.buttonItemCalendarSave_Click;
			GridSaveAs.Click += CalendarVisualizer.buttonItemCalendarSaveAs_Click;
			GridPreview.Click += CalendarVisualizer.buttonItemCalendarPreview_Click;
			GridPowerPoint.Click += CalendarVisualizer.buttonItemCalendarPowerPoint_Click;
			GridEmail.Click += CalendarVisualizer.buttonItemCalendarEmail_Click;
			GridHelp.Click += CalendarVisualizer.buttonItemCalendarHelp_Click;

			RateCard = new RateCardControl(BusinessWrapper.Instance.RateCardManager, RateCardCombo);
			RateCardHelp.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("ratecard");

			Gallery1 = new CalendarGallery1Control();
			Gallery1Help.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("gallery");

			Gallery2 = new CalendarGallery2Control();
			Gallery2Help.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("gallery");

			ConfigureTabPages();

			ConfigureSpecialButtons();

			Ribbon_SelectedRibbonTabChanged(Ribbon, EventArgs.Empty);
			Ribbon.SelectedRibbonTabChanged -= Ribbon_SelectedRibbonTabChanged;
			Ribbon.SelectedRibbonTabChanged += Ribbon_SelectedRibbonTabChanged;
		}

		public void RemoveInstance()
		{
			HomeControl.Dispose();
			CalendarVisualizer.RemoveInstance();
			Gallery1.Dispose();
			Gallery2.Dispose();
			FloaterRequested = null;
		}

		public void LoadData()
		{
			HomeControl.LoadCalendar(false);
			CalendarVisualizer.LoadData();

			BusinessWrapper.Instance.RateCardManager.LoadRateCards();
			TabRateCard.Enabled = BusinessWrapper.Instance.RateCardManager.RateCardFolders.Any();
		}

		public void UpdateScheduleTabs(bool enable)
		{
			TabCalendar.Enabled = enable;
			TabGrid.Enabled = enable;
		}

		private void ConfigureTabPages()
		{
			Ribbon.Items.Clear();
			var tabPages = new List<BaseItem>();
			foreach (var tabPageConfig in BusinessWrapper.Instance.TabPageManager.TabPageSettings)
			{
				switch (tabPageConfig.Id)
				{
					case "Home":
						TabHome.Text = tabPageConfig.Name;
						tabPages.Add(TabHome);
						break;
					case "Ninja Calendar":
						TabCalendar.Text = tabPageConfig.Name;
						tabPages.Add(TabCalendar);
						break;
					case "List View":
						TabGrid.Text = tabPageConfig.Name;
						tabPages.Add(TabGrid);
						break;
					case "Ratecard":
						TabRateCard.Text = tabPageConfig.Name;
						tabPages.Add(TabRateCard);
						break;
					case "Gallery1":
						TabGallery1.Text = tabPageConfig.Name;
						tabPages.Add(TabGallery1);
						break;
					case "Gallery2":
						TabGallery2.Text = tabPageConfig.Name;
						tabPages.Add(TabGallery2);
						break;
				}
			}
			Ribbon.Items.AddRange(tabPages.ToArray());
		}

		private void ConfigureSpecialButtons()
		{
			var specialLinkContainers = new[]
			{
				HomeSpecialButtons,
				CalendarSpecialButtons,
				GridSpecialButtons,
				RateCardSpecialButtons,
				Gallery1SpecialButtons,
				Gallery2SpecialButtons
			};
			foreach (var ribbonBar in specialLinkContainers)
			{
				if (Core.OnlineSchedule.ListManager.Instance.SpecialLinksEnable)
				{
					ribbonBar.Text = Core.OnlineSchedule.ListManager.Instance.SpecialLinksGroupName;
					var containerButton = new ButtonItem();
					containerButton.Image = Core.OnlineSchedule.ListManager.Instance.SpecialLinksGroupLogo;
					containerButton.AutoExpandOnClick = true;
					Supertip.SetSuperTooltip(containerButton, new SuperTooltipInfo("Links", "", "Helpful schedule building Links and resources", null, null, eTooltipColor.Gray));
					ribbonBar.Items.Add(containerButton);
					foreach (var specialLinkButton in Core.OnlineSchedule.ListManager.Instance.SpecialLinkButtons)
					{
						var clickAction = new Action(() => { specialLinkButton.Open(); });
						var button = new ButtonItem();
						button.Image = specialLinkButton.Logo;
						button.Text = String.Format("<b>{0}</b><p>{1}</p>", specialLinkButton.Name, specialLinkButton.Tooltip);
						button.Tag = specialLinkButton;
						button.Click += (o, e) => clickAction();
						containerButton.SubItems.Add(button);
					}
				}
				else
				{
					ribbonBar.Visible = false;
				}
			}
		}

		public void SaveSchedule(Schedule localSchedule, bool byUser, bool nameChanged, bool quickSave, Control sender)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nSaving settings...";
				form.TopMost = true;
				var thread = new Thread(() => BusinessWrapper.Instance.ScheduleManager.SaveSchedule(localSchedule, quickSave, sender));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			var options = new Dictionary<string, object>();
			options.Add("Advertiser", localSchedule.BusinessName);
			if (nameChanged)
				BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Saved As", localSchedule.Name, options));
			else if (byUser)
				BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Saved", localSchedule.Name, options));
			if (ScheduleChanged != null)
				ScheduleChanged(this, EventArgs.Empty);
		}

		public void ShowFloater(Action afterShow)
		{
			var args = new FloaterRequestedEventArgs { AfterShow = afterShow, Logo = Resources.RibbonLogo };
			if (FloaterRequested != null)
				FloaterRequested(null, args);
		}

		private void Ribbon_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new TabActivity(Ribbon.SelectedRibbonTabItem.Text, BusinessWrapper.Instance.ScheduleManager.CurrentAdvertiser));
			if (Ribbon.SelectedRibbonTabItem == TabRateCard)
				RateCard.LoadRateCards();
			else if (Ribbon.SelectedRibbonTabItem == TabGallery1)
				Gallery1.InitControl();
			else if (Ribbon.SelectedRibbonTabItem == TabGallery2)
				Gallery2.InitControl();
		}

		#region Command Controls

		#region Home
		public RibbonPanel HomePanel { get; set; }
		public RibbonBar HomeSpecialButtons { get; set; }
		public ButtonItem HomeHelp { get; set; }
		public ButtonItem HomeSave { get; set; }
		public ButtonItem HomeSaveAs { get; set; }
		public TextEdit HomeAccountNumberText { get; set; }
		public CheckBoxItem HomeAccountNumberCheck { get; set; }
		public ComboBoxEdit HomeBusinessName { get; set; }
		public ComboBoxEdit HomeDecisionMaker { get; set; }
		public ComboBoxEdit HomeClientType { get; set; }
		public DateEdit HomePresentationDate { get; set; }
		public DateEdit HomeFlightDatesStart { get; set; }
		public DateEdit HomeFlightDatesEnd { get; set; }
		public LabelItem HomeWeeks { get; set; }
		#endregion

		#region Calendar
		public RibbonBar CalendarSpecialButtons { get; set; }
		public ImageListBoxControl CalendarMonthsList { get; set; }
		public ButtonItem CalendarCopy { get; set; }
		public ButtonItem CalendarPaste { get; set; }
		public ButtonItem CalendarClone { get; set; }
		public ButtonItem CalendarHelp { get; set; }
		public ButtonItem CalendarSave { get; set; }
		public ButtonItem CalendarSaveAs { get; set; }
		public ButtonItem CalendarPreview { get; set; }
		public ButtonItem CalendarEmail { get; set; }
		public ButtonItem CalendarPowerPoint { get; set; }
		#endregion

		#region Calendar
		public RibbonBar GridSpecialButtons { get; set; }
		public ImageListBoxControl GridMonthsList { get; set; }
		public ButtonItem GridCopy { get; set; }
		public ButtonItem GridPaste { get; set; }
		public ButtonItem GridClone { get; set; }
		public ButtonItem GridHelp { get; set; }
		public ButtonItem GridSave { get; set; }
		public ButtonItem GridSaveAs { get; set; }
		public ButtonItem GridPreview { get; set; }
		public ButtonItem GridEmail { get; set; }
		public ButtonItem GridPowerPoint { get; set; }
		#endregion

		#region Rate Card
		public RibbonBar RateCardSpecialButtons { get; set; }
		public ButtonItem RateCardHelp { get; set; }
		public ComboBoxEdit RateCardCombo { get; set; }
		#endregion

		#region Gallery1
		public RibbonPanel Gallery1Panel { get; set; }
		public RibbonBar Gallery1SpecialButtons { get; set; }
		public RibbonBar Gallery1BrowseBar { get; set; }
		public RibbonBar Gallery1ImageBar { get; set; }
		public RibbonBar Gallery1ZoomBar { get; set; }
		public RibbonBar Gallery1CopyBar { get; set; }
		public ItemContainer Gallery1BrowseModeContainer { get; set; }
		public ButtonItem Gallery1View { get; set; }
		public ButtonItem Gallery1Edit { get; set; }
		public ButtonItem Gallery1ImageSelect { get; set; }
		public ButtonItem Gallery1ImageCrop { get; set; }
		public ButtonItem Gallery1ZoomIn { get; set; }
		public ButtonItem Gallery1ZoomOut { get; set; }
		public ButtonItem Gallery1Copy { get; set; }
		public ButtonItem Gallery1Help { get; set; }
		public ComboBoxEdit Gallery1Sections { get; set; }
		public ComboBoxEdit Gallery1Groups { get; set; }
		#endregion

		#region Gallery2
		public RibbonPanel Gallery2Panel { get; set; }
		public RibbonBar Gallery2SpecialButtons { get; set; }
		public RibbonBar Gallery2BrowseBar { get; set; }
		public RibbonBar Gallery2ImageBar { get; set; }
		public RibbonBar Gallery2ZoomBar { get; set; }
		public RibbonBar Gallery2CopyBar { get; set; }
		public ItemContainer Gallery2BrowseModeContainer { get; set; }
		public ButtonItem Gallery2View { get; set; }
		public ButtonItem Gallery2Edit { get; set; }
		public ButtonItem Gallery2ImageSelect { get; set; }
		public ButtonItem Gallery2ImageCrop { get; set; }
		public ButtonItem Gallery2ZoomIn { get; set; }
		public ButtonItem Gallery2ZoomOut { get; set; }
		public ButtonItem Gallery2Copy { get; set; }
		public ButtonItem Gallery2Help { get; set; }
		public ComboBoxEdit Gallery2Sections { get; set; }
		public ComboBoxEdit Gallery2Groups { get; set; }
		#endregion
		#endregion

		#region Forms
		public HomeControl HomeControl { get; set; }
		public CalendarVisualizer CalendarVisualizer { get; set; }
		public RateCardControl RateCard { get; private set; }
		public GalleryControl Gallery1 { get; private set; }
		public GalleryControl Gallery2 { get; private set; }
		#endregion
	}
}