using System;
using System.Drawing;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar;

namespace Asa.Schedules.Common.Controls.ContentEditors.Helpers
{
	public class ContentStatusBarManager
	{
		public Bar StatusBar { get; set; }
		public ItemContainer StatusBarItemsContainer { get; set; }
		public Color? TextColor { get; set; }

		public static ContentStatusBarManager Instance { get; } = new ContentStatusBarManager();

		private ContentStatusBarManager() { }

		public void FillStatusBarWithCommonInfo()
		{
			StatusBarItemsContainer.SubItems.Clear();
			var appInfoLabel = new LabelItem();
			appInfoLabel.Text = String.Format("{0} v{1}",
				PopupMessageHelper.Instance.Title,
				FileStorageManager.Instance.Version
			);

			if (TextColor.HasValue)
				appInfoLabel.ForeColor = TextColor.Value;

			StatusBarItemsContainer.SubItems.Add(appInfoLabel);
			StatusBarItemsContainer.RecalcSize();
			StatusBar.RecalcLayout();
		}
	}
}
