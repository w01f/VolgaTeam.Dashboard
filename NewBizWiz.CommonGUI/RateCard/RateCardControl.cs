using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Asa.CommonGUI.RateCard
{
	[ToolboxItem(false)]
	public partial class RateCardControl : UserControl
	{
		private readonly RateCardManager _manager;
		private readonly ComboBoxEdit _listControl;

		public RateCardControl(RateCardManager manager, ComboBoxEdit listControl)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_manager = manager;
			_listControl = listControl;
			_listControl.SelectedIndexChanged -= comboBoxEditRateCards_EditValueChanged;
			_listControl.SelectedIndexChanged += comboBoxEditRateCards_EditValueChanged;
			Disposed += RateCardControl_Disposed;
		}

		private void RateCardControl_Disposed(object sender, EventArgs e)
		{
			_manager.ReleaseRateCards();
		}

		public void LoadRateCards()
		{
			if (!_manager.RateCardFolders.Any()) return;
			if (_listControl.Properties.Items.Count != 0) return;
			_listControl.Properties.Items.Clear();
			_listControl.Properties.Items.AddRange(_manager.RateCardFolders.Select(x => x.FolderName).ToArray());
			_listControl.SelectedIndex = 0;
		}

		private void comboBoxEditRateCards_EditValueChanged(object sender, EventArgs e)
		{
			var comboBox = sender as ComboBoxEdit;
			if (comboBox == null) return;
			var selectedIndex = comboBox.SelectedIndex;
			if (selectedIndex < 0) return;
			var selectedFolder = _manager.RateCardFolders[selectedIndex];
			if (!Controls.Contains(selectedFolder.RateCardContainer))
			{
				selectedFolder.LoadRateCards();
				Controls.Add(selectedFolder.RateCardContainer);
			}
			selectedFolder.RateCardContainer.BringToFront();
		}
	}
}