using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Contract;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Solutions.Common.Helpers;
using Asa.Solutions.Common.PresentationClasses.Output;
using DevExpress.Skins;
using DevExpress.XtraEditors;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Contract
{
	[ToolboxItem(false)]
	public partial class ContractTabBControl : ChildTabBaseControl
	{
		private readonly List<ComboBoxEdit> _table1Column1Editors = new List<ComboBoxEdit>();
		private readonly List<SpinEdit> _table1Column2Editors = new List<SpinEdit>();
		private readonly List<ComboBoxEdit> _table1Column3Editors = new List<ComboBoxEdit>();

		private readonly List<ComboBoxEdit> _table2Column1Editors = new List<ComboBoxEdit>();
		private readonly List<SpinEdit> _table2Column2Editors = new List<SpinEdit>();
		private readonly List<ComboBoxEdit> _table2Column3Editors = new List<ComboBoxEdit>();

		public ContractTabBInfo CustomTabInfo => (ContractTabBInfo)TabInfo;

		public ContractTabBControl(IChildTabPageContainer slideContainer, ShiftChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			_table1Column1Editors.AddRange(new[]
			{
				comboBoxEditTable1Combo1A,
				comboBoxEditTable1Combo2A,
				comboBoxEditTable1Combo3A,
				comboBoxEditTable1Combo4A,
				comboBoxEditTable1Combo5A,
				comboBoxEditTable1Combo6A,
				comboBoxEditTable1Combo7A,
				comboBoxEditTable1Combo8A,
				comboBoxEditTable1Combo9A,
				comboBoxEditTable1Combo10A
			});

			_table1Column2Editors.AddRange(new[]
			{
				spinEditTable1Editor1B,
				spinEditTable1Editor2B,
				spinEditTable1Editor3B,
				spinEditTable1Editor4B,
				spinEditTable1Editor5B,
				spinEditTable1Editor6B,
				spinEditTable1Editor7B,
				spinEditTable1Editor8B,
				spinEditTable1Editor9B,
				spinEditTable1Editor10B,
			});

			_table1Column3Editors.AddRange(new[]
			{
				comboBoxEditTable1Combo1C,
				comboBoxEditTable1Combo2C,
				comboBoxEditTable1Combo3C,
				comboBoxEditTable1Combo4C,
				comboBoxEditTable1Combo5C,
				comboBoxEditTable1Combo6C,
				comboBoxEditTable1Combo7C,
				comboBoxEditTable1Combo8C,
				comboBoxEditTable1Combo9C,
				comboBoxEditTable1Combo10C
			});

			_table2Column1Editors.AddRange(new[]
			{
				comboBoxEditTable2Combo1A,
				comboBoxEditTable2Combo2A,
				comboBoxEditTable2Combo3A,
				comboBoxEditTable2Combo4A,
				comboBoxEditTable2Combo5A,
				comboBoxEditTable2Combo6A,
				comboBoxEditTable2Combo7A,
				comboBoxEditTable2Combo8A,
				comboBoxEditTable2Combo9A,
				comboBoxEditTable2Combo10A
			});

			_table2Column2Editors.AddRange(new[]
			{
				spinEditTable2Editor1B,
				spinEditTable2Editor2B,
				spinEditTable2Editor3B,
				spinEditTable2Editor4B,
				spinEditTable2Editor5B,
				spinEditTable2Editor6B,
				spinEditTable2Editor7B,
				spinEditTable2Editor8B,
				spinEditTable2Editor9B,
				spinEditTable2Editor10B,
			});

			_table2Column3Editors.AddRange(new[]
			{
				comboBoxEditTable2Combo1C,
				comboBoxEditTable2Combo2C,
				comboBoxEditTable2Combo3C,
				comboBoxEditTable2Combo4C,
				comboBoxEditTable2Combo5C,
				comboBoxEditTable2Combo6C,
				comboBoxEditTable2Combo7C,
				comboBoxEditTable2Combo8C,
				comboBoxEditTable2Combo9C,
				comboBoxEditTable2Combo10C
			});

			xtraTabPageTable1.Text = CustomTabInfo.Table1Configuration.HeaderName;
			simpleLabelItemTable1HeaderA.Text = String.Format("<size=+2><color=gray>{0}</color></size>",
				CustomTabInfo.Table1Configuration.Column1Name);
			simpleLabelItemTable1HeaderB.Text = String.Format("<size=+2><color=gray>{0}</color></size>",
				CustomTabInfo.Table1Configuration.Column2Name);
			simpleLabelItemTable1HeaderC.Text = String.Format("<size=+2><color=gray>{0}</color></size>",
				CustomTabInfo.Table1Configuration.Column3Name);

			xtraTabPageTable2.Text = CustomTabInfo.Table2Configuration.HeaderName;
			simpleLabelItemTable2HeaderA.Text = String.Format("<size=+2><color=gray>{0}</color></size>",
				CustomTabInfo.Table2Configuration.Column1Name);
			simpleLabelItemTable2HeaderB.Text = String.Format("<size=+2><color=gray>{0}</color></size>",
				CustomTabInfo.Table2Configuration.Column2Name);
			simpleLabelItemTable2HeaderC.Text = String.Format("<size=+2><color=gray>{0}</color></size>",
				CustomTabInfo.Table2Configuration.Column3Name);

			comboBoxEditSlideHeader.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.HeadersEditorConfiguration);
			comboBoxEditSummary2Combo.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SummaryConfiguration);
			spinEditSummary1.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SummaryConfiguration);
			spinEditSummary3.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.SummaryConfiguration);

			foreach (var editor in _table1Column1Editors)
				editor.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.TableComboConfiguration);
			foreach (var editor in _table1Column2Editors)
				editor.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.TableComboConfiguration);
			foreach (var editor in _table1Column3Editors)
				editor.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.TableComboConfiguration);
			foreach (var editor in _table2Column1Editors)
				editor.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.TableComboConfiguration);
			foreach (var editor in _table2Column2Editors)
				editor.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.TableComboConfiguration);
			foreach (var editor in _table2Column3Editors)
				editor.EnableSelectAll().RaiseNullValueIfEditorEmpty().AssignConfiguration(CustomTabInfo.TableComboConfiguration);

			comboBoxEditSlideHeader.Properties.Items.Clear();
			comboBoxEditSlideHeader.Properties.Items.AddRange(CustomTabInfo.HeadersItems
				.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSlideHeader.Properties.NullText =
				CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsPlaceholder)?.Value ??
				"Select or type";

			comboBoxEditSummary2Combo.Properties.Items.Clear();
			comboBoxEditSummary2Combo.Properties.Items.AddRange(CustomTabInfo.SummaryCombo2Items.Where(item => !item.IsPlaceholder).ToArray());
			comboBoxEditSummary2Combo.Properties.NullText =
				CustomTabInfo.SummaryCombo2Items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
				comboBoxEditSummary2Combo.Properties.NullText;

			for (var i = 0; i < CustomTabInfo.Table1Column1Lists.Count; i++)
			{
				var items = CustomTabInfo.Table1Column1Lists[i];
				var editor = _table1Column1Editors[i];

				editor.Properties.Items.Clear();
				editor.Properties.Items.AddRange(items.Where(item => !item.IsPlaceholder).ToArray());
				editor.Properties.NullText =
					items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
					editor.Properties.NullText;
			}

			for (var i = 0; i < CustomTabInfo.Table1Column3Lists.Count; i++)
			{
				var items = CustomTabInfo.Table1Column3Lists[i];
				var editor = _table1Column3Editors[i];

				editor.Properties.Items.Clear();
				editor.Properties.Items.AddRange(items.Where(item => !item.IsPlaceholder).ToArray());
				editor.Properties.NullText =
					items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
					editor.Properties.NullText;
			}

			for (var i = 0; i < CustomTabInfo.Table2Column1Lists.Count; i++)
			{
				var items = CustomTabInfo.Table2Column1Lists[i];
				var editor = _table2Column1Editors[i];

				editor.Properties.Items.Clear();
				editor.Properties.Items.AddRange(items.Where(item => !item.IsPlaceholder).ToArray());
				editor.Properties.NullText =
					items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
					editor.Properties.NullText;
			}

			for (var i = 0; i < CustomTabInfo.Table2Column3Lists.Count; i++)
			{
				var items = CustomTabInfo.Table2Column3Lists[i];
				var editor = _table2Column3Editors[i];

				editor.Properties.Items.Clear();
				editor.Properties.Items.AddRange(items.Where(item => !item.IsPlaceholder).ToArray());
				editor.Properties.NullText =
					items.FirstOrDefault(item => item.IsPlaceholder)?.Value ??
					editor.Properties.NullText;
			}

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			simpleLabelItemTable1HeaderA.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTable1HeaderA.MaxSize, scaleFactor);
			simpleLabelItemTable1HeaderA.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTable1HeaderA.MinSize, scaleFactor);
			simpleLabelItemTable1HeaderB.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTable1HeaderB.MaxSize, scaleFactor);
			simpleLabelItemTable1HeaderB.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTable1HeaderB.MinSize, scaleFactor);
			simpleLabelItemTable1HeaderC.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTable1HeaderC.MaxSize, scaleFactor);
			simpleLabelItemTable1HeaderC.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTable1HeaderC.MinSize, scaleFactor);
			simpleLabelItemTable2HeaderA.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTable2HeaderA.MaxSize, scaleFactor);
			simpleLabelItemTable2HeaderA.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTable2HeaderA.MinSize, scaleFactor);
			simpleLabelItemTable2HeaderB.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTable2HeaderB.MaxSize, scaleFactor);
			simpleLabelItemTable2HeaderB.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTable2HeaderB.MinSize, scaleFactor);
			simpleLabelItemTable2HeaderC.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTable2HeaderC.MaxSize, scaleFactor);
			simpleLabelItemTable2HeaderC.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTable2HeaderC.MinSize, scaleFactor);
			layoutControlItemSummary1Value.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSummary1Value.MaxSize, scaleFactor);
			layoutControlItemSummary1Value.MinSize = RectangleHelper.ScaleSize(layoutControlItemSummary1Value.MinSize, scaleFactor);
			simpleLabelItemSummary1Suffix.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSummary1Suffix.MaxSize, scaleFactor);
			simpleLabelItemSummary1Suffix.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSummary1Suffix.MinSize, scaleFactor);
			layoutControlItemSummary2Combo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSummary2Combo.MaxSize, scaleFactor);
			layoutControlItemSummary2Combo.MinSize = RectangleHelper.ScaleSize(layoutControlItemSummary2Combo.MinSize, scaleFactor);
			layoutControlItemSummary3Value.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSummary3Value.MaxSize, scaleFactor);
			layoutControlItemSummary3Value.MinSize = RectangleHelper.ScaleSize(layoutControlItemSummary3Value.MinSize, scaleFactor);
			simpleLabelItemSummary3Suffix.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSummary3Suffix.MaxSize, scaleFactor);
			simpleLabelItemSummary3Suffix.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSummary3Suffix.MinSize, scaleFactor);
		}

		public override void LoadData()
		{
			_allowToSave = false;

			comboBoxEditSlideHeader.EditValue = SlideContainer.EditedContent.ContractState.TabB.SlideHeader ??
												CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault);

			checkEditSummary1.Checked = SlideContainer.EditedContent.ContractState.TabB.SummaryCheckbox1 ??
										CustomTabInfo.SummaryCheckbox1.Value;

			checkEditSummary3.Checked = SlideContainer.EditedContent.ContractState.TabB.SummaryCheckbox3 ??
										CustomTabInfo.SummaryCheckbox3.Value;

			comboBoxEditSummary2Combo.EditValue = SlideContainer.EditedContent.ContractState.TabB.SummaryCombo1 ??
											CustomTabInfo.SummaryCombo2Items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault) ??
											CustomTabInfo.SummaryCombo2Items.FirstOrDefault(item => !item.IsPlaceholder);

			for (var i = 0; i < CustomTabInfo.Table1Column1Lists.Count; i++)
			{
				var items = CustomTabInfo.Table1Column1Lists[i];
				var editor = _table1Column1Editors[i];

				editor.EditValue = SlideContainer.EditedContent.ContractState.TabB.Table1Column1Values.ElementAtOrDefault(i) ??
					items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault);
			}

			for (var i = 0; i < _table1Column2Editors.Count; i++)
			{
				var editor = _table1Column2Editors[i];
				editor.EditValue = SlideContainer.EditedContent.ContractState.TabB.Table1Column2Values.ElementAtOrDefault(i) ?? 0;
			}

			for (var i = 0; i < CustomTabInfo.Table1Column3Lists.Count; i++)
			{
				var items = CustomTabInfo.Table1Column3Lists[i];
				var editor = _table1Column3Editors[i];

				editor.EditValue = SlideContainer.EditedContent.ContractState.TabB.Table1Column3Values.ElementAtOrDefault(i) ??
								   items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault);
			}

			for (var i = 0; i < CustomTabInfo.Table2Column1Lists.Count; i++)
			{
				var items = CustomTabInfo.Table2Column1Lists[i];
				var editor = _table2Column1Editors[i];

				editor.EditValue = SlideContainer.EditedContent.ContractState.TabB.Table2Column1Values.ElementAtOrDefault(i) ??
								   items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault);
			}

			for (var i = 0; i < _table2Column2Editors.Count; i++)
			{
				var editor = _table2Column2Editors[i];
				editor.EditValue = SlideContainer.EditedContent.ContractState.TabB.Table2Column2Values.ElementAtOrDefault(i) ?? 0;
			}

			for (var i = 0; i < CustomTabInfo.Table2Column3Lists.Count; i++)
			{
				var items = CustomTabInfo.Table2Column3Lists[i];
				var editor = _table2Column3Editors[i];

				editor.EditValue = SlideContainer.EditedContent.ContractState.TabB.Table2Column3Values.ElementAtOrDefault(i) ??
								   items.FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault);
			}

			UpdateSummaryControls();

			_allowToSave = true;
		}

		public override void ApplyChanges()
		{
			if (!_dataChanged) return;

			var slideHeaderValue = comboBoxEditSlideHeader.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSlideHeader.EditValue as String };
			SlideContainer.EditedContent.ContractState.TabB.SlideHeader = slideHeaderValue != CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault) ?
				slideHeaderValue :
				null;

			SlideContainer.EditedContent.ContractState.TabB.SummaryCheckbox1 =
				checkEditSummary1.Checked != CustomTabInfo.SummaryCheckbox1.Value ? checkEditSummary1.Checked : (bool?)null;

			SlideContainer.EditedContent.ContractState.TabB.SummaryCheckbox3 =
				checkEditSummary3.Checked != CustomTabInfo.SummaryCheckbox3.Value ? checkEditSummary3.Checked : (bool?)null;

			SlideContainer.EditedContent.ContractState.TabB.SummaryCombo1 = CustomTabInfo.SummaryCombo2Items.FirstOrDefault(h => !h.IsPlaceholder && h.IsDefault) != comboBoxEditSummary2Combo.EditValue ?
				comboBoxEditSummary2Combo.EditValue as ListDataItem ?? new ListDataItem { Value = comboBoxEditSummary2Combo.EditValue as String } :
				null;

			SlideContainer.EditedContent.ContractState.TabB.Table1Column1Values.Clear();
			for (var i = 0; i < CustomTabInfo.Table1Column1Lists.Count; i++)
			{
				var items = CustomTabInfo.Table1Column1Lists[i];
				var editor = _table1Column1Editors[i];

				SlideContainer.EditedContent.ContractState.TabB.Table1Column1Values.Add(items.FirstOrDefault(h => !h.IsPlaceholder && h.IsDefault) != editor.EditValue ?
					editor.EditValue as ListDataItem ?? new ListDataItem { Value = editor.EditValue as String } :
					null);
			}

			SlideContainer.EditedContent.ContractState.TabB.Table1Column2Values.Clear();
			foreach (var editor in _table1Column2Editors)
				SlideContainer.EditedContent.ContractState.TabB.Table1Column2Values.Add((decimal?)editor.EditValue);

			SlideContainer.EditedContent.ContractState.TabB.Table1Column3Values.Clear();
			for (var i = 0; i < CustomTabInfo.Table1Column3Lists.Count; i++)
			{
				var items = CustomTabInfo.Table1Column3Lists[i];
				var editor = _table1Column3Editors[i];

				SlideContainer.EditedContent.ContractState.TabB.Table1Column3Values.Add(items.FirstOrDefault(h => !h.IsPlaceholder && h.IsDefault) != editor.EditValue ?
					editor.EditValue as ListDataItem ?? new ListDataItem { Value = editor.EditValue as String } :
					null);
			}

			SlideContainer.EditedContent.ContractState.TabB.Table2Column1Values.Clear();
			for (var i = 0; i < CustomTabInfo.Table2Column1Lists.Count; i++)
			{
				var items = CustomTabInfo.Table2Column1Lists[i];
				var editor = _table2Column1Editors[i];

				SlideContainer.EditedContent.ContractState.TabB.Table2Column1Values.Add(items.FirstOrDefault(h => !h.IsPlaceholder && h.IsDefault) != editor.EditValue ?
					editor.EditValue as ListDataItem ?? new ListDataItem { Value = editor.EditValue as String } :
					null);
			}

			SlideContainer.EditedContent.ContractState.TabB.Table2Column2Values.Clear();
			foreach (var editor in _table2Column2Editors)
				SlideContainer.EditedContent.ContractState.TabB.Table2Column2Values.Add((decimal?)editor.EditValue);

			SlideContainer.EditedContent.ContractState.TabB.Table2Column3Values.Clear();
			for (var i = 0; i < CustomTabInfo.Table2Column3Lists.Count; i++)
			{
				var items = CustomTabInfo.Table2Column3Lists[i];
				var editor = _table2Column3Editors[i];

				SlideContainer.EditedContent.ContractState.TabB.Table2Column3Values.Add(items.FirstOrDefault(h => !h.IsPlaceholder && h.IsDefault) != editor.EditValue ?
					editor.EditValue as ListDataItem ?? new ListDataItem { Value = editor.EditValue as String } :
					null);
			}

			_dataChanged = false;
		}

		public override bool GetOutputEnableState()
		{
			return SlideContainer.EditedContent.ContractState.TabB.EnableOutput ?? CustomTabInfo.EnableOutput;
		}

		public override void ApplyOutputEnableState(Boolean outputEnabled)
		{
			SlideContainer.EditedContent.ContractState.TabB.EnableOutput =
				outputEnabled != CustomTabInfo.EnableOutput ? outputEnabled : (bool?)null;

			base.ApplyOutputEnableState(outputEnabled);
		}

		private void UpdateSummaryControls()
		{
			var investmentInfo = GetInvestmentInfo();

			spinEditSummary1.EditValue = investmentInfo.MonthlyInvestment;
			spinEditSummary3.EditValue = investmentInfo.TotalInvestment;
		}

		private InvestmentInfo GetInvestmentInfo()
		{
			var investmentInfo = new InvestmentInfo();

			for (var i = 0; i < _table1Column3Editors.Count; i++)
			{
				var termValue = _table1Column3Editors[i].EditValue as ListDataItem ??
								new ListDataItem { Value = _table1Column3Editors[i].EditValue as String };
				if (String.Equals(termValue?.Value, ContractTabBInfo.TermMonthlyInvestment, StringComparison.OrdinalIgnoreCase))
				{
					investmentInfo.MonthlyInvestment += ((decimal?)_table1Column2Editors[i].EditValue ?? 0m);
				}
				else if (String.Equals(termValue?.Value, ContractTabBInfo.TermOneTimeInvestment, StringComparison.OrdinalIgnoreCase))
				{
					var investmentValue = ((decimal?)_table1Column2Editors[i].EditValue ?? 0m);
					var descriptionValue = (_table1Column1Editors[i].EditValue as ListDataItem ??
						new ListDataItem { Value = _table1Column1Editors[i].EditValue as String }).Value;

					investmentInfo.OneTimeInvestmentsList.Add(new Tuple<String, Decimal>(descriptionValue, investmentValue));
				}
			}

			for (var i = 0; i < _table2Column3Editors.Count; i++)
			{
				var termValue = _table2Column3Editors[i].EditValue as ListDataItem ??
					new ListDataItem { Value = _table2Column3Editors[i].EditValue as String };
				if (String.Equals(termValue?.Value, ContractTabBInfo.TermMonthlyInvestment, StringComparison.OrdinalIgnoreCase))
				{
					investmentInfo.MonthlyInvestment += ((decimal?)_table2Column2Editors[i].EditValue ?? 0m);
				}
				else if (String.Equals(termValue?.Value, ContractTabBInfo.TermOneTimeInvestment, StringComparison.OrdinalIgnoreCase))
				{
					var investmentValue = ((decimal?)_table2Column2Editors[i].EditValue ?? 0m);

					var descriptionValue = (_table2Column1Editors[i].EditValue as ListDataItem ??
						new ListDataItem { Value = _table2Column1Editors[i].EditValue as String }).Value;

					investmentInfo.OneTimeInvestmentsList.Add(new Tuple<String, Decimal>(descriptionValue, investmentValue));
				}
			}

			try
			{
				investmentInfo.MonthCount = Int32.Parse((comboBoxEditSummary2Combo.EditValue as ListDataItem ??
					new ListDataItem { Value = comboBoxEditSummary2Combo.EditValue as String }).Value ?? "0");
			}
			catch { }

			return investmentInfo;
		}

		private void OnEditValueChanged(object sender, EventArgs e)
		{
			RaiseEditValueChanged();
		}

		private void OnFormulaArgumentEditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				UpdateSummaryControls();
			OnEditValueChanged(sender, e);
		}

		private void OnSummary1ToggleCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemSummary1Value.Enabled =
				simpleLabelItemSummary1Suffix.Enabled =
				checkEditSummary1.Checked;
			OnEditValueChanged(sender, e);
		}

		private void OnSummary3ToggleCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemSummary3Value.Enabled =
				simpleLabelItemSummary3Suffix.Enabled =
					checkEditSummary3.Checked;
			OnEditValueChanged(sender, e);
		}

		#region Output
		public override SlideType SlideType => SlideType.ShiftContract;

		protected override OutputDataPackage GetOutputData()
		{
			var outputDataPackage = new OutputDataPackage();

			outputDataPackage.Theme = SelectedTheme;

			outputDataPackage.TemplateName =
				MasterWizardManager.Instance.SelectedWizard.GetShiftContractFile("b_agreement.pptx");

			outputDataPackage.TextItems.Add("SHIFT15BHEADER".ToUpper(), (SlideContainer.EditedContent.ContractState.TabB.SlideHeader ?? CustomTabInfo.HeadersItems.FirstOrDefault(h => h.IsDefault))?.Value);

			var investmentInfo = GetInvestmentInfo();
			var outputInvestemntItems = new List<string>();
			if (investmentInfo.MonthlyInvestment > 0 && (SlideContainer.EditedContent.ContractState.TabB.SummaryCheckbox1 ?? CustomTabInfo.SummaryCheckbox1.Value))
				outputInvestemntItems.Add(String.Format("{0:$#,##0.##} Monthly Investment", investmentInfo.MonthlyInvestment));
			foreach (var item in investmentInfo.OneTimeInvestmentsList)
				outputInvestemntItems.Add(String.Format("{0:$#,##0.##} {1}", item.Item2, item.Item1));
			if (investmentInfo.TotalInvestment > 0 && (SlideContainer.EditedContent.ContractState.TabB.SummaryCheckbox3 ?? CustomTabInfo.SummaryCheckbox3.Value))
				outputInvestemntItems.Add(String.Format("{0:$#,##0.##}", investmentInfo.TotalInvestment));
			outputDataPackage.TextItems.Add("SHIFT15BSUBHEADER1".ToUpper(), String.Join("    |    ", outputInvestemntItems));

			var outputTable1Items = new List<string>();
			for (var i = 0; i < CustomTabInfo.Table1Column1Lists.Count; i++)
			{
				var outputItem = (SlideContainer.EditedContent.ContractState.TabB.Table1Column1Values.ElementAtOrDefault(i) ??
								CustomTabInfo.Table1Column1Lists[i].FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault))?.Value;
				if (!String.IsNullOrWhiteSpace(outputItem))
					outputTable1Items.Add(outputItem);
			}

			var outputTable2Items = new List<string>();
			for (var i = 0; i < CustomTabInfo.Table2Column1Lists.Count; i++)
			{
				var outputItem = (SlideContainer.EditedContent.ContractState.TabB.Table2Column1Values.ElementAtOrDefault(i) ??
								  CustomTabInfo.Table2Column1Lists[i].FirstOrDefault(item => !item.IsPlaceholder && item.IsDefault))?.Value;
				if (!String.IsNullOrWhiteSpace(outputItem))
					outputTable2Items.Add(outputItem);
			}

			outputDataPackage.TextItems.Add("SHIFT15BCOMBOMERGE1".ToUpper(), String.Join(String.Format("{0}", (char)13), outputTable1Items));
			outputDataPackage.TextItems.Add("SHIFT15BCOMBOMERGE2".ToUpper(), String.Join(String.Format("{0}", (char)13), outputTable2Items));

			return outputDataPackage;
		}
		#endregion

		internal class InvestmentInfo
		{
			public decimal MonthlyInvestment { get; set; }
			public decimal MonthCount { get; set; }

			public decimal TotalInvestment => MonthlyInvestment * MonthCount + OneTimeInvestmentsList.Sum(item => item.Item2);

			public List<Tuple<string, decimal>> OneTimeInvestmentsList { get; }

			public InvestmentInfo()
			{
				OneTimeInvestmentsList = new List<Tuple<String, Decimal>>();
			}
		}
	}
}