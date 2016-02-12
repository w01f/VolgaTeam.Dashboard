﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Dictionaries;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.Objects.Themes;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using Asa.Common.GUI.ToolForms;

namespace Asa.Common.GUI.Summary
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

		public abstract CheckEdit TableOutputToggle { get; }

		protected BaseScheduleSettings ScheduleSettings
		{
			get { return Schedule.BaseSettings; }
		}

		protected IEnumerable<TItemControl> OrderedItems
		{
			get { return CustomOrder ? _inputControls.OrderBy(it => it.Data.Order).ToList() : _inputControls; }
		}

		protected SummaryBaseControl()
		{
			SetClickEventHandler(this);

			checkEditBusinessName.CheckedChanged += checkEdit_CheckedChanged;
			checkEditDecisionMaker.CheckedChanged += checkEdit_CheckedChanged;
			checkEditTotalInvestment.CheckedChanged += checkEdit_CheckedChanged;
			checkEditPresentationDate.CheckedChanged += checkEdit_CheckedChanged;
			checkEditFlightDates.CheckedChanged += checkEdit_CheckedChanged;
			checkEditMonthlyInvestment.CheckedChanged += checkEdit_CheckedChanged;
			TableOutputToggle.CheckedChanged += checkEdit_CheckedChanged;
			comboBoxEditHeader.EditValueChanged += checkEdit_CheckedChanged;
			hyperLinkEditInfoContract.OpenLink += hyperLinkEditInfoContract_OpenLink;

			PowerPointButton.Click += (o, e) => Output();
			PreviewButton.Click += (o, e) => Preview();
			EmailButton.Click += (o, e) => Email();
			if (PdfButton != null)
				PdfButton.Click += (o, e) => OutputPdf();
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

		public virtual void LoadData(bool quickLoad)
		{
			_allowToSave = false;
			if (!quickLoad)
			{
				comboBoxEditHeader.Properties.Items.Clear();
				comboBoxEditHeader.Properties.Items.AddRange(ListManager.Instance.SimpleSummaryLists.Headers);
				checkEditBusinessName.Checked = Settings.ShowAdvertiser;
				checkEditDecisionMaker.Checked = Settings.ShowDecisionMaker;
				checkEditPresentationDate.Checked = Settings.ShowPresentationDate;
				checkEditFlightDates.Checked = Settings.ShowFlightDates;
				checkEditMonthlyInvestment.Checked = Settings.ShowMonthly;
				checkEditTotalInvestment.Checked = Settings.ShowTotal;
				TableOutputToggle.Checked = Settings.TableOutput;
				if (String.IsNullOrEmpty(Settings.SlideHeader))
				{
					if (comboBoxEditHeader.Properties.Items.Count > 0)
						comboBoxEditHeader.SelectedIndex = 0;
				}
				else
				{
					var index = comboBoxEditHeader.Properties.Items.IndexOf(Settings.SlideHeader);
					comboBoxEditHeader.SelectedIndex = index >= 0 ? index : 0;
				}

				hyperLinkEditInfoContract.Enabled = ContractTemplateFolder != null && ContractTemplateFolder.ExistsLocal();
			}
			LoadItems(quickLoad);
			UpdateTotalItems();
			UpdateTotals();
			UpdateOutput();
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
			item.DataChanged += (o, e) =>
			{
				SettingsNotSaved = true;
				UpdateTotalItems();
				UpdateOutput();
			};
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
			laTotalItems.Text = String.Format("Total Items: {0}", _inputControls.Count());
			if (SlidesCount > 0)
			{
				laSlideCount.Visible = true;
				laSlideCount.Text = String.Format("Estimated Slide Count: {0}", SlidesCount);
			}
			else
				laSlideCount.Visible = false;
		}

		protected abstract void UpdateTotals();

		protected void UpdateOutput()
		{
			var outputEnabled = ItemsCount > 0;
			PowerPointButton.Enabled = outputEnabled;
			PreviewButton.Enabled = outputEnabled;
			EmailButton.Enabled = outputEnabled;
			if (PdfButton != null)
				PdfButton.Enabled = outputEnabled;
		}

		public virtual void OpenHelp()
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
			Settings.ShowDecisionMaker = checkEditDecisionMaker.Checked;
			Settings.ShowPresentationDate = checkEditPresentationDate.Checked;
			Settings.ShowFlightDates = checkEditFlightDates.Checked;
			Settings.ShowMonthly = checkEditMonthlyInvestment.Checked;
			Settings.ShowTotal = checkEditTotalInvestment.Checked;
			Settings.TableOutput = TableOutputToggle.Checked;
			Settings.SlideHeader = comboBoxEditHeader.EditValue as String;
			UpdateTotalItems();
			SettingsNotSaved = true;
		}

		private void hyperLinkEditInfoContract_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormContractSettings())
			{
				form.checkEditShowSignatureLine.Checked = ContractSettings.ShowSignatureLine;
				form.checkEditShowRatesExpiration.Checked = ContractSettings.RateExpirationDate.HasValue;
				form.checkEditShowDisclaimer.Checked = ContractSettings.ShowDisclaimer;
				form.dateEditRatesExpirationDate.EditValue = ContractSettings.RateExpirationDate;
				if (form.ShowDialog() != DialogResult.OK) return;
				ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
				ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
				ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;
				SettingsNotSaved = true;
			}
		}

		#region Output Stuff
		public abstract ButtonItem PowerPointButton { get; }
		public abstract ButtonItem PreviewButton { get; }
		public abstract ButtonItem EmailButton { get; }
		public abstract ButtonItem PdfButton { get; }
		public abstract Theme SelectedTheme { get; }
		protected abstract void Output();
		protected abstract void OutputPdf();
		protected abstract bool CheckPowerPointRunning();
		protected abstract void PreparePreview(string tempFileName);
		protected abstract void ShowEmail(string tempFileName);
		protected abstract void ShowPreview(string tempFileName);

		public virtual StorageDirectory ContractTemplateFolder
		{
			get { return null; }
		}
		public ContractSettings ContractSettings
		{
			get { return Settings.ContractSettings; }
		}

		public int ItemsCount
		{
			get { return _inputControls.Count(it => it.Complited); }
		}

		public int SlidesCount
		{
			get
			{
				if (!TableOutput)
				{
					var main = ItemsCount / 5;
					var rest = ItemsCount % 5;
					return main + (rest > 0 ? 1 : 0);
				}
				else
				{
					var main = ItemsCount / 18;
					var rest = ItemsCount % 18;
					return main + (rest > 0 ? 1 : 0);
				}
			}
		}

		public string Title
		{
			get { return (comboBoxEditHeader.EditValue as String) ?? String.Empty; }
		}

		public string SummaryData
		{
			get
			{
				var values = new List<string>();
				if (!String.IsNullOrEmpty(PresentationDate))
					values.Add(PresentationDate);
				if (!String.IsNullOrEmpty(Advertiser))
					values.Add(Advertiser);
				if (!String.IsNullOrEmpty(DecisionMaker))
					values.Add(DecisionMaker);
				if (!String.IsNullOrEmpty(CampaignDates))
					values.Add(CampaignDates);
				if (!String.IsNullOrEmpty(TotalMonthlyValue))
					values.Add(String.Format("Monthly Investment: {0}", TotalMonthlyValue));
				if (!String.IsNullOrEmpty(TotalTotalValue))
					values.Add(String.Format("Total Investment: {0}", TotalTotalValue));
				return String.Join("   |   ", values);
			}
		}

		public string Advertiser
		{
			get
			{
				if (checkEditBusinessName.Checked)
					return ScheduleSettings.BusinessName;
				return String.Empty;
			}
		}

		public string DecisionMaker
		{
			get
			{
				if (checkEditDecisionMaker.Checked)
					return ScheduleSettings.DecisionMaker;
				return String.Empty;
			}
		}

		public string PresentationDate
		{
			get
			{
				if (checkEditPresentationDate.Checked && ScheduleSettings.PresentationDate.HasValue)
					return ScheduleSettings.PresentationDate.Value.ToString("MM/dd/yy");
				return String.Empty;
			}
		}

		public string CampaignDates
		{
			get
			{
				if (checkEditFlightDates.Checked)
					return ScheduleSettings.FlightDates;
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

		public bool TableOutput
		{
			get { return TableOutputToggle.Checked; }
		}

		public int ItemsPerTable
		{
			get { return ItemsCount > 18 ? 18 : ItemsCount; }
		}

		public abstract bool ShowIcons { get; }

		public string[] TableIcons
		{
			get { return OrderedItems.Where(it => it.Complited).Select(it => it.ItemIcon).ToArray(); }
		}

		public List<Dictionary<string, string>> OutputReplacementsLists { get; private set; }

		public void PopulateReplacementsList()
		{
			if (OutputReplacementsLists == null)
				OutputReplacementsLists = new List<Dictionary<string, string>>();
			OutputReplacementsLists.Clear();
			var recordsCount = ItemsCount;
			var monthlyValues = OrderedItems.Where(it => it.Complited).Select(it => it.OutputMonthlyValue.HasValue ? it.OutputMonthlyValue.Value.ToString("$#,##0") : String.Empty).ToArray();
			var totalValues = OrderedItems.Where(it => it.Complited).Select(it => it.OutputTotalValue.HasValue ? it.OutputTotalValue.Value.ToString("$#,##0") : String.Empty).ToArray();
			for (var i = 0; i < recordsCount; i += ItemsPerTable)
			{
				var slideRows = new Dictionary<string, string>();
				for (var j = 0; j < ItemsPerTable; j++)
				{
					if ((i + j) < recordsCount)
					{
						slideRows.Add(String.Format("Product{0}", j + 1), ItemTitles[i + j]);
						var details = new List<string>();
						if (!String.IsNullOrEmpty(ItemDetails[i + j]))
							details.Add(ItemDetails[i + j]);
						if (monthlyValues.Any() && !String.IsNullOrEmpty(monthlyValues[i + j]))
							details.Add(String.Format("({0}/mo)", monthlyValues[i + j]));
						if (totalValues.Any() && !String.IsNullOrEmpty(totalValues[i + j]))
							details.Add(String.Format("({0} inv)", totalValues[i + j]));
						slideRows.Add(String.Format("Details{0}", j + 1), String.Join(" ", details));
					}
					else
					{
						slideRows.Add(String.Format("Product{0}", j + 1), "DeleteRow");
						slideRows.Add(String.Format("Details{0}", j + 1), "DeleteRow");
					}
				}
				OutputReplacementsLists.Add(slideRows);
			}
		}

		protected void Email()
		{
			SaveSchedule();
			if (!CheckPowerPointRunning()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Email...");
			FormProgress.ShowProgress();
			var tempFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			PreparePreview(tempFileName);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			ShowEmail(tempFileName);
		}

		protected void Preview()
		{
			SaveSchedule();
			if (!CheckPowerPointRunning()) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Email...");
			FormProgress.ShowProgress();
			string tempFileName = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			PreparePreview(tempFileName);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			ShowPreview(tempFileName);
		}
		#endregion
	}
}