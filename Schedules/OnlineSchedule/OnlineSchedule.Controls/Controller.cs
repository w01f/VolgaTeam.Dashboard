using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.OnlineSchedule.Controls.BusinessClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses;
using NewBizWiz.OnlineSchedule.Controls.PresentationClasses.ToolForms;

namespace NewBizWiz.OnlineSchedule.Controls
{
	public class Controller
	{
		private static readonly Controller _instance = new Controller();
		private Controller() {}
		public static Controller Instance
		{
			get { return _instance; }
		}

		public Form FormMain { get; set; }
		public SuperTooltip Supertip { get; set; }
		public RibbonControl Ribbon { get; set; }
		public RibbonTabItem TabScheduleSlides { get; set; }
		public RibbonTabItem TabWebPackage { get; set; }
		public RibbonTabItem TabWebSummary { get; set; }
		public RibbonTabItem TabWebBundle { get; set; }

		public void Init()
		{
			#region Schedule Settings
			ScheduleSettings = new ScheduleSettingsControl();
			HomeHelp.Click += ScheduleSettings.buttonItemScheduleHelp_Click;
			HomeSave.Click += ScheduleSettings.buttonItemScheduleSettingsSave_Click;
			HomeSaveAs.Click += ScheduleSettings.buttonItemScheduleSettingsSaveAs_Click;
			HomeBusinessName.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeDecisionMaker.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.FlightDateStartEditValueChanged;
			HomeFlightDatesEnd.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesEnd.EditValueChanged += ScheduleSettings.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesStart.CloseUp += ScheduleSettings.dateEditFlightDatesStart_CloseUp;
			HomeFlightDatesEnd.CloseUp += ScheduleSettings.dateEditFlightDatesEnd_CloseUp;
			HomeBusinessName.Enter += Utilities.Instance.Editor_Enter;
			HomeBusinessName.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeBusinessName.MouseUp += Utilities.Instance.Editor_MouseUp;
			HomeDecisionMaker.Enter += Utilities.Instance.Editor_Enter;
			HomeDecisionMaker.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeDecisionMaker.MouseUp += Utilities.Instance.Editor_MouseUp;
			#endregion

			#region Schedule Slides
			ScheduleSlides = new ScheduleSlidesControl(FormMain);
			ScheduleSlidesSave.Click += ScheduleSlides.Save_Click;
			ScheduleSlidesSaveAs.Click += ScheduleSlides.SaveAs_Click;
			ScheduleSlidesPowerPoint.Click += ScheduleSlides.PowerPoint_Click;
			ScheduleSlidesEmail.Click += ScheduleSlides.Email_Click;
			ScheduleSlidesHelp.Click += ScheduleSlides.Help_Click;
			ScheduleSlidesOptions.Click += ScheduleSlides.Options_Click;
			#endregion

			#region Web Package
			WebPackage = new WebPackageControl();
			WebPackageSave.Click += WebPackage.Save_Click;
			WebPackageSaveAs.Click += WebPackage.SaveAs_Click;
			WebPackagePowerPoint.Click += WebPackage.PowerPoint_Click;
			WebPackageEmail.Click += WebPackage.Email_Click;
			WebPackageHelp.Click += WebPackage.Help_Click;
			WebPackageOptions.CheckedChanged += WebPackage.Options_CheckedChanged;
			#endregion

			#region Web Summary
			WebSummary = new ProductSummaryControl();
			WebSummarySave.Click += WebSummary.Save_Click;
			WebSummarySaveAs.Click += WebSummary.SaveAs_Click;

			WebSummaryActiveDays.CheckedChanged += WebSummary.PreviewOptions_CheckedChanged;
			WebSummaryAdRate.CheckedChanged += WebSummary.PreviewOptions_CheckedChanged;
			WebSummaryDimensions.CheckedChanged += WebSummary.PreviewOptions_CheckedChanged;
			WebSummaryTotalAds.CheckedChanged += WebSummary.PreviewOptions_CheckedChanged;
			WebSummaryWebsites.CheckedChanged += WebSummary.PreviewOptions_CheckedChanged;

			WebSummaryCPM.CheckedChanged += WebSummary.ColumnsOptions_CheckedChanged;
			WebSummaryProductImpressions.CheckedChanged += WebSummary.ColumnsOptions_CheckedChanged;
			WebSummaryProductInvestment.CheckedChanged += WebSummary.ColumnsOptions_CheckedChanged;

			WebSummaryMonthlyImpressions.CheckedChanged += WebSummary.TotalsOptions_CheckedChanged;
			WebSummaryMonthlyInvestment.CheckedChanged += WebSummary.TotalsOptions_CheckedChanged;
			WebSummaryTotalImpressions.CheckedChanged += WebSummary.TotalsOptions_CheckedChanged;
			WebSummaryTotalInvestment.CheckedChanged += WebSummary.TotalsOptions_CheckedChanged;

			WebSummaryHelp.Click += WebSummary.Help_Click;
			WebSummaryPowerPoint.Click += WebSummary.PowerPoint_Click;
			WebSummaryEmail.Click += WebSummary.Email_Click;
			#endregion

			#region Web Bundle
			WebBundle = new ProductBundleControl();
			WebBundleSave.Click += WebBundle.Save_Click;
			WebBundleSaveAs.Click += WebBundle.SaveAs_Click;

			WebBundleActiveDays.CheckedChanged += WebBundle.PreviewOptions_CheckedChanged;
			WebBundleAdRate.CheckedChanged += WebBundle.PreviewOptions_CheckedChanged;
			WebBundleDimensions.CheckedChanged += WebBundle.PreviewOptions_CheckedChanged;
			WebBundleTotalAds.CheckedChanged += WebBundle.PreviewOptions_CheckedChanged;
			WebBundleWebsites.CheckedChanged += WebBundle.PreviewOptions_CheckedChanged;

			WebBundleCPM.CheckedChanged += WebBundle.ColumnsOptions_CheckedChanged;
			WebBundleProductImpressions.CheckedChanged += WebBundle.ColumnsOptions_CheckedChanged;
			WebBundleProductInvestment.CheckedChanged += WebBundle.ColumnsOptions_CheckedChanged;

			WebBundleMonthlyImpressions.CheckedChanged += WebBundle.TotalsOptions_CheckedChanged;
			WebBundleMonthlyInvestment.CheckedChanged += WebBundle.TotalsOptions_CheckedChanged;
			WebBundleTotalImpressions.CheckedChanged += WebBundle.TotalsOptions_CheckedChanged;
			WebBundleTotalInvestment.CheckedChanged += WebBundle.TotalsOptions_CheckedChanged;

			WebBundleHelp.Click += WebBundle.Help_Click;
			WebBundlePowerPoint.Click += WebBundle.PowerPoint_Click;
			WebBundleEmail.Click += WebBundle.Email_Click;
			#endregion
		}

