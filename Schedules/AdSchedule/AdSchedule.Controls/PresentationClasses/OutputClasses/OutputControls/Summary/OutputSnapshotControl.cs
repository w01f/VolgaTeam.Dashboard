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
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

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
		}

		#region ISummaryOutputControl Members
		public Schedule LocalSchedule { get; set; }
		public bool SettingsNotSaved { get; set; }

		public SuperTooltipInfo HelpToolTip { get; private set; }

		public void UpdateOutput(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			laScheduleWindow.Text = string.Format("{0} - {1}", new object[] { LocalSchedule.FlightDateStart.ToString("MM/dd/yy"), LocalSchedule.FlightDateEnd.ToString("MM/dd/yy") });
			laScheduleName.Text = LocalSchedule.Name;
			laAdvertiser.Text = LocalSchedule.BusinessName + (!string.IsNullOrEmpty(LocalSchedule.AccountNumber) ? (" - " + LocalSchedule.AccountNumber) : string.Empty);
			if (!quickLoad)
			{
				_allowToSave = false;
				comboBoxEditSchedule.Properties.Items.Clear();
				comboBoxEditSchedule.Properties.Items.AddRange(Core.AdSchedule.ListManager.Instance.OutputHeaders.ToArray());
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

				checkEditDate.Text = LocalSchedule.PresentationDateObject != null ? LocalSchedule.PresentationDate.ToString("MM/dd/yy") : string.Empty;
				checkEditBusinessName.Text = " " + LocalSchedule.BusinessName + (!string.IsNullOrEmpty(LocalSchedule.AccountNumber) ? (" - " + LocalSchedule.AccountNumber) : string.Empty);
				checkEditDecisionMaker.Text = LocalSchedule.DecisionMaker;
				checkEditFlightDates.Text = " " + LocalSchedule.FlightDateStart.ToString("MM/dd/yy") + " - " + LocalSchedule.FlightDateEnd.ToString("MM/dd/yy");

				outputSnapshotContainer.UpdateSnapshots(LocalSchedule);

				LoadView();
			}
			SettingsNotSaved = false;
		}

		public void ResetToDefault()
		{
			LocalSchedule.ViewSettings.SnapshotViewSettings.ResetToDefault();
			LoadView();
			outputSnapshotContainer.UpdateColumns(LocalSchedule);
		}

		public void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("snapshot");
		}
		#endregion

		private void LoadView()
		{
			_allowToSave = false;
			Controller.Instance.SnapshotOptions.Checked = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowOptions;
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
			if (_allowToSave)
			{
				LocalSchedule.ViewSettings.SnapshotViewSettings.ShowOptions = Controller.Instance.SnapshotOptions.Checked;
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

		public void buttonItemSnapshotOptions_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SaveView();
			splitContainerControl.PanelVisibility = LocalSchedule.ViewSettings.SnapshotViewSettings.ShowOptions ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAdvertiser = checkEditBusinessName.Checked;
				LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDecisionMaker = checkEditDecisionMaker.Checked;
				LocalSchedule.ViewSettings.SnapshotViewSettings.ShowFlightDates = checkEditFlightDates.Checked;
				LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPresentationDate = checkEditDate.Checked;
				LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSlideHeader = checkEditSchedule.Checked;
				SettingsNotSaved = true;
			}
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

		private void pbPrintHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("snapshotnavbar");
		}
		#endregion

		#endregion

		#region Output Staff
		public string OutputFileIndex
		{
			get { return String.Format("{0}{1}", (buttonXLogo.Checked ? 1 : 2), (LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.Enabled ? "d" : String.Empty)); }
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
					if (LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPageSize && !string.IsNullOrEmpty(publication.SizeOptions.PageSize))
						adSpecs.Add("Page Size: " + publication.SizeOptions.PageSize);
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

		public string DigitalLegend
		{
			get
			{
				return LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend.StringRepresentation;
			}
		}

		public void EditDigitalLegend()
		{
			using (var form = new FormDigital(LocalSchedule.ViewSettings.SnapshotViewSettings.DigitalLegend))
			{
				form.WebsiteRequestDefault += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.DigitalWebsites;
				};
				form.SimpleDigitalInfoRequestDefault += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.DigitalSimpleInfo;
				};
				form.DetailedDigitalInfoRequestDefault += (o, e) =>
				{
					e.Editor.EditValue = LocalSchedule.DigitalDetailedInfo;
				};
				if (form.ShowDialog() == DialogResult.OK)
					SettingsNotSaved = true;
			}
		}

		public void PrintOutput()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				formProgress.Show();
				AdSchedulePowerPointHelper.Instance.AppendSnapshot();
				formProgress.Close();
			}
			using (var formOutput = new FormSlideOutput())
			{
				if (formOutput.ShowDialog() != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, Controller.Instance.FormMain.WindowState == FormWindowState.Maximized, false);
				else
				{
					Utilities.Instance.ActivatePowerPoint(AdSchedulePowerPointHelper.Instance.PowerPointObject);
					Utilities.Instance.ActivateMiniBar();
				}
			}
		}

		public void Email()
		{
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareSnapshotEmail(tempFileName);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formEmail = new FormEmail())
					{
						formEmail.Text = "Email this Ad Schedule Snapshot";
						formEmail.PresentationFile = tempFileName;
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
				string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareSnapshotEmail(tempFileName);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formPreview = new FormPreview())
					{
						formPreview.Text = "Preview this Ad Schedule Snapshot";
						formPreview.PresentationFile = tempFileName;
						RegistryHelper.MainFormHandle = formPreview.Handle;
						RegistryHelper.MaximizeMainForm = false;
						DialogResult previewResult = formPreview.ShowDialog();
						RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
						RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
						if (previewResult != DialogResult.OK)
							Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
						else
						{
							Utilities.Instance.ActivatePowerPoint(AdSchedulePowerPointHelper.Instance.PowerPointObject);
							Utilities.Instance.ActivateMiniBar();
						}
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