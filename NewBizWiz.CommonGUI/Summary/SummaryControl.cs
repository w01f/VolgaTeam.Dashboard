using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.Summary
{
	[ToolboxItem(false)]
	public abstract partial class SummaryControl : UserControl
	{
		private readonly List<SummaryInputItemControl> _inputControls = new List<SummaryInputItemControl>();
		private bool _allowToSave;

		public abstract ISummarySchedule Schedule { get; }
		public abstract HelpManager HelpManager { get; }

		protected SummaryControl()
		{
			InitializeComponent();
			SetClickEventHandler(this);
			Dock = DockStyle.Fill;
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
			if (control.GetType() != typeof(TextEdit) &&
				control.GetType() != typeof(MemoEdit) &&
				control.GetType() != typeof(ComboBoxEdit) &&
				control.GetType() != typeof(LookUpEdit) &&
				control.GetType() != typeof(DateEdit) &&
				control.GetType() != typeof(CheckedListBoxControl) &&
				control.GetType() != typeof(SpinEdit) &&
				control.GetType() != typeof(CheckEdit))
				control.Click += ControlClick;
		}

		private void ControlClick(object sender, EventArgs e)
		{
			((Control)sender).Select();
			if (((Control)sender).Parent != null)
				((Control)sender).Parent.Select();
		}

		public bool SettingsNotSaved { get; set; }

		public virtual void UpdateOutput(bool quickLoad)
		{
			if (!quickLoad)
			{
				_allowToSave = false;
				checkEditBusinessName.Checked = Schedule.Summary.ShowAdvertiser;
				checkEditPresentationDate.Checked = Schedule.Summary.ShowPresentationDate;
				checkEditFlightDates.Checked = Schedule.Summary.ShowFlightDates;
				checkEditMonthlyInvestment.Checked = Schedule.Summary.ShowMonthly;
				checkEditTotalInvestment.Checked = Schedule.Summary.ShowTotal;

				_inputControls.Clear();
				xtraScrollableControlInput.Controls.Clear();
				foreach (var summaryItem in Schedule.ProductSummaries)
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
				foreach (var summaryProduct in Schedule.ProductSummaries)
				{
					var inputControl = _inputControls.FirstOrDefault(ic => ic.Data.Parent.UniqueID.Equals(summaryProduct.UniqueID));
					if (inputControl != null)
						inputControl.Data = summaryProduct.SummaryItem;
					Application.DoEvents();
				}
			}
			SettingsNotSaved = false;
		}

		protected abstract bool SaveSchedule(string scheduleName = "");

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
			laTotalItems.Text = string.Format("Total Items: {0}", Schedule.ProductSummaries.Count());
		}

		public void UpdateTotals()
		{
			spinEditMonthly.Value = Schedule.Summary.MonthlyValue.HasValue ? Schedule.Summary.MonthlyValue.Value : Schedule.Summary.TotalMonthly;
			spinEditTotal.Value = Schedule.Summary.TotalValue.HasValue ? Schedule.Summary.TotalValue.Value : Schedule.Summary.TotalTotal;
		}

		public void OpenHelp()
		{
			HelpManager.OpenHelpLink("summary");
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
			Schedule.Summary.ShowAdvertiser = checkEditBusinessName.Checked;
			Schedule.Summary.ShowPresentationDate = checkEditPresentationDate.Checked;
			Schedule.Summary.ShowFlightDates = checkEditFlightDates.Checked;
			Schedule.Summary.ShowMonthly = checkEditMonthlyInvestment.Checked;
			Schedule.Summary.ShowTotal = checkEditTotalInvestment.Checked;
			SettingsNotSaved = true;
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Schedule.Summary.MonthlyValue = Schedule.Summary.TotalMonthly != spinEditMonthly.Value ? spinEditMonthly.Value : (decimal?)null;
			SettingsNotSaved = true;
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Schedule.Summary.TotalValue = Schedule.Summary.TotalTotal != spinEditTotal.Value ? spinEditTotal.Value : (decimal?)null;
			SettingsNotSaved = true;
		}

		private void spinEditMonthly_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			spinEditMonthly.Value = Schedule.Summary.TotalMonthly;
		}

		private void spinEditTotal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			spinEditTotal.Value = Schedule.Summary.TotalTotal;
		}

		#region Output Stuff
		public abstract Theme SelectedTheme { get; }
		public abstract void Output();
		protected abstract void PreparePreview(string tempFileName);
		protected abstract void ShowEmail(string tempFileName);
		protected abstract void ShowPreview(string tempFileName);

		public int ItemsCount
		{
			get { return _inputControls.Count(it => it.Complited); }
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
					return Schedule.BusinessName;
				return String.Empty;
			}
		}

		public string DecisionMaker
		{
			get
			{
				return Schedule.DecisionMaker;
			}
		}

		public string PresentationDate
		{
			get
			{
				if (checkEditPresentationDate.Checked && Schedule.PresentationDate.HasValue)
					return Schedule.PresentationDate.Value.ToString("MM/dd/yy");
				return String.Empty;
			}
		}

		public string CampaignDates
		{
			get
			{
				if (checkEditFlightDates.Checked)
					return Schedule.FlightDates;
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
			get { return checkEditMonthlyInvestment.Checked ? (Schedule.Summary.MonthlyValue.HasValue ? Schedule.Summary.MonthlyValue.Value : Schedule.Summary.TotalMonthly).ToString("$#,##0.00") : string.Empty; }
		}

		public string TotalTotalValue
		{
			get { return checkEditTotalInvestment.Checked ? (Schedule.Summary.TotalValue.HasValue ? Schedule.Summary.TotalValue.Value : Schedule.Summary.TotalTotal).ToString("$#,##0.00") : string.Empty; }
		}

		public bool ShowMonthlyHeader
		{
			get { return _inputControls.Where(it => it.Complited).Any(it => it.OutputMonthlyValue.HasValue); }
		}

		public bool ShowTotalHeader
		{
			get { return _inputControls.Where(it => it.Complited).Any(it => it.OutputTotalValue.HasValue); }
		}

		public void Email()
		{
			SaveSchedule();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				PreparePreview(tempFileName);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				ShowEmail(tempFileName);
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
				PreparePreview(tempFileName);
				formProgress.Close();
				if (!File.Exists(tempFileName)) return;
				ShowPreview(tempFileName);
			}
		}
		#endregion
	}
}