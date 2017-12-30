using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using CommandCentral.BusinessClasses.Common;
using CommandCentral.BusinessClasses.DataConvertors;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;

namespace CommandCentral
{
	public partial class FormMain : MetroForm
	{
		public FormMain()
		{
			InitializeComponent();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemOutputFoldersAdd.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOutputFoldersAdd.MaxSize, scaleFactor);
			layoutControlItemOutputFoldersAdd.MinSize = RectangleHelper.ScaleSize(layoutControlItemOutputFoldersAdd.MinSize, scaleFactor);
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, scaleFactor);
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			LoadOutputFolders();
			ConfigureConvertors();
		}

		private void OnFormCloseClick(object sender, EventArgs e)
		{
			Close();
		}

		#region Main Data Processing
		private readonly List<IExcel2XmlConvertor> _convertors = new List<IExcel2XmlConvertor>();

		private void ConfigureConvertors()
		{
			_convertors.Add(new UsersConvertor(AppManager.Instance.AppResources.MainDataSourceFilePath));
			_convertors.Add(new CoverConvertor(AppManager.Instance.AppResources.MainDataSourceFilePath));
			_convertors.Add(new LeadoffStatementsConvertor(AppManager.Instance.AppResources.MainDataSourceFilePath));
			_convertors.Add(new ClientGoalsConvertor(AppManager.Instance.AppResources.MainDataSourceFilePath));
			_convertors.Add(new TargetCustomersConvertor(AppManager.Instance.AppResources.MainDataSourceFilePath));
			_convertors.Add(new ClosingSummaryConvertor(AppManager.Instance.AppResources.MainDataSourceFilePath));
			_convertors.Add(new TVDataConvertor(
				AppManager.Instance.AppResources.TVDataSourceFilePath,
				AppManager.Instance.AppResources.CalendarDataSourceFilePath,
				AppManager.Instance.AppResources.TVImagesFolderPath
				));
			_convertors.Add(new RadioDataConvertor(
				AppManager.Instance.AppResources.RadioDataSourceFilePath,
				AppManager.Instance.AppResources.CalendarDataSourceFilePath,
				AppManager.Instance.AppResources.RadioImagesFolderPath
			));
			_convertors.Add(new OnlineDataConvertor(
				AppManager.Instance.AppResources.OnlineDataSourceFilePath,
				AppManager.Instance.AppResources.OnlineImagesFolderPath
			));
			_convertors.Add(new SalesLibrariesDataConvertor(AppManager.Instance.AppResources.SalesLibrariesDataSourceFilePath));
		}

		private void RunConvertors(IEnumerable<IExcel2XmlConvertor> convertors)
		{
			foreach (var convertor in convertors)
			{
				try
				{
					convertor.Convert(AppManager.Instance.AppSettings.OutputFolders.Select(folder => folder.Path).ToList());
				}
				catch (ConversionException ex)
				{
					PopupMessageHelper.Instance.ShowWarning(String.Format("Excel file reading error: {0}", Path.GetFileName(ex.SourceFilePath)));
					return;
				}
				catch (Exception)
				{
					PopupMessageHelper.Instance.ShowWarning("Excel file reading error");
					return;
				}
			}
			PopupMessageHelper.Instance.ShowInformation("Data was updated");
		}

		private void OnUsersButtonClick(object sender, EventArgs e)
		{
			RunConvertors(_convertors.OfType<UsersConvertor>());
		}

		private void OnCoverButtonClick(object sender, EventArgs e)
		{
			RunConvertors(_convertors.OfType<CoverConvertor>());
		}

		private void OnLeadoffStatementsButtonClick(object sender, EventArgs e)
		{
			RunConvertors(_convertors.OfType<LeadoffStatementsConvertor>());
		}

		private void OnClientGoalsButtonClick(object sender, EventArgs e)
		{
			RunConvertors(_convertors.OfType<ClientGoalsConvertor>());
		}

		private void OnTargetCustomersButtonClick(object sender, EventArgs e)
		{
			RunConvertors(_convertors.OfType<TargetCustomersConvertor>());
		}

		private void OnClosingSummaryButtonClick(object sender, EventArgs e)
		{
			RunConvertors(_convertors.OfType<ClosingSummaryConvertor>());
		}

		private void OnTVDataButtonClick(object sender, EventArgs e)
		{
			RunConvertors(_convertors.OfType<TVDataConvertor>());
		}

		private void OnRadioDataButtonClick(object sender, EventArgs e)
		{
			RunConvertors(_convertors.OfType<RadioDataConvertor>());
		}

		private void OnOnlineDataButtonClick(object sender, EventArgs e)
		{
			RunConvertors(_convertors.OfType<OnlineDataConvertor>());
		}

		private void OnSalesLibrariesDataButtonClick(object sender, EventArgs e)
		{
			RunConvertors(_convertors.OfType<SalesLibrariesDataConvertor>());
		}

		private void OnUpdateAllClick(object sender, EventArgs e)
		{
			RunConvertors(_convertors);
		}
		#endregion

		#region Output Folders Processing
		private void LoadOutputFolders()
		{
			gridControlOutputFolders.DataSource = AppManager.Instance.AppSettings.OutputFolders;
			gridViewOutputFolders.RefreshData();
		}

		private void OnOutputFoldersAddClick(object sender, EventArgs e)
		{
			gridViewOutputFolders.CloseEditor();
			AppManager.Instance.AppSettings.OutputFolders.RemoveAll(outputFolder => String.IsNullOrEmpty(outputFolder.Path));
			AppManager.Instance.AppSettings.OutputFolders.Add(new OutputFolder());

			LoadOutputFolders();

			if (gridViewOutputFolders.RowCount > 0)
			{
				gridViewOutputFolders.FocusedRowHandle = gridViewOutputFolders.RowCount - 1;
				gridViewOutputFolders.MakeRowVisible(gridViewOutputFolders.FocusedRowHandle, true);
			}
		}

		private void OnOutputFoldersButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var outputFolder = (OutputFolder)gridViewOutputFolders.GetFocusedRow();
			switch (e.Button.Index)
			{
				case 0:
					using (var dialog = new FolderBrowserDialogEx())
					{
						dialog.ShowEditBox = true;
						dialog.ShowFullPathInEditBox = true;
						dialog.SelectedPath = outputFolder.Path;
						if (dialog.ShowDialog(this) != DialogResult.OK) return;
						outputFolder.Path = dialog.SelectedPath;
						gridViewOutputFolders.CloseEditor();
						LoadOutputFolders();
					}
					break;
				case 1:
					if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure want to delete path?") == DialogResult.Yes)
					{
						AppManager.Instance.AppSettings.OutputFolders.Remove(outputFolder);
						LoadOutputFolders();
					}
					break;
			}
		}
		#endregion
	}
}
