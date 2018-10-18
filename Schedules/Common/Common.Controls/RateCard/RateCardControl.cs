using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.GUI.RateCard;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using Asa.Schedules.Common.Controls.ContentEditors.Interfaces;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;

namespace Asa.Schedules.Common.Controls.RateCard
{
	[ToolboxItem(false)]
	public abstract partial class RateCardControl : UserControl, IContentControl
	{
		protected RateCardManager _manager;
		protected ComboBoxEdit _listControl;

		protected RateCardControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region IContentControl
		public abstract string Identifier { get; }
		public bool IsActive { get; set; }
		public bool RequreScheduleInfo => false;
		public bool ShowScheduleInfo => true;
		public bool RibbonAlwaysCollapsed => false;
		public abstract RibbonTabItem TabPage { get;  }

		public void InitMetaData()
		{
			TabPage.Tag = Identifier;
		}

		public virtual void InitControl()
		{
			_listControl.SelectedIndexChanged -= OnRateCardsListEditValueChanged;
			_listControl.SelectedIndexChanged += OnRateCardsListEditValueChanged;
			Disposed += RateCardControl_Disposed;
		}

		public abstract void InitBusinessObjects();

		public virtual void ShowControl(ContentOpenEventArgs args = null)
		{
			IsActive = true;
			ContentStatusBarManager.Instance.FillStatusBarMainCommonInfo();
			ContentStatusBarManager.Instance.FillStatusBarAdditionalCommonInfo();
			LoadRateCards();
		}

		public abstract void GetHelp();
		#endregion

		private void RateCardControl_Disposed(object sender, EventArgs e)
		{
			_manager.ReleaseRateCards();
		}

		private void LoadRateCards()
		{
			if (!_manager.RateCardFolders.Any()) return;
			if (_listControl.Properties.Items.Count != 0) return;
			_listControl.Properties.Items.Clear();
			_listControl.Properties.Items.AddRange(_manager.RateCardFolders.Select(x => x.FolderName).ToArray());
			_listControl.SelectedIndex = 0;
		}

		private void OnRateCardsListEditValueChanged(object sender, EventArgs e)
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