﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.AdSchedule.SettingsManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class GridsControl : UserControl
	{
		private IGridOutputControl _selectedOutput;

		public OutputDetailedGridControl DetailedGrid { get; private set; }
		public OutputMultiGridControl MultiGrid { get; private set; }

		#region Operation Buttons
		public ButtonItem HelpButtonItem { get; set; }
		public ButtonItem OptionsButtonItem { get; set; }
		#endregion

		public GridsControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			DetailedGrid = new OutputDetailedGridControl();
			MultiGrid = new OutputMultiGridControl();
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (_selectedOutput.SettingsNotSaved)
				{
					SaveSchedule();
					result = true;
				}
				else
					result = true;
				return result;
			}
		}

		private void SaveSchedule(string newName = "")
		{
			if (_selectedOutput != null)
			{
				if (!string.IsNullOrEmpty(newName))
					_selectedOutput.LocalSchedule.Name = newName;
				_selectedOutput.SettingsNotSaved = false;
				_selectedOutput.LocalSchedule.ViewSettings.SaveDefaultViewSettings(SettingsManager.Instance.ViewSettingsPath);
				BusinessWrapper.Instance.ScheduleManager.SaveSchedule(_selectedOutput.LocalSchedule, true, _selectedOutput as Control);
				_selectedOutput.UpdateOutput(true);
			}
		}

		public void SelectGrid(GridType gridType)
		{
			switch (gridType)
			{
				case GridType.DetailedGrid:
					_selectedOutput = DetailedGrid;
					HelpButtonItem = Controller.Instance.DetailedGridHelp;
					OptionsButtonItem = Controller.Instance.DetailedGridOptions;
					break;
				case GridType.MultiGrid:
					_selectedOutput = MultiGrid;
					HelpButtonItem = Controller.Instance.MultiGridHelp;
					OptionsButtonItem = Controller.Instance.MultiGridOptions;
					break;
				default:
					_selectedOutput = null;
					break;
			}

			if (_selectedOutput != null)
			{
				UpdateButtonsStateAccordingSelectedOutput();

				if (!pnMain.Controls.Contains(_selectedOutput as Control))
				{
					Application.DoEvents();
					pnEmpty.BringToFront();
					Application.DoEvents();
					pnMain.Controls.Add(_selectedOutput as Control);
					Application.DoEvents();
					pnMain.BringToFront();
					Application.DoEvents();
				}
				(_selectedOutput as Control).BringToFront();

				if (!xtraTabPagePrint.Controls.Contains(_selectedOutput.ColumnsColumns))
				{
					Application.DoEvents();
					xtraTabPagePrint.Controls.Add(_selectedOutput.ColumnsColumns);
				}
				_selectedOutput.ColumnsColumns.BringToFront();

				if (!xtraTabPageAdNotes.Controls.Contains(_selectedOutput.AdNotes))
				{
					Application.DoEvents();
					xtraTabPageAdNotes.Controls.Add(_selectedOutput.AdNotes);
				}
				_selectedOutput.AdNotes.BringToFront();

				if (!xtraTabPageSlideHeaders.Controls.Contains(_selectedOutput.SlideHeader))
				{
					Application.DoEvents();
					xtraTabPageSlideHeaders.Controls.Add(_selectedOutput.SlideHeader);
				}
				_selectedOutput.SlideHeader.BringToFront();

				if (!xtraTabPageSlideBullets.Controls.Contains(_selectedOutput.SlideBullets))
				{
					Application.DoEvents();
					xtraTabPageSlideBullets.Controls.Add(_selectedOutput.SlideBullets);
				}
				_selectedOutput.SlideBullets.BringToFront();

				Controller.Instance.Supertip.SetSuperTooltip(HelpButtonItem, _selectedOutput.HelpToolTip);
			}
			else
			{
				pnEmpty.BringToFront();
				Controller.Instance.Supertip.SetSuperTooltip(HelpButtonItem, null);
			}
		}

		private void UpdateButtonsStateAccordingSelectedOutput()
		{
			if (_selectedOutput != null)
			{
				_selectedOutput.AllowToSave = false;
				OptionsButtonItem.Checked = _selectedOutput.ShowOptions;
				xtraTabControlOptions.SelectedTabPageIndex = _selectedOutput.SelectedOptionChapterIndex;
				_selectedOutput.AllowToSave = true;

				splitContainerControl.PanelVisibility = _selectedOutput.ShowOptions ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
			}
		}

		public void Details_CheckedChanged(object sender, EventArgs e)
		{
			if (_selectedOutput.AllowToSave)
			{
				_selectedOutput.ShowOptions = OptionsButtonItem.Checked;
				_selectedOutput.SaveView();
				splitContainerControl.PanelVisibility = _selectedOutput.ShowOptions ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
			}
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			_selectedOutput.Preview();
		}

		public void Digital_Click(object sender, EventArgs e)
		{
			_selectedOutput.EditDigitalLegend();
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			_selectedOutput.PrintOutput();
		}

		public void Email_Click(object sender, EventArgs e)
		{
			_selectedOutput.Email();
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
						SaveSchedule(from.ScheduleName);
						Utilities.Instance.ShowInformation("Schedule was saved");
					}
					else
					{
						Utilities.Instance.ShowWarning("Schedule Name can't be empty");
					}
				}
			}
		}

		public void Reset_Click(object sender, EventArgs e)
		{
			_selectedOutput.ResetToDefault();
			SaveSchedule();
		}

		public void Help_Click(object sender, EventArgs e)
		{
			_selectedOutput.OpenHelp();
		}

		#region Options Stuff
		private void xtraTabControlDetails_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (_selectedOutput.AllowToSave)
			{
				_selectedOutput.SelectedOptionChapterIndex = xtraTabControlOptions.SelectedTabPageIndex;
				_selectedOutput.SaveView();
			}
		}
		#endregion
	}

	public enum GridType
	{
		DetailedGrid,
		MultiGrid,
	}

	public interface IGridOutputControl : ISettingsContainer
	{
		Schedule LocalSchedule { get; set; }
		ColumnsControl ColumnsColumns { get; }
		AdNotesControl AdNotes { get; }
		SlideBulletsControl SlideBullets { get; }
		SlideHeaderControl SlideHeader { get; }

		bool AllowToSave { get; set; }

		SuperTooltipInfo HelpToolTip { get; }

		bool ShowOptions { get; set; }
		int SelectedOptionChapterIndex { get; set; }

		bool EnableIDHeader { get; set; }
		bool EnableIndexHeader { get; set; }
		bool EnableDateHeader { get; set; }
		bool EnableColorHeader { get; set; }
		bool EnableSectionHeader { get; set; }
		bool EnablePCIHeader { get; set; }
		bool EnableFinalCostHeader { get; set; }
		bool EnablePublicationHeader { get; set; }
		bool EnablePercentOfPageHeader { get; set; }
		bool EnableCostHeader { get; set; }
		bool EnableDimensionsHeader { get; set; }
		bool EnableMechanicalsHeader { get; set; }
		bool EnableDeliveryHeader { get; set; }
		bool EnableDiscountHeader { get; set; }
		bool EnablePageSizeHeader { get; set; }
		bool EnableSquareHeader { get; set; }
		bool EnableDeadlineHeader { get; set; }
		bool EnableReadershipHeader { get; set; }
		bool EnableCommentsHeader { get; set; }

		bool ShowIDHeader { get; set; }
		bool ShowIndexHeader { get; set; }
		bool ShowDateHeader { get; set; }
		bool ShowColorHeader { get; set; }
		bool ShowSectionHeader { get; set; }
		bool ShowPCIHeader { get; set; }
		bool ShowFinalCostHeader { get; set; }
		bool ShowPublicationHeader { get; set; }
		bool ShowPercentOfPageHeader { get; set; }
		bool ShowCostHeader { get; set; }
		bool ShowDimensionsHeader { get; set; }
		bool ShowMechanicalsHeader { get; set; }
		bool ShowDeliveryHeader { get; set; }
		bool ShowDiscountHeader { get; set; }
		bool ShowPageSizeHeader { get; set; }
		bool ShowSquareHeader { get; set; }
		bool ShowDeadlineHeader { get; set; }
		bool ShowReadershipHeader { get; set; }
		bool ShowCommentsHeader { get; set; }

		bool EnableCommentsInPreview { get; set; }
		bool EnableSectionInPreview { get; set; }
		bool EnableMechanicalsInPreview { get; set; }
		bool EnableColumnInchesInPreview { get; set; }
		bool EnablePublicationInPreview { get; set; }
		bool EnablePageSizeInPreview { get; set; }
		bool EnablePercentOfPageInPreview { get; set; }
		bool EnableDimensionsInPreview { get; set; }
		bool EnableReadershipInPreview { get; set; }
		bool EnableDeliveryInPreview { get; set; }
		bool EnableDeadlineInPreview { get; set; }

		bool ShowCommentsInPreview { get; set; }
		bool ShowSectionInPreview { get; set; }
		bool ShowMechanicalsInPreview { get; set; }
		bool ShowColumnInchesInPreview { get; set; }
		bool ShowPublicationInPreview { get; set; }
		bool ShowPageSizeInPreview { get; set; }
		bool ShowPercentOfPageInPreview { get; set; }
		bool ShowDimensionsInPreview { get; set; }
		bool ShowReadershipInPreview { get; set; }
		bool ShowDeliveryInPreview { get; set; }
		bool ShowDeadlineInPreview { get; set; }

		int PositionCommentsInPreview { get; set; }
		int PositionSectionInPreview { get; set; }
		int PositionMechanicalsInPreview { get; set; }
		int PositionColumnInchesInPreview { get; set; }
		int PositionPublicationInPreview { get; set; }
		int PositionPageSizeInPreview { get; set; }
		int PositionPercentOfPageInPreview { get; set; }
		int PositionDimensionsInPreview { get; set; }
		int PositionReadershipInPreview { get; set; }
		int PositionDeliveryInPreview { get; set; }
		int PositionDeadlineInPreview { get; set; }

		int SelectedColumnsCount { get; }

		void UpdateColumnsAccordingToggles();
		void SetToggleStateAfterAdNotesChange();
		void SetPreviewState();
		void SetSlideHeader();
		void SaveView();
		void UpdateOutput(bool quickLoad);
		void ResetToDefault();
		void OpenHelp();
		void EditDigitalLegend();
		void PrintOutput();
		void Email();
		void Preview();
	}
}