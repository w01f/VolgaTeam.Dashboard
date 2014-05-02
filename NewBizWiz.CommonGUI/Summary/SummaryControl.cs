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
		protected SummaryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}

	[ToolboxItem(false)]
	public abstract class SummaryBaseControl<TItemControl> : SummaryControl, ISummaryControl where TItemControl : ISummaryItemControl
	{
		protected readonly List<TItemControl> _inputControls = new List<TItemControl>();
		protected bool _allowToSave;

		public abstract ISummarySchedule Schedule { get; }
		public abstract BaseSummarySettings Settings { get; }
		public abstract List<CustomSummaryItem> Items { get; }
		public abstract HelpManager HelpManager { get; }

		protected abstract bool CustomOrder { get; }

		protected IEnumerable<TItemControl> OrderedItems
		{
			get { return CustomOrder ? _inputControls.OrderBy(it => it.Data.Order).ToList() : _inputControls; }
		}

		protected SummaryBaseControl()
		{
			SetClickEventHandler(this);
			checkEditBusinessName.CheckedChanged += checkEdit_CheckedChanged;
			checkEditTotalInvestment.CheckedChanged += checkEdit_CheckedChanged;
			checkEditPresentationDate.CheckedChanged += checkEdit_CheckedChanged;
			checkEditFlightDates.CheckedChanged += checkEdit_CheckedChanged;
			checkEditMonthlyInvestment.CheckedChanged += checkEdit_CheckedChanged;
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result;
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
			_allowToSave = false;
			if (!quickLoad)
			{
				checkEditBusinessName.Checked = Settings.ShowAdvertiser;
				checkEditPresentationDate.Checked = Settings.ShowPresentationDate;
				checkEditFlightDates.Checked = Settings.ShowFlightDates;
				checkEditMonthlyInvestment.Checked = Settings.ShowMonthly;
				checkEditTotalInvestment.Checked = Settings.ShowTotal;

			}
			LoadItems(quickLoad);
			UpdateTotalItems();
			UpdateTotals();
			_allowToSave = true;
			SettingsNotSaved = false;
		}

		protected abstract bool SaveSchedule(string scheduleName = "");

		protected virtual void LoadItems(bool quickLoad)
		{
			if (!quickLoad)
			{
				_inputControls.Clear();
				foreach (var summaryItem in Items)
				{
					AddItemToList(summaryItem);
					Application.DoEvents();
				}
			}
			else
			{
				foreach (var summaryItem in Items)
				{
					var inputControl = _inputControls.FirstOrDefault(ic => ic.Data.Id.Equals(summaryItem.Id));
					if (inputControl != null)
						inputControl.Data = summaryItem;
					Application.DoEvents();
				}
			}
		}

		protected TItemControl AddItemToList(CustomSummaryItem summaryItem)
		{
			var item = Activator.CreateInstance<TItemControl>();
			item.Data = summaryItem;
			InitItem(item);
			_inputControls.Add(item);
			var control = item as Control;
			if (control == null) return item;
			SetClickEventHandler(control);
			return item;
		}

		protected virtual void InitItem(TItemControl item)
		{
			item.LoadData();
			item.DataChanged += (o, e) => { SettingsNotSaved = true; };
			item.InvestmentChanged += (o, e) => UpdateTotals();
		}

		protected void UpdateControlsInList(Control focussed)
		{
			xtraScrollableControlInput.SuspendLayout();
			xtraScrollableControlInput.Controls.Clear();
			var items = OrderedItems.OfType<Control>().Reverse().ToArray();
			xtraScrollableControlInput.Controls.AddRange(items);
			xtraScrollableControlInput.ResumeLayout(true);
			if (focussed != null)
				xtraScrollableControlInput.ScrollControlIntoView(focussed);
		}

		protected void UpdateTotalItems()
		{
			laTotalItems.Text = string.Format("Total Items: {0}", _inputControls.Count());
		}

		protected abstract void UpdateTotals();

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
			Settings.ShowAdvertiser = checkEditBusinessName.Checked;
			Settings.ShowPresentationDate = checkEditPresentationDate.Checked;
			Settings.ShowFlightDates = checkEditFlightDates.Checked;
			Settings.ShowMonthly = checkEditMonthlyInvestment.Checked;
			Settings.ShowTotal = checkEditTotalInvestment.Checked;
			SettingsNotSaved = true;
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
			get { return OrderedItems.Where(it => it.Complited).Select(it => it.OutputItemTitle).ToArray(); }
		}

		public string[] ItemDetails
		{
			get { return OrderedItems.Where(it => it.Complited).Select(it => it.ItemDetailOutput).ToArray(); }
		}

		public string[] MonthlyValues
		{
			get
			{
				if (ShowMonthlyHeader && ShowTotalHeader)
					return OrderedItems.Where(it => it.Complited).Select(it => it.OutputMonthlyValue.HasValue ? it.OutputMonthlyValue.Value.ToString("$#,##0") : String.Empty).ToArray();
				return null;
			}
		}

		public string[] TotalValues
		{
			get
			{
				if (ShowMonthlyHeader && !ShowTotalHeader)
					return OrderedItems.Where(it => it.Complited).Select(it => it.OutputMonthlyValue.HasValue ? it.OutputMonthlyValue.Value.ToString("$#,##0") : String.Empty).ToArray();
				return OrderedItems.Where(it => it.Complited).Select(it => it.OutputTotalValue.HasValue ? it.OutputTotalValue.Value.ToString("$#,##0") : String.Empty).ToArray();
			}
		}

		public abstract string TotalMonthlyValue { get; }

		public abstract string TotalTotalValue { get; }

		public bool ShowMonthlyHeader
		{
			get { return OrderedItems.Where(it => it.Complited).Any(it => it.OutputMonthlyValue.HasValue); }
		}

		public bool ShowTotalHeader
		{
			get { return OrderedItems.Where(it => it.Complited).Any(it => it.OutputTotalValue.HasValue); }
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