﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.AdSchedule.Controls.ToolForms;
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

		public Schedule LocalSchedule { get; set; }
		public bool SettingsNotSaved { get; set; }

		public void UpdateOutput(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			checkEditBusinessName.Text = string.Format("Business Name: {0}", LocalSchedule.BusinessName);
			checkEditDecisionMaker.Text = string.Format("Decision Maker: {0}", LocalSchedule.DecisionMaker);
			checkEditPresentationDate.Text = string.Format("Presentation Date: {0}", LocalSchedule.PresentationDate.ToString("MM/dd/yyyy"));
			checkEditFlightDates.Text = string.Format("Campaign Dates: {0}", LocalSchedule.FlightDates);
			laSignatureLineTag.Text = LocalSchedule.DecisionMaker;
			if (!quickLoad)
			{
				_allowToSave = false;
				checkEditBusinessName.Checked = LocalSchedule.Summary.ShowAdvertiser;
				checkEditDecisionMaker.Checked = LocalSchedule.Summary.ShowDecisionMaker;
				checkEditPresentationDate.Checked = LocalSchedule.Summary.ShowPresentationDate;
				checkEditFlightDates.Checked = LocalSchedule.Summary.ShowFlightDates;
				checkEditEnableTotalEdit.Checked = LocalSchedule.Summary.EnableTotalsEdit;
				checkEditMonthlyInvestmentOutput.Checked = LocalSchedule.Summary.ShowMonthly;
				checkEditTotalInvestmentOutput.Checked = LocalSchedule.Summary.ShowTotal;
				checkEditSignatureLine.Checked = LocalSchedule.Summary.ShowSignature;

				_inputControls.Clear();
				xtraScrollableControlInput.Controls.Clear();
				foreach (SummaryItem summaryItem in LocalSchedule.Summary.Items.OrderBy(it => it.Order))
				{
					AddItemToList(summaryItem);
					Application.DoEvents();
				}
				UpdateTotalItems();
				UpdateTotals();
				_allowToSave = true;
			}
			else
			{
				foreach (SummaryItem summaryItem in LocalSchedule.Summary.Items)
				{
					SummaryInputItemControl inputControl = _inputControls.FirstOrDefault(ic => ic.Data.Identifier.Equals(summaryItem.Identifier));
					if (inputControl != null)
						inputControl.Data = summaryItem;
					Application.DoEvents();
				}
			}
			xtraTabControl_SelectedPageChanged(xtraTabControl, new TabPageChangedEventArgs(xtraTabControl.SelectedTabPage, xtraTabControl.SelectedTabPage));
			SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			if (!string.IsNullOrEmpty(scheduleName))
				LocalSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(LocalSchedule, true, this);
			SettingsNotSaved = false;
			return true;
		}

		public void AddItem(object sender, EventArgs e)
		{
			var item = new SummaryItem { Order = LocalSchedule.Summary.Items.Any() ? LocalSchedule.Summary.Items.Max(it => it.Order) + 1 : 0 };
			LocalSchedule.Summary.Items.Add(item);
			AddItemToList(item);
			UpdateTotalItems();
			SettingsNotSaved = true;
		}

		private void AddItemToList(SummaryItem summaryItem)
		{
			var item = new SummaryInputItemControl { Data = summaryItem };
			item.LoadData();
			item.ItemDeleted += (o, e) => DeleteItem(o as SummaryInputItemControl);
			item.ItemUp += (o, e) => UpItem(o as SummaryInputItemControl);
			item.ItemDown += (o, e) => DownItem(o as SummaryInputItemControl);
			item.DataChanged += (o, e) => { SettingsNotSaved = true; };
			item.InvestmentChanged += (o, e) => UpdateTotals();
			_inputControls.Add(item);
			xtraScrollableControlInput.Controls.Add(item);
			item.BringToFront();
			xtraScrollableControlInput.ScrollControlIntoView(item);
		}

		private void DeleteItem(SummaryInputItemControl item)
		{
			xtraScrollableControlInput.Controls.Remove(item);
			_inputControls.Remove(item);
			LocalSchedule.Summary.Items.Remove(item.Data);
			ReorderItemControls();
			UpdateTotalItems();
			UpdateTotals();
			SettingsNotSaved = true;
		}

		private void UpItem(SummaryInputItemControl item)
		{
			int itemOrder = item.Data.Order;
			if (itemOrder == 0) return;
			LocalSchedule.Summary.ReorderItems(itemOrder - 1);
			item.Data.Order = itemOrder - 1;
			ReorderItemControls();
			xtraScrollableControlInput.ScrollControlIntoView(item);
			SettingsNotSaved = true;
		}

		private void DownItem(SummaryInputItemControl item)
		{
			int currentIndex = _inputControls.IndexOf(item);
			if (!(currentIndex < (_inputControls.Count - 1))) return;
			SummaryInputItemControl nextItem = _inputControls[currentIndex + 1];
			int itemOrder = nextItem.Data.Order;
			LocalSchedule.Summary.ReorderItems(itemOrder - 1);
			nextItem.Data.Order = itemOrder - 1;
			ReorderItemControls();
			xtraScrollableControlInput.ScrollControlIntoView(item);
			SettingsNotSaved = true;
		}

		private void ReorderItemControls()
		{
			LocalSchedule.Summary.ReorderItems();
			_inputControls.Sort((x, y) => x.Data.Order.CompareTo(y.Data.Order));
			foreach (SummaryInputItemControl itemControl in _inputControls)
			{
				itemControl.UpdateItemNumber();
				itemControl.BringToFront();
			}
		}

		public void UpdateTotalItems()
		{
			laTotalItems.Text = string.Format("Total Items: {0}", LocalSchedule.Summary.Items.Count);
		}

		public void UpdateTotals()
		{
			laInputMonthlyInvestmentTag.Text = LocalSchedule.Summary.TotalMonthly.ToString("$#,##0.00");
			laInputTotalInvestmentTag.Text = LocalSchedule.Summary.TotalTotal.ToString("$#,##0.00");
			spinEditMonthly.Value = LocalSchedule.Summary.EnableTotalsEdit ? LocalSchedule.Summary.MonthlyValue : LocalSchedule.Summary.TotalMonthly;
			spinEditTotal.Value = LocalSchedule.Summary.EnableTotalsEdit ? LocalSchedule.Summary.TotalValue : LocalSchedule.Summary.TotalTotal;
		}

		public void UpdateOutputItems()
		{
			xtraScrollableControlOutput.Controls.Clear();
			if (_inputControls.Count > 0 && _inputControls.All(it => it.Complited))
			{
				pnOutputBorder.Visible = true;
				pnOutputHeader.Visible = true;
				pnOutputSummary.Visible = true;
				pnOutputWarning.Visible = false;
				foreach (var inputControl in _inputControls.Where(it => it.Complited).OrderBy(it => it.Data.Order))
				{
					xtraScrollableControlOutput.Controls.Add(inputControl.OutputItem);
					inputControl.OutputItem.BringToFront();
					inputControl.UpdateOutputItem();
				}
			}
			else
			{
				pnOutputBorder.Visible = false;
				pnOutputHeader.Visible = false;
				pnOutputSummary.Visible = false;
				pnOutputWarning.Visible = true;
			}
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
			using (var from = new FormNewSchedule())
			{
				from.Text = "Save Schedule";
				from.laLogo.Text = "Please set a new name for your Schedule:";
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						if (SaveSchedule(from.ScheduleName))
							Utilities.Instance.ShowInformation("Schedule was saved");
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
			}
		}

		private void checkEdit_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				LocalSchedule.Summary.ShowAdvertiser = checkEditBusinessName.Checked;
				LocalSchedule.Summary.ShowDecisionMaker = checkEditDecisionMaker.Checked;
				LocalSchedule.Summary.ShowPresentationDate = checkEditPresentationDate.Checked;
				LocalSchedule.Summary.ShowFlightDates = checkEditFlightDates.Checked;
				LocalSchedule.Summary.ShowMonthly = checkEditMonthlyInvestmentOutput.Checked;
				LocalSchedule.Summary.ShowTotal = checkEditTotalInvestmentOutput.Checked;
				LocalSchedule.Summary.ShowSignature = checkEditSignatureLine.Checked;
				SettingsNotSaved = true;
			}
		}

		private void checkEditEnableTotalEdit_CheckedChanged(object sender, EventArgs e)
		{
			spinEditMonthly.Enabled = checkEditEnableTotalEdit.Checked;
			spinEditTotal.Enabled = checkEditEnableTotalEdit.Checked;
			if (!checkEditEnableTotalEdit.Checked)
			{
				spinEditMonthly.EditValue = LocalSchedule.Summary.TotalMonthly;
				spinEditTotal.EditValue = LocalSchedule.Summary.TotalTotal;
			}
			if (_allowToSave)
			{
				LocalSchedule.Summary.EnableTotalsEdit = checkEditEnableTotalEdit.Checked;
				if (LocalSchedule.Summary.EnableTotalsEdit)
				{
					LocalSchedule.Summary.MonthlyValue = LocalSchedule.Summary.TotalMonthly;
					LocalSchedule.Summary.TotalValue = LocalSchedule.Summary.TotalValue;
				}
				SettingsNotSaved = true;
			}
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				LocalSchedule.Summary.MonthlyValue = LocalSchedule.Summary.EnableTotalsEdit ? spinEditMonthly.Value : 0;
				SettingsNotSaved = true;
			}
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				LocalSchedule.Summary.TotalValue = LocalSchedule.Summary.EnableTotalsEdit ? spinEditTotal.Value : 0;
				SettingsNotSaved = true;
			}
		}

		private void xtraTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (e.Page == xtraTabPageInput)
			{
				Controller.Instance.SummaryAddItem.Enabled = true;
				Controller.Instance.SummaryPreview.Enabled = false;
				Controller.Instance.SummaryEmail.Enabled = false;
				Controller.Instance.SummaryPowerPoint.Enabled = false;
			}
			else if (e.Page == xtraTabPageOutput)
			{
				Controller.Instance.SummaryAddItem.Enabled = false;
				Controller.Instance.SummaryPreview.Enabled = true;
				Controller.Instance.SummaryEmail.Enabled = true;
				Controller.Instance.SummaryPowerPoint.Enabled = true;
				UpdateOutputItems();
			}
		}

		#region Output Stuff
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
					return LocalSchedule.BusinessName;
				return String.Empty;
			}
		}

		public string DecisionMaker
		{
			get
			{
				if (checkEditDecisionMaker.Checked)
					return LocalSchedule.DecisionMaker;
				return String.Empty;
			}
		}

		public string PresentationDate
		{
			get
			{
				if (checkEditPresentationDate.Checked)
					return LocalSchedule.PresentationDate.ToString("MM/dd/yy");
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
			get { return _inputControls.Where(it => it.Complited).Select(it => it.OutputMonthlyValue.HasValue ? it.OutputMonthlyValue.Value.ToString("$#,##0") : String.Empty).ToArray(); }
		}

		public string[] TotalValues
		{
			get { return _inputControls.Where(it => it.Complited).Select(it => it.OutputTotalValue.HasValue ? it.OutputTotalValue.Value.ToString("$#,##0") : String.Empty).ToArray(); }
		}

		public string TotalMonthlyValue
		{
			get { return checkEditMonthlyInvestmentOutput.Checked ? (LocalSchedule.Summary.EnableTotalsEdit ? LocalSchedule.Summary.MonthlyValue : LocalSchedule.Summary.TotalMonthly).ToString("$#,##0.00") : string.Empty; }
		}

		public string TotalTotalValue
		{
			get { return checkEditTotalInvestmentOutput.Checked ? (LocalSchedule.Summary.EnableTotalsEdit ? LocalSchedule.Summary.TotalValue : LocalSchedule.Summary.TotalTotal).ToString("$#,##0.00") : string.Empty; }
		}

		public bool ShowMonthlyHeader
		{
			get { return _inputControls.Where(it => it.Complited).Any(it => it.ShowMonthly); }
		}

		public bool ShowTotalHeader
		{
			get { return _inputControls.Where(it => it.Complited).Any(it => it.ShowTotal); }
		}

		public void Output()
		{
			SaveSchedule();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				formProgress.Show();
				AdSchedulePowerPointHelper.Instance.AppendSummary();
				formProgress.Close();
			}
			using (var formOutput = new FormSlideOutput())
			{
				if (formOutput.ShowDialog() != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
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
				if (File.Exists(tempFileName))
					using (var formEmail = new FormEmail())
					{
						formEmail.Text = "Email this Summary";
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
			SaveSchedule();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				string tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
				AdSchedulePowerPointHelper.Instance.PrepareSummaryEmail(tempFileName);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formPreview = new FormPreview())
					{
						formPreview.Text = "Preview Summary";
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
	}
}