using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Paint;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.ImageGallery;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.RetractableBar;
using NewBizWiz.CommonGUI.Themes;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.MediaSchedule.Controls.Properties;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.Strategy
{
	[ToolboxItem(false)]
	public sealed partial class ProgramStrategyControl : UserControl
	{
		private bool _allowToSave;
		private RegularSchedule _localSchedule;
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
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadSchedule(e.QuickSave);
			});
			Controller.Instance.StrategyShowStationToggle.CheckStateChanged += UpdateRows;
			Controller.Instance.StrategyShowDescriptionToggle.CheckStateChanged += UpdateRows;
			retractableBarRight.StateChanged += Favorites_StateChanged;
			retractableBarRight.AddButtons(new[] { new ButtonInfo { Logo = Resources.FavoritesLogo, Tooltip = "Expand bar" } });
			favoriteImagesControl.ImageTooltip = "Drag and Drop this image to a program on the left";
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) =>
			{
				InitThemeSelector();
				Controller.Instance.StrategyThemeBar.RecalcLayout();
				Controller.Instance.StrategyPanel.PerformLayout();
			};
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

			_localSchedule = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();
			InitThemeSelector();
			SetDataSource();
			if (!quickLoad)
			{
				if (_localSchedule.ProgramStrategy.ShowFavorites)
					retractableBarRight.Expand(true);
				else
					retractableBarRight.Collapse(true);
				Controller.Instance.StrategyShowStationToggle.Checked = _localSchedule.ProgramStrategy.ShowStation;
				Controller.Instance.StrategyShowDescriptionToggle.Checked = _localSchedule.ProgramStrategy.ShowDescription;
			}

			hyperLinkEditInfoContract.Enabled = BusinessObjects.Instance.OutputManager.ContractTemplateFolder.ExistsLocal();

			_allowToSave = true;
			SettingsNotSaved = false;
		}

		private bool SaveSchedule(string scheduleName = "")
		{
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				_localSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(_localSchedule, nameChanged, true, false, false, this);
			SettingsNotSaved = false;
			return true;
		}

		private void InitThemeSelector()
		{
			FormThemeSelector.Link(Controller.Instance.StrategyTheme, BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Strategy), MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Strategy), (t =>
			{
				MediaMetaData.Instance.SettingsManager.SetSelectedTheme(SlideType.Strategy, t.Name);
				MediaMetaData.Instance.SettingsManager.SaveSettings();
				SettingsNotSaved = true;
			}));
		}

		private void SetDataSource()
		{
			gridControlItems.DataSource = _localSchedule.ProgramStrategy.Items;
			gridControlItems.RefreshDataSource();
		}

		private void AddLogoToFavorites(Image logo, string defaultName)
		{
			favoriteImagesControl.AddToFavorites(logo, defaultName);
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
			using (var form = new FormNewSchedule(ScheduleManager.GetShortScheduleList().Select(s => s.ShortFileName)))
			{
				form.Text = "Save Schedule";
				form.laLogo.Text = "Please set a new defaultName for your Schedule:";
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

		private void Favorites_StateChanged(object sender, StateChangedEventArgs e)
		{
			if (!_allowToSave) return;
			_localSchedule.ProgramStrategy.ShowFavorites = e.Expaned;
			SettingsNotSaved = true;
		}

		public void Help_Click(object sender, EventArgs e)
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("strategy");
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

		public void Pdf_Click(object sender, EventArgs e)
		{
			OutputPdf();
		}

		private void hyperLinkEditInfoContract_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormContractSettings())
			{
				form.checkEditShowSignatureLine.Checked = _localSchedule.ProgramStrategy.ContractSettings.ShowSignatureLine;
				form.checkEditShowRatesExpiration.Checked = _localSchedule.ProgramStrategy.ContractSettings.RateExpirationDate.HasValue;
				form.checkEditShowDisclaimer.Checked = _localSchedule.ProgramStrategy.ContractSettings.ShowDisclaimer;
				form.dateEditRatesExpirationDate.EditValue = _localSchedule.ProgramStrategy.ContractSettings.RateExpirationDate;
				if (form.ShowDialog() != DialogResult.OK) return;
				_localSchedule.ProgramStrategy.ContractSettings.ShowSignatureLine = form.checkEditShowSignatureLine.Checked;
				_localSchedule.ProgramStrategy.ContractSettings.ShowDisclaimer = form.checkEditShowDisclaimer.Checked;
				_localSchedule.ProgramStrategy.ContractSettings.RateExpirationDate = (DateTime?)form.dateEditRatesExpirationDate.EditValue;

				SettingsNotSaved = true;
			}
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
					targetItem.Logo = imageSource.Clone();
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
			ImageSource imageSource = null;
			var clipboardImage = Utilities.Instance.GetImageFormClipboard();
			if (clipboardImage != null)
				imageSource = ImageSource.FromImage(clipboardImage);
			else if (Clipboard.ContainsText(TextDataFormat.Html))
			{
				var textContent = Clipboard.GetText(TextDataFormat.Html);
				try
				{
					var doc = new XmlDocument();
					doc.LoadXml(textContent);
					imageSource = new ImageSource();
					imageSource.Deserialize(doc.FirstChild);
				}
				catch { }
			}
			e.Menu.Items.Add(new DXMenuItem("Paste Image", (o, ea) =>
			{
				sourceItem.Logo = imageSource.Clone();
				advBandedGridViewItems.UpdateCurrentRow();
				SettingsNotSaved = true;
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
				SettingsNotSaved = true;
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
				SettingsNotSaved = true;
			}
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
		public List<ImageSource> ItemLogos { get; private set; }

		public ContractSettings ContractSettings
		{
			get { return _localSchedule.ProgramStrategy.ContractSettings; }
		}

		public Theme SelectedTheme
		{
			get { return BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.Strategy).FirstOrDefault(t => t.Name.Equals(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Strategy)) || String.IsNullOrEmpty(MediaMetaData.Instance.SettingsManager.GetSelectedTheme(SlideType.Strategy))); }
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
				ItemLogos = new List<ImageSource>();
			ItemLogos.Clear();
			foreach (var strategyItem in items)
			{
				strategyItem.Logo.PrepareOutputFile();
				ItemLogos.Add(strategyItem.Logo);
			}
		}

		private void TrackOutput()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabStrategy.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.Section.Programs.Any())
			{
				options.Add("TotalSpots", _localSchedule.Section.TotalSpots);
				options.Add("AverageRate", _localSchedule.Section.AvgRate);
				options.Add("GrossInvestment", _localSchedule.Section.TotalCost);
			}
			BusinessObjects.Instance.ActivityManager.AddActivity(new UserActivity("Output", options));
		}

		private void TrackPreview()
		{
			var options = new Dictionary<string, object>();
			options.Add("Slide", Controller.Instance.TabStrategy.Text);
			options.Add("Advertiser", _localSchedule.BusinessName);
			if (_localSchedule.Section.Programs.Any())
			{
				options.Add("TotalSpots", _localSchedule.Section.TotalSpots);
				options.Add("AverageRate", _localSchedule.Section.AvgRate);
				options.Add("GrossInvestment", _localSchedule.Section.TotalCost);
			}
			BusinessObjects.Instance.ActivityManager.AddActivity(new UserActivity("Preview", options));
		}

		private void PreparePreview(string tempFileName)
		{
			RegularMediaSchedulePowerPointHelper.Instance.PrepareStrategyEmail(tempFileName, this);
		}

		private void Output()
		{
			SaveSchedule();
			PrepareOutput();
			TrackOutput();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				RegularMediaSchedulePowerPointHelper.Instance.AppendStrategy(this);
				FormProgress.CloseProgress();
			});
		}

		private void Email()
		{
			SaveSchedule();
			PrepareOutput();
			var tempFileName = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Email...");
			FormProgress.ShowProgress();
			PreparePreview(tempFileName);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			using (var formEmail = new FormEmail(RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager))
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
			var tempFileName = Path.Combine(Core.Common.ResourceManager.Instance.TempFolder.LocalPath, Path.GetFileName(Path.GetTempFileName()));
			FormProgress.SetTitle("Chill-Out for a few seconds...\nPreparing Presentation for Email...");
			FormProgress.ShowProgress();
			PreparePreview(tempFileName);
			FormProgress.CloseProgress();
			if (!File.Exists(tempFileName)) return;
			Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, RegularMediaSchedulePowerPointHelper.Instance, BusinessObjects.Instance.HelpManager, Controller.Instance.ShowFloater, TrackPreview))
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

		private void OutputPdf()
		{
			SaveSchedule();
			PrepareOutput();
			TrackOutput();
			FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
			Controller.Instance.ShowFloater(() =>
			{
				FormProgress.ShowProgress();
				var pdfFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), String.Format("{0}-{1}.pdf", _localSchedule.Name, DateTime.Now.ToString("MM-dd-yy-hmmss")));
				RegularMediaSchedulePowerPointHelper.Instance.PrepareStrategyPdf(pdfFileName, this);
				if (File.Exists(pdfFileName))
					try
					{
						Process.Start(pdfFileName);
					}
					catch { }
				FormProgress.CloseProgress();
			});
		}
		#endregion
	}
}