		public void RemoveInstance()
		{
			ScheduleSettings.Dispose();
			ScheduleSlides.Dispose();
			WebPackage.Dispose();
			WebSummary.Dispose();
			WebBundle.Dispose();
		}

		public void LoadData()
		{
			ScheduleSettings.LoadSchedule(false);
			ScheduleSlides.LoadSchedule(false);
			WebPackage.LoadSchedule(false);
			WebSummary.LoadSchedule();
			WebBundle.LoadSchedule();
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
		}

		public void UpdateSimpleOutputTabPageState(bool enable)
		{
			TabScheduleSlides.Enabled = enable;
			TabWebPackage.Enabled = enable;
		}

		public void UpdateSummaryOutputTabPageState(bool enable)
		{
			TabWebBundle.Enabled = enable;
			TabWebSummary.Enabled = enable;
		}

		#region Command Controls

		#region Home
		public RibbonPanel HomePanel { get; set; }
		public RibbonBar HomeAdvertiserProfileBar { get; set; }
		public ComboBoxEdit HomeBusinessName { get; set; }
		public ComboBoxEdit HomeDecisionMaker { get; set; }
		public DateEdit HomePresentationDate { get; set; }
		public RibbonBar HomeFlightDatesBar { get; set; }
		public DateEdit HomeFlightDatesStart { get; set; }
		public DateEdit HomeFlightDatesEnd { get; set; }
		public LabelItem HomeWeeks { get; set; }
		public RibbonBar HomeSaveBar { get; set; }
		public ButtonItem HomeSave { get; set; }
		public ButtonItem HomeSaveAs { get; set; }
		public RibbonBar HomeHelpBar { get; set; }
		public ButtonItem HomeHelp { get; set; }
		public RibbonBar HomeExitBar { get; set; }
		#endregion

