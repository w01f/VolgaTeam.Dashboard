﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using AdScheduleBuilder.BusinessClasses;
using AdScheduleBuilder.ToolForms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class GridsControl : UserControl
	{
		private static GridsControl _instance;
		private readonly SuperTooltipInfo _infoTabSlideBulletsHelpTooltip = new SuperTooltipInfo("Slide Totals Help", "", "Learn more about adding financial info and schedule totals to your schedule slides", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _infoTabSlideHeaderHelpTooltip = new SuperTooltipInfo("Slide Headers Help", "", "Learn more about adding Slide Header information to your schedule slides", null, null, eTooltipColor.Gray);
		private string _infoHelpKey = string.Empty;
		private IGridOutputControl _selectedOutput;

		#region Operation Buttons
		public ButtonItem HelpButtonItem { get; set; }
		public ButtonItem OptionsButtonItem { get; set; }
		#endregion

		private GridsControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public static GridsControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new GridsControl();
				return _instance;
			}
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
				_selectedOutput.LocalSchedule.ViewSettings.SaveDefaultViewSettings();
				ScheduleManager.Instance.SaveSchedule(_selectedOutput.LocalSchedule, true, _selectedOutput as Control);
				_selectedOutput.UpdateOutput(true);
			}
		}

		public static void RemoveInstance()
		{
			try
			{
				_instance.Dispose();
			}
			catch { }
			finally
			{
				_instance = null;
			}
		}

		public void SelectGrid(GridType gridType)
		{
			switch (gridType)
			{
				case GridType.DetailedGrid:
					_selectedOutput = OutputDetailedGridControl.Instance;
					HelpButtonItem = FormMain.Instance.buttonItemDetailedGridHelp;
					OptionsButtonItem = FormMain.Instance.buttonItemDetailedGridDetails;
					break;
				case GridType.MultiGrid:
					_selectedOutput = OutputMultiGridControl.Instance;
					HelpButtonItem = FormMain.Instance.buttonItemMultiGridHelp;
					OptionsButtonItem = FormMain.Instance.buttonItemMultiGridDetails;
					break;
				case GridType.ChronoGrid:
					_selectedOutput = OutputChronologicalControl.Instance;
					HelpButtonItem = FormMain.Instance.buttonItemChronoGridHelp;
					OptionsButtonItem = FormMain.Instance.buttonItemChronoGridDetails;
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

				if (!xtraTabPagePrint.Controls.Contains(_selectedOutput.PrintColumns))
				{
					Application.DoEvents();
					xtraTabPagePrint.Controls.Add(_selectedOutput.PrintColumns);
				}
				_selectedOutput.PrintColumns.BringToFront();

				if (!xtraTabPageAdNotes.Controls.Contains(_selectedOutput.AdNotes))
				{
					Application.DoEvents();
					xtraTabPageAdNotes.Controls.Add(_selectedOutput.AdNotes);
				}
				_selectedOutput.AdNotes.BringToFront();

				if (!pnSlideInfoBody.Controls.Contains(_selectedOutput.SlideBullets))
				{
					Application.DoEvents();
					pnSlideInfoBody.Controls.Add(_selectedOutput.SlideBullets);
				}
				if (!pnSlideInfoBody.Controls.Contains(_selectedOutput.SlideHeader))
				{
					Application.DoEvents();
					pnSlideInfoBody.Controls.Add(_selectedOutput.SlideHeader);
				}
				UpdateInfoTab();

				FormMain.Instance.superTooltip.SetSuperTooltip(HelpButtonItem, _selectedOutput.HelpToolTip);
			}
			else
			{
				pnEmpty.BringToFront();
				FormMain.Instance.superTooltip.SetSuperTooltip(HelpButtonItem, null);
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

		public void buttonItemGridsDetails_CheckedChanged(object sender, EventArgs e)
		{
			if (_selectedOutput.AllowToSave)
			{
				_selectedOutput.ShowOptions = OptionsButtonItem.Checked;
				_selectedOutput.SaveView();
				splitContainerControl.PanelVisibility = _selectedOutput.ShowOptions ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
			}
		}

		public void buttonItemGridsPreview_Click(object sender, EventArgs e)
		{
			_selectedOutput.Preview();
		}

		public void buttonItemGridsPowerPoint_Click(object sender, EventArgs e)
		{
			_selectedOutput.PrintOutput();
		}

		public void buttonItemGridsEmail_Click(object sender, EventArgs e)
		{
			_selectedOutput.Email();
		}

		public void buttonItemGridsSave_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			AppManager.ShowInformation("Schedule Saved");
		}

		public void buttonItemGridsSaveAs_Click(object sender, EventArgs e)
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
						AppManager.ShowInformation("Schedule was saved");
					}
					else
					{
						AppManager.ShowWarning("Schedule Name can't be empty");
					}
				}
			}
		}

		public void buttonItemGridsReset_Click(object sender, EventArgs e)
		{
			_selectedOutput.ResetToDefault();
			SaveSchedule();
		}

		public void buttonItemGridsHelp_Click(object sender, EventArgs e)
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

		private void UpdateInfoTab()
		{
			if (buttonXSlideBullets.Checked)
			{
				_selectedOutput.SlideBullets.BringToFront();
				_infoHelpKey = "totalsnavbar";
				superTooltip.SetSuperTooltip(pbInfoHelp, _infoTabSlideBulletsHelpTooltip);
			}
			else if (buttonXSlideHeaders.Checked)
			{
				_selectedOutput.SlideHeader.BringToFront();
				_infoHelpKey = "headersnavbar";
				superTooltip.SetSuperTooltip(pbInfoHelp, _infoTabSlideHeaderHelpTooltip);
			}
		}

		private void buttonXSlideInfoSelector_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button != null && !button.Checked)
			{
				buttonXSlideBullets.Checked = false;
				buttonXSlideHeaders.Checked = false;
			}
			button.Checked = true;
		}

		private void buttonXSlideHeaders_CheckedChanged(object sender, EventArgs e)
		{
			UpdateInfoTab();
		}

		private void InfoHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink(_infoHelpKey);
		}
		#endregion

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}

	public enum GridType
	{
		DetailedGrid,
		MultiGrid,
		ChronoGrid
	}

	public interface IGridOutputControl : ISettingsContainer
	{
		Schedule LocalSchedule { get; set; }
		PrintControl PrintColumns { get; }
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
		void PrintOutput();
		void Email();
		void Preview();
	}
}