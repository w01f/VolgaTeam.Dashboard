using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.AdSchedule.Controls.ToolForms;
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
				checkEditEnableTotalEdit.Checked = LocalSchedule.Summary.EnableTotalsEdit;
				checkEditMonthlyInvestmentOutput.Checked = LocalSchedule.Summary.ShowMonthly;
				checkEditTotalInvestmentOutput.Checked = LocalSchedule.Summary.ShowTotal;

				_inputControls.Clear();
				xtraScrollableControlInput.Controls.Clear();
				foreach (var summaryItem in LocalSchedule.Summary.Items.OrderBy(it => it.Order))
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
			SetClickEventHandler(item);
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
			var nextItem = _inputControls[currentIndex + 1];
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
			pnInputMonthlyInvestments.Visible = _inputControls.Any(ic => ic.ShowMonthly);
			pnInputTotalInvestments.Visible = _inputControls.Any(ic => ic.ShowTotal);
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
				var monthlyHeaderVisible = _inputControls.Any(it => it.OutputItem.MonthlyVisible);
				var totalHeaderVisible = _inputControls.Any(it => it.OutputItem.TotalVisible);
				foreach (var inputControl in _inputControls.Where(it => it.Complited).OrderBy(it => it.Data.Order))
				{
					inputControl.OutputItem.TotalVisible = totalHeaderVisible;
					inputControl.OutputItem.MonthlyVisible = monthlyHeaderVisible;
				}
				if (monthlyHeaderVisible && totalHeaderVisible)
				{
					laMonthly.Visible = true;
					laMonthly.Width = 150;
					laMonthly.Text = "Monthly$:";
					laMonthly.TextAlign = ContentAlignment.MiddleLeft;
					laTotal.Visible = true;
					laTotal.Width = 150;
					laTotal.Text = "Total$:";
					laTotal.TextAlign = ContentAlignment.MiddleLeft;
				}
				else if (monthlyHeaderVisible)
				{
					laMonthly.Visible = true;
					laMonthly.Width = 300;
					laMonthly.Text = "Monthly Investment:";
					laMonthly.TextAlign = ContentAlignment.MiddleRight;
					laTotal.Visible = false;
				}
				else if (totalHeaderVisible)
				{
					laMonthly.Visible = false;
					laTotal.Visible = true;
					laTotal.Width = 300;
					laTotal.Text = "Total Investment:";
					laTotal.TextAlign = ContentAlignment.MiddleRight;
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
			if (!_allowToSave) return;
			LocalSchedule.Summary.ShowAdvertiser = checkEditBusinessName.Checked;
			LocalSchedule.Summary.ShowPresentationDate = checkEditPresentationDate.Checked;
			LocalSchedule.Summary.ShowFlightDates = checkEditFlightDates.Checked;
			LocalSchedule.Summary.ShowMonthly = checkEditMonthlyInvestmentOutput.Checked;
			LocalSchedule.Summary.ShowTotal = checkEditTotalInvestmentOutput.Checked;
			SettingsNotSaved = true;
		}

		private void checkEditEnableTotalEdit_CheckedChanged(object sender, EventArgs e)
		{
			spinEditMonthly.Enabled = checkEditEnableTotalEdit.Checked;
			spinEditMonthly.Properties.ReadOnly = !checkEditEnableTotalEdit.Checked;
			spinEditTotal.Enabled = checkEditEnableTotalEdit.Checked;
			spinEditTotal.Properties.ReadOnly = !checkEditEnableTotalEdit.Checked;
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
			if (!_allowToSave) return;
			LocalSchedule.Summary.MonthlyValue = LocalSchedule.Summary.EnableTotalsEdit ? spinEditMonthly.Value : 0;
			SettingsNotSaved = true;
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
				Controller.Instance.SummaryTheme.Enabled = false;
			}
			else if (e.Page == xtraTabPageOutput)
			{
				Controller.Instance.SummaryAddItem.Enabled = false;
				Controller.Instance.SummaryPreview.Enabled = true;
				Controller.Instance.SummaryEmail.Enabled = true;
				Controller.Instance.SummaryPowerPoint.Enabled = true;
				Controller.Instance.SummaryTheme.Enabled = true;
				UpdateOutputItems();
			}
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
			get { return checkEditMonthlyInvestmentOutput.Checked ? (LocalSchedule.Summary.EnableTotalsEdit ? LocalSchedule.Summary.MonthlyValue : LocalSchedule.Summary.TotalMonthly).ToString("$#,##0.00") : string.Empty; }
		}

		public string TotalTotalValue
		{
			get { return checkEditTotalInvestmentOutput.Checked ? (LocalSchedule.Summary.EnableTotalsEdit ? LocalSchedule.Summary.TotalValue : LocalSchedule.Summary.TotalTotal).ToString("$#,##0.00") : string.Empty; }
		}

		public bool ShowMonthlyHeader
		{
			get { return _inputControls.Where(it => it.Complited).Any(it => it.OutputMonthlyValue.HasValue); }
		}

		public bool ShowTotalHeader
		{
			get { return _inputControls.Where(it => it.Complited).Any(it => it.OutputTotalValue.HasValue); }
		}

		public void Output()
		{
			SaveSchedule();
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
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formEmail = new FormEmail(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
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
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				formProgress.Close();
				if (File.Exists(tempFileName))
					using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater))
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
					}
			}
		}
		#endregion
	}
}