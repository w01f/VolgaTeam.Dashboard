using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.Themes;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ImageGallery;
using Asa.Common.GUI.Preview;
using Asa.Media.Controls.BusinessClasses;
using Asa.Media.Controls.InteropClasses;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Paint;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	[ToolboxItem(false)]
	//public sealed partial class StrategySummaryControl : UserControl, ISectionEditorControl
	public sealed partial class StrategySummaryControl : XtraTabPage, ISectionEditorControl, ISectionOutputControl
	{
		private bool _allowToSave;

		private SectionContainer _sectionContainer;
		private GridDragDropHelper _dragDropHelper;

		public SectionEditorType EditorType => SectionEditorType.Strategy;

		#region Calculated properties
		public bool ReadyForOutput => true;

		private StrategySummaryContent SummarySettings => _sectionContainer.SectionData.Summary.StrategySummary;

		#endregion

		public StrategySummaryControl(SectionContainer sectionContainer)
		{
			_sectionContainer = sectionContainer;
			InitializeComponent();
			Dock = DockStyle.Fill;
			Text = "Logo Summary";
		}

		#region ISectionEditorControl Memebers
		public void InitControls()
		{
			gridControlItems.DataSource = new Object[] {};
			_dragDropHelper = new GridDragDropHelper(advBandedGridViewItems, true);
			_dragDropHelper.AfterDrop += gridControlItems_DragDrop;
			new ProgramStrategyItemNameFormatHelper(advBandedGridViewItems);
		}

		public void Release()
		{
			gridControlItems.DataSource = null;

			if (_dragDropHelper != null)
				_dragDropHelper.AfterDrop -= gridControlItems_DragDrop;

			_sectionContainer = null;
		}
		#endregion

		#region Methods
		public void LoadData()
		{
			SetDataSource();
		}

		public void SaveData()
		{
			gridViewItems.CloseEditor();
		}

		private void SetDataSource()
		{
			gridControlItems.DataSource = SummarySettings.Items;
			gridControlItems.RefreshDataSource();
		}

		public void UpdateRows()
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
			_sectionContainer.RaiseDataChanged();
		}
		#endregion

		#region Settings Management
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
				selectedProgram.Logo = form.SelectedImageSource.Clone<ImageSource, ImageSource>();
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

		public ContractSettings ContractSettings => SummarySettings.ContractSettings;

		public Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVProgramSchedule : SlideType.RadioProgramSchedule).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? SlideType.TVProgramSchedule : SlideType.RadioProgramSchedule))); }
		}

		public int ItemsCount => SummarySettings.EnabledItems.Count();

		public int ItemsPerSlide => ItemsCount > 12 ? 12 : ItemsCount;

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

		public void GenerateOutput()
		{
			PrepareOutput();
			RegularMediaSchedulePowerPointHelper.Instance.AppendStrategy(this);
		}

		public PreviewGroup GeneratePreview()
		{
			PrepareOutput();
			var previewGroup = new PreviewGroup
			{
				Name = _sectionContainer.SectionData.Name.Replace("&", "&&"),
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
