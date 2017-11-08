using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.GUI.ContentEditors.Events;
using Asa.Common.GUI.ContentEditors.Interfaces;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;

namespace Asa.Common.GUI.RateCard
{
	[ToolboxItem(false)]
	public abstract partial class RateCardControl : UserControl, IContentControl
	{
		protected RateCardManager _manager;
		protected ComboBoxEdit _listControl;

		public RateCardControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region IContentControl
		public abstract string Identifier { get; }
		public bool IsActive { get; set; }
		public abstract RibbonTabItem TabPage { get;  }

		public void InitMetaData()
		{
			TabPage.Tag = Identifier;
		}

		public virtual void InitControl()
		{
			_listControl.SelectedIndexChanged -= comboBoxEditRateCards_EditValueChanged;
			_listControl.SelectedIndexChanged += comboBoxEditRateCards_EditValueChanged;
			Disposed += RateCardControl_Disposed;
		}

		public virtual void ShowControl(ContentOpenEventArgs args = null)
		{
			IsActive = true;
			LoadRateCards();
		}

		public abstract void GetHelp();
		#endregion

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