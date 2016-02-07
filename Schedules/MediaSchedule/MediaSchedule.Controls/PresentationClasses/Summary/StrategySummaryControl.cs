using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ImageGallery;
using Asa.Common.GUI.Preview;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.InteropClasses;
using Asa.Media.Controls.Properties;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Paint;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Asa.Media.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	public sealed partial class StrategySummaryControl : UserControl, ISectionSummaryControl
	{
		private bool _allowToSave;

		private GridDragDropHelper _dragDropHelper;
		private StrategyInfoControl _infoControl;
		private StrategyImageControl _favoriets;

		public SectionSummary SectionData { get; private set; }
		public List<ISummaryInfoControl> SettingsPages { get; private set; }
		public List<ButtonInfo> BarButtons { get; private set; }
		public event EventHandler<EventArgs> DataChanged;

		#region Calculated properties
		public bool ReadyForOutput { get { return true; } }

		private StrategySummaryContent SummarySettings
		{
			get { return (StrategySummaryContent)SectionData.Content; }
		}
		#endregion

		public StrategySummaryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			SettingsPages = new List<ISummaryInfoControl>();
			BarButtons = new List<ButtonInfo>();

			InitSettingsControls();

			Load += (o, e) =>
			{
				_dragDropHelper = new GridDragDropHelper(advBandedGridViewItems, true);
				_dragDropHelper.AfterDrop += gridControlItems_DragDrop;
				new ProgramStrategyItemNameFormatHelper(advBandedGridViewItems);
			};
		}

		#region Methods
		public void LoadData(SectionSummary sectionData, bool quickLoad)
		{
			SectionData = sectionData;
			Text = SectionData.Parent.Name;
			SetDataSource();
			LoadSettingsData();
		}

		public void SaveData() { }
		public void Release()
		{
			gridControlItems.DataSource = null;
			SectionData = null;

			if (_dragDropHelper != null)
				_dragDropHelper.AfterDrop -= gridControlItems_DragDrop;

			_infoControl = null;
			_favoriets = null;

			SettingsPages.ForEach(sp => sp.Release());
			SettingsPages.Clear();

			BarButtons.Clear();
			DataChanged = null;
		}

		private void SetDataSource()
		{
			gridControlItems.DataSource = SummarySettings.Items;
			gridControlItems.RefreshDataSource();
		}

		private void AddLogoToFavorites(Image logo, string defaultName)
		{
			_favoriets.AddLogoToFavorites(logo, defaultName);
		}

		private void UpdateRows()
		{
			if (SummarySettings.ShowDescription)
			{
				bandedGridColumnItemsName.RowCount = 2;
				bandedGridColumnItemsDescription.Visible = true;
			}
			else
			{
				bandedGridColumnItemsName.RowCount = 4;
				bandedGridColumnItemsDescription.Visible = false;
			}
			advBandedGridViewItems.RefreshData();
		}

		private void RaiseDataChanged()
		{
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}
		#endregion

		#region Settings Management
		private void InitSettingsControls()
		{
			SettingsPages.Clear();
			BarButtons.Clear();

			_infoControl = new StrategyInfoControl();
			_infoControl.DataChanged += OnSettingsChanged;
			SettingsPages.Add(_infoControl);
			var infoSettingsButton = new ButtonInfo
			{
				Tooltip = "Edit Summary Settings",
				Logo = Resources.SummaryOptionsInfo,
				Action = () => { _infoControl.TabControl.SelectedTabPage = _infoControl; }
			};
			BarButtons.Add(infoSettingsButton);


			_favoriets = new StrategyImageControl();
			SettingsPages.Add(_favoriets);
			var outputSettingsButton = new ButtonInfo
			{
				Tooltip = "Open Favorites",
				Logo = Resources.SummaryOptionsFavorites,
				Action = () => { _favoriets.TabControl.SelectedTabPage = _favoriets; }
			};
			BarButtons.Add(outputSettingsButton);
		}

		private void LoadSettingsData()
		{
			_infoControl.LoadData(SummarySettings);
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			UpdateRows();
			RaiseDataChanged();
		}
		#endregion

		#region Items Grid Events
		private void advBandedGridViewItems_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (!_allowToSave) return;
			RaiseDataChanged();
		}

		private void advBandedGridViewItems_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var sourceItem = advBandedGridViewItems.GetRow(e.RowHandle) as ProgramStrategyItem;
			if (sourceItem == null || sourceItem.Enabled) return;
			e.Appearance.ForeColor = Color.LightGray;
		}

		private void advBandedGridViewItems_ShownEditor(object sender, EventArgs e)
		{
			var view = sender as GridView;
			if (view == null || view.ActiveEditor == null) return;
			var viewInfo = view.GetViewInfo() as GridViewInfo;
			if (viewInfo == null) return;
			var cellInfo = viewInfo.GetGridCellInfo(view.FocusedRowHandle, view.FocusedColumn);
			if (cellInfo == null) return;
			view.ActiveEditor.Properties.Appearance.Assign(cellInfo.Appearance);
		}

		private void gridControlItems_DragDrop(object sender, DragEventArgs e)
		{
			var grid = sender as GridControl;
			var view = grid.MainView as GridView;
			var hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));

			var downHitInfo = e.Data.GetData(typeof(BandedGridHitInfo)) as BandedGridHitInfo;
			if (downHitInfo != null)
			{
				var sourceRow = downHitInfo.RowHandle;
				var targetRow = hitInfo.HitTest == GridHitTest.EmptyRow ? view.DataRowCount : hitInfo.RowHandle;
				SummarySettings.ChangeItemsOrder(sourceRow, targetRow);
				SetDataSource();
				view.RefreshData();
				view.FocusedRowHandle = targetRow > 0 ? targetRow - 1 : 0;
				RaiseDataChanged();
			}

			var imageSource = e.Data.GetData(typeof(ImageSource)) as ImageSource;
			if (imageSource != null)
			{
				var targetItem = view.GetRow(hitInfo.RowHandle) as ProgramStrategyItem;
				if (targetItem != null)
				{
					targetItem.Logo = imageSource.Clone<ImageSource, ImageSource>();
					advBandedGridViewItems.RefreshRow(hitInfo.RowHandle);
					RaiseDataChanged();
				}
			}
		}

		private void advBandedGridViewItems_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (!e.HitInfo.InRowCell) return;
			if (e.HitInfo.Column != bandedGridColumnItemsLogo) return;
			var sourceItem = advBandedGridViewItems.GetRow(e.HitInfo.RowHandle) as ProgramStrategyItem;
			if (sourceItem == null || !sourceItem.Enabled) return;
			ImageSource imageSource = null;
			var clipboardImage = ClipboardHelper.GetImageFormClipboard();
			if (clipboardImage != null)
				imageSource = ImageSource.FromImage(clipboardImage);
			else if (Clipboard.ContainsText(TextDataFormat.Html))
			{
				var textContent = Clipboard.GetText(TextDataFormat.Html);
				try
				{
					imageSource = ImageSource.FromString(textContent);
				}
				catch { }
			}
			e.Menu.Items.Add(new DXMenuItem("Paste Image", (o, ea) =>
			{
				sourceItem.Logo = imageSource.Clone<ImageSource, ImageSource>();
				advBandedGridViewItems.UpdateCurrentRow();
				RaiseDataChanged();
			})
			{
				Enabled = imageSource != null
			});

			e.Menu.Items.Add(new DXMenuItem("Add Image to Favorites...", (o, args) => AddLogoToFavorites(sourceItem.Logo.BigImage.Clone() as Image, sourceItem.Name))
			{
				Enabled = !sourceItem.IsDefaultLogo
			});

			e.Menu.Items.Add(new DXMenuItem("Reset Image", (o, ea) =>
			{
				sourceItem.Logo = new ImageSource();
				advBandedGridViewItems.UpdateCurrentRow();
				RaiseDataChanged();
			})
			{
				BeginGroup = true,
				Enabled = !sourceItem.IsDefaultLogo
			});
		}

		private void advBandedGridViewItems_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != bandedGridColumnItemsLogo) return;
			var selectedProgram = advBandedGridViewItems.GetFocusedRow() as ProgramStrategyItem;
			if (selectedProgram == null) return;
			using (var form = new FormImageGallery(MediaMetaData.Instance.ListManager.Images))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				if (form.SelectedImageSource == null) return;
				selectedProgram.Logo = form.SelectedImageSource;
				advBandedGridViewItems.UpdateCurrentRow();
				RaiseDataChanged();
			}
		}

		private void repositoryItemCheckEdit_CheckedChanged(object sender, EventArgs e)
		{
			advBandedGridViewItems.PostEditor();
			RaiseDataChanged();
		}
		#endregion

		#region Output Stuff
		public List<Dictionary<string, string>> OutputReplacementsLists { get; private set; }
		public List<ImageSource> ItemLogos { get; private set; }

		public ContractSettings ContractSettings
		{
			get { return SummarySettings.ContractSettings; }
		}

		public Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Summaries).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Summaries))); }
		}

		public int ItemsCount
		{
			get { return SummarySettings.EnabledItems.Count(); }
		}

		public int ItemsPerSlide
		{
			get { return ItemsCount > 12 ? 12 : ItemsCount; }
		}

		private void PrepareOutput()
		{
			if (OutputReplacementsLists == null)
				OutputReplacementsLists = new List<Dictionary<string, string>>();
			OutputReplacementsLists.Clear();
			var recordsCount = ItemsCount;
			var items = SummarySettings.EnabledItems.ToList();
			for (var i = 0; i < recordsCount; i += ItemsPerSlide)
			{
				var slideRows = new Dictionary<string, string>();
				for (var j = 0; j < ItemsPerSlide; j++)
				{
					if ((i + j) < recordsCount)
					{
						slideRows.Add(String.Format("Product{0}", j + 1), String.Format("{0}{1}", items[i + j].Name, SummarySettings.ShowStation ? String.Format("   ({0})", items[i + j].Station) : String.Empty));
						slideRows.Add(String.Format("Details{0}", j + 1), SummarySettings.ShowDescription ? items[i + j].Description : String.Empty);
					}
					else
					{
						slideRows.Add(String.Format("Product{0}", j + 1), "DeleteRow");
						slideRows.Add(String.Format("Details{0}", j + 1), "DeleteRow");
					}
				}
				OutputReplacementsLists.Add(slideRows);
			}

			if (ItemLogos == null)
				ItemLogos = new List<ImageSource>();
			ItemLogos.Clear();
			foreach (var strategyItem in items)
			{
				strategyItem.Logo.PrepareOutputFile();
				ItemLogos.Add(strategyItem.Logo);
			}
		}

		public void GeneratePowerPointOutput()
		{
			PrepareOutput();
			RegularMediaSchedulePowerPointHelper.Instance.AppendStrategy(this);
		}

		public PreviewGroup GeneratePreview()
		{
			PrepareOutput();
			var previewGroup = new PreviewGroup
			{
				Name = SectionData.Parent.Name.Replace("&", "&&"),
				PresentationSourcePath = Path.Combine(Common.Core.Configuration.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()))
			};
			RegularMediaSchedulePowerPointHelper.Instance.PrepareStrategyEmail(previewGroup.PresentationSourcePath, this);
			return previewGroup;
		}
		#endregion

		internal class ProgramStrategyItemNameFormatHelper
		{
			private readonly GridView _view;
			private readonly XPaint _paint = new XPaint();

			public ProgramStrategyItemNameFormatHelper(GridView view)
			{
				_view = view;
				_view.CustomDrawCell += View_CustomDrawCell;
			}

			private void View_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
			{
				if (e.Column.FieldName != "CompiledName") return;
				var format = e.Appearance.Clone() as AppearanceObject;
				if (format == null) return;
				e.Handled = true;

				e.Appearance.FillRectangle(e.Cache, e.Bounds);
				var rows = e.DisplayText.Split(new[] { '\n' });

				var totalHeight = rows.Sum(row => e.Graphics.MeasureString(row, format.Font).Height);
				var startHeight = 0;
				if (e.Bounds.Height > totalHeight)
					startHeight = (e.Bounds.Height - (int)totalHeight) / 2;
				var stationFont = new Font(format.Font.Name, 10, format.Font.Style);
				var rowCount = rows.Length;
				for (var i = 0; i < rowCount; i++)
				{
					var lst = new List<CharacterRangeWithFormat>();

					switch (i)
					{
						case 0:
							lst.Add(new CharacterRangeWithFormat(0, rows[i].Length, format.ForeColor, format.BackColor));
							break;
						case 1:
							lst.Add(new CharacterRangeWithFormat(0, rows[i].Length, format.ForeColor == Color.Black ? Color.Gray : format.ForeColor, format.BackColor));
							format.Font = stationFont;
							break;
					}
					var args = new MultiColorDrawStringParams(format);
					var rowRect = e.Bounds;
					rowRect.Y += startHeight;
					rowRect.Height = (int)e.Graphics.MeasureString(rows[i], args.Appearance.Font).Height;
					startHeight += rowRect.Height;
					args.Ranges = lst.ToArray();
					args.Bounds = rowRect;
					args.Text = rows[i];
					_paint.MultiColorDrawString(e.Cache, args);
				}
			}
		}
	}
}
