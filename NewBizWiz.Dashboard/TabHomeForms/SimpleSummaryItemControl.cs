using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.Core.Dashboard;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class SimpleSummaryItemControl : UserControl
	{
		private readonly SimpleSummaryItemContainer _parent;
		private int _itemNumber;

		public SimpleSummaryItemControl(SimpleSummaryItemContainer parent)
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			AppManager.Instance.SetClickEventHandler(this);
			_parent = parent;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTotal.Font = new Font(laTotal.Font.FontFamily, laTotal.Font.Size - 2, laTotal.Font.Style);
			}
			if (FormMain.Instance != null)
			{
				comboBoxEditItem.MouseUp += FormMain.Instance.Editor_MouseUp;
				comboBoxEditItem.MouseDown += FormMain.Instance.Editor_MouseDown;
				comboBoxEditItem.Enter += FormMain.Instance.Editor_Enter;
				spinEditMonthly.MouseUp += FormMain.Instance.Editor_MouseUp;
				spinEditMonthly.MouseDown += FormMain.Instance.Editor_MouseDown;
				spinEditMonthly.Enter += FormMain.Instance.Editor_Enter;
				spinEditTotal.MouseUp += FormMain.Instance.Editor_MouseUp;
				spinEditTotal.MouseDown += FormMain.Instance.Editor_MouseDown;
				spinEditTotal.Enter += FormMain.Instance.Editor_Enter;
				memoEditDetails.MouseUp += FormMain.Instance.Editor_MouseUp;
				memoEditDetails.MouseDown += FormMain.Instance.Editor_MouseDown;
				memoEditDetails.Enter += FormMain.Instance.Editor_Enter;
			}

			OutputItem = new SimpleSummaryOutputControl();
			if (_parent.OutputContainer != null)
				_parent.OutputContainer.AddItem(OutputItem);
		}

		public SimpleSummaryOutputControl OutputItem { get; set; }

		public int ItemNumber
		{
			get { return _itemNumber; }
			set
			{
				_itemNumber = value;
				if (OutputItem != null)
					OutputItem.ItemNumber = value;
				laNumber.Text = _itemNumber.ToString();
			}
		}

		public void LoadSavedState()
		{
			ckItem.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ItemsState[_itemNumber - 1].ShowValue;
			ckDetails.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ItemsState[_itemNumber - 1].ShowDescription;
			ckMonthly.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ItemsState[_itemNumber - 1].ShowMonthly;
			ckTotal.Checked = ViewSettingsManager.Instance.SimpleSummaryState.ItemsState[_itemNumber - 1].ShowTotal;

			comboBoxEditItem.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.SimpleSummaryState.ItemsState[_itemNumber - 1].Value) ? ViewSettingsManager.Instance.SimpleSummaryState.ItemsState[_itemNumber - 1].Value : null;
			memoEditDetails.EditValue = !string.IsNullOrEmpty(ViewSettingsManager.Instance.SimpleSummaryState.ItemsState[_itemNumber - 1].Description) ? ViewSettingsManager.Instance.SimpleSummaryState.ItemsState[_itemNumber - 1].Description : null;
			spinEditMonthly.Value = (decimal)ViewSettingsManager.Instance.SimpleSummaryState.ItemsState[_itemNumber - 1].Monthly;
			spinEditTotal.Value = (decimal)ViewSettingsManager.Instance.SimpleSummaryState.ItemsState[_itemNumber - 1].Total;
		}

		private void pbDelete_Click(object sender, EventArgs e)
		{
			_parent.DeleteItem(_itemNumber);
			_parent.OutputContainer.DeleteItem(OutputItem);
		}

		private void pbUp_Click(object sender, EventArgs e)
		{
			_parent.UpItem(_itemNumber);
			_parent.OutputContainer.UpItem(_itemNumber);
		}

		private void pbDown_Click(object sender, EventArgs e)
		{
			_parent.DownItem(_itemNumber);
			_parent.OutputContainer.DownItem(_itemNumber);
		}

		private void SimpleSummaryItemControl_Load(object sender, EventArgs e)
		{
			comboBoxEditItem.Properties.Items.Clear();
			if (ListManager.Instance != null && ListManager.Instance.SimpleSummaryLists != null && ListManager.Instance.SimpleSummaryLists.Details != null)
				comboBoxEditItem.Properties.Items.AddRange(ListManager.Instance.SimpleSummaryLists.Details);
		}

		private void ckMonthly_CheckedChanged(object sender, EventArgs e)
		{
			spinEditMonthly.Enabled = ckMonthly.Checked;
			OutputItem.MonthlyVisible = ckMonthly.Checked;
			if (SimpleSummaryControl.Instance.AllowToSave)
			{
				SimpleSummaryControl.Instance.UpdateTotalValues();
				SimpleSummaryControl.Instance.SettingsNotSaved = true;
			}
		}

		private void ckTotal_CheckedChanged(object sender, EventArgs e)
		{
			spinEditTotal.Enabled = ckTotal.Checked;
			OutputItem.TotalVisible = ckTotal.Checked;
			if (SimpleSummaryControl.Instance.AllowToSave)
			{
				SimpleSummaryControl.Instance.UpdateTotalValues();
				SimpleSummaryControl.Instance.SettingsNotSaved = true;
			}
		}

		private void ckItem_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditItem.Enabled = ckItem.Checked;
			OutputItem.ItemVisible = ckItem.Checked;
			if (SimpleSummaryControl.Instance.AllowToSave)
				SimpleSummaryControl.Instance.SettingsNotSaved = true;
		}

		private void ckDetails_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDetails.Enabled = ckDetails.Checked;
			OutputItem.DetailsVisible = ckDetails.Checked;
			if (SimpleSummaryControl.Instance.AllowToSave)
				SimpleSummaryControl.Instance.SettingsNotSaved = true;
		}

		private void comboBoxEditItem_EditValueChanged(object sender, EventArgs e)
		{
			if (comboBoxEditItem.EditValue != null)
				OutputItem.ItemValue = comboBoxEditItem.EditValue.ToString();
			else
				OutputItem.ItemValue = string.Empty;
			if (SimpleSummaryControl.Instance.AllowToSave)
			{
				SimpleSummaryControl.Instance.UpdateTotalValues();
				SimpleSummaryControl.Instance.SettingsNotSaved = true;
			}
		}

		private void memoEditDetails_EditValueChanged(object sender, EventArgs e)
		{
			if (memoEditDetails.EditValue != null)
				OutputItem.DetailsValue = memoEditDetails.EditValue.ToString().Replace(Environment.NewLine, "; ");
			else
				OutputItem.DetailsValue = string.Empty;
			if (SimpleSummaryControl.Instance.AllowToSave)
				SimpleSummaryControl.Instance.SettingsNotSaved = true;
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			OutputItem.MonthlyValue = spinEditMonthly.Value.ToString("$#,##0.00");
			if (SimpleSummaryControl.Instance.AllowToSave)
			{
				SimpleSummaryControl.Instance.UpdateTotalValues();
				SimpleSummaryControl.Instance.SettingsNotSaved = true;
			}
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			OutputItem.ToatlValue = spinEditTotal.Value.ToString("$#,##0.00");
			if (SimpleSummaryControl.Instance.AllowToSave)
			{
				SimpleSummaryControl.Instance.UpdateTotalValues();
				SimpleSummaryControl.Instance.SettingsNotSaved = true;
			}
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

		#region Output Stuff
		public bool ShowMonthly
		{
			get { return ckMonthly.Checked; }
		}

		public bool ShowTotal
		{
			get { return ckTotal.Checked; }
		}

		public bool ShowValueOutput
		{
			get { return ckItem.Checked & OutputItem.ItemChecked; }
		}

		public bool ShowDescriptionOutput
		{
			get { return ckDetails.Checked & OutputItem.DetailsChecked; }
		}

		public bool ShowMonthlyOutput
		{
			get { return ckMonthly.Checked & OutputItem.MonthlyChecked; }
		}

		public bool ShowTotalOutput
		{
			get { return ckTotal.Checked & OutputItem.TotalChecked; }
		}

		public string ItemTitle
		{
			get { return comboBoxEditItem.EditValue != null && ckItem.Checked ? comboBoxEditItem.EditValue.ToString() : string.Empty; }
		}

		public string OutputItemTitle
		{
			get { return comboBoxEditItem.EditValue != null && ckItem.Checked && OutputItem.ItemChecked ? comboBoxEditItem.EditValue.ToString() : string.Empty; }
		}

		public string ItemDetail
		{
			get { return memoEditDetails.EditValue != null && ckDetails.Checked ? memoEditDetails.EditValue.ToString() : string.Empty; }
		}

		public string ItemDetailOutput
		{
			get { return memoEditDetails.EditValue != null && ckDetails.Checked && OutputItem.DetailsChecked ? memoEditDetails.EditValue.ToString() : string.Empty; }
		}

		public double? MonthlyValue
		{
			get { return ckMonthly.Checked ? (double?)spinEditMonthly.Value : null; }
		}

		public double? TotalValue
		{
			get { return ckTotal.Checked ? (double?)spinEditTotal.Value : null; }
		}

		public double? OutputMonthlyValue
		{
			get { return ckMonthly.Checked && OutputItem.MonthlyChecked ? (double?)spinEditMonthly.Value : null; }
		}

		public double? OutputTotalValue
		{
			get { return ckTotal.Checked && OutputItem.TotalChecked ? (double?)spinEditTotal.Value : null; }
		}

		public bool Complited
		{
			get { return !ckItem.Checked || !string.IsNullOrEmpty(ItemTitle); }
		}
		#endregion
	}
}