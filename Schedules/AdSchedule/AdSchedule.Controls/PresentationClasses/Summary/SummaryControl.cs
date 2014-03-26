using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	public partial class SummaryControl : UserControl
	{
		private readonly List<SummaryInputItemControl> _inputControls = new List<SummaryInputItemControl>();
		private bool _allowToSave;

		public SummaryControl()
		{
			InitializeComponent();
			SetClickEventHandler(this);
			Dock = DockStyle.Fill;
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate()
			{
				if (sender != this)
					UpdateOutput(e.QuickSave);
			});
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (SettingsNotSaved)
				{
					SaveSchedule();
					result = true;
				}
				else
					result = true;
				return result;
			}
		}

		public void SetClickEventHandler(Control control)
		{
			foreach (Control childControl in control.Controls)
				SetClickEventHandler(childControl);
			if (control.GetType() != typeof(TextEdit) && control.GetType() != typeof(MemoEdit) && control.GetType() != typeof(ComboBoxEdit) && control.GetType() != typeof(LookUpEdit) && control.GetType() != typeof(DateEdit) && control.GetType() != typeof(CheckedListBoxControl) && control.GetType() != typeof(SpinEdit) && control.GetType() != typeof(CheckEdit))
				control.Click += ControlClick;
		}

		private void ControlClick(object sender, EventArgs e)
		{
			((Control)sender).Select();
			if (((Control)sender).Parent != null)
				((Control)sender).Parent.Select();
		}

		public Schedule LocalSchedule { get; set; }
		public bool SettingsNotSaved { get; set; }

		public void UpdateOutput(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			checkEditBusinessName.Text = string.Format("Business Name: {0}", LocalSchedule.BusinessName);
			laPresentationDate.Text = String.Format("{0}", LocalSchedule.PresentationDate.HasValue ? LocalSchedule.PresentationDate.Value.ToString("MM/dd/yyyy") : String.Empty);
			laFlightDates.Text = String.Format("{0}", LocalSchedule.FlightDates);
			FormThemeSelector.Link(Controller.Instance.SummaryTheme, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.PrintSimpleSummary), BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintSimpleSummary), (t =>
			{
				BusinessWrapper.Instance.SetSelectedTheme(SlideType.PrintSimpleSummary, t.Name);
				BusinessWrapper.Instance.SaveLocalSettings();
				SettingsNotSaved = true;
			}));
			if (!quickLoad)
			{
				_allowToSave = false;
				checkEditBusinessName.Checked = LocalSchedule.Summary.ShowAdvertiser;
				checkEditPresentationDate.Checked = LocalSchedule.Summary.ShowPresentationDate;
				checkEditFlightDates.Checked = LocalSchedule.Summary.ShowFlightDates;
				checkEditMonthlyInvestment.Checked = LocalSchedule.Summary.ShowMonthly;
				checkEditTotalInvestment.Checked = LocalSchedule.Summary.ShowTotal;

				_inputControls.Clear();
				xtraScrollableControlInput.Controls.Clear();
				foreach (var summaryItem in LocalSchedule.ProductSummaries)
				{
					AddItemToList(summaryItem.SummaryItem);
					Application.DoEvents();
				}
				UpdateTotalItems();
				UpdateTotals();
				_allowToSave = true;
			}
			else
			{
				foreach (var summaryProduct in LocalSchedule.ProductSummaries)
				{
					var inputControl = _inputControls.FirstOrDefault(ic => ic.Data.Parent.UniqueID.Equals(summaryProduct.UniqueID));
					if (inputControl != null)
						inputControl.Data = summaryProduct.SummaryItem;
					Application.DoEvents();
				}
			}
			SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				LocalSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(LocalSchedule, nameChanged, false, this);
			SettingsNotSaved = false;
			return true;
		}

		private void AddItemToList(SummaryItem summaryItem)
		{
			var item = new SummaryInputItemControl { Data = summaryItem };
			item.LoadData();
			item.DataChanged += (o, e) => { SettingsNotSaved = true; };
			item.InvestmentChanged += (o, e) => UpdateTotals();
			SetClickEventHandler(item);
			_inputControls.Add(item);
			xtraScrollableControlInput.Controls.Add(item);
			item.BringToFront();
		}

		public void UpdateTotalItems()
		{
			laTotalItems.Text = string.Format("Total Items: {0}", LocalSchedule.ProductSummaries.Count());
		}

		public void UpdateTotals()
		{
			spinEditMonthly.Value = LocalSchedule.Summary.MonthlyValue.HasValue ? LocalSchedule.Summary.MonthlyValue.Value : LocalSchedule.Summary.TotalMonthly;
			spinEditTotal.Value = LocalSchedule.Summary.TotalValue.HasValue ? LocalSchedule.Summary.TotalValue.Value : LocalSchedule.Summary.TotalTotal;
		}

		public void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("summary");
		}

		public void Save_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule())
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new name for your Schedule:";
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					if (SaveSchedule(form.ScheduleName))
						Utilities.Instance.ShowInformation("Schedule was saved");
				}
				else
				{
					Utilities.Instance.ShowWarning("Schedule Name can't be empty");
				}
			}
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			checkEditMonthlyInvestment.ForeColor =
				checkEditMonthlyInvestment.Properties.Appearance.ForeColor =
				checkEditMonthlyInvestment.Properties.AppearanceFocused.ForeColor =
				 checkEditMonthlyInvestment.Checked ? Color.Black : Color.Gray;
			checkEditMonthlyInvestment.Refresh();
			checkEditTotalInvestment.ForeColor =
				checkEditTotalInvestment.Properties.Appearance.ForeColor =
				checkEditTotalInvestment.Properties.AppearanceFocused.ForeColor =
				 checkEditTotalInvestment.Checked ? Color.Black : Color.Gray;
			checkEditTotalInvestment.Refresh();
			spinEditMonthly.Enabled = checkEditMonthlyInvestment.Checked;
			spinEditTotal.Enabled = checkEditTotalInvestment.Checked;
			if (!_allowToSave) return;
			LocalSchedule.Summary.ShowAdvertiser = checkEditBusinessName.Checked;
			LocalSchedule.Summary.ShowPresentationDate = checkEditPresentationDate.Checked;
			LocalSchedule.Summary.ShowFlightDates = checkEditFlightDates.Checked;
			LocalSchedule.Summary.ShowMonthly = checkEditMonthlyInvestment.Checked;
			LocalSchedule.Summary.ShowTotal = checkEditTotalInvestment.Checked;
			SettingsNotSaved = true;
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			LocalSchedule.Summary.MonthlyValue = LocalSchedule.Summary.TotalMonthly != spinEditMonthly.Value ? spinEditMonthly.Value : (decimal?)null;
			SettingsNotSaved = true;
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			LocalSchedule.Summary.TotalValue = LocalSchedule.Summary.TotalTotal != spinEditTotal.Value ? spinEditTotal.Value : (decimal?)null;
			SettingsNotSaved = true;
		}

		private void spinEditMonthly_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			spinEditMonthly.Value = LocalSchedule.Summary.TotalMonthly;
		}

		private void spinEditTotal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			spinEditTotal.Value = LocalSchedule.Summary.TotalTotal;
		}

		#region Output Stuff
		public int ItemsCount
		{
			get { return _inputControls.Count(it => it.Complited); }
		}

		public Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.PrintSimpleSummary).FirstOrDefault(t => t.Name.Equals(BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintSimpleSummary)) || String.IsNullOrEmpty(BusinessWrapper.Instance.GetSelectedTheme(SlideType.PrintSimpleSummary))); }
		}

		public string Title
		{
			get { return "Summary"; }
		}

		public string Advertiser
		{
			get
			{
				if (checkEditBusinessName.Checked)
					return LocalSchedule.BusinessName;
				return String.Empty;
			}
		}

		public string DecisionMaker
		{
			get
			{
				return LocalSchedule.DecisionMaker;
			}
		}

		public string PresentationDate
		{
			get
			{
				if (checkEditPresentationDate.Checked && LocalSchedule.PresentationDate.HasValue)
					return LocalSchedule.PresentationDate.Value.ToString("MM/dd/yy");
				return String.Empty;
			}
		}

		public string CampaignDates
		{
			get
			{
				if (checkEditFlightDates.Checked)
					return LocalSchedule.FlightDates;
				return String.Empty;
			}
		}

		public string[] ItemTitles
		{
			get { return _inputControls.Where(it => it.Complited).Select(it => it.OutputItemTitle).ToArray(); }
		}

		public string[] ItemDetails
		{
			get { return _inputControls.Where(it => it.Complited).Select(it => it.ItemDetailOutput).ToArray(); }
		}

		public string[] MonthlyValues
		{
			get
			{
				if (ShowMonthlyHeader && ShowTotalHeader)
					return _inputControls.Where(it => it.Complited).Select(it => it.OutputMonthlyValue.HasValue ? it.OutputMonthlyValue.Value.ToString("$#,##0") : String.Empty).ToArray();
				return null;
			}
		}

		public string[] TotalValues
		{
			get
			{
				if (ShowMonthlyHeader && !ShowTotalHeader)
					return _inputControls.Where(it => it.Complited).Select(it => it.OutputMonthlyValue.HasValue ? it.OutputMonthlyValue.Value.ToString("$#,##0") : String.Empty).ToArray();
				return _inputControls.Where(it => it.Complited).Select(it => it.OutputTotalValue.HasValue ? it.OutputTotalValue.Value.ToString("$#,##0") : String.Empty).ToArray();
			}
		}

		public string TotalMonthlyValue
		{
			get { return checkEditMonthlyInvestment.Checked ? (LocalSchedule.Summary.MonthlyValue.HasValue ? LocalSchedule.Summary.MonthlyValue.Value : LocalSchedule.Summary.TotalMonthly).ToString("$#,##0.00") : string.Empty; }
		}

		public string TotalTotalValue
		{
			get { return checkEditTotalInvestment.Checked ? (LocalSchedule.Summary.TotalValue.HasValue ? LocalSchedule.Summary.TotalValue.Value : LocalSchedule.Summary.TotalTotal).ToString("$#,##0.00") : string.Empty; }
		}

		public bool ShowMonthlyHeader
		{
			get { return _inputControls.Where(it => it.Complited).Any(it => it.OutputMonthlyValue.HasValue); }
		}

		public bool ShowTotalHeader
		{
			get { return _inputControls.Where(it => it.Complited).Any(it => it.OutputTotalValue.HasValue); }
		}

		private void TrackOutput()
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new OutputActivity(Controller.Instance.TabSummary.Text, LocalSchedule.BusinessName, null));
		}

		public void Output()
		{
			SaveSchedule();
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					AdSchedulePowerPointHelper.Instance.AppendSummary();
					formProgress.Close();
				});
			}
		}

		public void Email()
		{
			SaveSchedule();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareSummaryEmail(tempFileName);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formEmail = new FormEmail(AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
				{
					formEmail.Text = "Email this Summary";
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
			SaveSchedule();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareSummaryEmail(tempFileName);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackOutput))
				{
					formPreview.Text = "Preview Summary";
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
	}
}