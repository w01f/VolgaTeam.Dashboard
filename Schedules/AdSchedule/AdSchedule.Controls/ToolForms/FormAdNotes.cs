using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses;
using NewBizWiz.AdSchedule.Controls.Properties;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
	public partial class FormAdNotes : MetroForm
	{
		private int _selectedCommentRecordNumber = -1;
		private int _selectedDeadlineRecordNumber = -1;
		private int _selectedSectionRecordNumber = -1;
		private bool _useMultiLineComment;
		private bool _useMultiLineSection;

		public FormAdNotes()
		{
			InitializeComponent();
			if (ListManager.Instance.Mechanicals.Count > 0)
			{
				xtraTabControlMechanicals.TabPages.Clear();
				foreach (MechanicalType mechanicalType in ListManager.Instance.Mechanicals)
				{
					var mechanicalPage = new MechanicalControl(mechanicalType);
					xtraTabControlMechanicals.TabPages.Add(mechanicalPage);
				}
			}
			else
				tabItemMechanicals.Visible = false;
			tabItemDeadlines.Visible = ListManager.Instance.Deadlines.Count > 0;
		}

		public DateTime Date { get; set; }

		public string CustomComment
		{
			get { return ckComment.Checked && textEditComment.EditValue != null ? textEditComment.EditValue.ToString() : string.Empty; }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					textEditComment.EditValue = value;
					ckComment.Checked = true;
				}
				else
				{
					textEditComment.EditValue = null;
					ckComment.Checked = false;
				}
				UpdateCommentState();
			}
		}

		public NameCodePair[] Comments
		{
			get
			{
				var comments = new List<NameCodePair>();
				foreach (CheckedListBoxItem item in checkedListBoxControlComments.Items)
				{
					if (item.CheckState == CheckState.Checked)
					{
						var comment = new NameCodePair();
						comment.Name = item.Description;
						if (item.Value != null)
							comment.Code = item.Value.ToString();
						comments.Add(comment);
					}
				}
				return comments.ToArray();
			}
			set
			{
				checkedListBoxControlComments.Items.Clear();
				_useMultiLineComment = ListManager.Instance.Notes.Any(x => !string.IsNullOrEmpty(x.Code));
				int i = 0;
				foreach (NameCodePair comment in ListManager.Instance.Notes)
				{
					bool itemChecked = value.Any(x => x.Name.Equals(comment.Name) && x.Code.Equals(comment.Code));
					checkedListBoxControlComments.Items.Add(comment.Code, comment.Name, itemChecked ? CheckState.Checked : CheckState.Unchecked, true);
					if (itemChecked && _selectedCommentRecordNumber == -1)
						_selectedCommentRecordNumber = i;
					i++;
				}
				UpdateCommentState();
			}
		}

		public string CustomSection
		{
			get { return ckSection.Checked && textEditSection.EditValue != null ? textEditSection.EditValue.ToString() : string.Empty; }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					textEditSection.EditValue = value;
					ckSection.Checked = true;
				}
				else
				{
					textEditSection.EditValue = null;
					ckSection.Checked = false;
				}
				UpdateSectionState();
			}
		}

		public NameCodePair[] Sections
		{
			get
			{
				var Sections = new List<NameCodePair>();
				foreach (CheckedListBoxItem item in checkedListBoxControlSections.Items)
				{
					if (item.CheckState == CheckState.Checked)
					{
						var Section = new NameCodePair();
						Section.Name = item.Description;
						if (item.Value != null)
							Section.Code = item.Value.ToString();
						Sections.Add(Section);
					}
				}
				return Sections.ToArray();
			}
			set
			{
				checkedListBoxControlSections.Items.Clear();
				_useMultiLineSection = ListManager.Instance.Sections.Any(x => !string.IsNullOrEmpty(x.Code));
				int i = 0;
				foreach (NameCodePair section in ListManager.Instance.Sections)
				{
					bool itemChecked = value.Any(x => x.Name.Equals(section.Name) && x.Code.Equals(section.Code));
					checkedListBoxControlSections.Items.Add(section.Code, section.Name, itemChecked ? CheckState.Checked : CheckState.Unchecked, true);
					if (itemChecked && _selectedSectionRecordNumber == -1)
						_selectedSectionRecordNumber = i;
					i++;
				}
				UpdateSectionState();
			}
		}

		public string Deadline
		{
			get
			{
				if (ckDeadline.Checked)
					return textEditDeadline.Text;
				else
					return checkedListBoxControlDeadline.CheckedIndices.Count > 0 ? (checkedListBoxControlDeadline.Items[checkedListBoxControlDeadline.CheckedIndices[0]]).Description : string.Empty;
			}
			set
			{
				checkedListBoxControlDeadline.Items.Clear();
				foreach (string deadline in ListManager.Instance.Deadlines)
					checkedListBoxControlDeadline.Items.Add(deadline, deadline, CheckState.Unchecked, true);

				textEditDeadline.Text = value;
				int i = 0;
				foreach (CheckedListBoxItem item in checkedListBoxControlDeadline.Items)
				{
					if (item.Description.Equals(value))
					{
						item.CheckState = CheckState.Checked;
						_selectedDeadlineRecordNumber = i;
						textEditComment.Text = string.Empty;
						ckDeadline.Checked = false;
						break;
					}
					i++;
				}
			}
		}

		public string Mechanicals
		{
			get
			{
				if (ckMechanicals.Checked)
					return textEditMechanicals.Text;
				else
					return null;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					ckMechanicals.Checked = true;
					textEditMechanicals.Text = value;
				}
			}
		}

		private void UpdateCommentState()
		{
			if (textEditComment.EditValue != null || checkedListBoxControlComments.CheckedItems.Count > 0)
				tabItemComments.Image = Resources.Selected;
			else
				tabItemComments.Image = Resources.Unselected;
		}

		private void UpdateSectionState()
		{
			if (textEditSection.EditValue != null || checkedListBoxControlSections.CheckedItems.Count > 0)
				tabItemSections.Image = Resources.Selected;
			else
				tabItemSections.Image = Resources.Unselected;
		}

		private void UpdateMechanicalsState()
		{
			if (!string.IsNullOrEmpty(textEditMechanicals.Text))
				tabItemMechanicals.Image = Resources.Selected;
			else
				tabItemMechanicals.Image = Resources.Unselected;
		}

		private void UpdateDeadlineState()
		{
			if (!string.IsNullOrEmpty(textEditDeadline.Text) || checkedListBoxControlDeadline.CheckedItems.Count > 0)
				tabItemDeadlines.Image = Resources.Selected;
			else
				tabItemDeadlines.Image = Resources.Unselected;
		}

		private void ckComment_CheckedChanged(object sender, EventArgs e)
		{
			if (ckComment.Checked && !_useMultiLineComment)
				checkedListBoxControlComments.UnCheckAll();
			textEditComment.Enabled = ckComment.Checked;
			textEditComment.EditValue = ckComment.Checked ? textEditComment.EditValue : null;
		}

		private void textEditComment_EditValueChanged(object sender, EventArgs e)
		{
			UpdateCommentState();
		}

		private void checkedListBoxControlComments_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.State == CheckState.Checked && !_useMultiLineComment)
			{
				ckComment.Checked = false;
				foreach (CheckedListBoxItem item in checkedListBoxControlComments.Items)
				{
					if (item != checkedListBoxControlComments.Items[e.Index] && item.CheckState == CheckState.Checked)
					{
						item.CheckState = CheckState.Unchecked;
					}
				}
			}
			UpdateCommentState();
		}

		private void buttonXClearComment_Click(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure you want to clear selected comments?") == DialogResult.Yes)
			{
				ckComment.Checked = false;
				textEditComment.EditValue = null;
				foreach (CheckedListBoxItem item in checkedListBoxControlComments.Items)
					item.CheckState = CheckState.Unchecked;
			}
		}

		private void ckSection_CheckedChanged(object sender, EventArgs e)
		{
			if (ckSection.Checked && !_useMultiLineSection)
				checkedListBoxControlSections.UnCheckAll();
			textEditSection.Enabled = ckSection.Checked;
			textEditSection.EditValue = ckSection.Checked ? textEditSection.EditValue : null;
		}

		private void textEditSection_EditValueChanged(object sender, EventArgs e)
		{
			UpdateSectionState();
		}

		private void checkedListBoxControlSections_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.State == CheckState.Checked && !_useMultiLineSection)
			{
				ckSection.Checked = false;
				foreach (CheckedListBoxItem item in checkedListBoxControlSections.Items)
				{
					if (item != checkedListBoxControlSections.Items[e.Index] && item.CheckState == CheckState.Checked)
					{
						item.CheckState = CheckState.Unchecked;
					}
				}
			}
			UpdateSectionState();
		}

		private void buttonXClearSection_Click(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure you want to clear selected sections?") == DialogResult.Yes)
			{
				ckSection.Checked = false;
				textEditSection.EditValue = null;
				foreach (CheckedListBoxItem item in checkedListBoxControlSections.Items)
					item.CheckState = CheckState.Unchecked;
			}
		}

		private void ckDeadline_CheckedChanged(object sender, EventArgs e)
		{
			if (ckDeadline.Checked)
				checkedListBoxControlDeadline.UnCheckAll();
			else
				textEditDeadline.EditValue = string.Empty;
			textEditDeadline.Enabled = ckDeadline.Checked;
		}

		private void textEditDeadline_EditValueChanged(object sender, EventArgs e)
		{
			UpdateDeadlineState();
		}

		private void checkedListBoxControlDeadline_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.State == CheckState.Checked)
			{
				ckDeadline.Checked = false;
				foreach (CheckedListBoxItem item in checkedListBoxControlDeadline.Items)
				{
					if (item != checkedListBoxControlDeadline.Items[e.Index] && item.CheckState == CheckState.Checked)
					{
						item.CheckState = CheckState.Unchecked;
					}
				}
			}
			UpdateDeadlineState();
		}

		private void buttonXClearDeadline_Click(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure you want to clear selected deadline?") == DialogResult.Yes)
			{
				ckDeadline.Checked = true;
				textEditDeadline.EditValue = string.Empty;
			}
		}

		private void ckMechanicals_CheckedChanged(object sender, EventArgs e)
		{
			if (!ckMechanicals.Checked)
				textEditMechanicals.EditValue = string.Empty;
			textEditMechanicals.Enabled = ckMechanicals.Checked;
		}

		private void textEditMechanicals_EditValueChanged(object sender, EventArgs e)
		{
			UpdateMechanicalsState();
		}

		private void buttonXClearMechanicals_Click(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure you want to clear mechanicals?") == DialogResult.Yes)
			{
				ckMechanicals.Checked = false;
				textEditMechanicals.EditValue = string.Empty;
			}
		}

		private void FormAdNotes_Load(object sender, EventArgs e)
		{
			if (_selectedCommentRecordNumber != -1)
				checkedListBoxControlComments.MakeItemVisible(_selectedCommentRecordNumber);
			if (_selectedSectionRecordNumber != -1)
				checkedListBoxControlSections.MakeItemVisible(_selectedSectionRecordNumber);
			if (_selectedDeadlineRecordNumber != -1)
				checkedListBoxControlDeadline.MakeItemVisible(_selectedDeadlineRecordNumber);
		}

		private void tabControlAdNotes_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
		{
			FormAdNotes_Load(null, null);
		}

		private void pbHelp_Click(object sender, EventArgs e)
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("adnotes");
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