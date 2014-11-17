using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.Properties;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.RetractableBar;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using Point = System.Drawing.Point;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class OutputSnapshotControl : UserControl, ISummaryOutputControl
	{
		private bool _allowToSave;

		public OutputSnapshotControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			HelpToolTip = new SuperTooltipInfo("HELP", "", "Help me understand how to use the Advertising Snapshot slide", null, null, eTooltipColor.Gray);

			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
			{
				if (sender != this)
					UpdateOutput(e.QuickSave);
			});
			retractableBar.StateChanged += retractableBar_StateChanged;
		}

		#region ISummaryOutputControl Members
		public Schedule LocalSchedule { get; set; }
		public bool SettingsNotSaved { get; set; }

		public SuperTooltipInfo HelpToolTip { get; private set; }

		public void UpdateOutput(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			Controller.Instance.SnapshotDigitalLegend.Image = Controller.Instance.SnapshotDigitalLegend.Enabled && !LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.Enabled ? Resources.DigitalDisabled : Resources.Digital;
			Controller.Instance.Supertip.SetSuperTooltip(Controller.Instance.SnapshotDigitalLegend, new SuperTooltipInfo("Digital Products", "",
				Controller.Instance.SnapshotDigitalLegend.Enabled && LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.Enabled ?
				"Digital Products are Enabled for this slide" :
				"Digital Products are Disabled for this slide"
				, null, null, eTooltipColor.Gray));
			FormThemeSelector.Link(Controller.Instance.SnapshotTheme, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.PrintSnapshot), BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintSnapshot), (t =>
			{
				BusinessWrapper.Instance.SetSelectedTheme(SlideType.PrintSnapshot, t.Name);
				BusinessWrapper.Instance.SaveLocalSettings();
				SettingsNotSaved = true;
			}));
			if (!quickLoad)
			{
				_allowToSave = false;
				comboBoxEditSchedule.Properties.Items.Clear();
				comboBoxEditSchedule.Properties.Items.AddRange(Core.AdSchedule.ListManager.Instance.OutputHeaders);
				if (string.IsNullOrEmpty(LocalSchedule.ViewSettings.SnapshotViewSettings.SlideHeader))
				{
					if (comboBoxEditSchedule.Properties.Items.Count > 0)
						comboBoxEditSchedule.SelectedIndex = 0;
				}
				else
				{
					int index = comboBoxEditSchedule.Properties.Items.IndexOf(LocalSchedule.ViewSettings.SnapshotViewSettings.SlideHeader);
					if (index >= 0)
						comboBoxEditSchedule.SelectedIndex = index;
					else
						comboBoxEditSchedule.SelectedIndex = 0;
				}
				checkEditSchedule.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSlideHeader;
				checkEditBusinessName.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAdvertiser;
				checkEditDate.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPresentationDate;
				checkEditDecisionMaker.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDecisionMaker;
				checkEditFlightDates.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowFlightDates;
				_allowToSave = true;

				checkEditDate.Text = LocalSchedule.PresentationDate.HasValue ? LocalSchedule.PresentationDate.Value.ToString("MM/dd/yy") : string.Empty;
				checkEditBusinessName.Text = LocalSchedule.BusinessName + (!string.IsNullOrEmpty(LocalSchedule.AccountNumber) ? (" - " + LocalSchedule.AccountNumber) : string.Empty);
				checkEditDecisionMaker.Text = LocalSchedule.DecisionMaker;
				checkEditFlightDates.Text = String.Format("Campaign: {0}", LocalSchedule.FlightDates);

				outputSnapshotContainer.UpdateSnapshots(LocalSchedule);

				LoadView();
			}
			SettingsNotSaved = false;
		}

		public void Save() { }

		public void OnOptionChanged(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void ResetToDefault()
		{
			LocalSchedule.ViewSettings.SnapshotViewSettings.ResetToDefault();
			LoadView();
			outputSnapshotContainer.UpdateColumns(LocalSchedule);
			SettingsNotSaved = false;
			Controller.Instance.SaveSchedule(LocalSchedule, false, true, this);
		}

		public void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("snapshot");
		}
		#endregion

		private void LoadView()
		{
			_allowToSave = false;

			if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowOptions)
				retractableBar.Expand(true);
			else
				retractableBar.Collapse(true);

			xtraTabControlOptions.SelectedTabPageIndex = LocalSchedule.ViewSettings.SnapshotViewSettings.SelectedOptionChapterIndex;

			buttonXLogo.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableLogo;
			buttonXTotalInserts.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableTotalInserts;
			buttonXTotalFinalCost.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableTotalFinalCost;
			buttonXPageSize.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnablePageSize;
			buttonXDimensions.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableDimensions;
			buttonXSquare.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableSquare;
			buttonXTotalSquare.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableTotalSquare;
			buttonXAvgPCI.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableAvgPCI;
			buttonXAvgAdCost.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableAvgCost;
			buttonXAvgFinalCost.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableAvgFinalCost;
			buttonXTotalColorRate.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableTotalColor;
			buttonXTotalDiscounts.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableTotalDiscounts;
			buttonXReadership.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableReadership;
			buttonXDelivery.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnableDelivery;
			buttonXPercentOfPage.Enabled = LocalSchedule.ViewSettings.SnapshotViewSettings.EnablePercentOfPage & Core.AdSchedule.ListManager.Instance.ShareUnits.Count > 0;

			buttonXLogo.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowLogo;
			buttonXTotalInserts.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalInserts;
			buttonXTotalFinalCost.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalFinalCost;
			buttonXPageSize.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPageSize;
			buttonXDimensions.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDimensions;
			buttonXSquare.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSquare;
			buttonXTotalSquare.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalSquare;
			buttonXAvgPCI.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgPCI;
			buttonXAvgAdCost.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgCost;
			buttonXAvgFinalCost.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgFinalCost;
			buttonXTotalColorRate.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalColor;
			buttonXTotalDiscounts.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalDiscounts;
			buttonXReadership.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowReadership;
			buttonXDelivery.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDelivery;
			buttonXPercentOfPage.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPercentOfPage & buttonXPercentOfPage.Enabled;

			_allowToSave = true;
		}

		private void SaveView()
		{
			if (!_allowToSave) return;
			LocalSchedule.ViewSettings.SnapshotViewSettings.SelectedOptionChapterIndex = xtraTabControlOptions.SelectedTabPageIndex;

			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgCost = buttonXAvgAdCost.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgFinalCost = buttonXAvgFinalCost.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgPCI = buttonXAvgPCI.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDelivery = buttonXDelivery.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDimensions = buttonXDimensions.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowLogo = buttonXLogo.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPageSize = buttonXPageSize.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPercentOfPage = buttonXPercentOfPage.Checked & buttonXPercentOfPage.Enabled;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowReadership = buttonXReadership.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSquare = buttonXSquare.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalColor = buttonXTotalColorRate.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalDiscounts = buttonXTotalDiscounts.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalFinalCost = buttonXTotalFinalCost.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalInserts = buttonXTotalInserts.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalSquare = buttonXTotalSquare.Checked;
			SettingsNotSaved = true;
		}

		private bool AllowShowColumn()
		{
			int count = 0;
			if (buttonXAvgAdCost.Checked)
				count++;
			if (buttonXAvgFinalCost.Checked)
				count++;
			if (buttonXAvgPCI.Checked)
				count++;
			if (buttonXDelivery.Checked)
				count++;
			if (buttonXDimensions.Checked)
				count++;
			if (buttonXPageSize.Checked)
				count++;
			if (buttonXPercentOfPage.Checked)
				count++;
			if (buttonXReadership.Checked)
				count++;
			if (buttonXSquare.Checked)
				count++;
			if (buttonXTotalColorRate.Checked)
				count++;
			if (buttonXTotalDiscounts.Checked)
				count++;
			if (buttonXTotalFinalCost.Checked)
				count++;
			if (buttonXTotalInserts.Checked)
				count++;
			if (buttonXTotalSquare.Checked)
				count++;
			return count < 5;
		}

		private void retractableBar_StateChanged(object sender, StateChangedEventArgs e)
		{
			if (!_allowToSave) return;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowOptions = e.Expaned;
			SettingsNotSaved = true;
		}

		private void checkEditSchedule_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditSchedule.Enabled = checkEditSchedule.Checked;
			checkEdit_CheckedChanged(null, null);
		}

		private void comboBoxEditSchedule_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				LocalSchedule.ViewSettings.SnapshotViewSettings.SlideHeader = comboBoxEditSchedule.EditValue != null ? comboBoxEditSchedule.EditValue.ToString() : string.Empty;
				SettingsNotSaved = true;
			}
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAdvertiser = checkEditBusinessName.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDecisionMaker = checkEditDecisionMaker.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowFlightDates = checkEditFlightDates.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPresentationDate = checkEditDate.Checked;
			LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSlideHeader = checkEditSchedule.Checked;
			SettingsNotSaved = true;
		}

		private void checkEdit_MouseDown(object sender, MouseEventArgs e)
		{
			var cEdit = (CheckEdit)sender;
			var cInfo = (CheckEditViewInfo)cEdit.GetViewInfo();
			Rectangle r = cInfo.CheckInfo.GlyphRect;
			var editorRect = new Rectangle(new Point(0, 0), cEdit.Size);
			if (!r.Contains(e.Location) && editorRect.Contains(e.Location))
				((DXMouseEventArgs)e).Handled = true;
		}

		public void SetFocusToScrollbar()
		{
			xtraScrollableControl.Focus();
		}

		private void hyperLinkEditReset_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			ResetToDefault();
			e.Handled = true;
		}

		#region Options Panel Stuff
		private void xtraTabControlOptions_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (_allowToSave)
				SaveView();
		}

		#region Print
		public void buttonItemSnapshotToggle_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SaveView();
			outputSnapshotContainer.UpdateColumns(LocalSchedule);
		}

		public void buttonItemSnapshotButton_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button != null && !button.Checked)
			{
				if (AllowShowColumn())
					button.Checked = true;
				else
					Utilities.Instance.ShowWarning("You already have 5 items enabled");
			}
			else
				button.Checked = false;
		}
		#endregion

		#endregion

		#region Output Staff

		public int RecordsPerSlide
		{
			get
			{
				var totalRecords = PublicationNames.Length;
				switch (totalRecords)
				{
					case 7:
					case 8:
						return 4;
					case 9:
					case 10:
					case 13:
					case 14:
					case 15:
						return 5;
					case 11:
					case 12:
					case 16:
						return 6;
					default:
						if (totalRecords <= 6)
							return totalRecords;
						if (totalRecords > 16)
							return 6;
						return 0;
				}
			}
		}

		public string GetOutputTemplatePath(int slideIndex)
		{
			var template = String.Empty;
			if (buttonXLogo.Checked && LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.Enabled && (slideIndex == 0 || !LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.OutputOnlyOnce))
				template = "snapshot-{0}logo_digital.pptx";
			else if (buttonXLogo.Checked)
				template = "snapshot-{0}logo.pptx";
			else if (LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.Enabled && (slideIndex == 0 || !LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.OutputOnlyOnce))
				template = "snapshot-{0}nologo_digital.pptx";
			else
				template = "snapshot-{0}nologo.pptx";
			var templateIndex = 6;
			var totalRecords = PublicationNames.Length;
			if (slideIndex == 0)
				switch (PublicationNames.Length)
				{
					case 7:
					case 8:
						templateIndex = 4;
						break;
					case 9:
					case 10:
					case 13:
					case 14:
					case 15:
						templateIndex = 5;
						break;
					case 11:
					case 12:
					case 16:
						templateIndex = 6;
						break;
					default:
						if (totalRecords <= 6)
							templateIndex = totalRecords;
						else if (totalRecords > 16)
							templateIndex = 6;
						break;
				}
			else if (slideIndex == 1)
				switch (PublicationNames.Length)
				{
					case 7:
						templateIndex = 7;
						break;
					case 8:
						templateIndex = 4;
						break;
					case 9:
						templateIndex = 9;
						break;
					case 10:
					case 13:
					case 14:
					case 15:
						templateIndex = 5;
						break;
					case 11:
						templateIndex = 11;
						break;
					case 12:
					case 16:
						templateIndex = 6;
						break;
					default:
						templateIndex = 6;
						break;
				}
			else if (slideIndex == 2)
				switch (PublicationNames.Length)
				{
					case 13:
						templateIndex = 13;
						break;
					case 14:
						templateIndex = 14;
						break;
					case 15:
						templateIndex = 5;
						break;
					case 16:
						templateIndex = 16;
						break;
					default:
						templateIndex = 6;
						break;
				}
			return String.Format(template, templateIndex);
		}

		public Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.PrintSnapshot).FirstOrDefault(t => t.Name.Equals(BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintSnapshot)) || String.IsNullOrEmpty(BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintSnapshot))); }
		}

		public string Header
		{
			get
			{
				string result = string.Empty;
				if (comboBoxEditSchedule.EditValue != null && checkEditSchedule.Checked)
					result = comboBoxEditSchedule.EditValue.ToString();
				return result;
			}
		}

		public string Date
		{
			get
			{
				string result = string.Empty;
				if (checkEditDate.Checked)
					result = checkEditDate.Text;
				return result;
			}
		}

		public string BusinessName
		{
			get
			{
				string result = string.Empty;
				if (checkEditBusinessName.Checked)
					result = checkEditBusinessName.Text;
				return result;
			}
		}

		public string DecisionMaker
		{
			get
			{
				string result = string.Empty;
				if (checkEditDecisionMaker.Checked)
					result = checkEditDecisionMaker.Text;
				return result;
			}
		}

		public string FlightDates
		{
			get
			{
				string result = string.Empty;
				if (checkEditFlightDates.Checked)
					result = checkEditFlightDates.Text;
				return result;
			}
		}

		public string[] LogoFiles
		{
			get
			{
				var result = new List<string>();
				foreach (PrintProduct publication in LocalSchedule.PrintProducts.Where(x => x.Inserts.Count > 0))
				{
					string fileName = Path.GetTempFileName();
					publication.BigLogo.Save(fileName);
					result.Add(fileName);
				}
				return result.ToArray();
			}
		}

		public string[] PublicationNames
		{
			get
			{
				var result = new List<string>();
				foreach (PrintProduct publication in LocalSchedule.PrintProducts.Where(x => x.Inserts.Count > 0))
					result.Add(publication.Name.Replace("&&", "&"));
				return result.ToArray();
			}
		}

		public string[][] AdSpecs
		{
			get
			{
				var result = new List<string[]>();
				foreach (PrintProduct publication in LocalSchedule.PrintProducts.Where(x => x.Inserts.Count > 0))
				{
					var adSpecs = new List<string>();
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalInserts)
						adSpecs.Add("Total Ads: " + publication.TotalInserts.ToString("#,##0"));
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPageSize && !String.IsNullOrEmpty(publication.SizeOptions.PageSizeAndGroup))
						adSpecs.Add("Page Size: " + publication.SizeOptions.PageSizeAndGroup);
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPercentOfPage && !string.IsNullOrEmpty(publication.SizeOptions.PercentOfPage) && publication.AdPricingStrategy == AdPricingStrategies.SharePage)
						adSpecs.Add(publication.SizeOptions.PercentOfPage + " Share of Page");
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDimensions && !string.IsNullOrEmpty(publication.SizeOptions.Dimensions))
						adSpecs.Add("Dimensions: " + publication.SizeOptions.Dimensions);
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSquare && publication.SizeOptions.Square.HasValue && publication.AdPricingStrategy != AdPricingStrategies.SharePage)
						adSpecs.Add("Col. Inches: " + publication.SizeOptions.Square.Value.ToString("#,###.00#"));
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalSquare && publication.TotalSquare.HasValue && publication.AdPricingStrategy != AdPricingStrategies.SharePage)
						adSpecs.Add("Total Inches: " + publication.TotalSquare.Value.ToString("#,###.00#"));
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgPCI && publication.AvgPCIRate > 0)
						adSpecs.Add("Avg PCI: " + publication.AvgPCIRate.ToString("$#,###.00"));
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgCost)
						adSpecs.Add("BW Ad Cost: " + publication.AvgADRate.ToString("$#,###.00"));
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgFinalCost)
						adSpecs.Add("Final Ad Cost: " + publication.AvgFinalRate.ToString("$#,###.00"));
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalFinalCost)
						adSpecs.Add("Investment: " + publication.TotalFinalRate.ToString("$#,###.00"));
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalColor)
						adSpecs.Add("Total Color: " + (publication.TotalColorPricingCalculated > 0 ? publication.TotalColorPricingCalculated.ToString("$#,###.00") : publication.Inserts.FirstOrDefault().ColorPricingObject.ToString()));
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalDiscounts)
						adSpecs.Add("Discounts: " + publication.TotalDiscountRate.ToString("$#,###.00"));
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowReadership && publication.DailyReadership != null)
						adSpecs.Add("Readership: " + publication.DailyReadership.Value.ToString("#,##0"));
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDelivery && publication.DailyDelivery != null)
						adSpecs.Add("Delivery: " + publication.DailyDelivery.Value.ToString("#,##0"));
					result.Add(adSpecs.ToArray());
				}
				return result.ToArray();
			}
		}

		public bool ShowDigitalLegendOnlyFirstSlide
		{
			get
			{
				return LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.OutputOnlyOnce;
			}
		}

		public string DigitalLegend
		{
			get
			{
				if (!LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.Enabled) return String.Empty;
				if (!LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.AllowEdit)
					return String.Format("Digital Product Info: {0}", LocalSchedule.GetDigitalInfo(LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.RequestOptions));
				if (!String.IsNullOrEmpty(LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.CompiledInfo))
					return String.Format("Digital Product Info: {0}", LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.CompiledInfo);
				return String.Empty;
			}
		}

		public void EditDigitalLegend()
		{
			var digitalLegend = LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend;
			using (var form = new FormDigital(digitalLegend))
			{
				form.ShowOutputOnce = LocalSchedule.PrintProducts.Count(x => x.Inserts.Any()) - RecordsPerSlide > 0;
				form.OutputOnlyFirstSlide = true;
				form.RequestDefaultInfo += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.GetDigitalInfo(e);
					e.Editor.Tag = e.Editor.EditValue;
				};
				if (form.ShowDialog() != DialogResult.OK) return;
				if (digitalLegend.ApplyForAll)
					LocalSchedule.ApplyDigitalLegendForAllViews(digitalLegend);
				Controller.Instance.SnapshotDigitalLegend.Image = !digitalLegend.Enabled ? Resources.DigitalDisabled : Resources.Digital;
				Controller.Instance.Supertip.SetSuperTooltip(Controller.Instance.SnapshotDigitalLegend, new SuperTooltipInfo("Digital Products", "",
					digitalLegend.Enabled ?
					"Digital Products are Enabled for this slide" :
					"Digital Products are Disabled for this slide"
					, null, null, eTooltipColor.Gray));
				SettingsNotSaved = true;
			}
		}

		private void TrackOutput()
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new OutputActivity(Controller.Instance.TabSnapshot.Text, LocalSchedule.BusinessName, (decimal)LocalSchedule.PrintProducts.Sum(p => p.TotalFinalRate)));
		}

		public void PrintOutput()
		{
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					AdSchedulePowerPointHelper.Instance.AppendSnapshot();
					formProgress.Close();
				});
			}
		}

		public void Email()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				var tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareSnapshotEmail(tempFileName);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formEmail = new FormEmail(AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
				{
					formEmail.Text = "Email this Ad Schedule Snapshot";
					formEmail.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
					RegistryHelper.MainFormHandle = formEmail.Handle;
					RegistryHelper.MaximizeMainForm = false;
					formEmail.ShowDialog();
					RegistryHelper.MaximizeMainForm = true;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				}
			}
		}

		public void Preview()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
				formProgress.TopMost = true;
				formProgress.Show();
				var tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareSnapshotEmail(tempFileName);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackOutput))
				{
					formPreview.Text = "Preview this Ad Schedule Snapshot";
					formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
					RegistryHelper.MainFormHandle = formPreview.Handle;
					RegistryHelper.MaximizeMainForm = false;
					DialogResult previewResult = formPreview.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (previewResult != DialogResult.OK)
						Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				}
			}
		}
		#endregion

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}
}