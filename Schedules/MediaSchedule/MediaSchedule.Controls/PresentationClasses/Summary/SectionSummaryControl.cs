using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.CommonGUI.Preview;
using Asa.CommonGUI.RetractableBar;
using Asa.CommonGUI.Summary;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;
using Asa.MediaSchedule.Controls.BusinessClasses;
using Asa.MediaSchedule.Controls.InteropClasses;
using Asa.MediaSchedule.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	public partial class SectionSummaryControl : UserControl
	{
		public SectionSummaryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}

	[ToolboxItem(false)]
	public abstract class SectionSummaryBaseControl<TItemControl, TSettingsControl> : SectionSummaryControl, ISummaryControl, ISectionSummaryControl
		where TItemControl : ISummaryItemControl
		where TSettingsControl : BaseSummaryInfoControl
	{
		private bool _allowToSave;
		protected readonly List<TItemControl> _inputControls = new List<TItemControl>();

		protected abstract bool CustomOrder { get; }
		public abstract List<CustomSummaryItem> Items { get; }

		public SectionSummary SectionData { get; private set; }
		public List<XtraTabPage> SettingsPages { get; private set; }
		public List<ButtonInfo> BarButtons { get; private set; }
		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<EventArgs> TotalsUpdated;

		#region Calculated properties
		protected IEnumerable<TItemControl> OrderedItems
		{
			get { return CustomOrder ? _inputControls.OrderBy(it => it.Data.Order).ToList() : _inputControls; }
		}

		protected BaseSummarySettings SummarySettings
		{
			get { return (BaseSummarySettings)SectionData.Content; }
		}
		#endregion

		protected SectionSummaryBaseControl()
		{
			SettingsPages = new List<XtraTabPage>();
			BarButtons = new List<ButtonInfo>();

			InitSettingsControls();

			comboBoxEditHeader.Properties.Items.Clear();
			comboBoxEditHeader.Properties.Items.AddRange(Core.Dashboard.ListManager.Instance.SimpleSummaryLists.Headers);
			comboBoxEditHeader.EditValueChanged += OnSettingsChanged;
		}

		public virtual void LoadData(SectionSummary sectionData, bool quickLoad = false)
		{
			SectionData = sectionData;
			Text = SectionData.Parent.Name;

			comboBoxEditHeader.EditValue = String.IsNullOrEmpty(SummarySettings.SlideHeader) ?
				Core.Dashboard.ListManager.Instance.SimpleSummaryLists.Headers.FirstOrDefault() :
				SummarySettings.SlideHeader;

			LoadItems(quickLoad);
			UpdateTotalItems();

			LoadSettingsData();
		}

		public void SaveData()
		{
			SummarySettings.SlideHeader = comboBoxEditHeader.EditValue as String;
		}

		protected void RaiseDataChanged()
		{
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		#region Settings Management
		private void InitSettingsControls()
		{
			SettingsPages.Clear();
			BarButtons.Clear();

			var infoSettingsControl = Activator.CreateInstance<TSettingsControl>();
			infoSettingsControl.DataChanged += OnSettingsChanged;
			SettingsPages.Add(infoSettingsControl);
			var infoSettingsButton = new ButtonInfo
			{
				Tooltip = "Edit Summary Settings",
				Logo = Resources.SummaryOptionsInfo,
				Action = () => { infoSettingsControl.TabControl.SelectedTabPage = infoSettingsControl; }
			};
			BarButtons.Add(infoSettingsButton);
		}

		private void LoadSettingsData()
		{
			SettingsPages.OfType<ISummarySettingsEditorControl>().ToList().ForEach(e => e.LoadData(SummarySettings));
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			UpdateTotalItems();
			RaiseDataChanged();
		}
		#endregion

		#region Items Management
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
			return item;
		}

		protected virtual void InitItem(TItemControl item)
		{
			item.LoadData();
			item.DataChanged += (o, e) =>
			{
				UpdateTotalItems();
				RaiseDataChanged();
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
		}

		protected void UpdateTotals()
		{
			SettingsPages.OfType<BaseSummaryInfoControl>().First().UpdateTotals();
		}
		#endregion

		#region ISummaryControl Implementation
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
				return SummarySettings.ShowAdvertiser ?
					SectionData.Parent.ParentSchedule.BusinessName :
					String.Empty;
			}
		}

		public string DecisionMaker
		{
			get
			{
				return SummarySettings.ShowDecisionMaker ?
					SectionData.Parent.ParentSchedule.DecisionMaker :
					String.Empty;
			}
		}

		public string PresentationDate
		{
			get
			{
				return SummarySettings.ShowPresentationDate ?
					SectionData.Parent.ParentSchedule.PresentationDate.Value.ToString("MM/dd/yy") :
					String.Empty;
			}
		}

		public string CampaignDates
		{
			get
			{
				return SummarySettings.ShowFlightDates ?
					SectionData.Parent.ParentSchedule.FlightDates :
					String.Empty;
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

		public StorageDirectory ContractTemplateFolder
		{
			get { return BusinessObjects.Instance.OutputManager.ContractTemplateFolder; }
		}

		public ContractSettings ContractSettings
		{
			get { return SummarySettings.ContractSettings; }
		}

		public Theme SelectedTheme
		{
			get
			{
				return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Summaries)
					.FirstOrDefault(t =>
				  t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summaries)) ||
				  String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summaries)));
			}
		}

		public bool TableOutput
		{
			get { return SummarySettings.TableOutput; }
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
		#endregion

		#region Output Stuff
		public bool ReadyForOutput
		{
			get { return ItemsCount > 0; }
		}

		public void GeneratePowerPointOutput()
		{
			RegularMediaSchedulePowerPointHelper.Instance.AppendSummary(this);
		}

		public PreviewGroup GeneratePreview()
		{
			var previewGroup = new PreviewGroup
			{
				Name = SectionData.Parent.Name.Replace("&", "&&"),
				PresentationSourcePath = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareSummaryEmail(previewGroup.PresentationSourcePath, this);
			return previewGroup;
		}
		#endregion
	}
}