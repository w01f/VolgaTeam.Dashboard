using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Paint;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public sealed partial class ProgramStrategyControl : UserControl
	{
		private bool _allowToSave;
		private Schedule _localSchedule;
		private GridDragDropHelper _dragDropHelper;
		public bool SettingsNotSaved { get; set; }

		public ProgramStrategyControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Load += (o, e) =>
			{
				_dragDropHelper = new GridDragDropHelper(advBandedGridViewItems, true);
				_dragDropHelper.AfterDrop += gridControlItems_DragDrop;
				favoriteImagesControl.Init();
				new ProgramStrategyItemNameFormatHelper(advBandedGridViewItems);
			};
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			Controller.Instance.StrategyShowStationToggle.CheckStateChanged += UpdateRows;
			Controller.Instance.StrategyShowDescriptionToggle.CheckStateChanged += UpdateRows;
		}

		#region Methods
		public bool AllowToLeaveControl
		{
			get
			{
				if (SettingsNotSaved)
					SaveSchedule();
				return true;
			}
		}

		public void LoadSchedule(bool quickLoad)
		{
			_allowToSave = false;

			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			FormThemeSelector.Link(Controller.Instance.StrategyTheme, BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.Strategy), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Strategy), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType.Strategy, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				SettingsNotSaved = true;
			}));
			SetDataSource();
			if (!quickLoad)
			{
				Controller.Instance.StrategyFavorites.Checked = _localSchedule.ProgramStrategy.ShowFavorites;
				Controller.Instance.StrategyShowStationToggle.Checked = _localSchedule.ProgramStrategy.ShowStation;
				Controller.Instance.StrategyShowDescriptionToggle.Checked = _localSchedule.ProgramStrategy.ShowDescription;
			}
			_allowToSave = true;
			SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				_localSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, true, false, this);
			SettingsNotSaved = false;
			return true;
		}

		private void SetDataSource()
		{
			gridControlItems.DataSource = _localSchedule.ProgramStrategy.Items;
			gridControlItems.RefreshDataSource();
		}

		private void AddLogoToFavorites(Image logo)
		{
			favoriteImagesControl.AddToFavorites(logo);
		}

		private void UpdateRows(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				_localSchedule.ProgramStrategy.ShowStation = Controller.Instance.StrategyShowStationToggle.Checked;
				_localSchedule.ProgramStrategy.ShowDescription = Controller.Instance.StrategyShowDescriptionToggle.Checked;
				SettingsNotSaved = true;
			}
			if (_localSchedule.ProgramStrategy.ShowDescription)
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
		#endregion

		#region Ribbon Button Clicks
		public void Save_Click(object sender, EventArgs e)
		{
			SaveSchedule();
			Utilities.Instance.ShowInformation("Schedule Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule())
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new name for your Schedule:";
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					if (SaveSchedule(form.ScheduleName))
						Utilities.Instance.ShowInformation("Schedule was saved");
				}
				else
				{
					Utilities.Instance.ShowWarning("Schedule Name can't be empty");
				}
			}
		}

		public void Favorites_ChackedChanged(object sender, EventArgs e)
		{
			splitContainerControl.PanelVisibility = Controller.Instance.StrategyFavorites.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel1;
			if (!_allowToSave) return;
			_localSchedule.ProgramStrategy.ShowFavorites = Controller.Instance.StrategyFavorites.Checked;
			SettingsNotSaved = true;
		}

		public void Help_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("strategy");
		}

		public void Preview_Click(object sender, EventArgs e)
		{
			Preview();
		}

		public void PowerPoint_Click(object sender, EventArgs e)
		{
			Output();
		}

		public void Email_Click(object sender, EventArgs e)
		{
			Email();
		}
		#endregion

		#region Items Grid Events
		private void advBandedGridViewItems_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (!_allowToSave) return;
			SettingsNotSaved = true;
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
			var cellInfo = viewInfo.GetGridCellInfo(view.FocusedRowHandle, view.FocusedColumn.AbsoluteIndex);
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
				_localSchedule.ProgramStrategy.ChangeItemsOrder(sourceRow, targetRow);
				SetDataSource();
				view.RefreshData();
				view.FocusedRowHandle = targetRow > 0 ? targetRow - 1 : 0;
				SettingsNotSaved = true;
			}

			var imageSource = e.Data.GetData(typeof(ImageSource)) as ImageSource;
			if (imageSource != null)
			{
				var targetItem = view.GetRow(hitInfo.RowHandle) as ProgramStrategyItem;
				if (targetItem != null)
				{
					targetItem.Logo = imageSource.BigImage;
					advBandedGridViewItems.RefreshRow(hitInfo.RowHandle);
					SettingsNotSaved = true;
				}
			}
		}

		private void advBandedGridViewItems_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (!e.HitInfo.InRowCell) return;
			if (e.HitInfo.Column != bandedGridColumnItemsLogo) return;
			var sourceItem = advBandedGridViewItems.GetRow(e.HitInfo.RowHandle) as ProgramStrategyItem;
			if (sourceItem == null || !sourceItem.Enabled) return;
			var clipboardImage = Clipboard.GetImage();

			e.Menu.Items.Add(new DXMenuItem("Paste Image", (o, ea) =>
			{
				sourceItem.Logo = clipboardImage;
				advBandedGridViewItems.UpdateCurrentRow();
				SettingsNotSaved = true;
			})
			{
				Enabled = clipboardImage != null
			});

			e.Menu.Items.Add(new DXMenuItem("Add Image to Favorites...", (o, args) => AddLogoToFavorites(sourceItem.Logo.Clone() as Image))
			{
				Enabled = !sourceItem.IsDefaultLogo
			});

			e.Menu.Items.Add(new DXMenuItem("Reset Image", (o, ea) =>
			{
				sourceItem.Logo = null;
				advBandedGridViewItems.UpdateCurrentRow();
				SettingsNotSaved = true;
			})
			{
				BeginGroup = true,
				Enabled = !sourceItem.IsDefaultLogo
			});
		}

		private void repositoryItemCheckEdit_CheckedChanged(object sender, EventArgs e)
		{
			advBandedGridViewItems.CloseEditor();
		}

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
		#endregion

		#region Output Stuff
		public List<Dictionary<string, string>> OutputReplacementsLists { get; private set; }
		public List<string> ItemLogos { get; private set; }

		public Theme SelectedTheme
		{
			get { return BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.Strategy).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Strategy)) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Strategy))); }
		}

		public int ItemsCount
		{
			get { return _localSchedule.ProgramStrategy.EnabledItems.Count(); }
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
			var items = _localSchedule.ProgramStrategy.EnabledItems.ToList();
			for (var i = 0; i < recordsCount; i += ItemsPerSlide)
			{
				var slideRows = new Dictionary<string, string>();
				for (var j = 0; j < ItemsPerSlide; j++)
				{
					if ((i + j) < recordsCount)
					{
						slideRows.Add(String.Format("Product{0}", j + 1), String.Format("{0}{1}", items[i + j].Name, _localSchedule.ProgramStrategy.ShowStation ? String.Format("   ({0})", items[i + j].Station) : String.Empty));
						slideRows.Add(String.Format("Details{0}", j + 1), _localSchedule.ProgramStrategy.ShowDescription ? items[i + j].Description : String.Empty);
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
				ItemLogos = new List<string>();
			ItemLogos.Clear();
			foreach (var strategyItem in items)
			{
				var tempFileName = Path.GetTempFileName();
				strategyItem.Logo.Save(tempFileName);
				ItemLogos.Add(tempFileName);
			}
		}

		private void TrackOutput()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabStrategy.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.WeeklySchedule.Programs.Any())
			{
				options.Add("WeeklyTotalSpots", _localSchedule.WeeklySchedule.TotalSpots);
				options.Add("WeeklyAverageRate", _localSchedule.WeeklySchedule.AvgRate);
				options.Add("WeeklyGrossInvestment", _localSchedule.WeeklySchedule.TotalCost);
			}
			if (_localSchedule.MonthlySchedule.Programs.Any())
			{
				options.Add("MonthlyTotalSpots", _localSchedule.MonthlySchedule.TotalSpots);
				options.Add("MonthlyAverageRate", _localSchedule.MonthlySchedule.AvgRate);
				options.Add("MonthlyGrossInvestment", _localSchedule.MonthlySchedule.TotalCost);
			}
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Output", options));
		}

		private void TrackPreview()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabStrategy.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.WeeklySchedule.Programs.Any())
			{
				options.Add("WeeklyTotalSpots", _localSchedule.WeeklySchedule.TotalSpots);
				options.Add("WeeklyAverageRate", _localSchedule.WeeklySchedule.AvgRate);
				options.Add("WeeklyGrossInvestment", _localSchedule.WeeklySchedule.TotalCost);
			}
			if (_localSchedule.MonthlySchedule.Programs.Any())
			{
				options.Add("MonthlyTotalSpots", _localSchedule.MonthlySchedule.TotalSpots);
				options.Add("MonthlyAverageRate", _localSchedule.MonthlySchedule.AvgRate);
				options.Add("MonthlyGrossInvestment", _localSchedule.MonthlySchedule.TotalCost);
			}
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Preview", options));
		}

		private void PreparePreview(string tempFileName)
		{
			MediaSchedulePowerPointHelper.Instance.PrepareStrategyEmail(tempFileName, this);
		}

		private void Output()
		{
			SaveSchedule();
			PrepareOutput();
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
				formProgress.TopMost = true;
				Controller.Instance.ShowFloater(() =>
				{
					formProgress.Show();
					MediaSchedulePowerPointHelper.Instance.AppendStrategy(this);
					formProgress.Close();
				});
			}
		}

		private void Email()
		{
			SaveSchedule();
			PrepareOutput();
			var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				PreparePreview(tempFileName);
				formProgress.Close();
			}
			if (!File.Exists(tempFileName)) return;
			using (var formEmail = new FormEmail(MediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
			{
				formEmail.Text = "Email this Strategy";
				formEmail.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = true;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}

		private void Preview()
		{
			SaveSchedule();
			PrepareOutput();
			var tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
			using (var formProgress = new FormProgress())
			{
				formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
				formProgress.TopMost = true;
				formProgress.Show();
				PreparePreview(tempFileName);
				formProgress.Close();
			}
			if (!File.Exists(tempFileName)) return;
			Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, MediaSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackPreview))
			{
				formPreview.Text = "Preview Strategy";
				formPreview.LoadGroups(new[] { new PreviewGroup { Name = "Preview", PresentationSourcePath = tempFileName } });
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			}
		}
		#endregion
	}
}
