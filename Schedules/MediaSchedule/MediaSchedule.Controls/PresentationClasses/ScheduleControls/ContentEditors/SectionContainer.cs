using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.BusinessClasses.Output.DigitalInfo;
using Asa.Media.Controls.PresentationClasses.OptionsControls.ContentEditors;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Output;
using Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings;
using Asa.Media.Controls.PresentationClasses.SnapshotControls.ContentEditors;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	//public partial class SectionContainer : UserControl
	public partial class SectionContainer : XtraTabPage
	{
		public ScheduleSection SectionData { get; private set; }

		private SectionControl _sectionControl;
		private SectionDigitalInfoControl _digitalInfoControl;
		private SectionSummaryControl _customSummaryControl;
		private bool _sectionDataChanged;

		public event EventHandler<SectionDataChangedEventArgs> DataChanged;
		public event EventHandler<EventArgs> SectionEditorChanged;

		public ISectionEditorControl ActiveEditor => (ISectionEditorControl)xtraTabControl.SelectedTabPage;
		public ISectionItemCollectionControl ActiveItemCollection => ActiveEditor as ISectionItemCollectionControl;

		#region Totals Calculation
		public string TotalPeriodsValueFormatted => SectionData.TotalActivePeriods.ToString("#,##0");

		public string TotalSpotsValueFormatted => SectionData.TotalSpots.ToString("#,##0");

		public string TotalGRPValueFormatted => SectionData.TotalGRP.ToString(
			SectionData.ParentScheduleSettings.DemoType == DemoType.Rtg ? "#,###.0" : "#,##0");

		public string TotalCPPValueFormatted => SectionData.TotalCPP.ToString("$#,###.00");

		public string AvgRateValueFormatted => SectionData.AvgRate.ToString("$#,###.00");

		public string TotalCostValuesFormatted => SectionData.TotalCost.ToString(SectionData.UseDecimalRates ? "$#,##0.00" : "$#,##0");

		public string NetRateValueFormatted => SectionData.NetRate.ToString("$#,###.00");

		public string TotalDiscountValueFormatted => SectionData.Discount.ToString("$#,###.00");
		#endregion

		public SectionContainer()
		{
			InitializeComponent();

			simpleLabelItemCollectionItemsInfo.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemCollectionItemsInfo.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemCollectionItemsInfo.MinSize = RectangleHelper.ScaleSize(simpleLabelItemCollectionItemsInfo.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemWarnings.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemWarnings.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemWarnings.MinSize = RectangleHelper.ScaleSize(simpleLabelItemWarnings.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		public void InitControls()
		{
			_sectionControl = new SectionControl(this);
			_digitalInfoControl = new SectionDigitalInfoControl(this);
			_customSummaryControl = new SectionSummaryControl(this);

			xtraTabControl.TabPages.AddRange(new XtraTabPage[]
			{
				_sectionControl,
				_digitalInfoControl,
				_customSummaryControl,
			});

			_sectionControl.InitControls();
			_digitalInfoControl.InitControls();
			_customSummaryControl.InitControls();

			xtraTabControl.SelectedPageChanged += OnSelectedSectionEditorChanged;
		}

		public void Release()
		{
			_sectionControl.Release();
			_sectionControl = null;

			_digitalInfoControl.Release();
			_digitalInfoControl = null;

			_customSummaryControl.Release();
			_customSummaryControl = null;

			DataChanged = null;
			SectionEditorChanged = null;
			SectionData = null;
		}

		#region Section Data Management
		public void LoadData(ScheduleSection sectionData, bool quickLoad = false)
		{
			SectionData = sectionData;
			Text = SectionData.Name;
			SectionData.DataChanged += OnSectionDataChanged;

			_sectionControl.LoadData();
			_digitalInfoControl.LoadData();
			_customSummaryControl.LoadData(quickLoad);

			UpdateCollectionItemsInfo();
			UpdateSummaryState();
			UpdateWarnings();
		}

		public void SaveData()
		{
			ActiveEditor?.SaveData();
		}

		public void UpdateAccordingSettings(SettingsChangedEventArgs args)
		{
			switch (args.ChangedSettingsType)
			{
				case ScheduleSettingsType.Columns:
				case ScheduleSettingsType.Totals:
				case ScheduleSettingsType.AdvancedColumns:
					_sectionDataChanged = true;
					_sectionControl.UpdateGridView();
					if (args.UpdateGridColums)
						_sectionControl.UpdateGridData(true);
					_sectionControl.UpdateSpotsByQuarter();
					UpdateSummaryState();
					UpdateWarnings();
					break;
				case ScheduleSettingsType.Quarters:
					_sectionControl.UpdateSpotsByQuarter();
					break;
				case ScheduleSettingsType.DigitalInfo:
					_digitalInfoControl.UpdateGridView();
					break;
			}
		}

		public void RaiseDataChanged(SectionDataChangedEventArgs e)
		{
			_sectionDataChanged = true;
			UpdateCollectionItemsInfo();
			UpdateSummaryState();
			DataChanged?.Invoke(ActiveEditor, e);
		}

		private void OnSectionDataChanged(object sender, EventArgs e)
		{
			RaiseDataChanged(new SectionDataChangedEventArgs());
		}

		private void OnSelectedSectionEditorChanged(object sender, TabPageChangedEventArgs e)
		{
			var previuseEditor = e.PrevPage as ISectionEditorControl;
			previuseEditor?.SaveData();
			if (_sectionDataChanged)
			{
				SectionData.Summary.SynchronizeSectionContent();
				_customSummaryControl.LoadData();
				_sectionDataChanged = false;
			}
			UpdateCollectionItemsInfo();
			UpdateWarnings();
			SectionEditorChanged?.Invoke(this, EventArgs.Empty);
		}

		private void UpdateCollectionItemsInfo()
		{
			switch (ActiveEditor?.EditorType)
			{
				case SectionEditorType.Schedule:
					simpleLabelItemCollectionItemsInfo.Visibility = LayoutVisibility.Always;
					simpleLabelItemCollectionItemsInfo.Text = String.Format("<color=gray>Total Programs: {0}</color>", SectionData.Programs.Count);
					break;
				case SectionEditorType.DigitalInfo:
					simpleLabelItemCollectionItemsInfo.Visibility = LayoutVisibility.Always;
					if (SectionData.DigitalInfo.Records.Count < BaseDigitalInfoOneSheetOutputModel.MaxRecords)
						simpleLabelItemCollectionItemsInfo.Text = String.Format("<color=gray>DIGITAL Marketing Products: {0}</color>", SectionData.DigitalInfo.Records.Count);
					else
						simpleLabelItemCollectionItemsInfo.Text = "<color=red>Maximum DIGITAL Marketing Products: <b><u>6</u></b></color>";
					break;
				case SectionEditorType.CustomSummary:
					simpleLabelItemCollectionItemsInfo.Visibility = LayoutVisibility.Always;
					simpleLabelItemCollectionItemsInfo.Text = String.Format("<color=gray>Summary Items: {0}</color>", SectionData.Summary.CustomSummary.Items.Count);
					break;
				default:
					simpleLabelItemCollectionItemsInfo.Visibility = LayoutVisibility.Never;
					break;
			}
		}

		private void UpdateSummaryState()
		{
			_customSummaryControl.PageEnabled =
				SectionData.Programs.Any(p => p.TotalSpots > 0) || SectionData.DigitalInfo.Records.Any();
		}

		private void UpdateWarnings()
		{
			var warnings = new List<string>();
			if (ActiveEditor == _sectionControl)
			{
				if (SectionData.UseGenericDateColumns)
					warnings.Add(String.Format("*Generic {0}s", SectionData.Parent.ScheduleSettings.SelectedSpotType));
				if (SectionData.UseDecimalRates)
					warnings.Add("*Decimals Enabled");
			}
			if (warnings.Any())
			{
				simpleLabelItemWarnings.Visibility = LayoutVisibility.Always;
				simpleLabelItemWarnings.Text = String.Format("<color=red>{0}</color>", String.Join("    ", warnings));
			}
			else
				simpleLabelItemWarnings.Visibility = LayoutVisibility.Never;
		}

		private void OnTabControlMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			var menuHitInfo = xtraTabControl.CalcHitInfo(new Point(e.X, e.Y));
			if (menuHitInfo.HitTest != XtraTabHitTest.PageHeader) return;
			if (menuHitInfo.Page == _sectionControl && SectionData.Programs.Any())
				contextMenuStripSchedule.Show((Control)sender, e.Location);
			else if (menuHitInfo.Page == _digitalInfoControl && SectionData.DigitalInfo.Records.Any())
			{
				toolStripMenuItemDigitalToSnapshot.DropDownItems.Clear();
				toolStripMenuItemDigitalToSnapshot.DropDownItems.Add(new ToolStripMenuItem(
					"Create New...",
					null,
					(s, args) =>
					{
						_digitalInfoControl.SaveData();
						using (var form = new FormSnapshotName())
						{
							form.SnapshotName = SectionData.Name;
							if (form.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
							{
								var newSnapshot = SectionData.CopyDigitalToSnapshot(form.SnapshotName);
								RaiseDataChanged(new SectionDataChangedEventArgs { SnapshotsChanged = true });
								using (var confirmation = new FormCopyContentConfirmation())
								{
									confirmation.Text = "Send to Snapshot";
									confirmation.simpleLabelItemTitle.Text = String.Format(confirmation.simpleLabelItemTitle.Text, "Digital successfully copied");
									confirmation.buttonXOK.Text = String.Format("Go to {0}", Controller.Instance.TabSnapshot.Text);
									if (confirmation.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
										ContentRibbonManager<MediaScheduleChangeInfo>.ShowRibbonTab(
											ContentIdentifiers.Snapshots,
											new SnapshotOpenEventArgs
											{
												SnapshotId = newSnapshot.UniqueID,
												EditorType = SnapshotEditorType.DigitalInfo
											});
								}
							}
						}
					}));
				foreach (var snapshot in SectionData.ParentSchedule.SnapshotContent.Snapshots)
				{
					toolStripMenuItemDigitalToSnapshot.DropDownItems.Add(new ToolStripMenuItem(
					snapshot.Name.Replace("&", "&&"),
					null,
					(s, args) =>
					{
						_digitalInfoControl.SaveData();
						SectionData.CopyDigitalToSnapshot(snapshot);
						RaiseDataChanged(new SectionDataChangedEventArgs { SnapshotsChanged = true });
						using (var confirmation = new FormCopyContentConfirmation())
						{
							confirmation.Text = "Send to Snapshot";
							confirmation.simpleLabelItemTitle.Text = String.Format(confirmation.simpleLabelItemTitle.Text, "Digital successfully copied");
							confirmation.buttonXOK.Text = String.Format("Go to {0}", Controller.Instance.TabSnapshot.Text);
							if (confirmation.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
								ContentRibbonManager<MediaScheduleChangeInfo>.ShowRibbonTab(
									ContentIdentifiers.Snapshots,
									new SnapshotOpenEventArgs
									{
										SnapshotId = snapshot.UniqueID,
										EditorType = SnapshotEditorType.DigitalInfo
									});
						}
					}));
				}

				toolStripMenuItemDigitalToOptions.DropDownItems.Clear();
				toolStripMenuItemDigitalToOptions.DropDownItems.Add(new ToolStripMenuItem(
					"Create New...",
					null,
					(s, args) =>
					{
						_digitalInfoControl.SaveData();
						using (var form = new FormOptionSetName())
						{
							form.OptionSetName = SectionData.Name;
							if (form.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
							{
								var newOptionsSet = SectionData.CopyDigitalToOptionsSet(form.OptionSetName);
								RaiseDataChanged(new SectionDataChangedEventArgs { OptionsSetsChanged = true });
								using (var confirmation = new FormCopyContentConfirmation())
								{
									confirmation.Text = "Send to Flex-Grid";
									confirmation.simpleLabelItemTitle.Text = String.Format(confirmation.simpleLabelItemTitle.Text, "Digital successfully copied");
									confirmation.buttonXOK.Text = String.Format("Go to {0}", Controller.Instance.TabOptions.Text);
									if (confirmation.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
										ContentRibbonManager<MediaScheduleChangeInfo>.ShowRibbonTab(
											ContentIdentifiers.Options,
											new OptionsSetOpenEventArgs
											{
												OptionsSetId = newOptionsSet.UniqueID,
												EditorType = OptionEditorType.DigitalInfo
											});
								}
							}
						}
					}));
				foreach (var optionSet in SectionData.ParentSchedule.OptionsContent.Options)
				{
					toolStripMenuItemDigitalToOptions.DropDownItems.Add(new ToolStripMenuItem(
					optionSet.Name.Replace("&", "&&"),
					null,
					(s, args) =>
					{
						_digitalInfoControl.SaveData();
						SectionData.CopyDigitalToOptionsSet(optionSet);
						RaiseDataChanged(new SectionDataChangedEventArgs { OptionsSetsChanged = true });
						using (var confirmation = new FormCopyContentConfirmation())
						{
							confirmation.Text = "Send to Flex-Grid";
							confirmation.simpleLabelItemTitle.Text = String.Format(confirmation.simpleLabelItemTitle.Text, "Digital successfully copied");
							confirmation.buttonXOK.Text = String.Format("Go to {0}", Controller.Instance.TabOptions.Text);
							if (confirmation.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
								ContentRibbonManager<MediaScheduleChangeInfo>.ShowRibbonTab(
									ContentIdentifiers.Options,
									new OptionsSetOpenEventArgs
									{
										OptionsSetId = optionSet.UniqueID,
										EditorType = OptionEditorType.DigitalInfo
									});
						}
					}));
				}

				contextMenuStripDigital.Show((Control)sender, e.Location);
			}
		}

		private void OnScheduleConvertToOptionsSetClick(object sender, EventArgs e)
		{
			using (var form = new FormOptionSetName())
			{
				form.OptionSetName = SectionData.Name;
				if (form.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
				{
					var newOptionSet = SectionData.CopyScheduleToOptionsSet(form.OptionSetName);
					RaiseDataChanged(new SectionDataChangedEventArgs { OptionsSetsChanged = true });
					using (var confirmation = new FormCopyContentConfirmation())
					{
						confirmation.Text = "Create Flex-Grid";
						confirmation.simpleLabelItemTitle.Text = String.Format(confirmation.simpleLabelItemTitle.Text, "Flex-Grid successfully added");
						confirmation.buttonXOK.Text = String.Format("Go to {0}", Controller.Instance.TabOptions.Text);
						if (confirmation.ShowDialog(Controller.Instance.FormMain) == DialogResult.OK)
							ContentRibbonManager<MediaScheduleChangeInfo>.ShowRibbonTab(
								ContentIdentifiers.Options,
								new OptionsSetOpenEventArgs
								{
									OptionsSetId = newOptionSet.UniqueID,
									EditorType = OptionEditorType.Schedule
								});
					}
				}
			}
		}
		#endregion

		#region Schedule Management
		public void AddItem()
		{
			ActiveItemCollection?.AddItem();
		}

		public void DeleteItem()
		{
			ActiveItemCollection?.DeleteItem();
		}
		#endregion

		#region Output Stuff
		public bool ReadyForOutput => GetOutputGroup().Items.Any();

		public OutputGroup GetOutputGroup()
		{
			return new OutputGroup
			{
				Name = SectionData.Name,
				IsCurrent = TabControl != null && TabControl.SelectedTabPage == this,
				Items = xtraTabControl.TabPages.OfType<ISectionOutputControl>().SelectMany(sectionOutputControl => sectionOutputControl.GetOutputItems()).ToList()
			};
		}
		#endregion
	}
}