		#region Schedule Slides
		public ButtonItem ScheduleSlidesHelp { get; set; }
		public ButtonItem ScheduleSlidesSave { get; set; }
		public ButtonItem ScheduleSlidesSaveAs { get; set; }
		public ButtonItem ScheduleSlidesOptions { get; set; }
		public ButtonItem ScheduleSlidesEmail { get; set; }
		public ButtonItem ScheduleSlidesPowerPoint { get; set; }
		#endregion

		#region Web Package
		public ButtonItem WebPackageHelp { get; set; }
		public ButtonItem WebPackageSave { get; set; }
		public ButtonItem WebPackageSaveAs { get; set; }
		public ButtonItem WebPackageEmail { get; set; }
		public ButtonItem WebPackagePowerPoint { get; set; }
		public ButtonItem WebPackageOptions { get; set; }
		#endregion

		#region Web Summary
		public ButtonItem WebSummaryHelp { get; set; }
		public ButtonItem WebSummarySave { get; set; }
		public ButtonItem WebSummarySaveAs { get; set; }
		public ButtonItem WebSummaryEmail { get; set; }
		public ButtonItem WebSummaryPowerPoint { get; set; }
		public ButtonItem WebSummaryWebsites { get; set; }
		public ButtonItem WebSummaryDimensions { get; set; }
		public ButtonItem WebSummaryProductImpressions { get; set; }
		public ButtonItem WebSummaryTotalAds { get; set; }
		public ButtonItem WebSummaryActiveDays { get; set; }
		public ButtonItem WebSummaryAdRate { get; set; }
		public ButtonItem WebSummaryProductInvestment { get; set; }
		public ButtonItem WebSummaryCPM { get; set; }
		public ButtonItem WebSummaryMonthlyImpressions { get; set; }
		public ButtonItem WebSummaryTotalImpressions { get; set; }
		public ButtonItem WebSummaryMonthlyInvestment { get; set; }
		public ButtonItem WebSummaryTotalInvestment { get; set; }
		#endregion

		#region Web Bundle
		public ButtonItem WebBundleHelp { get; set; }
		public ButtonItem WebBundleSave { get; set; }
		public ButtonItem WebBundleSaveAs { get; set; }
		public ButtonItem WebBundleEmail { get; set; }
		public ButtonItem WebBundlePowerPoint { get; set; }
		public ButtonItem WebBundleWebsites { get; set; }
		public ButtonItem WebBundleDimensions { get; set; }
		public ButtonItem WebBundleProductImpressions { get; set; }
		public ButtonItem WebBundleTotalAds { get; set; }
		public ButtonItem WebBundleActiveDays { get; set; }
		public ButtonItem WebBundleAdRate { get; set; }
		public ButtonItem WebBundleProductInvestment { get; set; }
		public ButtonItem WebBundleCPM { get; set; }
		public ButtonItem WebBundleMonthlyImpressions { get; set; }
		public ButtonItem WebBundleTotalImpressions { get; set; }
		public ButtonItem WebBundleMonthlyInvestment { get; set; }
		public ButtonItem WebBundleTotalInvestment { get; set; }
		#endregion

		#endregion

		#region Forms
		public ScheduleSettingsControl ScheduleSettings { get; private set; }
		public ScheduleSlidesControl ScheduleSlides { get; private set; }
		public WebPackageControl WebPackage { get; private set; }
		public ProductSummaryControl WebSummary { get; private set; }
		public ProductBundleControl WebBundle { get; private set; }
		#endregion
	}
}