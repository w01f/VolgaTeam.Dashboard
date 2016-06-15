using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Common.Dictionaries;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.Summary;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Output;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	[ToolboxItem(false)]
	//public partial class SectionSummaryControl : UserControl, ISummaryControl, ISectionEditorControl, ISectionOutputControl
	public partial class SectionSummaryControl : XtraTabPage, ISummaryControl, ISectionEditorControl, ISectionOutputControl
	{
		protected SectionContainer _sectionContainer;

		protected readonly List<SummaryCustomItemControl> _inputControls = new List<SummaryCustomItemControl>();

		public List<CustomSummaryItem> Items => SummaryContent.Items;

		public SectionEditorType EditorType => SectionEditorType.CustomSummary;

		#region Calculated properties
		protected IEnumerable<SummaryCustomItemControl> OrderedItems
		{
			get { return _inputControls.OrderBy(it => it.Data.Order).ToList(); }
		}
		private CustomSummaryContent SummaryContent => _sectionContainer.SectionData.Summary.CustomSummary;
		private BaseSummarySettings SummarySettings => _sectionContainer.SectionData.Summary.CustomSummary;
		#endregion

		public SectionSummaryControl(SectionContainer sectionContainer)
		{
			InitializeComponent();
			_sectionContainer = sectionContainer;
			Text = "Summary Slide";
			buttonXAddItem.Visible = true;
			buttonXAddItem.Click += OnAddItem;
			if ((CreateGraphics()).DpiX > 96)
			{
				laHeaderTitle.Font = new Font(laHeaderTitle.Font.FontFamily, laHeaderTitle.Font.Size - 2, laHeaderTitle.Font.Style);
				laTotalItems.Font = new Font(laTotalItems.Font.FontFamily, laTotalItems.Font.Size - 2, laTotalItems.Font.Style);
				buttonXAddItem.Font = new Font(buttonXAddItem.Font.FontFamily, buttonXAddItem.Font.Size - 2, buttonXAddItem.Font.Style);
			}
		}

		#region ISectionEditorControl Memebers
		public void InitControls()
		{
			comboBoxEditHeader.Properties.Items.Clear();
			comboBoxEditHeader.Properties.Items.AddRange(ListManager.Instance.SimpleSummaryLists.Headers);
			comboBoxEditHeader.EditValueChanged += OnSettingsChanged;
		}

		public virtual void Release()
		{
			_inputControls.ForEach(i => i.Release());
			_inputControls.Clear();
			xtraScrollableControlInput.Controls.Clear();
			_sectionContainer = null;
		}
		#endregion

		public void LoadData(bool quickLoad = true)
		{
			comboBoxEditHeader.EditValue = String.IsNullOrEmpty(SummarySettings.SlideHeader) ?
				ListManager.Instance.SimpleSummaryLists.Headers.FirstOrDefault() :
				SummarySettings.SlideHeader;

			LoadItems(quickLoad);
			UpdateTotalItems();
			if (!quickLoad)
				UpdateControlsInList(OrderedItems.OfType<Control>().FirstOrDefault());
		}

		public void SaveData()
		{
			SummarySettings.SlideHeader = comboBoxEditHeader.EditValue as String;
		}

		protected void RaiseDataChanged()
		{
			_sectionContainer.RaiseDataChanged();
		}

		#region Settings Management
		private void OnSettingsChanged(object sender, EventArgs e)
		{
			UpdateTotalItems();
			RaiseDataChanged();
		}
		#endregion

		#region Items Management
		private void LoadItems(bool quickLoad)
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

		private SummaryCustomItemControl AddItemToList(CustomSummaryItem summaryItem)
		{
			var item = new SummaryCustomItemControl();
			item.Data = summaryItem;
			InitItem(item);
			_inputControls.Add(item);
			return item;
		}

		private void InitItem(SummaryCustomItemControl item)
		{
			item.LoadData();
			item.DataChanged += (o, e) =>
			{
				UpdateTotalItems();
				RaiseDataChanged();
			};
			item.InvestmentChanged += (o, e) => UpdateTotals();
			item.ItemPositionChanged += ItemOnItemPositionChanged;
			item.ItemDeleted += ItemOnItemDeleted;
		}

		private void UpdateControlsInList(Control focussed)
		{
			xtraScrollableControlInput.SuspendLayout();
			xtraScrollableControlInput.Controls.Clear();
			var items = OrderedItems.OfType<Control>().Reverse().ToArray();
			xtraScrollableControlInput.Controls.AddRange(items);
			xtraScrollableControlInput.ResumeLayout(true);
			if (focussed != null)
				xtraScrollableControlInput.ScrollControlIntoView(focussed);
		}

		public void UpdateTotalItems()
		{
			laTotalItems.Text = String.Format("Total Items: {0}", _inputControls.Count());
		}

		private void UpdateTotals()
		{
			RaiseDataChanged();
		}

		private void ItemOnItemDeleted(object sender, SummaryItemEventArgs e)
		{
			SummaryContent.DeleteItem(e.SummaryItem.Data);
			_inputControls.Remove((SummaryCustomItemControl)e.SummaryItem);
			SummaryContent.ReorderItems();
			UpdateControlsInList(OrderedItems.OfType<Control>().FirstOrDefault());
			UpdateNumbers();
			UpdateTotalItems();
			UpdateTotals();
			RaiseDataChanged();
		}

		private void ItemOnItemPositionChanged(object sender, SummaryItemEventArgs e)
		{
			SummaryContent.ReorderItems();
			UpdateNumbers();
			UpdateControlsInList(e.SummaryItem as SummaryCustomItemControl);
			RaiseDataChanged();
		}

		private void UpdateNumbers()
		{
			foreach (var itemControl in _inputControls)
				itemControl.UpdateNumber();
		}

		private void OnAddItem(object sender, EventArgs e)
		{
			var newItemData = SummaryContent.AddItem();
			var focussed = AddItemToList(newItemData);
			UpdateControlsInList(focussed);
			UpdateTotalItems();
			RaiseDataChanged();
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

		public string Advertiser => SummarySettings.ShowAdvertiser ?
			_sectionContainer.SectionData.ParentScheduleSettings.BusinessName :
			String.Empty;

		public string DecisionMaker => SummarySettings.ShowDecisionMaker ?
			_sectionContainer.SectionData.ParentScheduleSettings.DecisionMaker :
			String.Empty;

		public string PresentationDate => SummarySettings.ShowPresentationDate ?
			_sectionContainer.SectionData.ParentScheduleSettings.PresentationDate.Value.ToString("MM/dd/yy") :
			String.Empty;

		public string CampaignDates => SummarySettings.ShowFlightDates ?
			_sectionContainer.SectionData.ParentScheduleSettings.FlightDates :
			String.Empty;

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

		public string TotalMonthlyValue => SummaryContent.ShowMonthly && (SummaryContent.MonthlyValue.HasValue || SummaryContent.TotalMonthly.HasValue) ?
			(SummaryContent.MonthlyValue ?? SummaryContent.TotalMonthly).Value.ToString("$#,##0.00") :
			String.Empty;

		public string TotalTotalValue => SummaryContent.ShowTotal && (SummaryContent.TotalValue.HasValue || SummaryContent.TotalTotal.HasValue) ?
			(SummaryContent.TotalValue ?? SummaryContent.TotalTotal).Value.ToString("$#,##0.00") :
			String.Empty;

		public bool ShowMonthlyHeader
		{
			get { return OrderedItems.Where(it => it.Complited).Any(it => it.OutputMonthlyValue.HasValue); }
		}

		public bool ShowTotalHeader
		{
			get { return OrderedItems.Where(it => it.Complited).Any(it => it.OutputTotalValue.HasValue); }
		}

		public StorageDirectory ContractTemplateFolder => BusinessObjects.Instance.OutputManager.ContractTemplateFolder;

		public ContractSettings ContractSettings => _sectionContainer.SectionData.ContractSettings;

		public Theme SelectedTheme
		{
			get
			{
				return BusinessObjects.Instance.ThemeManager.GetThemes(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVProgramSchedule : SlideType.RadioProgramSchedule)
					.FirstOrDefault(t =>
				  t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVProgramSchedule : SlideType.RadioProgramSchedule)) ||
				  String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVProgramSchedule : SlideType.RadioProgramSchedule)));
			}
		}

		public bool TableOutput => SummarySettings.TableOutput;

		public int ItemsPerTable => ItemsCount > 18 ? 18 : ItemsCount;

		public bool ShowIcons => false;

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
		public IEnumerable<ScheduleSectionOutputType> GetAvailableOutputOptions()
		{
			return Items.Any() ?
				new[] { ScheduleSectionOutputType.Summary } :
				new ScheduleSectionOutputType[] { };
		}

		public void GenerateOutput()
		{
			RegularMediaSchedulePowerPointHelper.Instance.AppendSummary(this);
		}

		public PreviewGroup GeneratePreview()
		{
			var previewGroup = new PreviewGroup
			{
				Name = Text,
				PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareSummaryEmail(previewGroup.PresentationSourcePath, this);
			return previewGroup;
		}
		#endregion
	}
}