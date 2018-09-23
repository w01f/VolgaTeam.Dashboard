using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Objects.Output
{
	public class MasterWizard
	{
		private readonly StorageDirectory _sourceFolder;

		public string Name { get; private set; }
		public bool Hide { get; private set; }

		public MasterWizard(StorageDirectory sourceDirectory)
		{
			_sourceFolder = sourceDirectory;
			Name = _sourceFolder.Name;
		}

		public override string ToString()
		{
			return Name;
		}

		public void Init()
		{
			LoadSettings();
		}

		public bool HasSlideConfiguration(SlideSettings slideSettings)
		{
			return new StorageDirectory(_sourceFolder.RelativePathParts.Merge(slideSettings.SlideFolder)).ExistsLocal();
		}

		private void LoadSettings()
		{
			Hide = false;
			var settingsFile = new StorageFile(_sourceFolder.RelativePathParts.Merge("Settings.xml"));
			if (!settingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			try
			{
				document.Load(settingsFile.LocalPath);
				var node = document.SelectSingleNode(@"/settings/hide");
				if (node != null)
				{
					if (bool.TryParse(node.InnerText, out var tempBool))
						Hide = tempBool;
				}
			}
			catch { }
		}

		#region Slide Template Getters

		#region Dashboard Slides
		private string GetBasicTemplateFile(IEnumerable<string> fileNameParts)
		{
			var file = new StorageFile(_sourceFolder.RelativePathParts.Merge(new[] { SlideSettingsManager.Instance.SlideSettings.SlideFolder, "Basic Slides" }).Merge(fileNameParts));
			return file.LocalPath;
		}

		public string GetCleanslateFile()
		{
			return GetBasicTemplateFile(new[] { "CleanSlate.pptx" });
		}

		public string GetCoverFile()
		{
			return GetBasicTemplateFile(new[] { "WizCover.pptx" });
		}

		public string GetGenericCoverFile()
		{
			return GetBasicTemplateFile(new[] { "WizCover2.pptx" });
		}

		public string GetLeadoffStatementsFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "intro slide", fileName });
		}

		public string GetClientGoalsFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "needs analysis", fileName });
		}

		public string GetTargetCustomersFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "target customer", fileName });
		}

		public string GetSimpleSummaryTemlateFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "closing summary", fileName });
		}

		public string GetSimpleSummaryTableFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "closing summary", "tables", fileName });
		}

		public string GetSimpleSummaryIconFile(string fileName)
		{
			return GetBasicTemplateFile(new[] { "closing summary", "tables", "icons", fileName });
		}
		#endregion

		#region Online Slides
		private string GetOnlineTemplateFile(IEnumerable<string> fileNameParts)
		{
			var file = new StorageFile(_sourceFolder.RelativePathParts.Merge(new[] { SlideSettingsManager.Instance.SlideSettings.SlideFolder, "Online Slides" }).Merge(fileNameParts));
			return file.LocalPath;
		}

		public string GetOnlineOneSheetFile(string[] fileNameParts)
		{
			return GetOnlineTemplateFile(new[] { "onesheets" }.Merge(fileNameParts));
		}

		public string GetOnlinePackageFile(int rowCount, bool showScreenshot)
		{
			return GetOnlineTemplateFile(new[]
			{
				"table",
				String.Format("digitaltable_{0}{1}.pptx", rowCount, (showScreenshot ? "p" : String.Empty))
			});
		}

		public string GetAdPlanFile(int totalRecords, bool moreSlides)
		{
			string fileName;
			switch (totalRecords)
			{
				case 6:
				case 7:
				case 8:
				case 11:
				case 12:
					fileName = moreSlides ? "adplan4.pptx" : "adplan6.pptx";
					break;
				case 9:
				case 10:
				case 13:
				case 14:
				case 15:
					fileName = moreSlides ? "adplan5.pptx" : "adplan6.pptx";
					break;
				default:
					if (totalRecords < 6)
						fileName = String.Format("adplan{0}.pptx", totalRecords);
					else
						fileName = "adplan5.pptx";
					break;
			}
			return GetOnlineTemplateFile(new[]
			{
				"adplan",
				fileName
			});
		}

		public string GetOnlineSummaryFile()
		{
			return GetOnlineTemplateFile(new[] { "summary", "digital_summary.pptx" });
		}
		#endregion

		#region Star Slides
		private string GetStarSolutionTemplateFile(IEnumerable<string> fileName)
		{
			var file = new StorageFile(_sourceFolder.RelativePathParts.Merge(new[] { SlideSettingsManager.Instance.SlideSettings.SlideFolder, "STAR Slides" }).Merge(fileName));
			return file.LocalPath;
		}

		public string GetStarCoverFile(string fileName)
		{
			return GetStarSolutionTemplateFile(new[] { "01_cover", fileName });
		}

		public string GetStarCNAFile(string fileName)
		{
			return GetStarSolutionTemplateFile(new[] { "02_cna", fileName });
		}

		public string GetStarFishingFile(string fileName)
		{
			return GetStarSolutionTemplateFile(new[] { "03_fishing", fileName });
		}

		public string GetStarCustomerFile(string fileName)
		{
			return GetStarSolutionTemplateFile(new[] { "04_customer", fileName });
		}

		public string GetStarShareFile(string fileName)
		{
			return GetStarSolutionTemplateFile(new[] { "05_share", fileName });
		}

		public string GetStarROIFile(string fileName)
		{
			return GetStarSolutionTemplateFile(new[] { "06_roi", fileName });
		}

		public string GetStarMarketFile(string fileName)
		{
			return GetStarSolutionTemplateFile(new[] { "07_market", fileName });
		}

		public string GetStarVideoFile(string fileName)
		{
			return GetStarSolutionTemplateFile(new[] { "08_video", fileName });
		}

		public string GetStarAudienceFile(string fileName)
		{
			return GetStarSolutionTemplateFile(new[] { "09_audience", fileName });
		}

		public string GetStarSolutionFile(string fileName)
		{
			return GetStarSolutionTemplateFile(new[] { "10_solution", fileName });
		}

		public string GetStarClosersFile(string fileName)
		{
			return GetStarSolutionTemplateFile(new[] { "11_closers", fileName });
		}
		#endregion

		#region Shift Slides
		private string GetShiftSolutionTemplateFile(IEnumerable<string> fileName)
		{
			var file = new StorageFile(_sourceFolder.RelativePathParts.Merge(new[] { SlideSettingsManager.Instance.SlideSettings.SlideFolder, "SHIFT Slides" }).Merge(fileName));
			return file.LocalPath;
		}

		public string GetShiftCoverFile(string fileName)
		{
			return GetShiftSolutionTemplateFile(new[] { "01_cover_slide", fileName });
		}

		public string GetShiftIntroFile(string fileName)
		{
			return GetShiftSolutionTemplateFile(new[] { "02_introduction", fileName });
		}

		public string GetShiftAgendaFile(string fileName)
		{
			return GetShiftSolutionTemplateFile(new[] { "03_agenda", fileName });
		}

		public string GetShiftGoalsFile(string fileName)
		{
			return GetShiftSolutionTemplateFile(new[] { "04_your_goals", fileName });
		}

		public string GetShiftMarketFile(string fileName)
		{
			return GetShiftSolutionTemplateFile(new[] { "05_market_opportunity", fileName });
		}

		public string GetShiftPartnershipFile(string fileName)
		{
			return GetShiftSolutionTemplateFile(new[] { "06_our_partnership", fileName });
		}

		public string GetShiftNeedsSolutionsFile(string fileName)
		{
			return GetShiftSolutionTemplateFile(new[] { "07_needs_solutions", fileName });
		}

		public string GetShiftCBCFile(string fileName)
		{
			return GetShiftSolutionTemplateFile(new[] { "08_cbc", fileName });
		}

		public string GetShiftIntegratedSolutionFile(string fileName)
		{
			return GetShiftSolutionTemplateFile(new[] { "10_integrated_solution", fileName });
		}

		public string GetShiftApproachFile(string fileName)
		{
			return GetShiftSolutionTemplateFile(new[] { "09_our_approach", fileName });
		}

		public string GetShiftROIFile(string fileName)
		{
			return GetShiftSolutionTemplateFile(new[] { "13_roi", fileName });
		}
		#endregion

		#region Newspapaer Slides
		private string GetNewspaperTemplateFile(IEnumerable<string> fileName)
		{
			var file = new StorageFile(_sourceFolder.RelativePathParts.Merge(new[] { SlideSettingsManager.Instance.SlideSettings.SlideFolder, "Newspaper Slides" }).Merge(fileName));
			return file.LocalPath;
		}

		public string GetBasicOverviewSlideFile(string fileSuffix)
		{
			return GetNewspaperTemplateFile(new[]
			{
				"basic overview",
				String.Format("basic-{0}.pptx", fileSuffix)
			});
		}

		public string GetBasicOverviewSummaryFile()
		{
			return GetNewspaperTemplateFile(new[]
			{
				"product summary",
				"product_summary.pptx"
			});
		}

		public string GetMultiSummaryFile(string fileSuffix)
		{
			return GetNewspaperTemplateFile(new[]
			{
				"multi summary",
				String.Format("summary-{0}.pptx", fileSuffix)
			});
		}

		public string GetSnapshotFile(string fileName)
		{
			return GetNewspaperTemplateFile(new[]
			{
				"snapshotnew",
				fileName
			});
		}

		public string GetDetailedGridFile(int columnsCount, int rowCount, bool showNotes)
		{
			return GetNewspaperTemplateFile(new[]
			{
				"tables",
				String.Format("{0} columns_detailed",columnsCount),
				String.Format("tables_{0}",showNotes?"adnotes":"no_adnotes"),
				String.Format("table{0}_{1}.pptx",rowCount,showNotes?"adnotes":"no_adnotes")
			});
		}

		public string GetMultiGridFile(int columnsCount, int rowCount, bool showNotes)
		{
			return GetNewspaperTemplateFile(new[]
			{
				"tables",
				String.Format("{0} columns_multi",columnsCount),
				String.Format("tables_{0}_logos",showNotes?"adnotes":"no_adnotes"),
				String.Format("table{0}_{1}.pptx",rowCount,showNotes?"adnotes":"no_adnotes")
			});
		}
		#endregion
		#endregion
	}
}
