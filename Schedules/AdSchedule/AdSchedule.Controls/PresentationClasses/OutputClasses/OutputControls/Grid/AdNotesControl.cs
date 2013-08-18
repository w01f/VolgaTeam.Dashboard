using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.Core.Common;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class AdNotesControl : UserControl
	{
		private readonly IGridOutputControl _settingsContainer;

		public event EventHandler<EventArgs> OnHelp;

		public AdNotesControl(IGridOutputControl settingsContainer)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_settingsContainer = settingsContainer;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				checkEditShowAdNotes.Font = new Font(checkEditShowAdNotes.Font.FontFamily, checkEditShowAdNotes.Font.Size - 3, checkEditShowAdNotes.Font.Style);
			}
		}

		public void LoadAdNotes()
		{
			var adNotes = new SortedDictionary<int, string>();
			int maxNumber = 12;
			if (_settingsContainer.EnableColumnInchesInPreview)
				adNotes.Add(_settingsContainer.PositionColumnInchesInPreview > 0 && !adNotes.Keys.Contains(_settingsContainer.PositionColumnInchesInPreview) ? _settingsContainer.PositionColumnInchesInPreview : ++maxNumber, "Total Col In");
			if (_settingsContainer.EnableCommentsInPreview)
				adNotes.Add(_settingsContainer.PositionCommentsInPreview, "Comment");
			if (_settingsContainer.EnableDeadlineInPreview)
				adNotes.Add(_settingsContainer.PositionDeadlineInPreview > 0 && !adNotes.Keys.Contains(_settingsContainer.PositionDeadlineInPreview) ? _settingsContainer.PositionDeadlineInPreview : ++maxNumber, "Deadline");
			if (_settingsContainer.EnableDeliveryInPreview)
				adNotes.Add(_settingsContainer.PositionDeliveryInPreview > 0 && !adNotes.Keys.Contains(_settingsContainer.PositionDeliveryInPreview) ? _settingsContainer.PositionDeliveryInPreview : ++maxNumber, "Delivery");
			if (_settingsContainer.EnableDimensionsInPreview)
				adNotes.Add(_settingsContainer.PositionDimensionsInPreview > 0 && !adNotes.Keys.Contains(_settingsContainer.PositionDimensionsInPreview) ? _settingsContainer.PositionDimensionsInPreview : ++maxNumber, "Col. x Inches");
			if (_settingsContainer.EnableMechanicalsInPreview)
				adNotes.Add(_settingsContainer.PositionMechanicalsInPreview > 0 && !adNotes.Keys.Contains(_settingsContainer.PositionMechanicalsInPreview) ? _settingsContainer.PositionMechanicalsInPreview : ++maxNumber, "Mechanicals");
			if (_settingsContainer.EnablePageSizeInPreview)
				adNotes.Add(_settingsContainer.PositionPageSizeInPreview > 0 && !adNotes.Keys.Contains(_settingsContainer.PositionPageSizeInPreview) ? _settingsContainer.PositionPageSizeInPreview : ++maxNumber, "Page Size");
			if (_settingsContainer.EnablePercentOfPageInPreview && ListManager.Instance.ShareUnits.Count > 0)
				adNotes.Add(_settingsContainer.PositionPercentOfPageInPreview > 0 && !adNotes.Keys.Contains(_settingsContainer.PositionPercentOfPageInPreview) ? _settingsContainer.PositionPercentOfPageInPreview : ++maxNumber, "% of Page");
			if (_settingsContainer.EnablePublicationInPreview)
				adNotes.Add(_settingsContainer.PositionPublicationInPreview > 0 && !adNotes.Keys.Contains(_settingsContainer.PositionPublicationInPreview) ? _settingsContainer.PositionPublicationInPreview : ++maxNumber, "Publication");
			if (_settingsContainer.EnableReadershipInPreview)
				adNotes.Add(_settingsContainer.PositionReadershipInPreview > 0 && !adNotes.Keys.Contains(_settingsContainer.PositionReadershipInPreview) ? _settingsContainer.PositionReadershipInPreview : ++maxNumber, "Readership");
			if (_settingsContainer.EnableSectionInPreview)
				adNotes.Add(_settingsContainer.PositionSectionInPreview > 0 && !adNotes.Keys.Contains(_settingsContainer.PositionSectionInPreview) ? _settingsContainer.PositionSectionInPreview : ++maxNumber, "Section");

			checkEditShowAdNotes.Enabled = _settingsContainer.EnableCommentsHeader;
			checkEditShowAdNotes.Checked = _settingsContainer.ShowCommentsHeader;

			checkedListBoxAdNotes.Items.Clear();
			foreach (string adNotesPart in adNotes.Values)
			{
				switch (adNotesPart)
				{
					case "Total Col In":
						checkedListBoxAdNotes.Items.Add(adNotesPart, adNotesPart, _settingsContainer.ShowColumnInchesInPreview & !_settingsContainer.ShowSquareHeader ? CheckState.Checked : CheckState.Unchecked, true);
						break;
					case "Comment":
						checkedListBoxAdNotes.Items.Add(adNotesPart, adNotesPart, _settingsContainer.ShowCommentsInPreview ? CheckState.Checked : CheckState.Unchecked, true);
						break;
					case "Deadline":
						checkedListBoxAdNotes.Items.Add(adNotesPart, adNotesPart, _settingsContainer.ShowDeadlineInPreview & !_settingsContainer.ShowDeadlineHeader ? CheckState.Checked : CheckState.Unchecked, true);
						break;
					case "Col. x Inches":
						checkedListBoxAdNotes.Items.Add(adNotesPart, adNotesPart, _settingsContainer.ShowDimensionsInPreview & !_settingsContainer.ShowDimensionsHeader ? CheckState.Checked : CheckState.Unchecked, true);
						break;
					case "Mechanicals":
						checkedListBoxAdNotes.Items.Add(adNotesPart, adNotesPart, _settingsContainer.ShowMechanicalsInPreview & !_settingsContainer.ShowMechanicalsHeader ? CheckState.Checked : CheckState.Unchecked, true);
						break;
					case "Delivery":
						checkedListBoxAdNotes.Items.Add(adNotesPart, adNotesPart, _settingsContainer.ShowDeliveryInPreview & !_settingsContainer.ShowDeliveryHeader ? CheckState.Checked : CheckState.Unchecked, true);
						break;
					case "Section":
						checkedListBoxAdNotes.Items.Add(adNotesPart, adNotesPart, _settingsContainer.ShowSectionInPreview & !_settingsContainer.ShowSectionHeader ? CheckState.Checked : CheckState.Unchecked, true);
						break;
					case "Page Size":
						checkedListBoxAdNotes.Items.Add(adNotesPart, adNotesPart, _settingsContainer.ShowPageSizeInPreview & !_settingsContainer.ShowPageSizeHeader ? CheckState.Checked : CheckState.Unchecked, true);
						break;
					case "% of Page":
						checkedListBoxAdNotes.Items.Add(adNotesPart, adNotesPart, _settingsContainer.ShowPercentOfPageInPreview & !_settingsContainer.ShowPercentOfPageHeader ? CheckState.Checked : CheckState.Unchecked, true);
						break;
					case "Publication":
						checkedListBoxAdNotes.Items.Add(adNotesPart, adNotesPart, _settingsContainer.ShowPublicationInPreview & !_settingsContainer.ShowPublicationHeader ? CheckState.Checked : CheckState.Unchecked, true);
						break;
					case "Readership":
						checkedListBoxAdNotes.Items.Add(adNotesPart, adNotesPart, _settingsContainer.ShowReadershipInPreview & !_settingsContainer.ShowReadershipHeader ? CheckState.Checked : CheckState.Unchecked, true);
						break;
				}
			}
		}

		private void SaveAdNotes()
		{
			_settingsContainer.AllowToSave = false;
			_settingsContainer.ShowCommentsHeader = checkEditShowAdNotes.Checked;
			int position = 1;
			foreach (CheckedListBoxItem item in checkedListBoxAdNotes.Items)
			{
				switch (item.Description)
				{
					case "Total Col In":
						_settingsContainer.ShowColumnInchesInPreview = item.CheckState == CheckState.Checked ? true : false;
						_settingsContainer.PositionColumnInchesInPreview = position;
						break;
					case "Comment":
						_settingsContainer.ShowCommentsInPreview = item.CheckState == CheckState.Checked ? true : false;
						_settingsContainer.PositionCommentsInPreview = position;
						break;
					case "Deadline":
						_settingsContainer.ShowDeadlineInPreview = item.CheckState == CheckState.Checked ? true : false;
						_settingsContainer.PositionDeadlineInPreview = position;
						break;
					case "Col. x Inches":
						_settingsContainer.ShowDimensionsInPreview = item.CheckState == CheckState.Checked ? true : false;
						_settingsContainer.PositionDimensionsInPreview = position;
						break;
					case "Mechanicals":
						_settingsContainer.ShowMechanicalsInPreview = item.CheckState == CheckState.Checked ? true : false;
						_settingsContainer.PositionMechanicalsInPreview = position;
						break;
					case "Delivery":
						_settingsContainer.ShowDeliveryInPreview = item.CheckState == CheckState.Checked ? true : false;
						_settingsContainer.PositionDeliveryInPreview = position;
						break;
					case "Section":
						_settingsContainer.ShowSectionInPreview = item.CheckState == CheckState.Checked ? true : false;
						_settingsContainer.PositionSectionInPreview = position;
						break;
					case "Page Size":
						_settingsContainer.ShowPageSizeInPreview = item.CheckState == CheckState.Checked ? true : false;
						_settingsContainer.PositionPageSizeInPreview = position;
						break;
					case "% of Page":
						_settingsContainer.ShowPercentOfPageInPreview = item.CheckState == CheckState.Checked ? true : false;
						_settingsContainer.PositionPercentOfPageInPreview = position;
						break;
					case "Publication":
						_settingsContainer.ShowPublicationInPreview = item.CheckState == CheckState.Checked ? true : false;
						_settingsContainer.PositionPublicationInPreview = position;
						break;
					case "Readership":
						_settingsContainer.ShowReadershipInPreview = item.CheckState == CheckState.Checked ? true : false;
						_settingsContainer.PositionReadershipInPreview = position;
						break;
				}
				position++;
			}
			_settingsContainer.AllowToSave = true;
			_settingsContainer.SetToggleStateAfterAdNotesChange();
			_settingsContainer.SetPreviewState();
			_settingsContainer.SaveView();
		}

		private void checkEditShowAdNotes_CheckedChanged(object sender, EventArgs e)
		{
			checkedListBoxAdNotes.Enabled = checkEditShowAdNotes.Checked;
			buttonXDown.Enabled = checkEditShowAdNotes.Checked;
			buttonXUp.Enabled = checkEditShowAdNotes.Checked;
			SaveAdNotes();
		}

		private void buttonXUp_Click(object sender, EventArgs e)
		{
			if (checkedListBoxAdNotes.SelectedIndex >= 1)
			{
				checkedListBoxAdNotes.ItemChecking -= checkedListBoxAdNotes_ItemChecking;
				CheckedListBoxItem currentItem = checkedListBoxAdNotes.Items[checkedListBoxAdNotes.SelectedIndex];
				CheckedListBoxItem nextItem = checkedListBoxAdNotes.Items[checkedListBoxAdNotes.SelectedIndex - 1];
				string tempValue = nextItem.Value.ToString();
				string tempDescription = nextItem.Description;
				CheckState tempState = nextItem.CheckState;
				nextItem.Value = currentItem.Value;
				nextItem.Description = currentItem.Description;
				nextItem.CheckState = currentItem.CheckState;
				currentItem.Value = tempValue;
				currentItem.Description = tempDescription;
				currentItem.CheckState = tempState;
				checkedListBoxAdNotes.SelectedItem = nextItem;
				checkedListBoxAdNotes.ItemChecking += checkedListBoxAdNotes_ItemChecking;
				SaveAdNotes();
			}
		}

		private void buttonXDown_Click(object sender, EventArgs e)
		{
			if (checkedListBoxAdNotes.SelectedIndex >= 0 && checkedListBoxAdNotes.SelectedIndex < checkedListBoxAdNotes.Items.Count - 1)
			{
				checkedListBoxAdNotes.ItemChecking -= checkedListBoxAdNotes_ItemChecking;
				CheckedListBoxItem currentItem = checkedListBoxAdNotes.Items[checkedListBoxAdNotes.SelectedIndex];
				CheckedListBoxItem nextItem = checkedListBoxAdNotes.Items[checkedListBoxAdNotes.SelectedIndex + 1];
				string tempValue = nextItem.Value.ToString();
				string tempDescription = nextItem.Description;
				CheckState tempState = nextItem.CheckState;
				nextItem.Value = currentItem.Value;
				nextItem.Description = currentItem.Description;
				nextItem.CheckState = currentItem.CheckState;
				currentItem.Value = tempValue;
				currentItem.Description = tempDescription;
				currentItem.CheckState = tempState;
				checkedListBoxAdNotes.SelectedItem = nextItem;
				checkedListBoxAdNotes.ItemChecking += checkedListBoxAdNotes_ItemChecking;
				SaveAdNotes();
			}
		}

		private void checkedListBoxAdNotes_ItemChecking(object sender, ItemCheckingEventArgs e)
		{
			e.Cancel = false;
			if (e.NewValue == CheckState.Checked)
			{
				if (checkedListBoxAdNotes.CheckedItems.Count > 3)
				{
					Utilities.Instance.ShowWarning("You can select only up to 4 Ad-Notes");
					e.Cancel = true;
				}
			}
		}

		private void checkedListBoxAdNotes_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			SaveAdNotes();
		}

		private void pbHelp_Click(object sender, EventArgs e)
		{
			if (OnHelp != null)
				OnHelp(this, EventArgs.Empty);
		}

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
}