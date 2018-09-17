using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Asa.Business.Solutions.StarApp.Configuration;
using Asa.Common.GUI.Preview;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils.Svg;

namespace Asa.Solutions.StarApp.PresentationClasses.ContentEditors
{
	public partial class TilesTabControl : ChildTabBaseControl
	{
		private TilesChildTabInfo CustomTabInfo => (TilesChildTabInfo)TabInfo;

		public TilesTabControl(IChildTabPageContainer slideContainer, StarChildTabInfo tabInfo) : base(slideContainer, tabInfo)
		{
			InitializeComponent();

			CustomTabInfo.LoadTiles();

			InitTiles();
		}

		private void InitTiles()
		{
			metroTilePanelMain.LayoutOrientation = eOrientation.Vertical;

			foreach (var tileGroup in CustomTabInfo.Tiles.Groups)
			{
				var itemContainer = new ItemContainer
				{
					MultiLine = true,
					Orientation = eOrientation.Vertical
				};

				if (!String.IsNullOrWhiteSpace(tileGroup.Title))
				{
					itemContainer.TitleVisible = true;
					itemContainer.Text =
						itemContainer.TitleText = tileGroup.Title;
					if (tileGroup.Font != null)
						itemContainer.TitleStyle.Font = tileGroup.Font;
					if (tileGroup.ForeColor != Color.Empty)
						itemContainer.TitleStyle.TextColor = tileGroup.ForeColor;
				}
				else
					itemContainer.TitleVisible = false;

				foreach (var tileItem in tileGroup.Items)
				{
					var item = new MetroTileItem();
					item.TitleText = tileItem.Title;
					item.Tooltip = tileItem.Tooltip;
					item.CheckBehavior = eMetroTileCheckBehavior.None;

					if (!String.IsNullOrWhiteSpace(tileItem.ImagePath))
					{
						var extension = Path.GetExtension(tileItem.ImagePath);
						if (String.Equals(extension, ".svg", StringComparison.OrdinalIgnoreCase))
						{
							var svgBitmap = SvgBitmap.FromFile(tileItem.ImagePath);
							item.Image = svgBitmap.Render(null, 1.0D);
						}
						else
							item.Image = Image.FromFile(tileItem.ImagePath);
					}

					if (tileItem.TextAlignment.HasValue)
						item.TitleTextAlignment = tileItem.TextAlignment.Value;

					if (!tileItem.Size.IsEmpty)
						item.TileSize = tileItem.Size;
					else if (item.Image != null)
						item.TileSize = item.Image.Size;

					item.TileColor = tileItem.BackColor;

					if (!tileItem.ImageIdent.IsEmpty)
						item.ImageIndent = tileItem.ImageIdent;

					if (!tileItem.ForeColor.IsEmpty)
						item.TitleTextColor = tileItem.ForeColor;

					if (tileItem.Font != null)
						item.TitleTextFont = tileItem.Font;

					item.Click += (o, args) =>
					{
						try
						{
							Process.Start(tileItem.GetExecutablePath());
						}
						catch { }
					};
					itemContainer.SubItems.Add(item);
				}
				metroTilePanelMain.Items.Add(itemContainer);
			}

			metroTilePanelMain.Invalidate();
		}

		public override void ApplyBackground()
		{
			if (TabInfo.BackgroundLogo != null)
			{
				metroTilePanelMain.BackgroundImage = TabInfo.BackgroundLogo;
				metroTilePanelMain.BackgroundImageLayout = ImageLayout.Stretch;
			}
		}

		public override void LoadData() { }

		public override void ApplyChanges() { }

		public override bool GetOutputEnableState()
		{
			return false;
		}

		public override void ApplyOutputEnableState(bool outputEnabled) { }

		#region Output
		public override bool MultipleSlidesAllowed => false;
		public override bool ReadyForOutput => false;

		public override OutputItem GetOutputItem()
		{
			return null;
		}
		#endregion
	}
}
