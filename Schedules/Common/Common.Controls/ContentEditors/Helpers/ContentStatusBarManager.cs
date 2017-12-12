using System;
using System.Drawing;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;

namespace Asa.Schedules.Common.Controls.ContentEditors.Helpers
{
	public class ContentStatusBarManager
	{
		private LabelItem _slideInfoLabel;

		public Bar StatusBar { get; set; }
		public ItemContainer StatusBarMainItemsContainer { get; set; }
		public ItemContainer StatusBarAdditionalItemsContainer { get; set; }
		public Color? TextColor { get; set; }

		public static ContentStatusBarManager Instance { get; } = new ContentStatusBarManager();

		private ContentStatusBarManager() { }

		public void FillStatusBarMainCommonInfo()
		{
			StatusBarMainItemsContainer.SubItems.Clear();
			var appInfoLabel = new LabelItem();
			appInfoLabel.Text = String.Format("{0} v{1}",
				PopupMessageHelper.Instance.Title,
				FileStorageManager.Instance.Version
			);

			if (TextColor.HasValue)
				appInfoLabel.ForeColor = TextColor.Value;

			StatusBarMainItemsContainer.SubItems.Add(appInfoLabel);
			StatusBarMainItemsContainer.RecalcSize();
			StatusBar.RecalcLayout();
		}

		public void FillStatusBarAdditionalCommonInfo()
		{
			InitSlideInfoLabel();
			StatusBarAdditionalItemsContainer.SubItems.Clear();
			StatusBarAdditionalItemsContainer.SubItems.Add(_slideInfoLabel);
			StatusBarAdditionalItemsContainer.RecalcSize();
			StatusBar.RecalcLayout();
		}

		private void InitSlideInfoLabel()
		{
			if (_slideInfoLabel != null) return;

			_slideInfoLabel = new LabelItem();
			if (TextColor.HasValue)
				_slideInfoLabel.ForeColor = TextColor.Value;

			OnSlideSettingsChanged(null, EventArgs.Empty);

			SlideSettingsManager.Instance.SettingsChanged += OnSlideSettingsChanged;
		}

		private void OnSlideSettingsChanged(object o, EventArgs e)
		{
			_slideInfoLabel.Text = String.Format("{0}  {1}",
				MasterWizardManager.Instance.SelectedWizard.Name,
				SlideSettingsManager.Instance.SlideSettings.SizeFormatted);
			StatusBarAdditionalItemsContainer.RecalcSize();
			StatusBar.RecalcLayout();
		}
	}
}
