using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using CommandCentral.BusinessClasses.Common;
using CommandCentral.BusinessClasses.DataConvertors;
using CommandCentral.BusinessClasses.DataConvertors.MainData;
using CommandCentral.BusinessClasses.DataConvertors.StarAppData;
using CommandCentral.BusinessClasses.DataConvertors.StarAppData.Audience;
using CommandCentral.BusinessClasses.DataConvertors.StarAppData.CNA;
using CommandCentral.BusinessClasses.DataConvertors.StarAppData.Customer;
using CommandCentral.BusinessClasses.DataConvertors.StarAppData.Fishing;
using CommandCentral.BusinessClasses.DataConvertors.StarAppData.Market;
using CommandCentral.BusinessClasses.DataConvertors.StarAppData.Solution;
using CommandCentral.BusinessClasses.DataConvertors.StarAppData.Video;
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

		#region Convertors functionality
		private readonly List<IExcel2XmlConvertor> _convertors = new List<IExcel2XmlConvertor>();

		private void ConfigureConvertors()
		{
			#region Main Data
			_convertors.Add(new UsersConvertor(AppManager.Instance.AppResources.MainDataSourceFilePath));
			_convertors.Add(new DashboardCoverConvertor(AppManager.Instance.AppResources.MainDataSourceFilePath));
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
			#endregion

			#region Star App Data
			_convertors.Add(new StarAppCoverConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));

			_convertors.Add(new AudiencePartAConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new AudiencePartBConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new AudiencePartCConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));

			_convertors.Add(new CNAPartAConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new CNAPartBConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));

			_convertors.Add(new CustomerPartAConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new CustomerPartBConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new CustomerPartCConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));

			_convertors.Add(new FishingPartAConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new FishingPartBConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new FishingPartCConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));

			_convertors.Add(new MarketPartAConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new MarketPartBConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new MarketPartCConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));

			_convertors.Add(new SolutionPartAConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new SolutionPartBConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new SolutionPartCConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new SolutionPartDConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));

			_convertors.Add(new VideoPartAConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new VideoPartBConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new VideoPartCConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			_convertors.Add(new VideoPartDConvertor(AppManager.Instance.AppResources.StarAppDataSourceFilePath));
			#endregion
		}

		private async Task RunConvertors(IEnumerable<IExcel2XmlConvertor> convertors)
		{
			Enabled = false;
			splashScreenManager.ShowWaitForm();
			try
			{
				await Task.Run(() =>
					{
						foreach (var convertor in convertors)
							convertor.Convert(AppManager.Instance.AppSettings.OutputFolders.Select(folder => folder.Path).ToList());
					}
				);
			}
			catch (ConversionException ex)
			{
				splashScreenManager.CloseWaitForm();
				PopupMessageHelper.Instance.ShowWarning(String.Format("Excel file reading error: {0}",
					Path.GetFileName(ex.SourceFilePath)));
				return;
			}
			catch (Exception)
			{
				splashScreenManager.CloseWaitForm();
				PopupMessageHelper.Instance.ShowWarning("Excel file reading error");
				return;
			}
			splashScreenManager.CloseWaitForm();
			splashScreenManager.WaitForSplashFormClose();
			PopupMessageHelper.Instance.ShowInformation("Data was updated");
			Enabled = true;
		}

		private async void OnUpdateAllClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors);
		}
		#endregion

		#region Main Data Processing
		private async void OnUsersButtonClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<UsersConvertor>());
		}

		private async void OnCoverButtonClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<DashboardCoverConvertor>());
		}

		private async void OnLeadoffStatementsButtonClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<LeadoffStatementsConvertor>());
		}

		private async void OnClientGoalsButtonClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<ClientGoalsConvertor>());
		}

		private async void OnTargetCustomersButtonClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<TargetCustomersConvertor>());
		}

		private async void OnClosingSummaryButtonClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<ClosingSummaryConvertor>());
		}

		private async void OnTVDataButtonClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<TVDataConvertor>());
		}

		private async void OnRadioDataButtonClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<RadioDataConvertor>());
		}

		private async void OnOnlineDataButtonClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<OnlineDataConvertor>());
		}

		private async void OnSalesLibrariesDataButtonClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<SalesLibrariesDataConvertor>());
		}
		#endregion

		#region Star App Data Processing
		private async void OnStarAppDataCoverClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<StarAppCoverConvertor>());
		}

		private async void OnStarAppDataCNAClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<ICNAConvertor>());
		}

		private async void OnStarAppDataFishingClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<IFishingConvertor>());
		}

		private async void OnStarAppDataCustomerClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<ICustomerConvertor>());
		}

		private void OnStarAppDataShareClick(object sender, EventArgs e)
		{
		}

		private void OnStarAppDataROIClick(object sender, EventArgs e)
		{
		}

		private async void OnStarAppDataMarketClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<IMarketConvertor>());
		}

		private async void OnStarAppDataVideoClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<IVideoConvertor>());
		}

		private async void OnStarAppDataAudienceClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<IAudienceConvertor>());
		}

		private async void OnStarAppDataSolutionClick(object sender, EventArgs e)
		{
			await RunConvertors(_convertors.OfType<ISolutionConvertor>());
		}

		private void OnStarAppDataClosersClick(object sender, EventArgs e)
		{
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
