using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.Gallery;
using NewBizWiz.CommonGUI.RateCard;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;

namespace NewBizWiz.OnlineSchedule.Controls
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
		public RibbonTabItem TabScheduleSlides { get; set; }
		public RibbonTabItem TabDigitalPackage { get; set; }
		public RibbonTabItem TabAdPlan { get; set; }
		public RibbonTabItem TabGallery { get; set; }
		public RibbonTabItem TabRateCard { get; set; }

		public void Init()
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Application Started"));

			#region Schedule Settings
			ScheduleSettings = new ScheduleSettingsControl();
			HomeHelp.Click += ScheduleSettings.ScheduleSettingsHelp_Click;
			HomeSave.Click += ScheduleSettings.ScheduleSettingsSave_Click;
			HomeSaveAs.Click += ScheduleSettings.ScheduleSettingsSaveAs_Click;
			HomeBusinessName.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeDecisionMaker.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeClientType.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeAccountNumberText.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeAccountNumberCheck.CheckedChanged += ScheduleSettings.checkBoxItemAccountNumber_CheckedChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.FlightDateStartEditValueChanged;
			HomeFlightDatesEnd.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesEnd.EditValueChanged += ScheduleSettings.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesStart.CloseUp += ScheduleSettings.dateEditFlightDatesStart_CloseUp;
			HomeFlightDatesEnd.CloseUp += ScheduleSettings.dateEditFlightDatesEnd_CloseUp;
			HomeProductClone.Click += ScheduleSettings.DigitalProductClone;
			HomeBusinessName.Enter += Utilities.Instance.Editor_Enter;
			HomeBusinessName.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeBusinessName.MouseUp += Utilities.Instance.Editor_MouseUp;
			HomeDecisionMaker.Enter += Utilities.Instance.Editor_Enter;
			HomeDecisionMaker.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeDecisionMaker.MouseUp += Utilities.Instance.Editor_MouseUp;

			HomeBusinessName.TabIndex = 0;
			HomeBusinessName.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			HomeDecisionMaker.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			HomeClientType.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			HomePresentationDate.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			HomeFlightDatesStart.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			HomeFlightDatesEnd.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			#endregion

			#region Schedule Slides
			ScheduleSlides = new ScheduleSlidesControl(FormMain);
			DigitalSlidesSave.Click += ScheduleSlides.Save_Click;
			DigitalSlidesSaveAs.Click += ScheduleSlides.SaveAs_Click;
			DigitalSlidesPowerPoint.Click += ScheduleSlides.PowerPoint_Click;
			DigitalSlidesPreview.Click += ScheduleSlides.Preview_Click;
			DigitalSlidesEmail.Click += ScheduleSlides.Email_Click;
			DigitalSlidesHelp.Click += ScheduleSlides.Help_Click;
			#endregion

			#region Web Package
			DigitalPackage = new OnlineWebPackageControl(FormMain);
			DigitalPackageSave.Click += DigitalPackage.Save_Click;
			DigitalPackageSaveAs.Click += DigitalPackage.SaveAs_Click;
			DigitalPackagePowerPoint.Click += DigitalPackage.PowerPoint_Click;
			DigitalPackagePreview.Click += DigitalPackage.Preview_Click;
			DigitalPackageEmail.Click += DigitalPackage.Email_Click;
			DigitalPackageHelp.Click += DigitalPackage.Help_Click;
			DigitalPackageOptions.CheckedChanged += DigitalPackage.TogledButton_CheckedChanged;
			#endregion

			#region AdPlan
			AdPlan = new DigitalAdPlanControl(FormMain);
			AdPlanPreview.Click += AdPlan.Preview_Click;
			AdPlanEmail.Click += AdPlan.Email_Click;
			AdPlanHelp.Click += AdPlan.Help_Click;
			AdPlanSave.Click += AdPlan.Save_Click;
			AdPlanSaveAs.Click += AdPlan.SaveAs_Click;
			AdPlanPowerPoint.Click += AdPlan.PowerPoint_Click;
			#endregion

			#region Rate Card Events
			RateCard = new RateCardControl(BusinessWrapper.Instance.RateCardManager, RateCardCombo);
			RateCardHelp.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("ratecard");
			#endregion

			#region Gallery
			Gallery = new DigitalGalleryControl();
			GalleryHelp.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("gallery");
			#endregion

			ConfigureTabPages();

			UpdateOutputButtonsAccordingThemeStatus();

			ConfigureSpecialButtons();

			Ribbon_SelectedRibbonTabChanged(Ribbon, EventArgs.Empty);
			Ribbon.SelectedRibbonTabChanged -= Ribbon_SelectedRibbonTabChanged;
			Ribbon.SelectedRibbonTabChanged += Ribbon_SelectedRibbonTabChanged;
		}

		public void RemoveInstance()
		{
			ScheduleSettings.Dispose();
			ScheduleSlides.Dispose();
			DigitalPackage.Dispose();
			AdPlan.Dispose();
			Gallery.Dispose();
			FloaterRequested = null;
		}

		public void LoadData()
		{
			ScheduleSettings.LoadSchedule(false);
			ScheduleSlides.LoadSchedule(false);
			DigitalPackage.LoadSchedule(false);
			AdPlan.LoadSchedule(false);

			BusinessWrapper.Instance.RateCardManager.LoadRateCards();
			TabRateCard.Enabled = BusinessWrapper.Instance.RateCardManager.RateCardFolders.Any();
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
					case "Digital Slides":
						TabScheduleSlides.Text = tabPageConfig.Name;
						tabPages.Add(TabScheduleSlides);
						break;
					case "Digital PKG":
						TabDigitalPackage.Text = tabPageConfig.Name;
						tabPages.Add(TabDigitalPackage);
						break;
					case "AdPlan":
						TabAdPlan.Text = tabPageConfig.Name;
						tabPages.Add(TabAdPlan);
						break;
					case "Gallery":
						TabGallery.Text = tabPageConfig.Name;
						tabPages.Add(TabGallery);
						break;
					case "Ratecard":
						TabRateCard.Text = tabPageConfig.Name;
						tabPages.Add(TabRateCard);
						break;
				}
			}
			Ribbon.Items.AddRange(tabPages.ToArray());
		}

		public void SaveSchedule(Schedule localSchedule, bool nameChanged, bool quickSave, Control sender)
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
			if (nameChanged)
				BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Saved As", localSchedule.Name));
			if (ScheduleChanged != null)
				ScheduleChanged(this, EventArgs.Empty);
		}

		public void UpdateSimpleOutputTabPageState(bool enable)
		{
			TabScheduleSlides.Enabled = enable;
			TabDigitalPackage.Enabled = enable && DigitalPackage.SlidesAvailable;
			TabAdPlan.Enabled = enable;
		}

		public void UpdateOutputButtonsAccordingThemeStatus()
		{
			if (!BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.None).Any())
			{
				var selectorToolTip = new SuperTooltipInfo("Important Info", "", "Click to get more info why output is disabled", null, null, eTooltipColor.Gray);
				var themesDisabledHandler = new Action(() => BusinessWrapper.Instance.HelpManager.OpenHelpLink("NoTheme"));

				DigitalSlidesPowerPoint.Visible = false;
				(DigitalSlidesPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(DigitalSlidesEmail.ContainerControl as RibbonBar).Visible = false;
				(DigitalSlidesPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(DigitalSlidesTheme, selectorToolTip);
				DigitalSlidesTheme.Click += (o, e) => themesDisabledHandler();

				DigitalPackagePowerPoint.Visible = false;
				(DigitalPackagePowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(DigitalPackageEmail.ContainerControl as RibbonBar).Visible = false;
				(DigitalPackagePreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
				DigitalPackageTheme.Click += (o, e) => themesDisabledHandler();

				AdPlanPowerPoint.Visible = false;
				(AdPlanPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(AdPlanEmail.ContainerControl as RibbonBar).Visible = false;
				(AdPlanPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(AdPlanTheme, selectorToolTip);
				AdPlanTheme.Click += (o, e) => themesDisabledHandler();
			}
			else
			{
				var selectorToolTip = new SuperTooltipInfo("Slide Theme", "", "Select the PowerPoint Slide theme you want to use for this schedule", null, null, eTooltipColor.Gray);
				Supertip.SetSuperTooltip(DigitalSlidesTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
				Supertip.SetSuperTooltip(AdPlanTheme, selectorToolTip);
			}

			Ribbon.SelectedRibbonTabChanged += (o, e) =>
			{
				(DigitalSlidesPowerPoint.ContainerControl as RibbonBar).Text = (DigitalSlidesTheme.Tag as Theme).Name;
				(DigitalPackagePowerPoint.ContainerControl as RibbonBar).Text = (DigitalPackageTheme.Tag as Theme).Name;
				(AdPlanPowerPoint.ContainerControl as RibbonBar).Text = (AdPlanTheme.Tag as Theme).Name;
			};
		}

		private void ConfigureSpecialButtons()
		{
			var specialLinkContainers = new[]
			{
				HomeSpecialButtons,
				DigitalPackageSpecialButtons,
				AdPlanSpecialButtons,
				RateCardSpecialButtons,
				GallerySpecialButtons
			};
			foreach (var ribbonBar in specialLinkContainers)
			{
				if (Core.OnlineSchedule.ListManager.Instance.SpecialLinksEnable)
				{
					ribbonBar.Text = Core.OnlineSchedule.ListManager.Instance.SpecialLinksGroupName;
					var containerButton = new ButtonItem();
					containerButton.Image = Core.OnlineSchedule.ListManager.Instance.SpecialLinksGroupLogo;
					containerButton.AutoExpandOnClick = true;
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

		public void ShowFloater(Action afterShow)
		{
			var args = new FloaterRequestedEventArgs { AfterShow = afterShow };
			if (FloaterRequested != null)
				FloaterRequested(null, args);
		}

		private void Ribbon_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new TabActivity(Ribbon.SelectedRibbonTabItem.Text));
			if (Ribbon.SelectedRibbonTabItem == TabRateCard)
				RateCard.LoadRateCards();
			else if (Ribbon.SelectedRibbonTabItem == TabGallery)
				Gallery.InitControl();
		}

		#region Command Controls

		#region Home
		public RibbonBar HomeSpecialButtons { get; set; }
		public ButtonItem HomeHelp { get; set; }
		public ButtonItem HomeSave { get; set; }
		public ButtonItem HomeSaveAs { get; set; }
		public ButtonItem HomeProductAdd { get; set; }
		public ButtonItem HomeProductClone { get; set; }
		public ComboBoxEdit HomeBusinessName { get; set; }
		public ComboBoxEdit HomeDecisionMaker { get; set; }
		public ComboBoxEdit HomeClientType { get; set; }
		public TextEdit HomeAccountNumberText { get; set; }
		public CheckBoxItem HomeAccountNumberCheck { get; set; }
		public DateEdit HomePresentationDate { get; set; }
		public DateEdit HomeFlightDatesStart { get; set; }
		public DateEdit HomeFlightDatesEnd { get; set; }
		public LabelItem HomeWeeks { get; set; }
		#endregion

		#region Schedule Slides
		public RibbonBar DigitalSlidesSpecialButtons { get; set; }
		public ButtonItem DigitalSlidesHelp { get; set; }
		public ButtonItem DigitalSlidesSave { get; set; }
		public ButtonItem DigitalSlidesSaveAs { get; set; }
		public ButtonItem DigitalSlidesPreview { get; set; }
		public ButtonItem DigitalSlidesEmail { get; set; }
		public ButtonItem DigitalSlidesPowerPoint { get; set; }
		public ButtonItem DigitalSlidesTheme { get; set; }
		#endregion

		#region Web Package
		public RibbonBar DigitalPackageSpecialButtons { get; set; }
		public ButtonItem DigitalPackageHelp { get; set; }
		public ButtonItem DigitalPackageSave { get; set; }
		public ButtonItem DigitalPackageSaveAs { get; set; }
		public ButtonItem DigitalPackagePreview { get; set; }
		public ButtonItem DigitalPackageEmail { get; set; }
		public ButtonItem DigitalPackagePowerPoint { get; set; }
		public ButtonItem DigitalPackageTheme { get; set; }
		public ButtonItem DigitalPackageOptions { get; set; }
		#endregion

		#region AdPlan
		public RibbonBar AdPlanSpecialButtons { get; set; }
		public ButtonItem AdPlanHelp { get; set; }
		public ButtonItem AdPlanSave { get; set; }
		public ButtonItem AdPlanSaveAs { get; set; }
		public ButtonItem AdPlanPreview { get; set; }
		public ButtonItem AdPlanEmail { get; set; }
		public ButtonItem AdPlanPowerPoint { get; set; }
		public ButtonItem AdPlanTheme { get; set; }
		#endregion

		#region Rate Card
		public RibbonBar RateCardSpecialButtons { get; set; }
		public ButtonItem RateCardHelp { get; set; }
		public ComboBoxEdit RateCardCombo { get; set; }
		#endregion

		#region Gallery
		public RibbonBar GallerySpecialButtons { get; set; }
		public RibbonBar GalleryBrowseBar { get; set; }
		public RibbonBar GalleryImageBar { get; set; }
		public RibbonBar GalleryZoomBar { get; set; }
		public RibbonBar GalleryCopyBar { get; set; }
		public ButtonItem GalleryScreenshots { get; set; }
		public ButtonItem GalleryAdSpecs { get; set; }
		public ButtonItem GalleryView { get; set; }
		public ButtonItem GalleryEdit { get; set; }
		public ButtonItem GalleryImageSelect { get; set; }
		public ButtonItem GalleryImageCrop { get; set; }
		public ButtonItem GalleryZoomIn { get; set; }
		public ButtonItem GalleryZoomOut { get; set; }
		public ButtonItem GalleryCopy { get; set; }
		public ButtonItem GalleryHelp { get; set; }
		public ComboBoxEdit GallerySections { get; set; }
		public ComboBoxEdit GalleryGroups { get; set; }
		#endregion
		#endregion

		#region Forms
		public ScheduleSettingsControl ScheduleSettings { get; private set; }
		public ScheduleSlidesControl ScheduleSlides { get; private set; }
		public OnlineWebPackageControl DigitalPackage { get; private set; }
		public DigitalAdPlanControl AdPlan { get; private set; }
		public RateCardControl RateCard { get; private set; }
		public GalleryControl Gallery { get; private set; }
		#endregion
	}
}